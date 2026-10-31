using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using MySqlConnector;
using TallerApp.Web.Models;

namespace TallerApp.Web.Data
{
    public class RepositorioRepuesto : RepositorioBase, IRepositorioRepuesto
    {
        public RepositorioRepuesto(IConfiguration configuration) : base(configuration) { }

        public List<Repuesto> ObtenerTodos(int pagina = 1, int registrosPorPagina = 10, string? busqueda = null)
        {
            var lista = new List<Repuesto>();
            using var conexion = ObtenerConexion();
            conexion.Open();
            
            string sql = "SELECT * FROM Repuestos";
            if (!string.IsNullOrEmpty(busqueda))
            {
                sql += " WHERE Nombre LIKE @busqueda OR Codigo LIKE @busqueda";
            }
            sql += " LIMIT @limite OFFSET @offset";
            
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
                lista.Add(new Repuesto {
                    IdRepuesto = reader.GetInt32("IdRepuesto"),
                    Nombre = reader.GetString("Nombre"),
                    Codigo = reader.GetString("Codigo"),
                    Precio = reader.GetDecimal("Precio"),
                    Stock = reader.GetInt32("Stock"),
                    Foto = reader.IsDBNull(reader.GetOrdinal("Foto")) ? "" : reader.GetString("Foto")
                });
            }
            return lista;
        }

        public int ObtenerTotalRepuestos(string? busqueda = null)
        {
            using var conexion = ObtenerConexion();
            conexion.Open();
            
            string sql = "SELECT COUNT(*) FROM Repuestos";
            if (!string.IsNullOrEmpty(busqueda))
            {
                sql += " WHERE Nombre LIKE @busqueda OR Codigo LIKE @busqueda";
            }
            
            using var cmd = new MySqlCommand(sql, conexion);
            if (!string.IsNullOrEmpty(busqueda))
            {
                cmd.Parameters.AddWithValue("@busqueda", $"%{busqueda}%");
            }
            
            return Convert.ToInt32(cmd.ExecuteScalar());
        }

        public Repuesto? ObtenerPorId(int id)
        {
            using var conexion = ObtenerConexion();
            conexion.Open();
            using var cmd = new MySqlCommand("SELECT * FROM Repuestos WHERE IdRepuesto = @id", conexion);
            cmd.Parameters.AddWithValue("@id", id);
            using var reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                return new Repuesto {
                    IdRepuesto = reader.GetInt32("IdRepuesto"),
                    Nombre = reader.GetString("Nombre"),
                    Codigo = reader.GetString("Codigo"),
                    Precio = reader.GetDecimal("Precio"),
                    Stock = reader.GetInt32("Stock"),
                    Foto = reader.IsDBNull(reader.GetOrdinal("Foto")) ? "" : reader.GetString("Foto")
                };
            }
            return null;
        }

        public void Alta(Repuesto repuesto)
        {
            using var conexion = ObtenerConexion();
            conexion.Open();
            using var cmd = new MySqlCommand("INSERT INTO Repuestos (Nombre, Codigo, Precio, Stock, Foto) VALUES (@nom, @cod, @pre, @sto, @foto)", conexion);
            cmd.Parameters.AddWithValue("@nom", repuesto.Nombre);
            cmd.Parameters.AddWithValue("@cod", repuesto.Codigo);
            cmd.Parameters.AddWithValue("@pre", repuesto.Precio);
            cmd.Parameters.AddWithValue("@sto", repuesto.Stock);
            cmd.Parameters.AddWithValue("@foto", (object)repuesto.Foto ?? DBNull.Value);
            cmd.ExecuteNonQuery();
        }

        public void Modificar(Repuesto repuesto)
        {
            using var conexion = ObtenerConexion();
            conexion.Open();
            using var cmd = new MySqlCommand("UPDATE Repuestos SET Nombre=@nom, Codigo=@cod, Precio=@pre, Stock=@sto, Foto=@foto WHERE IdRepuesto=@id", conexion);
            cmd.Parameters.AddWithValue("@id", repuesto.IdRepuesto);
            cmd.Parameters.AddWithValue("@nom", repuesto.Nombre);
            cmd.Parameters.AddWithValue("@cod", repuesto.Codigo);
            cmd.Parameters.AddWithValue("@pre", repuesto.Precio);
            cmd.Parameters.AddWithValue("@sto", repuesto.Stock);
            cmd.Parameters.AddWithValue("@foto", (object)repuesto.Foto ?? DBNull.Value);
            cmd.ExecuteNonQuery();
        }

        public void Eliminar(int id)
        {
            using var conexion = ObtenerConexion();
            conexion.Open();
            using var cmd = new MySqlCommand("DELETE FROM Repuestos WHERE IdRepuesto = @id", conexion);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.ExecuteNonQuery();
        }
    }
}