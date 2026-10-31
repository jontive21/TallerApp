using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using MySqlConnector;
using TallerApp.Web.Models;

namespace TallerApp.Web.Data
{
    public class RepositorioComprobantePago : RepositorioBase, IRepositorioComprobantePago
    {
        public RepositorioComprobantePago(IConfiguration configuration) : base(configuration) { }

        public List<ComprobantePago> ObtenerTodos(int pagina = 1, int registrosPorPagina = 10, string? busqueda = null)
        {
            var lista = new List<ComprobantePago>();
            using var conexion = ObtenerConexion();
            conexion.Open();
            
            string sql = "SELECT * FROM ComprobantesPago";
            if (!string.IsNullOrEmpty(busqueda))
            {
                sql += " WHERE Estado LIKE @busqueda OR ArchivoComprobante LIKE @busqueda";
            }
            sql += " ORDER BY FechaPago DESC LIMIT @limite OFFSET @offset";
            
            using var cmd = new MySqlCommand(sql, conexion);
            if (!string.IsNullOrEmpty(busqueda))
            {
                cmd.Parameters.AddWithValue("@busqueda", $"%{busqueda}%");
            }
            cmd.Parameters.AddWithValue("@limite", registrosPorPagina);
            cmd.Parameters.AddWithValue("@offset", (pagina - 1) * registrosPorPagina);
            
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                lista.Add(new ComprobantePago {
                    IdComprobante = reader.GetInt32("IdComprobante"),
                    OrdenId = reader.GetInt32("OrdenId"),
                    Monto = reader.GetDecimal("Monto"),
                    FechaPago = reader.GetDateTime("FechaPago"),
                    ArchivoComprobante = reader.IsDBNull(reader.GetOrdinal("ArchivoComprobante")) ? "" : reader.GetString("ArchivoComprobante"),
                    Estado = reader.GetString("Estado"),
                    IdUsuarioCreacion = reader.GetInt32("IdUsuarioCreacion")
                });
            }
            return lista;
        }

        public int ObtenerTotalComprobantes(string? busqueda = null)
        {
            using var conexion = ObtenerConexion();
            conexion.Open();
            
            string sql = "SELECT COUNT(*) FROM ComprobantesPago";
            if (!string.IsNullOrEmpty(busqueda))
            {
                sql += " WHERE Estado LIKE @busqueda OR ArchivoComprobante LIKE @busqueda";
            }
            
            using var cmd = new MySqlCommand(sql, conexion);
            if (!string.IsNullOrEmpty(busqueda))
            {
                cmd.Parameters.AddWithValue("@busqueda", $"%{busqueda}%");
            }
            
            return Convert.ToInt32(cmd.ExecuteScalar());
        }

        public ComprobantePago? ObtenerPorId(int id)
        {
            using var conexion = ObtenerConexion();
            conexion.Open();
            using var cmd = new MySqlCommand("SELECT * FROM ComprobantesPago WHERE IdComprobante = @id", conexion);
            cmd.Parameters.AddWithValue("@id", id);
            using var reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                return new ComprobantePago {
                    IdComprobante = reader.GetInt32("IdComprobante"),
                    OrdenId = reader.GetInt32("OrdenId"),
                    Monto = reader.GetDecimal("Monto"),
                    FechaPago = reader.GetDateTime("FechaPago"),
                    ArchivoComprobante = reader.IsDBNull(reader.GetOrdinal("ArchivoComprobante")) ? "" : reader.GetString("ArchivoComprobante"),
                    Estado = reader.GetString("Estado"),
                    IdUsuarioCreacion = reader.GetInt32("IdUsuarioCreacion")
                };
            }
            return null;
        }

        public void Alta(ComprobantePago comprobante)
        {
            using var conexion = ObtenerConexion();
            conexion.Open();
            using var cmd = new MySqlCommand("INSERT INTO ComprobantesPago (OrdenId, Monto, FechaPago, ArchivoComprobante, Estado, IdUsuarioCreacion) VALUES (@orden, @monto, @fecha, @archivo, @estado, @usuario)", conexion);
            cmd.Parameters.AddWithValue("@orden", comprobante.OrdenId);
            cmd.Parameters.AddWithValue("@monto", comprobante.Monto);
            cmd.Parameters.AddWithValue("@fecha", comprobante.FechaPago);
            cmd.Parameters.AddWithValue("@archivo", (object)comprobante.ArchivoComprobante ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@estado", comprobante.Estado);
            cmd.Parameters.AddWithValue("@usuario", comprobante.IdUsuarioCreacion);
            cmd.ExecuteNonQuery();
        }

        public void Modificar(ComprobantePago comprobante)
        {
            using var conexion = ObtenerConexion();
            conexion.Open();
            using var cmd = new MySqlCommand("UPDATE ComprobantesPago SET OrdenId=@orden, Monto=@monto, FechaPago=@fecha, ArchivoComprobante=@archivo, Estado=@estado, IdUsuarioCreacion=@usuario WHERE IdComprobante=@id", conexion);
            cmd.Parameters.AddWithValue("@id", comprobante.IdComprobante);
            cmd.Parameters.AddWithValue("@orden", comprobante.OrdenId);
            cmd.Parameters.AddWithValue("@monto", comprobante.Monto);
            cmd.Parameters.AddWithValue("@fecha", comprobante.FechaPago);
            cmd.Parameters.AddWithValue("@archivo", (object)comprobante.ArchivoComprobante ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@estado", comprobante.Estado);
            cmd.Parameters.AddWithValue("@usuario", comprobante.IdUsuarioCreacion);
            cmd.ExecuteNonQuery();
        }

        public void Eliminar(int id)
        {
            using var conexion = ObtenerConexion();
            conexion.Open();
            using var cmd = new MySqlCommand("DELETE FROM ComprobantesPago WHERE IdComprobante = @id", conexion);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.ExecuteNonQuery();
        }
    }
}