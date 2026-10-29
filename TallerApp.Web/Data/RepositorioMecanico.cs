using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using MySqlConnector;
using TallerApp.Web.Models;

namespace TallerApp.Web.Data
{
    public class RepositorioMecanico : RepositorioBase, IRepositorioMecanico
    {
        
        public RepositorioMecanico(IConfiguration configuration) : base(configuration) { }
public List<Mecanico> ObtenerTodos()
        {
            var lista = new List<Mecanico>();
            using var conexion = ObtenerConexion();
            using var cmd = new MySqlCommand("SELECT * FROM Mecanicos", conexion);
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

        public Mecanico? ObtenerPorId(int id)
        {
            using var conexion = ObtenerConexion();
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
            using var cmd = new MySqlCommand("DELETE FROM Mecanicos WHERE IdMecanico = @id", conexion);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.ExecuteNonQuery();
        }
    }
}
