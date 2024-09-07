using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace Inmobiliaria2Cuatri.Models
{
    public class RepositorioInmueble
    {
        string ConectionString = "Server=localhost;User Id=root;Password=;Database=inmobiliaria2;";

        public List<Inmueble> ObtenerTodos()
        {
            List<Inmueble> inmuebles = new List<Inmueble>();
            using (MySqlConnection connection = new MySqlConnection(ConectionString))
            {
                var query =
                    $@"SELECT {nameof(Inmueble.IdInmueble)},
                                      {nameof(Inmueble.IdPropietario)},
                                      {nameof(Inmueble.Direccion)},
                                      {nameof(Inmueble.Uso)},
                                      {nameof(Inmueble.Tipo)},
                                      {nameof(Inmueble.CantAmbiente)},
                                      {nameof(Inmueble.Valor)},
                                      {nameof(Inmueble.Estado)}
                            FROM inmueble
                            WHERE {nameof(Inmueble.Estado)} = true"; // Solo traer activos
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
                                Direccion = reader.GetString(nameof(Inmueble.Direccion)),
                                Uso = reader.GetString(nameof(Inmueble.Uso)),
                                Tipo = Enum.Parse<TipoInmueble>(reader.GetString(nameof(Inmueble.Tipo))),
                                CantAmbiente = reader.GetInt32(nameof(Inmueble.CantAmbiente)),
                                Valor = reader.GetDecimal(nameof(Inmueble.Valor)),
                                Estado = reader.GetBoolean(nameof(Inmueble.Estado)),
                            }
                        );
                    }
                    connection.Close();
                }
                return inmuebles;
            }
        }

        public Inmueble? Obtener(int id)
        {
            Inmueble? res = null;
            using (MySqlConnection connection = new MySqlConnection(ConectionString))
            {
                var query =
                    $@"SELECT {nameof(Inmueble.IdInmueble)},
                                      {nameof(Inmueble.IdPropietario)},
                                      {nameof(Inmueble.Direccion)},
                                      {nameof(Inmueble.Uso)},
                                      {nameof(Inmueble.Tipo)},
                                      {nameof(Inmueble.CantAmbiente)},
                                      {nameof(Inmueble.Valor)},
                                      {nameof(Inmueble.Estado)}
                            FROM inmueble
                            WHERE {nameof(Inmueble.IdInmueble)} = @IdInmueble";
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
                            Direccion = reader.GetString(nameof(Inmueble.Direccion)),
                            Uso = reader.GetString(nameof(Inmueble.Uso)),
                            Tipo = Enum.Parse<TipoInmueble>(reader.GetString(nameof(Inmueble.Tipo))),
                            CantAmbiente = reader.GetInt32(nameof(Inmueble.CantAmbiente)),
                            Valor = reader.GetDecimal(nameof(Inmueble.Valor)),
                            Estado = reader.GetBoolean(nameof(Inmueble.Estado)),
                        };
                    }
                    connection.Close();
                }
                return res;
            }
        }

        public int CrearInmueble(Inmueble inmueble)
        {
            int res = 0;
            using (MySqlConnection connection = new MySqlConnection(ConectionString))
            {
                var query =
                    @$"INSERT INTO inmueble 
                            ({nameof(Inmueble.IdPropietario)}, 
                             {nameof(Inmueble.Direccion)}, 
                             {nameof(Inmueble.Uso)}, 
                             {nameof(Inmueble.Tipo)}, 
                             {nameof(Inmueble.CantAmbiente)},
                             {nameof(Inmueble.Valor)},
                             {nameof(Inmueble.Estado)}) 
                         VALUES (@IdPropietario, @Direccion, @Uso, @Tipo, @CantAmbiente, @Valor, @Estado); 
                         SELECT LAST_INSERT_ID();";
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@IdPropietario", inmueble.IdPropietario);
                    command.Parameters.AddWithValue("@Direccion", inmueble.Direccion);
                    command.Parameters.AddWithValue("@Uso", inmueble.Uso);
                    command.Parameters.AddWithValue("@Tipo", inmueble.Tipo.ToString());
                    command.Parameters.AddWithValue("@CantAmbiente", inmueble.CantAmbiente);
                    command.Parameters.AddWithValue("@Valor", inmueble.Valor);
                    command.Parameters.AddWithValue("@Estado", inmueble.Estado);

                    connection.Open();
                    res = Convert.ToInt32(command.ExecuteScalar());
                }
            return res;
        }
        }

        public bool ActualizarInmueble(Inmueble inmueble)
        {
            using (MySqlConnection connection = new MySqlConnection(ConectionString))
            {
                var sql =
                    @$"UPDATE inmueble 
                         SET {nameof(Inmueble.IdPropietario)} = @IdPropietario, 
                             {nameof(Inmueble.Direccion)} = @Direccion, 
                             {nameof(Inmueble.Uso)} = @Uso, 
                             {nameof(Inmueble.Tipo)} = @Tipo, 
                             {nameof(Inmueble.CantAmbiente)} = @CantAmbiente, 
                             {nameof(Inmueble.Valor)} = @Valor,
                             {nameof(Inmueble.Estado)} = @Estado
                         WHERE {nameof(Inmueble.IdInmueble)} = @IdInmueble;";
                using (MySqlCommand command = new MySqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@IdInmueble", inmueble.IdInmueble);
                    command.Parameters.AddWithValue("@IdPropietario", inmueble.IdPropietario);
                    command.Parameters.AddWithValue("@Direccion", inmueble.Direccion);
                    command.Parameters.AddWithValue("@Uso", inmueble.Uso);
                    command.Parameters.AddWithValue("@Tipo", inmueble.Tipo.ToString());
                    command.Parameters.AddWithValue("@CantAmbiente", inmueble.CantAmbiente);
                    command.Parameters.AddWithValue("@Valor", inmueble.Valor);
                    command.Parameters.AddWithValue("@Estado", inmueble.Estado);

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
    }
}

