using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace Inmobiliaria2Cuarti.Models
{
    public class RepositorioContrato
    {
        string ConectionString = "Server=localhost;User Id=root;Password=;Database=inmobiliaria2;";

        public List<Contrato> ObtenerTodos()
        {
            List<Contrato> contratos = new List<Contrato>();
            using (MySqlConnection connection = new MySqlConnection(ConectionString))
            {
                var query =
                    $@"SELECT c.{nameof(Contrato.IdContrato)},
                              c.{nameof(Contrato.IdInmueble)},
                              c.{nameof(Contrato.IdInquilino)},
                              c.{nameof(Contrato.FechaInicio)},
                              c.{nameof(Contrato.FechaFin)},
                              c.{nameof(Contrato.MontoRenta)},
                              c.{nameof(Contrato.Deposito)},
                              c.{nameof(Contrato.Comision)},
                              c.{nameof(Contrato.Condiciones)},
                              p.Nombre AS PropietarioNombre,
                              p.Apellido AS PropietarioApellido,
                              i.Direccion AS InmuebleDireccion,
                              inq.Nombre AS InquilinoNombre,
                              inq.Apellido AS InquilinoApellido
                       FROM contrato c
                       JOIN inmueble i ON c.IdInmueble = i.IdInmueble
                       JOIN propietario p ON i.IdPropietario = p.IdPropietario
                       JOIN inquilino inq ON c.IdInquilino = inq.IdInquilino";
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    connection.Open();
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        contratos.Add(
                            new Contrato
                            {
                                IdContrato = reader.GetInt32(nameof(Contrato.IdContrato)),
                                IdInmueble = reader.GetInt32(nameof(Contrato.IdInmueble)),
                                IdInquilino = reader.GetInt32(nameof(Contrato.IdInquilino)),
                                FechaInicio = reader.GetDateTime(nameof(Contrato.FechaInicio)),
                                FechaFin = reader.GetDateTime(nameof(Contrato.FechaFin)),
                                MontoRenta = reader.GetDecimal(nameof(Contrato.MontoRenta)),
                                Deposito = reader.GetDecimal(nameof(Contrato.Deposito)),
                                Comision = reader.GetDecimal(nameof(Contrato.Comision)),
                                Condiciones = reader.GetString(nameof(Contrato.Condiciones)),
                                PropietarioNombre = reader.GetString("PropietarioNombre"),
                                PropietarioApellido = reader.GetString("PropietarioApellido"),
                                InmuebleDireccion = reader.GetString("InmuebleDireccion"),
                                InquilinoNombre = reader.GetString("InquilinoNombre"),
                                InquilinoApellido = reader.GetString("InquilinoApellido"),
                            }
                        );
                    }
                    connection.Close();
                }
                return contratos;
            }
        }

        public List<Contrato> ObtenerPorPlazo(int plazo)
        {
            List<Contrato> contratos = new List<Contrato>();
            using (MySqlConnection connection = new MySqlConnection(ConectionString))
            {
                var query =
                    $@"SELECT c.{nameof(Contrato.IdContrato)},
                      c.{nameof(Contrato.IdInmueble)},
                      c.{nameof(Contrato.IdInquilino)},
                      c.{nameof(Contrato.FechaInicio)},
                      c.{nameof(Contrato.FechaFin)},
                      c.{nameof(Contrato.MontoRenta)},
                      c.{nameof(Contrato.Deposito)},
                      c.{nameof(Contrato.Comision)},
                      c.{nameof(Contrato.Condiciones)},
                      p.Nombre AS PropietarioNombre,
                      p.Apellido AS PropietarioApellido,
                      i.Direccion AS InmuebleDireccion,
                      inq.Nombre AS InquilinoNombre,
                      inq.Apellido AS InquilinoApellido
               FROM contrato c
               JOIN inmueble i ON c.IdInmueble = i.IdInmueble
               JOIN propietario p ON i.IdPropietario = p.IdPropietario
               JOIN inquilino inq ON c.IdInquilino = inq.IdInquilino
               WHERE (@plazo = 0 OR DATEDIFF(c.FechaFin, CURDATE()) BETWEEN @plazo - 30 AND @plazo)";
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@plazo", plazo);
                    connection.Open();
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        contratos.Add(
                            new Contrato
                            {
                                IdContrato = reader.GetInt32(nameof(Contrato.IdContrato)),
                                IdInmueble = reader.GetInt32(nameof(Contrato.IdInmueble)),
                                IdInquilino = reader.GetInt32(nameof(Contrato.IdInquilino)),
                                FechaInicio = reader.GetDateTime(nameof(Contrato.FechaInicio)),
                                FechaFin = reader.GetDateTime(nameof(Contrato.FechaFin)),
                                MontoRenta = reader.GetDecimal(nameof(Contrato.MontoRenta)),
                                Deposito = reader.GetDecimal(nameof(Contrato.Deposito)),
                                Comision = reader.GetDecimal(nameof(Contrato.Comision)),
                                Condiciones = reader.GetString(nameof(Contrato.Condiciones)),
                                PropietarioNombre = reader.GetString("PropietarioNombre"),
                                PropietarioApellido = reader.GetString("PropietarioApellido"),
                                InmuebleDireccion = reader.GetString("InmuebleDireccion"),
                                InquilinoNombre = reader.GetString("InquilinoNombre"),
                                InquilinoApellido = reader.GetString("InquilinoApellido"),
                            }
                        );
                    }
                    connection.Close();
                }
                return contratos;
            }
        }

        public Contrato? Obtener(int id)
        {
            Contrato? res = null;
            using (MySqlConnection connection = new MySqlConnection(ConectionString))
            {
                var query =
                    $@"SELECT c.{nameof(Contrato.IdContrato)},
                      c.{nameof(Contrato.IdInmueble)},
                      c.{nameof(Contrato.IdInquilino)},
                      c.{nameof(Contrato.FechaInicio)},
                      c.{nameof(Contrato.FechaFin)},
                      c.{nameof(Contrato.MontoRenta)},
                      c.{nameof(Contrato.Deposito)},
                      c.{nameof(Contrato.Comision)},
                      c.{nameof(Contrato.Condiciones)},
                      p.Nombre AS PropietarioNombre,
                      p.Apellido AS PropietarioApellido,
                      i.Direccion AS InmuebleDireccion,
                      inq.Nombre AS InquilinoNombre,
                      inq.Apellido AS InquilinoApellido
                FROM contrato c
                JOIN inmueble i ON c.IdInmueble = i.IdInmueble
                JOIN propietario p ON i.IdPropietario = p.IdPropietario
                JOIN inquilino inq ON c.IdInquilino = inq.IdInquilino
                WHERE c.{nameof(Contrato.IdContrato)} = @id";
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", id);
                    connection.Open();
                    var reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        res = new Contrato
                        {
                            IdContrato = reader.GetInt32(nameof(Contrato.IdContrato)),
                            IdInmueble = reader.GetInt32(nameof(Contrato.IdInmueble)),
                            IdInquilino = reader.GetInt32(nameof(Contrato.IdInquilino)),
                            FechaInicio = reader.GetDateTime(nameof(Contrato.FechaInicio)),
                            FechaFin = reader.GetDateTime(nameof(Contrato.FechaFin)),
                            MontoRenta = reader.GetDecimal(nameof(Contrato.MontoRenta)),
                            Deposito = reader.GetDecimal(nameof(Contrato.Deposito)),
                            Comision = reader.GetDecimal(nameof(Contrato.Comision)),
                            Condiciones = reader.GetString(nameof(Contrato.Condiciones)),
                    
                    // Asignar los datos del propietario e inquilino
                            PropietarioNombre = reader.GetString("PropietarioNombre"),
                            PropietarioApellido = reader.GetString("PropietarioApellido"),
                            InmuebleDireccion = reader.GetString("InmuebleDireccion"),
                            InquilinoNombre = reader.GetString("InquilinoNombre"),
                            InquilinoApellido = reader.GetString("InquilinoApellido")
                        };
                    }
                    connection.Close();
                }
            }
            return res;
        }


        

        public int CrearContrato(Contrato contrato)
        {
            int res = 0;
            using (MySqlConnection connection = new MySqlConnection(ConectionString))
            {
                var query =
                    @$"INSERT INTO contrato 
                            ({nameof(Contrato.IdInmueble)}, 
                             {nameof(Contrato.IdInquilino)}, 
                             {nameof(Contrato.FechaInicio)}, 
                             {nameof(Contrato.FechaFin)}, 
                             {nameof(Contrato.MontoRenta)}, 
                             {nameof(Contrato.Deposito)},
                             {nameof(Contrato.Comision)},
                             {nameof(Contrato.Condiciones)}) 
                         VALUES (@IdInmueble, @IdInquilino, @FechaInicio, @FechaFin, @MontoRenta, @Deposito, @Comision, @Condiciones); 
                         SELECT LAST_INSERT_ID();";
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@IdInmueble", contrato.IdInmueble);
                    command.Parameters.AddWithValue("@IdInquilino", contrato.IdInquilino);
                    command.Parameters.AddWithValue("@FechaInicio", contrato.FechaInicio);
                    command.Parameters.AddWithValue("@FechaFin", contrato.FechaFin);
                    command.Parameters.AddWithValue("@MontoRenta", contrato.MontoRenta);
                    command.Parameters.AddWithValue("@Deposito", contrato.Deposito);
                    command.Parameters.AddWithValue("@Comision", contrato.Comision);
                    command.Parameters.AddWithValue("@Condiciones", contrato.Condiciones);

                    connection.Open();
                    res = Convert.ToInt32(command.ExecuteScalar());
                }
            }
            return res;
        }

        public bool ActualizarContrato(Contrato contrato)
        {
            using (MySqlConnection connection = new MySqlConnection(ConectionString))
            {
                var sql =
                    @$"UPDATE contrato 
                         SET {nameof(Contrato.IdInmueble)} = @IdInmueble, 
                             {nameof(Contrato.IdInquilino)} = @IdInquilino, 
                             {nameof(Contrato.FechaInicio)} = @FechaInicio, 
                             {nameof(Contrato.FechaFin)} = @FechaFin, 
                             {nameof(Contrato.MontoRenta)} = @MontoRenta, 
                             {nameof(Contrato.Deposito)} = @Deposito,
                             {nameof(Contrato.Comision)} = @Comision,
                             {nameof(Contrato.Condiciones)} = @Condiciones
                         WHERE {nameof(Contrato.IdContrato)} = @IdContrato;";
                using (MySqlCommand command = new MySqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@IdContrato", contrato.IdContrato);
                    command.Parameters.AddWithValue("@IdInmueble", contrato.IdInmueble);
                    command.Parameters.AddWithValue("@IdInquilino", contrato.IdInquilino);
                    command.Parameters.AddWithValue("@FechaInicio", contrato.FechaInicio);
                    command.Parameters.AddWithValue("@FechaFin", contrato.FechaFin);
                    command.Parameters.AddWithValue("@MontoRenta", contrato.MontoRenta);
                    command.Parameters.AddWithValue("@Deposito", contrato.Deposito);
                    command.Parameters.AddWithValue("@Comision", contrato.Comision);
                    command.Parameters.AddWithValue("@Condiciones", contrato.Condiciones);

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
                    @$"UPDATE contrato 
                            SET {nameof(Contrato.Condiciones)} = 'Eliminado' 
                            WHERE {nameof(Contrato.IdContrato)} = @IdContrato;";
                using (MySqlCommand command = new MySqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@IdContrato", id);
                    connection.Open();
                    int result = command.ExecuteNonQuery();
                    connection.Close();
                    return result;
                }
            }
        }
    }
}
