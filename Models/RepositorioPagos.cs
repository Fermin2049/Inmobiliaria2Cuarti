using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace Inmobiliaria2Cuarti.Models
{
    public class RepositorioPagos
    {
        string ConectionString = "Server=localhost;User Id=root;Password=;Database=inmobiliaria2;";

        public List<Pagos> ObtenerPagossPorContrato(int IdPago)
        {
            List<Pagos> Pagoss = new List<Pagos>();
            using (MySqlConnection connection = new MySqlConnection(ConectionString))
            {
                var query =
                    IdPago == 0
                        ? $@"SELECT {nameof(Pagos.IdPago)},
                           {nameof(Pagos.IdContrato)},
                           {nameof(Pagos.NroPago)},
                           {nameof(Pagos.FechaPago)},
                           {nameof(Pagos.Importe)},
                           {nameof(Pagos.Detalle)},
                           {nameof(Pagos.Estado)},
                           {nameof(Pagos.UsuarioCreacion)},
                           {nameof(Pagos.UsuarioAnulacion)},
                           {nameof(Pagos.UsuarioEliminacion)}
                    FROM Pagos"
                        : $@"SELECT {nameof(Pagos.IdPago)},
                           {nameof(Pagos.IdContrato)},
                           {nameof(Pagos.NroPago)},
                           {nameof(Pagos.FechaPago)},
                           {nameof(Pagos.Importe)},
                           {nameof(Pagos.Detalle)},
                           {nameof(Pagos.Estado)},
                           {nameof(Pagos.UsuarioCreacion)},
                           {nameof(Pagos.UsuarioAnulacion)},
                           {nameof(Pagos.UsuarioEliminacion)}
                    FROM Pagos
                    WHERE {nameof(Pagos.IdPago)} = @idPago";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    if (IdPago != 0)
                    {
                        command.Parameters.AddWithValue("@idPago", IdPago);
                    }
                    connection.Open();
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Pagoss.Add(
                            new Pagos
                            {
                                IdPago = reader.GetInt32(nameof(Pagos.IdPago)),
                                IdContrato = reader.GetInt32(nameof(Pagos.IdContrato)),
                                NroPago = reader.GetInt32(nameof(Pagos.NroPago)),

                                // Verificación de valores nulos antes de asignarlos
                                FechaPago = reader.IsDBNull(
                                    reader.GetOrdinal(nameof(Pagos.FechaPago))
                                )
                                    ? DateTime.MinValue // Valor predeterminado si es nulo
                                    : reader.GetDateTime(nameof(Pagos.FechaPago)),

                                Importe = reader.IsDBNull(reader.GetOrdinal(nameof(Pagos.Importe)))
                                    ? 0m // Valor predeterminado si es nulo
                                    : reader.GetDecimal(nameof(Pagos.Importe)),

                                Detalle = reader.IsDBNull(reader.GetOrdinal(nameof(Pagos.Detalle)))
                                    ? string.Empty // Valor predeterminado si es nulo
                                    : reader.GetString(nameof(Pagos.Detalle)),

                                Estado = reader.IsDBNull(reader.GetOrdinal(nameof(Pagos.Estado)))
                                    ? false
                                    : reader.GetBoolean(nameof(Pagos.Estado)),

                                UsuarioCreacion = reader.IsDBNull(
                                    reader.GetOrdinal(nameof(Pagos.UsuarioCreacion))
                                )
                                    ? null
                                    : reader.GetString(nameof(Pagos.UsuarioCreacion)),

                                UsuarioAnulacion = reader.IsDBNull(
                                    reader.GetOrdinal(nameof(Pagos.UsuarioAnulacion))
                                )
                                    ? null
                                    : reader.GetString(nameof(Pagos.UsuarioAnulacion)),

                                UsuarioEliminacion = reader.IsDBNull(
                                    reader.GetOrdinal(nameof(Pagos.UsuarioEliminacion))
                                )
                                    ? null
                                    : reader.GetString(nameof(Pagos.UsuarioEliminacion)),
                            }
                        );
                    }
                    connection.Close();
                }
            }
            return Pagoss;
        }

        public int CrearPagos(Pagos Pagos, string usuarioCreacion)
        {
            int res = 0;
            using (MySqlConnection connection = new MySqlConnection(ConectionString))
            {
                var query =
                    @$"INSERT INTO Pagos 
                               ({nameof(Pagos.IdContrato)}, 
                                {nameof(Pagos.FechaPago)}, 
                                {nameof(Pagos.Importe)}, 
                                {nameof(Pagos.Detalle)}, 
                                {nameof(Pagos.Estado)},
                                {nameof(Pagos.UsuarioCreacion)}) 
                               VALUES (@IdContrato, @FechaPagos, @Importe, @Detalle, @Anulado, @UsuarioCreacion); 
                               SELECT LAST_INSERT_ID();";
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@IdContrato", Pagos.IdContrato);
                    command.Parameters.AddWithValue("@FechaPagos", Pagos.FechaPago);
                    command.Parameters.AddWithValue("@Importe", Pagos.Importe);
                    command.Parameters.AddWithValue("@Detalle", Pagos.Detalle);
                    command.Parameters.AddWithValue("@Anulado", Pagos.Estado);
                    command.Parameters.AddWithValue("@UsuarioCreacion", usuarioCreacion);

                    connection.Open();
                    res = Convert.ToInt32(command.ExecuteScalar());

                    var updateNroPagoQuery =
                        @$"UPDATE Pagos 
                            SET {nameof(Pagos.NroPago)} = @NroPago 
                            WHERE {nameof(Pagos.IdPago)} = @IdPago;";
                    using (var updateCommand = new MySqlCommand(updateNroPagoQuery, connection))
                    {
                        updateCommand.Parameters.AddWithValue("@NroPago", res);
                        updateCommand.Parameters.AddWithValue("@IdPago", res);
                        updateCommand.ExecuteNonQuery();
                    }
                }
            }
            return res;
        }

        public void CrearPago2(Pagos pago)
        {
            using (MySqlConnection connection = new MySqlConnection(ConectionString))
            {
                var query =
                    @"INSERT INTO pagos (IdContrato, Importe, Detalle, FechaPago, Estado, UsuarioCreacion) 
                      VALUES (@IdContrato, @Importe, @Detalle, @FechaPago, @Estado, @UsuarioCreacion); 
                      SELECT LAST_INSERT_ID();"; // Asegurarse de obtener el último Id generado.

                try
                {
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        // Asegurar que los parámetros son válidos y tienen los valores adecuados
                        command.Parameters.AddWithValue("@IdContrato", pago.IdContrato);
                        command.Parameters.AddWithValue("@Importe", pago.Importe);
                        command.Parameters.AddWithValue("@Detalle", pago.Detalle); // Detalle es correcto para el método de pago
                        command.Parameters.AddWithValue("@FechaPago", pago.FechaPago);
                        command.Parameters.AddWithValue("@Estado", pago.Estado);
                        command.Parameters.AddWithValue("@UsuarioCreacion", pago.UsuarioCreacion);

                        connection.Open();

                        // Ejecutar la inserción y obtener el ID generado
                        int idPago = Convert.ToInt32(command.ExecuteScalar());

                        // Actualizar el número de pago para el nuevo registro
                        var updateNroPagoQuery =
                            "UPDATE pagos SET NroPago = @NroPago WHERE IdPago = @IdPago;";
                        using (var updateCommand = new MySqlCommand(updateNroPagoQuery, connection))
                        {
                            updateCommand.Parameters.AddWithValue("@NroPago", idPago); // Utilizar el ID del pago como número de pago
                            updateCommand.Parameters.AddWithValue("@IdPago", idPago);
                            updateCommand.ExecuteNonQuery();
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Manejar cualquier excepción que ocurra
                    throw new Exception($"Error al registrar el pago: {ex.Message}");
                }
                finally
                {
                    // Asegurar que la conexión siempre se cierre
                    connection.Close();
                }
            }
        }

        public bool ActualizarPago(Pagos pago)
        {
            using (MySqlConnection connection = new MySqlConnection(ConectionString))
            {
                var query =
                    @"
                    UPDATE Pagos 
                    SET 
                        IdContrato = @IdContrato,
                        FechaPago = @FechaPago,
                        Importe = @Importe,
                        Detalle = @Detalle,
                        Estado = @Estado
                    WHERE IdPago = @IdPago;";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@IdPago", pago.IdPago);
                    command.Parameters.AddWithValue("@IdContrato", pago.IdContrato);
                    command.Parameters.AddWithValue("@FechaPago", pago.FechaPago);
                    command.Parameters.AddWithValue("@Importe", pago.Importe);
                    command.Parameters.AddWithValue("@Detalle", pago.Detalle);
                    command.Parameters.AddWithValue("@Estado", pago.Estado);

                    connection.Open();
                    int result = command.ExecuteNonQuery();
                    return result > 0;
                }
            }
        }

        public bool AnularPagos(int idPagos, string usuarioAnulacion)
        {
            using (MySqlConnection connection = new MySqlConnection(ConectionString))
            {
                var query =
                    @$"UPDATE Pagos 
                               SET {nameof(Pagos.Estado)} = 0,
                                   {nameof(Pagos.UsuarioAnulacion)} = @UsuarioAnulacion
                               WHERE {nameof(Pagos.IdPago)} = @IdPagos;";
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@IdPagos", idPagos);
                    command.Parameters.AddWithValue("@UsuarioAnulacion", usuarioAnulacion);
                    connection.Open();
                    int result = command.ExecuteNonQuery();
                    return result > 0;
                }
            }
        }

        public List<int> ObtenerPagosMensuales()
        {
            List<int> pagosMensuales = new List<int>(new int[12]); // Inicializar lista con 12 ceros
            using (MySqlConnection connection = new MySqlConnection(ConectionString))
            {
                var query =
                    @"SELECT MONTH(FechaPago) AS Mes, COUNT(IdPago) AS CantidadPagos
                              FROM Pagos
                              WHERE YEAR(FechaPago) = YEAR(CURDATE())
                              GROUP BY MONTH(FechaPago)";
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    connection.Open();
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        int mes = reader.GetInt32("Mes");
                        int cantidadPagos = reader.GetInt32("CantidadPagos");
                        pagosMensuales[mes - 1] = cantidadPagos; // Meses en MySQL van de 1 a 12
                    }
                    connection.Close();
                }
            }
            return pagosMensuales;
        }
    }
}
