using System;
using System.Collections.Generic;
using Inmobiliaria2Cuatri.Models;
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
            Contrato? contrato = null;
            using (MySqlConnection connection = new MySqlConnection(ConectionString))
            {
                var query =
                    $@"
                    SELECT c.{nameof(Contrato.IdContrato)},
                           c.{nameof(Contrato.IdInmueble)},
                           c.{nameof(Contrato.IdInquilino)},
                           c.{nameof(Contrato.FechaInicio)},
                           c.{nameof(Contrato.FechaFin)},
                           c.{nameof(Contrato.MontoRenta)},
                           c.{nameof(Contrato.Deposito)},
                           c.{nameof(Contrato.Comision)},
                           c.{nameof(Contrato.Condiciones)},
                           c.{nameof(Contrato.UsuarioCreacion)},
                           c.{nameof(Contrato.UsuarioTerminacion)},
                           c.{nameof(Contrato.MultaTerminacionTemprana)},
                           c.{nameof(Contrato.FechaTerminacionTemprana)},
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
                        contrato = new Contrato
                        {
                            IdContrato = reader.GetInt32(nameof(Contrato.IdContrato)),
                            IdInmueble = reader.GetInt32(nameof(Contrato.IdInmueble)),
                            IdInquilino = reader.GetInt32(nameof(Contrato.IdInquilino)),
                            FechaInicio = reader.GetDateTime(nameof(Contrato.FechaInicio)),
                            FechaFin = reader.GetDateTime(nameof(Contrato.FechaFin)),
                            MontoRenta = reader.GetDecimal(nameof(Contrato.MontoRenta)),
                            Deposito = reader.GetDecimal(nameof(Contrato.Deposito)),
                            Comision = reader.GetDecimal(nameof(Contrato.Comision)),
                            Condiciones = reader.IsDBNull(
                                reader.GetOrdinal(nameof(Contrato.Condiciones))
                            )
                                ? null
                                : reader.GetString(nameof(Contrato.Condiciones)),
                            UsuarioCreacion = reader.IsDBNull(
                                reader.GetOrdinal(nameof(Contrato.UsuarioCreacion))
                            )
                                ? null
                                : reader.GetString(nameof(Contrato.UsuarioCreacion)),
                            UsuarioTerminacion = reader.IsDBNull(
                                reader.GetOrdinal(nameof(Contrato.UsuarioTerminacion))
                            )
                                ? null
                                : reader.GetString(nameof(Contrato.UsuarioTerminacion)),
                            MultaTerminacionTemprana = reader.IsDBNull(
                                reader.GetOrdinal(nameof(Contrato.MultaTerminacionTemprana))
                            )
                                ? (decimal?)null
                                : reader.GetDecimal(nameof(Contrato.MultaTerminacionTemprana)),
                            FechaTerminacionTemprana = reader.IsDBNull(
                                reader.GetOrdinal(nameof(Contrato.FechaTerminacionTemprana))
                            )
                                ? (DateTime?)null
                                : reader.GetDateTime(nameof(Contrato.FechaTerminacionTemprana)),
                            PropietarioNombre = reader.GetString("PropietarioNombre"),
                            PropietarioApellido = reader.GetString("PropietarioApellido"),
                            InmuebleDireccion = reader.GetString("InmuebleDireccion"),
                            InquilinoNombre = reader.GetString("InquilinoNombre"),
                            InquilinoApellido = reader.GetString("InquilinoApellido"),
                        };
                    }
                    connection.Close();
                }
            }
            return contrato;
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
                             {nameof(Contrato.Condiciones)},
                             {nameof(Contrato.UsuarioCreacion)}) 
                         VALUES (@IdInmueble, @IdInquilino, @FechaInicio, @FechaFin, @MontoRenta, @Deposito, @Comision, @Condiciones, @UsuarioCreacion); 
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
                    command.Parameters.AddWithValue("@UsuarioCreacion", contrato.UsuarioCreacion);

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
                var query =
                    $@"
                            UPDATE contrato SET
                                IdInmueble = @IdInmueble,
                                IdInquilino = @IdInquilino,
                                FechaInicio = @FechaInicio,
                                FechaFin = @FechaFin,
                                MontoRenta = @MontoRenta,
                                Deposito = @Deposito,
                                Comision = @Comision,
                                Condiciones = @Condiciones,
                                UsuarioTerminacion = @UsuarioTerminacion,
                                MultaTerminacionTemprana = @MultaTerminacionTemprana,
                                FechaTerminacionTemprana = @FechaTerminacionTemprana
                            WHERE IdContrato = @IdContrato";
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
                    command.Parameters.AddWithValue(
                        "@UsuarioTerminacion",
                        contrato.UsuarioTerminacion
                    );
                    command.Parameters.AddWithValue(
                        "@MultaTerminacionTemprana",
                        contrato.MultaTerminacionTemprana
                    );
                    command.Parameters.AddWithValue(
                        "@FechaTerminacionTemprana",
                        contrato.FechaTerminacionTemprana
                    );
                    command.Parameters.AddWithValue("@IdContrato", contrato.IdContrato);
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

        public List<int> ObtenerNuevosContratosPorMes()
        {
            List<int> nuevosContratosPorMes = new List<int>(new int[12]); // Inicializar lista con 12 ceros
            using (MySqlConnection connection = new MySqlConnection(ConectionString))
            {
                var query =
                    @"SELECT MONTH(FechaInicio) AS Mes, COUNT(IdContrato) AS CantidadContratos
                              FROM contrato
                              WHERE YEAR(FechaInicio) = YEAR(CURDATE())
                              GROUP BY MONTH(FechaInicio)";
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    connection.Open();
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        int mes = reader.GetInt32("Mes");
                        int cantidadContratos = reader.GetInt32("CantidadContratos");
                        nuevosContratosPorMes[mes - 1] = cantidadContratos; // Meses en MySQL van de 1 a 12
                    }
                    connection.Close();
                }
            }
            return nuevosContratosPorMes;
        }

        public List<Inmueble> ObtenerInmueblesNoOcupados(DateTime fechaInicio, DateTime fechaFin)
        {
            List<Inmueble> inmuebles = new List<Inmueble>();
            using (MySqlConnection connection = new MySqlConnection(ConectionString))
            {
                string query =
                    @"SELECT I.IdInmueble, I.IdPropietario, I.IdTipoInmueble, I.Direccion, I.Uso, I.CantAmbiente, I.Valor, I.Estado, I.Disponible
                                 FROM inmueble I
                                 LEFT JOIN contrato C ON I.IdInmueble = C.IdInmueble
                                 WHERE (C.FechaInicio IS NULL OR C.FechaFin < @fechaInicio OR C.FechaInicio > @fechaFin) AND I.Disponible = 1";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@fechaInicio", fechaInicio);
                    command.Parameters.AddWithValue("@fechaFin", fechaFin);

                    connection.Open();
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Inmueble inmueble = new Inmueble
                            {
                                IdInmueble = reader.IsDBNull(reader.GetOrdinal("IdInmueble"))
                                    ? 0
                                    : reader.GetInt32(reader.GetOrdinal("IdInmueble")),
                                IdPropietario = reader.IsDBNull(reader.GetOrdinal("IdPropietario"))
                                    ? 0
                                    : reader.GetInt32(reader.GetOrdinal("IdPropietario")),
                                IdTipoInmueble = reader.IsDBNull(
                                    reader.GetOrdinal("IdTipoInmueble")
                                )
                                    ? 0
                                    : reader.GetInt32(reader.GetOrdinal("IdTipoInmueble")),
                                Direccion = reader.IsDBNull(reader.GetOrdinal("Direccion"))
                                    ? string.Empty
                                    : reader.GetString(reader.GetOrdinal("Direccion")),
                                Uso = reader.IsDBNull(reader.GetOrdinal("Uso"))
                                    ? Uso.Residencial
                                    : (Uso)reader.GetInt32(reader.GetOrdinal("Uso")),
                                CantAmbiente = reader.IsDBNull(reader.GetOrdinal("CantAmbiente"))
                                    ? 0
                                    : reader.GetInt32(reader.GetOrdinal("CantAmbiente")),
                                Valor = reader.IsDBNull(reader.GetOrdinal("Valor"))
                                    ? 0
                                    : reader.GetInt32(reader.GetOrdinal("Valor")),
                                Estado = reader.IsDBNull(reader.GetOrdinal("Estado"))
                                    ? false
                                    : reader.GetBoolean(reader.GetOrdinal("Estado")),
                                Disponible = reader.IsDBNull(reader.GetOrdinal("Disponible"))
                                    ? false
                                    : reader.GetBoolean(reader.GetOrdinal("Disponible")),
                            };
                            inmuebles.Add(inmueble);
                        }
                    }
                }
            }
            return inmuebles;
        }

        public bool ExisteSuperposicionFechas(
            int idInmueble,
            DateTime fechaInicio,
            DateTime fechaFin,
            int? idContrato = null
        )
        {
            using (MySqlConnection connection = new MySqlConnection(ConectionString))
            {
                var query =
                    $@"
                    SELECT COUNT(*)
                    FROM contrato
                    WHERE IdInmueble = @IdInmueble
                      AND (FechaInicio <= @FechaFin AND FechaFin >= @FechaInicio)
                      {(idContrato.HasValue ? "AND IdContrato != @IdContrato" : "")}";
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@IdInmueble", idInmueble);
                    command.Parameters.AddWithValue("@FechaInicio", fechaInicio);
                    command.Parameters.AddWithValue("@FechaFin", fechaFin);
                    if (idContrato.HasValue)
                    {
                        command.Parameters.AddWithValue("@IdContrato", idContrato.Value);
                    }
                    connection.Open();
                    return Convert.ToInt32(command.ExecuteScalar()) > 0;
                }
            }
        }

        public List<Contrato> ObtenerContratosSuperpuestos(
            int idInmueble,
            DateTime fechaInicio,
            DateTime fechaFin
        )
        {
            List<Contrato> contratos = new List<Contrato>();
            using (MySqlConnection connection = new MySqlConnection(ConectionString))
            {
                var query =
                    $@"
                    SELECT * FROM contrato
                    WHERE IdInmueble = @idInmueble
                    AND (
                        (FechaInicio <= @fechaFin AND FechaFin >= @fechaInicio)
                    )";
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@idInmueble", idInmueble);
                    command.Parameters.AddWithValue("@fechaInicio", fechaInicio);
                    command.Parameters.AddWithValue("@fechaFin", fechaFin);
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
                            }
                        );
                    }
                    connection.Close();
                }
            }
            return contratos;
        }
    }
}
