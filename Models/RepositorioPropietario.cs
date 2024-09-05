using MySql.Data.MySqlClient;

namespace Inmobiliaria2Cuatri.Models;
    public class RepositorioPropietario
    {
        string ConectionString = "Server=localhost;User Id=root;Password=;Database=inmobiliaria2;";

        public List<Propietario> ObtenerTodos()
        {
            List<Propietario> propietarios = new List<Propietario>();
            using(MySqlConnection connection = new MySqlConnection(ConectionString))
            {
                var query = $@"SELECT {nameof(Propietario.IdPropietario)},
                                      {nameof(Propietario.Nombre)},
                                      {nameof(Propietario.Apellido)},
                                      {nameof(Propietario.Dni)},
                                      {nameof(Propietario.Email)},
                                      {nameof(Propietario.Telefono)},
                                      {nameof(Propietario.Estado)}
                            FROM propietario
                            WHERE {nameof(Propietario.Estado)} = true"; // Solo traer activos"; 
                using(MySqlCommand command = new MySqlCommand(query, connection))
                {
                    connection.Open();
                    var reader = command.ExecuteReader();
                    while(reader.Read())
                    {
                        propietarios.Add(new Propietario
                        {
                            IdPropietario = reader.GetInt32(nameof(Propietario.IdPropietario)),
                            Nombre = reader.GetString(nameof(Propietario.Nombre)),
                            Apellido = reader.GetString(nameof(Propietario.Apellido)),
                            Dni = reader.GetInt32(nameof(Propietario.Dni)),
                            Email = reader.GetString(nameof(Propietario.Email)),
                            Telefono = reader.GetString(nameof(Propietario.Telefono)),
                            Estado = reader.GetBoolean(nameof(Propietario.Estado))
                        });
                    }
                    connection.Close();
                }
                return propietarios;
            }
        }

        public Propietario? Obtener(int id)
        {
            Propietario? res = null;
            using(MySqlConnection connection = new MySqlConnection(ConectionString))
            {
                var query = $@"SELECT {nameof(Propietario.IdPropietario)},
                                      {nameof(Propietario.Nombre)},
                                      {nameof(Propietario.Apellido)},
                                      {nameof(Propietario.Dni)},
                                      {nameof(Propietario.Email)},
                                      {nameof(Propietario.Telefono)},
                                      {nameof(Propietario.Estado)}
                            FROM propietario
                            WHERE {nameof(Propietario.IdPropietario)} = @IdPropietario";
                            
                using(MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@IdPropietario", id);
                    connection.Open();
                    var reader = command.ExecuteReader();
                    if(reader.Read())
                    {
                        res = new Propietario
                        {
                            IdPropietario = reader.GetInt32(nameof(Propietario.IdPropietario)),
                            Nombre = reader.GetString(nameof(Propietario.Nombre)),
                            Apellido = reader.GetString(nameof(Propietario.Apellido)),
                            Dni = reader.GetInt32(nameof(Propietario.Dni)),
                            Email = reader.GetString(nameof(Propietario.Email)),
                            Telefono = reader.GetString(nameof(Propietario.Telefono)),
                            Estado = reader.GetBoolean(nameof(Propietario.Estado))
                        };
                    }
                    connection.Close();
                }
                return res;
            }
        }

        public int CrearPropietario(Propietario propietario)
        {
            int res = 0;
            using (MySqlConnection connection = new MySqlConnection(ConectionString))
            {
                var query = @$"INSERT INTO propietario 
                            ({nameof(Propietario.Nombre)}, 
                             {nameof(Propietario.Apellido)}, 
                             {nameof(Propietario.Dni)}, 
                             {nameof(Propietario.Email)}, 
                             {nameof(Propietario.Telefono)},
                             {nameof(Propietario.Estado)}) 
                         VALUES (@Nombre, @Apellido, @Dni, @Email, @Telefono, @Estado); 
                         SELECT LAST_INSERT_ID();";
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Nombre", propietario.Nombre);
                    command.Parameters.AddWithValue("@Apellido", propietario.Apellido);
                    command.Parameters.AddWithValue("@Dni", propietario.Dni);
                    command.Parameters.AddWithValue("@Email", propietario.Email);
                    command.Parameters.AddWithValue("@Telefono", propietario.Telefono);
                    command.Parameters.AddWithValue("@Estado", propietario.Estado);
             
                    connection.Open();
                    res = Convert.ToInt32(command.ExecuteScalar());
                }
            } 
            return res;
        }

    public bool ActualizarPropietario(Propietario propietario)
    {
        
        using (MySqlConnection connection = new MySqlConnection(ConectionString))
        {
            var sql = @$"UPDATE propietario 
                         SET {nameof(Propietario.Nombre)} = @Nombre, 
                             {nameof(Propietario.Apellido)} = @Apellido, 
                             {nameof(Propietario.Dni)} = @Dni, 
                             {nameof(Propietario.Email)} = @Email, 
                             {nameof(Propietario.Telefono)} = @Telefono,
                             {nameof(Propietario.Estado)} = @Estado
                         WHERE {nameof(Propietario.IdPropietario)} = @IdPropietario;";
            using (MySqlCommand command = new MySqlCommand(sql, connection))
            {
                command.Parameters.AddWithValue("@idPropietario", propietario.IdPropietario);
                command.Parameters.AddWithValue("@Nombre", propietario.Nombre);
                command.Parameters.AddWithValue("@Apellido", propietario.Apellido);
                command.Parameters.AddWithValue("@Dni", propietario.Dni);
                command.Parameters.AddWithValue("@Email", propietario.Email);
                command.Parameters.AddWithValue("@Telefono", propietario.Telefono);
                command.Parameters.AddWithValue("@Estado", propietario.Estado);
                
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
            string sql = @$"UPDATE propietario 
                            SET {nameof(Propietario.Estado)} = @Estado 
                            WHERE {nameof(Propietario.IdPropietario)} = @idPropietario;";
            using (MySqlCommand command = new MySqlCommand(sql, connection))
            {
                command.Parameters.AddWithValue("@idPropietario", id);
                command.Parameters.AddWithValue("@Estado", false); // Marcar como eliminado lógico
                connection.Open();
                int result = command.ExecuteNonQuery();
                connection.Close();
                return result;
            }
        }
    }
}
    
    