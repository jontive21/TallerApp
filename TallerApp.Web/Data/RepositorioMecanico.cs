using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using MySqlConnector;
using TallerApp.Web.Models;

namespace TallerApp.Web.Data
{
    public class RepositorioMecanico : RepositorioBase, IRepositorioMecanico
    {
        public RepositorioMecanico(IConfiguration configuration) : base(configuration) { }

        public List<Mecanico> ObtenerTodos(int pagina = 1, int registrosPorPagina = 10, string? busqueda = null)
        {
            var lista = new List<Mecanico>();
            using var conexion = ObtenerConexion();
            conexion.Open();
            
            string sql = "SELECT * FROM Mecanicos";
            if (!string.IsNullOrEmpty(busqueda))
            {
                sql += " WHERE NombreCompleto LIKE @busqueda OR Matricula LIKE @busqueda OR Especialidad LIKE @busqueda";
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
                lista.Add(new Mecanico {
                    IdMecanico = reader.GetInt32("IdMecanico"),
                    NombreCompleto = reader.GetString("NombreCompleto"),
                    Matricula = reader.GetString("Matricula"),
                    Especialidad = reader.IsDBNull(reader.GetOrdinal("Especialidad")) ? "" : reader.GetString("Especialidad"),
                    Email = reader.IsDBNull(reader.GetOrdinal("Email")) ? "" : reader.GetString("Email"),
                    Telefono = reader.IsDBNull(reader.GetOrdinal("Telefono")) ? "" : reader.GetString("Telefono")
                });
            }
            return lista;
        }

        public int ObtenerTotalMecanicos(string? busqueda = null)
        {
            using var conexion = ObtenerConexion();
            conexion.Open();
            
            string sql = "SELECT COUNT(*) FROM Mecanicos";
            if (!string.IsNullOrEmpty(busqueda))
            {
                sql += " WHERE NombreCompleto LIKE @busqueda OR Matricula LIKE @busqueda OR Especialidad LIKE @busqueda";
            }
            
            using var cmd = new MySqlCommand(sql, conexion);
            if (!string.IsNullOrEmpty(busqueda))
            {
                cmd.Parameters.AddWithValue("@busqueda", $"%{busqueda}%");
            }
            
            return Convert.ToInt32(cmd.ExecuteScalar());
        }

        public Mecanico? ObtenerPorId(int id)
        {
            using var conexion = ObtenerConexion();
            conexion.Open();
            using var cmd = new MySqlCommand("SELECT * FROM Mecanicos WHERE IdMecanico = @id", conexion);
            cmd.Parameters.AddWithValue("@id", id);
            using var reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                return new Mecanico {
                    IdMecanico = reader.GetInt32("IdMecanico"),
                    NombreCompleto = reader.GetString("NombreCompleto"),
                    Matricula = reader.GetString("Matricula"),
                    Especialidad = reader.IsDBNull(reader.GetOrdinal("Especialidad")) ? "" : reader.GetString("Especialidad"),
                    Email = reader.IsDBNull(reader.GetOrdinal("Email")) ? "" : reader.GetString("Email"),
                    Telefono = reader.IsDBNull(reader.GetOrdinal("Telefono")) ? "" : reader.GetString("Telefono")
                };
            }
            return null;
        }

        public void Alta(Mecanico mecanico)
        {
            using var conexion = ObtenerConexion();
            conexion.Open();
            using var cmd = new MySqlCommand("INSERT INTO Mecanicos (NombreCompleto, Matricula, Especialidad, Email, Telefono) VALUES (@nom, @mat, @esp, @ema, @tel)", conexion);
            cmd.Parameters.AddWithValue("@nom", mecanico.NombreCompleto);
            cmd.Parameters.AddWithValue("@mat", mecanico.Matricula);
            cmd.Parameters.AddWithValue("@esp", (object)mecanico.Especialidad ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@ema", (object)mecanico.Email ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@tel", (object)mecanico.Telefono ?? DBNull.Value);
            cmd.ExecuteNonQuery();
        }

        public void Modificar(Mecanico mecanico)
        {
            using var conexion = ObtenerConexion();
            conexion.Open();
            using var cmd = new MySqlCommand("UPDATE Mecanicos SET NombreCompleto=@nom, Matricula=@mat, Especialidad=@esp, Email=@ema, Telefono=@tel WHERE IdMecanico=@id", conexion);
            cmd.Parameters.AddWithValue("@id", mecanico.IdMecanico);
            cmd.Parameters.AddWithValue("@nom", mecanico.NombreCompleto);
            cmd.Parameters.AddWithValue("@mat", mecanico.Matricula);
            cmd.Parameters.AddWithValue("@esp", (object)mecanico.Especialidad ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@ema", (object)mecanico.Email ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@tel", (object)mecanico.Telefono ?? DBNull.Value);
            cmd.ExecuteNonQuery();
        }

        public void Eliminar(int id)
        {
            using var conexion = ObtenerConexion();
            conexion.Open();
            using var cmd = new MySqlCommand("DELETE FROM Mecanicos WHERE IdMecanico = @id", conexion);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.ExecuteNonQuery();
        }
    }
}