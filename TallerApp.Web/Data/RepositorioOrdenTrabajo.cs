using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using MySqlConnector;
using TallerApp.Web.Models;

namespace TallerApp.Web.Data
{
    public class RepositorioOrdenTrabajo : RepositorioBase, IRepositorioOrdenTrabajo
    {
        public RepositorioOrdenTrabajo(IConfiguration configuration) : base(configuration) { }

        public int CrearOrdenConRepuestos(OrdenTrabajo orden, List<OrdenRepuesto> detalles)
        {
            using var conexion = ObtenerConexion();
            conexion.Open();
            using var transaction = conexion.BeginTransaction();
            
            try
            {
                string sqlOrden = @"INSERT INTO ordenestrabajo 
                    (VehiculoId, MecanicoId, FechaIngreso, FechaEntregaEstimada, Estado, DescripcionProblema, Observaciones, IdUsuarioCreacion) 
                    VALUES (@vehiculo, @mecanico, @fechaIngreso, @fechaEntrega, @estado, @descripcion, @observaciones, @usuario)";
                
                using var cmdOrden = new MySqlCommand(sqlOrden, conexion, transaction);
                cmdOrden.Parameters.AddWithValue("@vehiculo", orden.VehiculoId);
                cmdOrden.Parameters.AddWithValue("@mecanico", orden.MecanicoId);
                cmdOrden.Parameters.AddWithValue("@fechaIngreso", orden.FechaIngreso);
                cmdOrden.Parameters.AddWithValue("@fechaEntrega", (object)orden.FechaEntregaEstimada ?? DBNull.Value);
                cmdOrden.Parameters.AddWithValue("@estado", orden.Estado);
                cmdOrden.Parameters.AddWithValue("@descripcion", orden.DescripcionProblema);
                cmdOrden.Parameters.AddWithValue("@observaciones", (object)orden.Observaciones ?? DBNull.Value);
                cmdOrden.Parameters.AddWithValue("@usuario", orden.IdUsuarioCreacion);
                
                cmdOrden.ExecuteNonQuery();
                cmdOrden.CommandText = "SELECT LAST_INSERT_ID();";
                int nuevaOrdenId = Convert.ToInt32(cmdOrden.ExecuteScalar());

                string sqlDetalle = @"INSERT INTO OrdenRepuesto (OrdenId, RepuestoId, Cantidad, PrecioUnitario) 
                                      VALUES (@ordenId, @repuestoId, @cantidad, @precio)";
                string sqlStock = "UPDATE Repuestos SET Stock = Stock - @cantidad WHERE IdRepuesto = @repuestoId";

                foreach (var detalle in detalles)
                {
                    using var cmdDetalle = new MySqlCommand(sqlDetalle, conexion, transaction);
                    cmdDetalle.Parameters.AddWithValue("@ordenId", nuevaOrdenId);
                    cmdDetalle.Parameters.AddWithValue("@repuestoId", detalle.RepuestoId);
                    cmdDetalle.Parameters.AddWithValue("@cantidad", detalle.Cantidad);
                    cmdDetalle.Parameters.AddWithValue("@precio", detalle.PrecioUnitario);
                    cmdDetalle.ExecuteNonQuery();

                    using var cmdStock = new MySqlCommand(sqlStock, conexion, transaction);
                    cmdStock.Parameters.AddWithValue("@cantidad", detalle.Cantidad);
                    cmdStock.Parameters.AddWithValue("@repuestoId", detalle.RepuestoId);
                    cmdStock.ExecuteNonQuery();
                }

                transaction.Commit();
                return nuevaOrdenId;
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }

        // ==========================================
        // IMPLEMENTACIÓN DE LOS NUEVOS MÉTODOS
        // ==========================================

        public List<OrdenTrabajo> ObtenerTodos()
        {
            var lista = new List<OrdenTrabajo>();
            using var conexion = ObtenerConexion();
            conexion.Open();
            
            // Consulta simple SIN JOIN para evitar errores de nombres de tablas
            string sql = @"SELECT IdOrden, FechaIngreso, Estado, DescripcionProblema 
                           FROM ordenestrabajo 
                           ORDER BY FechaIngreso DESC";
            
            using var cmd = new MySqlCommand(sql, conexion);
            using var reader = cmd.ExecuteReader();
            
            while (reader.Read())
            {
                lista.Add(new OrdenTrabajo {
                    IdOrden = reader.GetInt32("IdOrden"),
                    FechaIngreso = reader.GetDateTime("FechaIngreso"),
                    Estado = reader.GetString("Estado"),
                    DescripcionProblema = reader.GetString("DescripcionProblema")
                });
            }
            return lista;
        }

        public void Eliminar(int id)
        {
            using var conexion = ObtenerConexion();
            conexion.Open();
            
            using var transaction = conexion.BeginTransaction();
            try
            {
                // 1. Borrar los repuestos asociados a esta orden
                using var cmdDetalles = new MySqlCommand("DELETE FROM OrdenRepuesto WHERE OrdenId = @id", conexion, transaction);
                cmdDetalles.Parameters.AddWithValue("@id", id);
                cmdDetalles.ExecuteNonQuery();

                // 2. Borrar la orden de trabajo
                using var cmdOrden = new MySqlCommand("DELETE FROM ordenestrabajo WHERE IdOrden = @id", conexion, transaction);
                cmdOrden.Parameters.AddWithValue("@id", id);
                cmdOrden.ExecuteNonQuery();

                // 3. Confirmar
                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }
    }
}