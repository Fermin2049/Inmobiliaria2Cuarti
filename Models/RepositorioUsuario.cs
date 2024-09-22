using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace Inmobiliaria2Cuarti.Models
{
    public class RepositorioUsuario
    {
        string ConectionString = "Server=localhost;User Id=root;Password=;Database=inmobiliaria2;";

        public List<Usuario> ObtenerTodos()
        {
            List<Usuario> usuarios = new List<Usuario>();
            using (MySqlConnection connection = new MySqlConnection(ConectionString))
            {
                var query =
                    $@"SELECT {nameof(Usuario.IdUsuario)},
                                  {nameof(Usuario.Nombre)},
                                  {nameof(Usuario.Apellido)},
                                  {nameof(Usuario.Email)},
                                  {nameof(Usuario.Contrasenia)},
                                  {nameof(Usuario.Avatar)},
                                  {nameof(Usuario.Rol)},
                                  {nameof(Usuario.Estado)}
                        FROM usuario
                        WHERE {nameof(Usuario.Estado)} = true";
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    connection.Open();
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        usuarios.Add(
                            new Usuario
                            {
                                IdUsuario = reader.GetInt32(nameof(Usuario.IdUsuario)),
                                Nombre = reader.IsDBNull(reader.GetOrdinal(nameof(Usuario.Nombre)))
                                    ? null
                                    : reader.GetString(nameof(Usuario.Nombre)),
                                Apellido = reader.IsDBNull(
                                    reader.GetOrdinal(nameof(Usuario.Apellido))
                                )
                                    ? null
                                    : reader.GetString(nameof(Usuario.Apellido)),
                                Email = reader.IsDBNull(reader.GetOrdinal(nameof(Usuario.Email)))
                                    ? null
                                    : reader.GetString(nameof(Usuario.Email)),
                                Contrasenia = reader.IsDBNull(
                                    reader.GetOrdinal(nameof(Usuario.Contrasenia))
                                )
                                    ? null
                                    : reader.GetString(nameof(Usuario.Contrasenia)),
                                Avatar = reader.IsDBNull(reader.GetOrdinal(nameof(Usuario.Avatar)))
                                    ? null
                                    : reader.GetString(nameof(Usuario.Avatar)),
                                Rol = reader.GetInt32(nameof(Usuario.Rol)),
                                Estado = reader.GetBoolean(nameof(Usuario.Estado)),
                            }
                        );
                    }
                    connection.Close();
                }
                return usuarios;
            }
        }

        public Usuario? Obtener(int id)
        {
            Usuario? res = null;
            using (MySqlConnection connection = new MySqlConnection(ConectionString))
            {
                var query =
                    $@"SELECT {nameof(Usuario.IdUsuario)},
                                  {nameof(Usuario.Nombre)},
                                  {nameof(Usuario.Apellido)},
                                  {nameof(Usuario.Email)},
                                  {nameof(Usuario.Contrasenia)},
                                  {nameof(Usuario.Avatar)},
                                  {nameof(Usuario.Rol)},
                                  {nameof(Usuario.Estado)}
                        FROM usuario
                        WHERE {nameof(Usuario.IdUsuario)} = @IdUsuario";
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@IdUsuario", id);
                    connection.Open();
                    var reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        res = new Usuario
                        {
                            IdUsuario = reader.GetInt32(nameof(Usuario.IdUsuario)),
                            Nombre = reader.IsDBNull(reader.GetOrdinal(nameof(Usuario.Nombre)))
                                ? null
                                : reader.GetString(nameof(Usuario.Nombre)),
                            Apellido = reader.IsDBNull(reader.GetOrdinal(nameof(Usuario.Apellido)))
                                ? null
                                : reader.GetString(nameof(Usuario.Apellido)),
                            Email = reader.IsDBNull(reader.GetOrdinal(nameof(Usuario.Email)))
                                ? null
                                : reader.GetString(nameof(Usuario.Email)),
                            Contrasenia = reader.IsDBNull(
                                reader.GetOrdinal(nameof(Usuario.Contrasenia))
                            )
                                ? null
                                : reader.GetString(nameof(Usuario.Contrasenia)),
                            Avatar = reader.IsDBNull(reader.GetOrdinal(nameof(Usuario.Avatar)))
                                ? null
                                : reader.GetString(nameof(Usuario.Avatar)),
                            Rol = reader.GetInt32(nameof(Usuario.Rol)),
                            Estado = reader.GetBoolean(nameof(Usuario.Estado)),
                        };
                    }
                    connection.Close();
                }
                return res;
            }
        }

        public Usuario? ObtenerPorEmail(string email)
        {
            Usuario? res = null;
            using (MySqlConnection connection = new MySqlConnection(ConectionString))
            {
                var query =
                    $@"SELECT {nameof(Usuario.IdUsuario)},
                                  {nameof(Usuario.Nombre)},
                                  {nameof(Usuario.Apellido)},
                                  {nameof(Usuario.Email)},
                                  {nameof(Usuario.Contrasenia)},
                                  {nameof(Usuario.Avatar)},
                                  {nameof(Usuario.Rol)},
                                  {nameof(Usuario.Estado)}
                        FROM usuario
                        WHERE {nameof(Usuario.Email)} = @Email";
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Email", email);
                    connection.Open();
                    var reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        res = new Usuario
                        {
                            IdUsuario = reader.GetInt32(nameof(Usuario.IdUsuario)),
                            Nombre = reader.IsDBNull(reader.GetOrdinal(nameof(Usuario.Nombre)))
                                ? null
                                : reader.GetString(nameof(Usuario.Nombre)),
                            Apellido = reader.IsDBNull(reader.GetOrdinal(nameof(Usuario.Apellido)))
                                ? null
                                : reader.GetString(nameof(Usuario.Apellido)),
                            Email = reader.IsDBNull(reader.GetOrdinal(nameof(Usuario.Email)))
                                ? null
                                : reader.GetString(nameof(Usuario.Email)),
                            Contrasenia = reader.IsDBNull(
                                reader.GetOrdinal(nameof(Usuario.Contrasenia))
                            )
                                ? null
                                : reader.GetString(nameof(Usuario.Contrasenia)),
                            Avatar = reader.IsDBNull(reader.GetOrdinal(nameof(Usuario.Avatar)))
                                ? null
                                : reader.GetString(nameof(Usuario.Avatar)),
                            Rol = reader.GetInt32(nameof(Usuario.Rol)),
                            Estado = reader.GetBoolean(nameof(Usuario.Estado)),
                        };
                    }
                    connection.Close();
                }
            }
            return res;
        }

        public int CrearUsuario(Usuario usuario)
        {
            using (MySqlConnection connection = new MySqlConnection(ConectionString))
            {
                var query =
                    $@"INSERT INTO usuario (Nombre, Apellido, Email, Contrasenia, Avatar, Rol, Estado)
                       VALUES (@Nombre, @Apellido, @Email, @Contrasenia, @Avatar, @Rol, @Estado);
                       SELECT LAST_INSERT_ID();";
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Nombre", usuario.Nombre);
                    command.Parameters.AddWithValue("@Apellido", usuario.Apellido);
                    command.Parameters.AddWithValue("@Email", usuario.Email);
                    command.Parameters.AddWithValue("@Contrasenia", usuario.Contrasenia); // Ya hasheada en el controlador
                    command.Parameters.AddWithValue("@Avatar", usuario.Avatar);
                    command.Parameters.AddWithValue("@Rol", usuario.Rol);
                    command.Parameters.AddWithValue("@Estado", usuario.Estado);
                    connection.Open();
                    var id = Convert.ToInt32(command.ExecuteScalar());
                    connection.Close();
                    return id;
                }
            }
        }

        public bool ActualizarUsuario(Usuario usuario)
        {
            using (MySqlConnection connection = new MySqlConnection(ConectionString))
            {
                // Construimos la query dinámicamente para no actualizar el avatar si no ha sido modificado
                var query =
                    $@"
            UPDATE usuario SET
                {nameof(Usuario.Nombre)} = @Nombre,
                {nameof(Usuario.Apellido)} = @Apellido,
                {nameof(Usuario.Email)} = @Email,
                {nameof(Usuario.Contrasenia)} = @Contrasenia,
                {nameof(Usuario.Rol)} = @Rol,
                {nameof(Usuario.Estado)} = @Estado"
                    + (usuario.Avatar != null ? $", {nameof(Usuario.Avatar)} = @Avatar" : "")
                    + $" WHERE {nameof(Usuario.IdUsuario)} = @IdUsuario";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@IdUsuario", usuario.IdUsuario);
                    command.Parameters.AddWithValue("@Nombre", usuario.Nombre);
                    command.Parameters.AddWithValue("@Apellido", usuario.Apellido);
                    command.Parameters.AddWithValue("@Email", usuario.Email);
                    command.Parameters.AddWithValue("@Contrasenia", usuario.Contrasenia);

                    if (usuario.Avatar != null)
                    {
                        command.Parameters.AddWithValue("@Avatar", usuario.Avatar);
                    }

                    command.Parameters.AddWithValue("@Rol", usuario.Rol);
                    command.Parameters.AddWithValue("@Estado", usuario.Estado);

                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();
                    connection.Close();

                    return rowsAffected > 0;
                }
            }
        }

        public int EliminarLogico(int id)
        {
            using (MySqlConnection connection = new MySqlConnection(ConectionString))
            {
                var query =
                    $@"UPDATE usuario SET {nameof(Usuario.Estado)} = false WHERE {nameof(Usuario.IdUsuario)} = @IdUsuario";
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@IdUsuario", id);
                    connection.Open();
                    int result = command.ExecuteNonQuery();
                    connection.Close();
                    return result;
                }
            }
        }
    }
}
