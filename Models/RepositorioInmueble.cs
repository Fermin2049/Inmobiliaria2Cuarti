using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace Inmobiliaria2Cuatri.Models;

    public class RepositorioInmueble
    {
        string ConectionString = "Server=localhost;User Id=root;Password=;Database=inmobiliaria2;";

        public List<Inmueble> ObtenerTodos()
        {
            List<Inmueble> inmuebles = new List<Inmueble>();
            using (MySqlConnection connection = new MySqlConnection(ConectionString))
            {
                var query =
                    @$"SELECT I.{nameof(Inmueble.IdInmueble)},
                        I.{nameof(Inmueble.IdPropietario)},
                        I.{nameof(Inmueble.IdTipoInmueble)},
                        I.{nameof(Inmueble.Direccion)},
                        I.{nameof(Inmueble.Uso)},
                        I.{nameof(Inmueble.CantAmbiente)},
                        I.{nameof(Inmueble.Valor)},
                        I.{nameof(Inmueble.Estado)},
                        I.{nameof(Inmueble.Disponible)},
                        P.Nombre AS PropietarioNombre,
                        P.Apellido AS PropietarioApellido,
                        P.Dni AS PropietarioDni,
                        t.Nombre AS TipoInmuebleNombre
                    FROM inmueble I
                    JOIN propietario P ON P.IdPropietario = I.IdPropietario
                    JOIN tipoinmueble t ON t.IdTipoInmueble = I.IdTipoInmueble
                    WHERE I.{nameof(Inmueble.Estado)} = true";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    connection.Open();
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        inmuebles.Add(
                            new Inmueble
                            {
                                IdInmueble = reader.GetInt32(nameof(Inmueble.IdInmueble)),
                                IdPropietario = reader.GetInt32(nameof(Inmueble.IdPropietario)),
                                IdTipoInmueble = reader.GetInt32(nameof(Inmueble.IdTipoInmueble)),
                                Direccion = reader.GetString(nameof(Inmueble.Direccion)),
                                Uso = (Uso)reader.GetInt32(nameof(Inmueble.Uso)),
                                CantAmbiente = reader.GetInt32(nameof(Inmueble.CantAmbiente)),
                                Valor = reader.GetInt32(nameof(Inmueble.Valor)),
                                Estado = reader.GetBoolean(nameof(Inmueble.Estado)),
                                Disponible = reader.GetBoolean(nameof(Inmueble.Disponible)),
                                Propietario = new Propietario
                                {
                                    IdPropietario = reader.GetInt32("IdPropietario"),
                                    Nombre = reader.GetString("PropietarioNombre"),
                                    Apellido = reader.GetString("PropietarioApellido"),
                                    Dni = reader.GetInt32("PropietarioDni"),
                                },
                                TipoInmueble = new TipoInmueble
                                {
                                    IdTipoInmueble = reader.GetInt32("IdTipoInmueble"),
                                    Nombre = reader.GetString("TipoInmuebleNombre"),   
                                }
                            }
                        );
                    }
                }
            }
            return inmuebles;
        }

        public Inmueble? Obtener(int id)
        {
            Inmueble? res = null;
            using (MySqlConnection connection = new MySqlConnection(ConectionString))
            {
                var query =
                    $@"SELECT i.{nameof(Inmueble.IdInmueble)},
                                  i.{nameof(Inmueble.IdPropietario)},
                                  i.{nameof(Inmueble.IdTipoInmueble)},
                                  i.{nameof(Inmueble.Direccion)},
                                  i.{nameof(Inmueble.Uso)},
                                  i.{nameof(Inmueble.CantAmbiente)},
                                  i.{nameof(Inmueble.Valor)},
                                  i.{nameof(Inmueble.Estado)},
                                  i.{nameof(Inmueble.Disponible)},
                                  p.Nombre,
                                  p.Apellido,
                                  p.Dni,
                                  t.Nombre AS TipoInmueble
                        FROM inmueble i
                        INNER JOIN propietario p ON i.IdPropietario = p.IdPropietario
                        INNER JOIN tipoinmueble t ON i.IdTipoInmueble = t.IdTipoInmueble
                        WHERE i.{nameof(Inmueble.IdInmueble)} = @IdInmueble";
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@IdInmueble", id);
                    connection.Open();
                    var reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        res = new Inmueble
                        {
                            IdInmueble = reader.GetInt32(nameof(Inmueble.IdInmueble)),
                            IdPropietario = reader.GetInt32(nameof(Inmueble.IdPropietario)),
                            IdTipoInmueble = reader.GetInt32(nameof(Inmueble.IdTipoInmueble)),
                            Direccion = reader.GetString(nameof(Inmueble.Direccion)),
                            Uso = (Uso)reader.GetInt32(nameof(Inmueble.Uso)),
                            CantAmbiente = reader.GetInt32(nameof(Inmueble.CantAmbiente)),
                            Valor = reader.GetInt32(nameof(Inmueble.Valor)),
                            Estado = reader.GetBoolean(nameof(Inmueble.Estado)),
                            Disponible = reader.GetBoolean(nameof(Inmueble.Disponible)),
                            Propietario = new Propietario
                            {
                                IdPropietario = reader.GetInt32("IdPropietario"),
                                Nombre = reader.GetString("Nombre"),
                                Apellido = reader.GetString("Apellido"),
                                Dni = reader.GetInt32("Dni"),
                            },
                            TipoInmueble = new TipoInmueble
                            {
                                IdTipoInmueble = reader.GetInt32("IdTipoInmueble"),
                                Nombre = reader.GetString("TipoInmueble"),
                            }
                        };
                    }
                }
            }
            return res;
        }

        public int CrearInmueble(Inmueble inmueble)
        {
            int res = 0;
            using (MySqlConnection connection = new MySqlConnection(ConectionString))
            {
                var query =
                    @$"INSERT INTO inmueble 
                    ({nameof(Inmueble.IdPropietario)}, 
                     {nameof(Inmueble.IdTipoInmueble)},
                     {nameof(Inmueble.Direccion)}, 
                     {nameof(Inmueble.Uso)}, 
                     {nameof(Inmueble.CantAmbiente)},
                     {nameof(Inmueble.Valor)},
                     {nameof(Inmueble.Estado)}, 
                     {nameof(Inmueble.Disponible)}) 
                 VALUES (@IdPropietario, @IdTipoInmueble, @Direccion, @Uso, @CantAmbiente, @Valor, @Estado, @Disponible); 
                 SELECT LAST_INSERT_ID();";
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@IdPropietario", inmueble.IdPropietario);
                    command.Parameters.AddWithValue("@IdTipoInmueble", inmueble.IdTipoInmueble);
                    command.Parameters.AddWithValue("@Direccion", inmueble.Direccion);
                    command.Parameters.AddWithValue("@Uso", (int)inmueble.Uso);
                    command.Parameters.AddWithValue("@CantAmbiente", inmueble.CantAmbiente);
                    command.Parameters.AddWithValue("@Valor", inmueble.Valor);
                    command.Parameters.AddWithValue("@Estado", inmueble.Estado);
                    command.Parameters.AddWithValue("@Disponible", inmueble.Disponible);

                    connection.Open();
                    res = Convert.ToInt32(command.ExecuteScalar());
                    inmueble.IdInmueble = res;
                    connection.Close();
                }
                return res;
            }
        }

        public bool ActualizarInmueble(Inmueble inmueble)
        {
            using (MySqlConnection connection = new MySqlConnection(ConectionString))
            {
                var sql =
                    $@"UPDATE inmueble SET 
                                IdPropietario = @IdPropietario,
                                IdTipoInmueble = @IdTipoInmueble,
                                Direccion = @Direccion,
                                Uso = @Uso,   
                                CantAmbiente = @CantAmbiente,
                                Valor = @Valor,
                                Estado = @Estado,
                                Disponible = @Disponible
                            WHERE IdInmueble = @IdInmueble";
                using (MySqlCommand command = new MySqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@IdPropietario", inmueble.IdPropietario);
                    command.Parameters.AddWithValue("@IdTipoInmueble", inmueble.IdTipoInmueble);
                    command.Parameters.AddWithValue("@Direccion", inmueble.Direccion);
                    command.Parameters.AddWithValue("@Uso", (int)inmueble.Uso);
                    command.Parameters.AddWithValue("@CantAmbiente", inmueble.CantAmbiente);
                    command.Parameters.AddWithValue("@Valor", inmueble.Valor);
                    command.Parameters.AddWithValue("@Estado", inmueble.Estado);
                    command.Parameters.AddWithValue("@Disponible", inmueble.Disponible);
                    command.Parameters.AddWithValue("@IdInmueble", inmueble.IdInmueble);
                    connection.Open();
                    return command.ExecuteNonQuery() > 0;
                }
            }
        }

        public int EliminarLogico(int id)
        {
            using (MySqlConnection connection = new MySqlConnection(ConectionString))
            {
                string sql =
                    @$"UPDATE inmueble 
                            SET {nameof(Inmueble.Estado)} = @Estado 
                            WHERE {nameof(Inmueble.IdInmueble)} = @IdInmueble;";
                using (MySqlCommand command = new MySqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@IdInmueble", id);
                    command.Parameters.AddWithValue("@Estado", false); // Marcar como eliminado lógico
                    connection.Open();
                    int result = command.ExecuteNonQuery();
                    connection.Close();
                    return result;
                }
            }
        }

        public List<int> ObtenerInquilinosPorInmueble()
        {
            List<int> inquilinosPorInmueble = new List<int>();
            using (MySqlConnection connection = new MySqlConnection(ConectionString))
            {
                var query =
                    @"SELECT COUNT(i.IdInquilino) AS CantidadInquilinos
                              FROM inmueble AS im
                              LEFT JOIN contrato AS c ON im.IdInmueble = c.IdInmueble
                              LEFT JOIN inquilino AS i ON c.IdInquilino = i.IdInquilino
                              GROUP BY im.IdInmueble";
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    connection.Open();
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        inquilinosPorInmueble.Add(reader.GetInt32("CantidadInquilinos"));
                    }
                    connection.Close();
                }
            }
            return inquilinosPorInmueble;
        }
    }

