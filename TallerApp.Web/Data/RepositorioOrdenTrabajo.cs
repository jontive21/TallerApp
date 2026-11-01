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
            
            // Iniciamos una transacción para garantizar integridad
            using var transaction = conexion.BeginTransaction();
            
            try
            {
                // 1. Insertar la Orden de Trabajo
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
                
                // Obtener el ID generado
                cmdOrden.CommandText = "SELECT LAST_INSERT_ID();";
                int nuevaOrdenId = Convert.ToInt32(cmdOrden.ExecuteScalar());

                // 2. Insertar los detalles (Repuestos) y descontar stock
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

                    // Descontar del stock
                    using var cmdStock = new MySqlCommand(sqlStock, conexion, transaction);
                    cmdStock.Parameters.AddWithValue("@cantidad", detalle.Cantidad);
                    cmdStock.Parameters.AddWithValue("@repuestoId", detalle.RepuestoId);
                    cmdStock.ExecuteNonQuery();
                }

                // 3. Confirmar la transacción
                transaction.Commit();
                return nuevaOrdenId;
            }
            catch
            {
                // Si algo falla, deshacer todo
                transaction.Rollback();
                throw;
            }
        }
    }
}