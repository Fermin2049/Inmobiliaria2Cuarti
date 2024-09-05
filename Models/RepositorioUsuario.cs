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
                                Nombre = reader.GetString(nameof(Usuario.Nombre)),
                                Apellido = reader.GetString(nameof(Usuario.Apellido)),
                                Email = reader.GetString(nameof(Usuario.Email)),
                                Contrasenia = reader.GetString(nameof(Usuario.Contrasenia)),
                                Avatar = reader.GetString(nameof(Usuario.Avatar)),
                                Rol = reader.GetString(nameof(Usuario.Rol)),
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
                            Nombre = reader.GetString(nameof(Usuario.Nombre)),
                            Apellido = reader.GetString(nameof(Usuario.Apellido)),
                            Email = reader.GetString(nameof(Usuario.Email)),
                            Contrasenia = reader.GetString(nameof(Usuario.Contrasenia)),
                            Avatar = reader.GetString(nameof(Usuario.Avatar)),
                            Rol = reader.GetString(nameof(Usuario.Rol)),
                            Estado = reader.GetBoolean(nameof(Usuario.Estado)),
                        };
                    }
                    connection.Close();
                }
                return res;
            }
        }

        public int CrearUsuario(Usuario usuario)
        {
            int res = 0;
            using (MySqlConnection connection = new MySqlConnection(ConectionString))
            {
                var query =
                    @$"INSERT INTO usuario 
                            ({nameof(Usuario.Nombre)}, 
                             {nameof(Usuario.Apellido)}, 
                             {nameof(Usuario.Email)}, 
                             {nameof(Usuario.Contrasenia)}, 
                             {nameof(Usuario.Avatar)},
                             {nameof(Usuario.Rol)},
                            {nameof(Usuario.Estado)}) 
                         VALUES (@Nombre, @Apellido, @Email, @Clave, @Avatar, @Rol, @Estado); 
                         SELECT LAST_INSERT_ID();";
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Nombre", usuario.Nombre);
                    command.Parameters.AddWithValue("@Apellido", usuario.Apellido);
                    command.Parameters.AddWithValue("@Email", usuario.Email);
                    command.Parameters.AddWithValue("@Clave", usuario.Contrasenia);
                    command.Parameters.AddWithValue("@Avatar", usuario.Avatar);
                    command.Parameters.AddWithValue("@Rol", usuario.Rol);
                    command.Parameters.AddWithValue("@Estado", usuario.Estado);

                    connection.Open();
                    res = Convert.ToInt32(command.ExecuteScalar());
                }
            }
            return res;
        }

        public bool ActualizarUsuario(Usuario usuario)
        {
            using (MySqlConnection connection = new MySqlConnection(ConectionString))
            {
                var sql =
                    @$"UPDATE usuario 
                         SET {nameof(Usuario.Nombre)} = @Nombre, 
                             {nameof(Usuario.Apellido)} = @Apellido, 
                             {nameof(Usuario.Email)} = @Email, 
                             {nameof(Usuario.Contrasenia)} = @Clave, 
                             {nameof(Usuario.Avatar)} = @Avatar,
                             {nameof(Usuario.Rol)} = @Rol,
                             {nameof(Usuario.Estado)} = @Estado
                         WHERE {nameof(Usuario.IdUsuario)} = @IdUsuario;";
                using (MySqlCommand command = new MySqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@IdUsuario", usuario.IdUsuario);
                    command.Parameters.AddWithValue("@Nombre", usuario.Nombre);
                    command.Parameters.AddWithValue("@Apellido", usuario.Apellido);
                    command.Parameters.AddWithValue("@Email", usuario.Email);
                    command.Parameters.AddWithValue("@Clave", usuario.Contrasenia);
                    command.Parameters.AddWithValue("@Avatar", usuario.Avatar);
                    command.Parameters.AddWithValue("@Rol", usuario.Rol);
                    command.Parameters.AddWithValue("@Estado", usuario.Estado);

                    connection.Open();
                    int result = command.ExecuteNonQuery();
                    return result > 0;
                }
            }
        }

        public int EliminarLogico(int id)
        {
            using (MySqlConnection connection = new MySqlConnection(ConectionString))
            {
                string sql =
                    @$"UPDATE usuario 
                            SET {nameof(Usuario.Rol)} = 'Eliminado' 
                            WHERE {nameof(Usuario.IdUsuario)} = @IdUsuario;";
                using (MySqlCommand command = new MySqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);
                    connection.Open();
                    int result = command.ExecuteNonQuery();
                    connection.Close();
                    return result;
                }
            }
        }
    }
}
