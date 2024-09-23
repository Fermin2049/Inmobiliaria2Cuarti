using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace Inmobiliaria2Cuarti.Models
{
    public class RepositorioPagos
    {
        string ConectionString = "Server=localhost;User Id=root;Password=;Database=inmobiliaria2;";

        public List<Pagos> ObtenerPagossPorContrato(int idContrato)
        {
            List<Pagos> Pagoss = new List<Pagos>();
            using (MySqlConnection connection = new MySqlConnection(ConectionString))
            {
                var query =
                    idContrato == 0
                        ? $@"SELECT {nameof(Pagos.IdPago)},
                               {nameof(Pagos.IdContrato)},
                               {nameof(Pagos.NroPago)},
                               {nameof(Pagos.FechaPago)},
                               {nameof(Pagos.Importe)},
                               {nameof(Pagos.Detalle)},
                               {nameof(Pagos.Estado)},
                               {nameof(Pagos.UsuarioCreacion)},
                               {nameof(Pagos.UsuarioAnulacion)}
                        FROM Pagos"
                        : $@"SELECT {nameof(Pagos.IdPago)},
                               {nameof(Pagos.IdContrato)},
                               {nameof(Pagos.NroPago)},
                               {nameof(Pagos.FechaPago)},
                               {nameof(Pagos.Importe)},
                               {nameof(Pagos.Detalle)},
                               {nameof(Pagos.Estado)},
                               {nameof(Pagos.UsuarioCreacion)},
                               {nameof(Pagos.UsuarioAnulacion)}
                        FROM Pagos
                        WHERE {nameof(Pagos.IdContrato)} = @idContrato";
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    if (idContrato != 0)
                    {
                        command.Parameters.AddWithValue("@idContrato", idContrato);
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
                                FechaPago = reader.GetDateTime(nameof(Pagos.FechaPago)),
                                Importe = reader.GetDecimal(nameof(Pagos.Importe)),
                                Detalle = reader.GetString(nameof(Pagos.Detalle)),
                                Estado = reader.GetBoolean(nameof(Pagos.Estado)),
                                UsuarioCreacion = reader.GetString(nameof(Pagos.UsuarioCreacion)),
                                UsuarioAnulacion = reader.GetString(nameof(Pagos.UsuarioAnulacion)),
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
                               SET {nameof(Pagos.Estado)} = 1,
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
