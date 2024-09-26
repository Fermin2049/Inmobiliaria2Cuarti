
using Inmobiliaria2Cuatri.Models;
using MySql.Data.MySqlClient;

    public class RepositorioTipoInmueble
    {
        string connectionString = "Server=localhost;User Id=root;Password=;Database=inmobiliaria2;";

        // Obtener todos los tipos de inmuebles
        public List<TipoInmueble> ObtenerTodos()
        {
            var tiposInmueble = new List<TipoInmueble>();
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                var query = 
                    @$"SELECT {nameof(TipoInmueble.IdTipoInmueble)}, 
                               {nameof(TipoInmueble.Nombre)}, 
                               {nameof(TipoInmueble.Activo)} 
                       FROM tipoinmueble 
                       WHERE {nameof(TipoInmueble.Activo)} = 1";
                using (var command = new MySqlCommand(query, connection))
                {
                    connection.Open();
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        tiposInmueble.Add(new TipoInmueble
                        {
                            IdTipoInmueble = reader.GetInt32(nameof(TipoInmueble.IdTipoInmueble)),
                            Nombre = reader.GetString(nameof(TipoInmueble.Nombre)),
                            Activo = reader.GetBoolean(nameof(TipoInmueble.Activo))
                        });
                    }
                }
            }
            return tiposInmueble;
        }

        // Obtener un tipo de inmueble por su Id
        public TipoInmueble? ObtenerPorId(int id)
        {
            TipoInmueble? tipoInmueble = null;
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                var query = 
                    $@"SELECT {nameof(TipoInmueble.IdTipoInmueble)}, 
                               {nameof(TipoInmueble.Nombre)}, 
                               {nameof(TipoInmueble.Activo)} 
                       FROM tipoinmueble 
                       WHERE {nameof(TipoInmueble.IdTipoInmueble)} = @IdTipoInmueble";
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@IdTipoInmueble", id);
                    connection.Open();
                    var reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        tipoInmueble = new TipoInmueble
                        {
                            IdTipoInmueble = reader.GetInt32(nameof(TipoInmueble.IdTipoInmueble)),
                            Nombre = reader.GetString(nameof(TipoInmueble.Nombre)),
                            Activo = reader.GetBoolean(nameof(TipoInmueble.Activo))
                        };
                    }
                }
            }
            return tipoInmueble;
        }

        // Crear un nuevo tipo de inmueble
        public int Crear(TipoInmueble tipoInmueble)
        {
            int res = 0;
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                var query = 
                    @$"INSERT INTO tipoinmueble 
                        ({nameof(TipoInmueble.Nombre)}, 
                         {nameof(TipoInmueble.Activo)}) 
                    VALUES (@Nombre, @Activo); 
                    SELECT LAST_INSERT_ID();";
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Nombre", tipoInmueble.Nombre);
                    command.Parameters.AddWithValue("@Activo", tipoInmueble.Activo);
                    connection.Open();
                    res = Convert.ToInt32(command.ExecuteScalar());
                    connection.Close();
                }
            }
            return res;
        }

        // Actualizar un tipo de inmueble
        public bool Actualizar(TipoInmueble tipoInmueble)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                var query = 
                    $@"UPDATE tipoinmueble SET 
                                {nameof(TipoInmueble.Nombre)} = @Nombre, 
                                {nameof(TipoInmueble.Activo)} = @Activo 
                            WHERE {nameof(TipoInmueble.IdTipoInmueble)} = @IdTipoInmueble";
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@IdTipoInmueble", tipoInmueble.IdTipoInmueble);
                    command.Parameters.AddWithValue("@Nombre", tipoInmueble.Nombre);
                    command.Parameters.AddWithValue("@Activo", tipoInmueble.Activo);
                    connection.Open();
                    return command.ExecuteNonQuery() > 0;
                }
            }
        }

        // Eliminar un tipo de inmueble de manera lógica
        public bool EliminarLogico(int id)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                var query = 
                    $@"UPDATE tipoinmueble 
                       SET {nameof(TipoInmueble.Activo)} = 0 
                       WHERE {nameof(TipoInmueble.IdTipoInmueble)} = @IdTipoInmueble";
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@IdTipoInmueble", id);
                    connection.Open();
                    return command.ExecuteNonQuery() > 0;
                }
            }
        }
    }

