using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using MySqlConnector;
using TallerApp.Web.Models;

namespace TallerApp.Web.Data
{
    public class RepositorioCliente : RepositorioBase, IRepositorioCliente
    {
        
        public RepositorioCliente(IConfiguration configuration) : base(configuration) { }
public List<Cliente> ObtenerTodos()
        {
            var lista = new List<Cliente>();
            using var conexion = ObtenerConexion();
            using var cmd = new MySqlCommand("SELECT * FROM Clientes", conexion);
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                lista.Add(new Cliente {
                    IdCliente = reader.GetInt32("IdCliente"),
                    NombreCompleto = reader.GetString("NombreCompleto"),
                    DNI = reader.GetString("DNI"),
                    Telefono = reader.IsDBNull(reader.GetOrdinal("Telefono")) ? "" : reader.GetString("Telefono"),
                    Email = reader.IsDBNull(reader.GetOrdinal("Email")) ? "" : reader.GetString("Email"),
                    Direccion = reader.IsDBNull(reader.GetOrdinal("Direccion")) ? "" : reader.GetString("Direccion")
                });
            }
            return lista;
        }

        public Cliente? ObtenerPorId(int id)
        {
            using var conexion = ObtenerConexion();
            using var cmd = new MySqlCommand("SELECT * FROM Clientes WHERE IdCliente = @id", conexion);
            cmd.Parameters.AddWithValue("@id", id);
            using var reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                return new Cliente {
                    IdCliente = reader.GetInt32("IdCliente"),
                    NombreCompleto = reader.GetString("NombreCompleto"),
                    DNI = reader.GetString("DNI"),
                    Telefono = reader.IsDBNull(reader.GetOrdinal("Telefono")) ? "" : reader.GetString("Telefono"),
                    Email = reader.IsDBNull(reader.GetOrdinal("Email")) ? "" : reader.GetString("Email"),
                    Direccion = reader.IsDBNull(reader.GetOrdinal("Direccion")) ? "" : reader.GetString("Direccion")
                };
            }
            return null;
        }

        public void Alta(Cliente cliente)
        {
            using var conexion = ObtenerConexion();
            using var cmd = new MySqlCommand("INSERT INTO Clientes (NombreCompleto, DNI, Telefono, Email, Direccion) VALUES (@nombre, @dni, @tel, @email, @dir)", conexion);
            cmd.Parameters.AddWithValue("@nombre", cliente.NombreCompleto);
            cmd.Parameters.AddWithValue("@dni", cliente.DNI);
            cmd.Parameters.AddWithValue("@tel", (object)cliente.Telefono ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@email", (object)cliente.Email ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@dir", (object)cliente.Direccion ?? DBNull.Value);
            cmd.ExecuteNonQuery();
        }

        public void Modificar(Cliente cliente)
        {
            using var conexion = ObtenerConexion();
            using var cmd = new MySqlCommand("UPDATE Clientes SET NombreCompleto=@nombre, DNI=@dni, Telefono=@tel, Email=@email, Direccion=@dir WHERE IdCliente=@id", conexion);
            cmd.Parameters.AddWithValue("@id", cliente.IdCliente);
            cmd.Parameters.AddWithValue("@nombre", cliente.NombreCompleto);
            cmd.Parameters.AddWithValue("@dni", cliente.DNI);
            cmd.Parameters.AddWithValue("@tel", (object)cliente.Telefono ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@email", (object)cliente.Email ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@dir", (object)cliente.Direccion ?? DBNull.Value);
            cmd.ExecuteNonQuery();
        }

        public void Eliminar(int id)
        {
            using var conexion = ObtenerConexion();
            using var cmd = new MySqlCommand("DELETE FROM Clientes WHERE IdCliente = @id", conexion);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.ExecuteNonQuery();
        }
    }
}
