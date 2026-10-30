using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using MySqlConnector;
using TallerApp.Web.Models;

namespace TallerApp.Web.Data
{
    public class RepositorioRepuesto : RepositorioBase, IRepositorioRepuesto
    {
        
        public RepositorioRepuesto(IConfiguration configuration) : base(configuration) { }
public List<Repuesto> ObtenerTodos()
        {
            var lista = new List<Repuesto>();
            using var conexion = ObtenerConexion();
            conexion.Open();
            using var cmd = new MySqlCommand("SELECT * FROM Repuestos", conexion);
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
            using var cmd = new MySqlCommand("INSERT INTO Repuestos (Nombre, Codigo, Precio, Stock, Foto) VALUES (@nom, @cod, @pre, @sto, @fot)", conexion);
            cmd.Parameters.AddWithValue("@nom", repuesto.Nombre);
            cmd.Parameters.AddWithValue("@cod", repuesto.Codigo);
            cmd.Parameters.AddWithValue("@pre", repuesto.Precio);
            cmd.Parameters.AddWithValue("@sto", repuesto.Stock);
            cmd.Parameters.AddWithValue("@fot", (object)repuesto.Foto ?? DBNull.Value);
            cmd.ExecuteNonQuery();
        }

        public void Modificar(Repuesto repuesto)
        {
            using var conexion = ObtenerConexion();
            conexion.Open();
            using var cmd = new MySqlCommand("UPDATE Repuestos SET Nombre=@nom, Codigo=@cod, Precio=@pre, Stock=@sto, Foto=@fot WHERE IdRepuesto=@id", conexion);
            cmd.Parameters.AddWithValue("@id", repuesto.IdRepuesto);
            cmd.Parameters.AddWithValue("@nom", repuesto.Nombre);
            cmd.Parameters.AddWithValue("@cod", repuesto.Codigo);
            cmd.Parameters.AddWithValue("@pre", repuesto.Precio);
            cmd.Parameters.AddWithValue("@sto", repuesto.Stock);
            cmd.Parameters.AddWithValue("@fot", (object)repuesto.Foto ?? DBNull.Value);
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
