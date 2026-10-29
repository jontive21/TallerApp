using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using MySqlConnector;
using TallerApp.Web.Models;

namespace TallerApp.Web.Data
{
    public class RepositorioVehiculo : RepositorioBase, IRepositorioVehiculo
    {
        
        public RepositorioVehiculo(IConfiguration configuration) : base(configuration) { }
public List<Vehiculo> ObtenerTodos()
        {
            var lista = new List<Vehiculo>();
            using var conexion = ObtenerConexion();
            using var cmd = new MySqlCommand("SELECT * FROM Vehiculos", conexion);
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                lista.Add(new Vehiculo {
                    IdVehiculo = reader.GetInt32("IdVehiculo"),
                    Patente = reader.GetString("Patente"),
                    Marca = reader.GetString("Marca"),
                    Modelo = reader.GetString("Modelo"),
                    Anio = reader.GetInt32("Anio"),
                    Color = reader.IsDBNull(reader.GetOrdinal("Color")) ? "" : reader.GetString("Color"),
                    ClienteId = reader.GetInt32("ClienteId")
                });
            }
            return lista;
        }

        public List<Vehiculo> ObtenerPorCliente(int clienteId)
        {
            var lista = new List<Vehiculo>();
            using var conexion = ObtenerConexion();
            using var cmd = new MySqlCommand("SELECT * FROM Vehiculos WHERE ClienteId = @id", conexion);
            cmd.Parameters.AddWithValue("@id", clienteId);
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                lista.Add(new Vehiculo {
                    IdVehiculo = reader.GetInt32("IdVehiculo"),
                    Patente = reader.GetString("Patente"),
                    Marca = reader.GetString("Marca"),
                    Modelo = reader.GetString("Modelo"),
                    Anio = reader.GetInt32("Anio"),
                    Color = reader.IsDBNull(reader.GetOrdinal("Color")) ? "" : reader.GetString("Color"),
                    ClienteId = reader.GetInt32("ClienteId")
                });
            }
            return lista;
        }

        public Vehiculo? ObtenerPorId(int id)
        {
            using var conexion = ObtenerConexion();
            using var cmd = new MySqlCommand("SELECT * FROM Vehiculos WHERE IdVehiculo = @id", conexion);
            cmd.Parameters.AddWithValue("@id", id);
            using var reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                return new Vehiculo {
                    IdVehiculo = reader.GetInt32("IdVehiculo"),
                    Patente = reader.GetString("Patente"),
                    Marca = reader.GetString("Marca"),
                    Modelo = reader.GetString("Modelo"),
                    Anio = reader.GetInt32("Anio"),
                    Color = reader.IsDBNull(reader.GetOrdinal("Color")) ? "" : reader.GetString("Color"),
                    ClienteId = reader.GetInt32("ClienteId")
                };
            }
            return null;
        }

        public void Alta(Vehiculo vehiculo)
        {
            using var conexion = ObtenerConexion();
            using var cmd = new MySqlCommand("INSERT INTO Vehiculos (Patente, Marca, Modelo, Anio, Color, ClienteId) VALUES (@pat, @mar, @mod, @anio, @col, @cli)", conexion);
            cmd.Parameters.AddWithValue("@pat", vehiculo.Patente);
            cmd.Parameters.AddWithValue("@mar", vehiculo.Marca);
            cmd.Parameters.AddWithValue("@mod", vehiculo.Modelo);
            cmd.Parameters.AddWithValue("@anio", vehiculo.Anio);
            cmd.Parameters.AddWithValue("@col", (object)vehiculo.Color ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@cli", vehiculo.ClienteId);
            cmd.ExecuteNonQuery();
        }

        public void Modificar(Vehiculo vehiculo)
        {
            using var conexion = ObtenerConexion();
            using var cmd = new MySqlCommand("UPDATE Vehiculos SET Patente=@pat, Marca=@mar, Modelo=@mod, Anio=@anio, Color=@col, ClienteId=@cli WHERE IdVehiculo=@id", conexion);
            cmd.Parameters.AddWithValue("@id", vehiculo.IdVehiculo);
            cmd.Parameters.AddWithValue("@pat", vehiculo.Patente);
            cmd.Parameters.AddWithValue("@mar", vehiculo.Marca);
            cmd.Parameters.AddWithValue("@mod", vehiculo.Modelo);
            cmd.Parameters.AddWithValue("@anio", vehiculo.Anio);
            cmd.Parameters.AddWithValue("@col", (object)vehiculo.Color ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@cli", vehiculo.ClienteId);
            cmd.ExecuteNonQuery();
        }

        public void Eliminar(int id)
        {
            using var conexion = ObtenerConexion();
            using var cmd = new MySqlCommand("DELETE FROM Vehiculos WHERE IdVehiculo = @id", conexion);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.ExecuteNonQuery();
        }
    }
}
