using MySql.Data.MySqlClient;

namespace Inmobiliaria2Cuatri.Models;

public class RepositorioInquilino
{
    string ConectionString = "Server=localhost;User Id=root;Password=;Database=inmobiliaria2;";

    public List<Inquilino> ObtenerTodos()
    {
        List<Inquilino> inquilino = new List<Inquilino>();
        using (MySqlConnection connection = new MySqlConnection(ConectionString))
        {
            var query =
                $@"SELECT {nameof(Inquilino.IdInquilino)},
                                      {nameof(Inquilino.Nombre)},
                                      {nameof(Inquilino.Apellido)},
                                      {nameof(Inquilino.Dni)},
                                      {nameof(Inquilino.Email)},
                                      {nameof(Inquilino.Telefono)},
                                      {nameof(Inquilino.Estado)}
                            FROM inquilino
                            WHERE {nameof(Inquilino.Estado)} = true"; // Solo traer activos";
            using (MySqlCommand command = new MySqlCommand(query, connection))
            {
                connection.Open();
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    inquilino.Add(
                        new Inquilino
                        {
                            IdInquilino = reader.GetInt32(nameof(Inquilino.IdInquilino)),
                            Nombre = reader.GetString(nameof(Inquilino.Nombre)),
                            Apellido = reader.GetString(nameof(Inquilino.Apellido)),
                            Dni = reader.GetInt32(nameof(Inquilino.Dni)),
                            Email = reader.GetString(nameof(Inquilino.Email)),
                            Telefono = reader.GetString(nameof(Inquilino.Telefono)),
                            Estado = reader.GetBoolean(nameof(Inquilino.Estado)),
                        }
                    );
                }
                connection.Close();
            }
            return inquilino;
        }
    }

    public Inquilino? Obtener(int id)
    {
        Inquilino? res = null;
        using (MySqlConnection connection = new MySqlConnection(ConectionString))
        {
            var query =
                $@"SELECT {nameof(Inquilino.IdInquilino)},
                                      {nameof(Inquilino.Nombre)},
                                      {nameof(Inquilino.Apellido)},
                                      {nameof(Inquilino.Dni)},
                                      {nameof(Inquilino.Email)},
                                      {nameof(Inquilino.Telefono)},
                                      {nameof(Inquilino.Estado)}
                            FROM inquilino
                            WHERE {nameof(Inquilino.IdInquilino)} = @IdInquilino";

            using (MySqlCommand command = new MySqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@IdInquilino", id);
                connection.Open();
                var reader = command.ExecuteReader();
                if (reader.Read())
                {
                    res = new Inquilino
                    {
                        IdInquilino = reader.GetInt32(nameof(Inquilino.IdInquilino)),
                        Nombre = reader.GetString(nameof(Inquilino.Nombre)),
                        Apellido = reader.GetString(nameof(Inquilino.Apellido)),
                        Dni = reader.GetInt32(nameof(Inquilino.Dni)),
                        Email = reader.GetString(nameof(Inquilino.Email)),
                        Telefono = reader.GetString(nameof(Inquilino.Telefono)),
                        Estado = reader.GetBoolean(nameof(Inquilino.Estado)),
                    };
                }
                connection.Close();
            }
            return res;
        }
    }

    public int CrearInquilino(Inquilino inquilino)
    {
        int res = 0;
        using (MySqlConnection connection = new MySqlConnection(ConectionString))
        {
            var query =
                @$"INSERT INTO inquilino 
                            ({nameof(Inquilino.Nombre)}, 
                             {nameof(Inquilino.Apellido)}, 
                             {nameof(Inquilino.Dni)}, 
                             {nameof(Inquilino.Email)}, 
                             {nameof(Inquilino.Telefono)},
                             {nameof(Inquilino.Estado)}) 
                         VALUES (@Nombre, @Apellido, @Dni, @Email, @Telefono, @Estado); 
                         SELECT LAST_INSERT_ID();";
            using (MySqlCommand command = new MySqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Nombre", inquilino.Nombre);
                command.Parameters.AddWithValue("@Apellido", inquilino.Apellido);
                command.Parameters.AddWithValue("@Dni", inquilino.Dni);
                command.Parameters.AddWithValue("@Email", inquilino.Email);
                command.Parameters.AddWithValue("@Telefono", inquilino.Telefono);
                command.Parameters.AddWithValue("@Estado", inquilino.Estado);

                connection.Open();
                try
                {
                    res = Convert.ToInt32(command.ExecuteScalar());
                }
                catch (MySqlException ex) when (ex.Number == 1062) // Código de error para duplicados
                {
                    throw new Exception(
                        "Ya existe un inquilino con el mismo DNI, Email o Teléfono."
                    );
                }
            }
        }
        return res;
    }

    public bool ActualizarInquilino(Inquilino inquilino)
    {
        using (MySqlConnection connection = new MySqlConnection(ConectionString))
        {
            var sql =
                @$"UPDATE inquilino 
                         SET {nameof(Inquilino.Nombre)} = @Nombre, 
                             {nameof(Inquilino.Apellido)} = @Apellido, 
                             {nameof(Inquilino.Dni)} = @Dni, 
                             {nameof(Inquilino.Email)} = @Email, 
                             {nameof(Inquilino.Telefono)} = @Telefono,
                             {nameof(Inquilino.Estado)} = @Estado
                         WHERE {nameof(Inquilino.IdInquilino)} = @IdInquilino;";
            using (MySqlCommand command = new MySqlCommand(sql, connection))
            {
                command.Parameters.AddWithValue("@IdInquilino", inquilino.IdInquilino);
                command.Parameters.AddWithValue("@Nombre", inquilino.Nombre);
                command.Parameters.AddWithValue("@Apellido", inquilino.Apellido);
                command.Parameters.AddWithValue("@Dni", inquilino.Dni);
                command.Parameters.AddWithValue("@Email", inquilino.Email);
                command.Parameters.AddWithValue("@Telefono", inquilino.Telefono);
                command.Parameters.AddWithValue("@Estado", inquilino.Estado);

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
                @$"UPDATE inquilino 
                            SET {nameof(Inquilino.Estado)} = @Estado 
                            WHERE {nameof(Inquilino.IdInquilino)} = @IdInquilino;";
            using (MySqlCommand command = new MySqlCommand(sql, connection))
            {
                command.Parameters.AddWithValue("@idInquilino", id);
                command.Parameters.AddWithValue("@Estado", false); // Marcar como eliminado lógico
                connection.Open();
                int result = command.ExecuteNonQuery();
                connection.Close();
                return result;
            }
        }
    }
}
