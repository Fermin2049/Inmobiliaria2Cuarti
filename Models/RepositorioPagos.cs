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
                    $@"SELECT {nameof(Pagos.IdPago)},
                                      {nameof(Pagos.IdContrato)},
                                      {nameof(Pagos.NroPago)},
                                      {nameof(Pagos.FechaPago)},
                                      {nameof(Pagos.Importe)},
                                      {nameof(Pagos.Detalle)},
                                      {nameof(Pagos.Estado)}
                               FROM Pagos
                               WHERE {nameof(Pagos.IdContrato)} = @idContrato";
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@idContrato", idContrato);
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
                            }
                        );
                    }
                    connection.Close();
                }
            }
            return Pagoss;
        }

        public int CrearPagos(Pagos Pagos)
        {
            int res = 0;
            using (MySqlConnection connection = new MySqlConnection(ConectionString))
            {
                var query =
                    @$"INSERT INTO Pagos 
                               ({nameof(Pagos.IdContrato)}, 
                                {nameof(Pagos.NroPago)}, 
                                {nameof(Pagos.FechaPago)}, 
                                {nameof(Pagos.Importe)}, 
                                {nameof(Pagos.Detalle)}, 
                                {nameof(Pagos.Estado)}) 
                               VALUES (@IdContrato, @NumeroPagos, @FechaPagos, @Importe, @Detalle, @Anulado); 
                               SELECT LAST_INSERT_ID();";
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@IdContrato", Pagos.IdContrato);
                    command.Parameters.AddWithValue("@NumeroPagos", Pagos.NroPago);
                    command.Parameters.AddWithValue("@FechaPagos", Pagos.FechaPago);
                    command.Parameters.AddWithValue("@Importe", Pagos.Importe);
                    command.Parameters.AddWithValue("@Detalle", Pagos.Detalle);
                    command.Parameters.AddWithValue("@Anulado", Pagos.Estado);

                    connection.Open();
                    res = Convert.ToInt32(command.ExecuteScalar());
                }
            }
            return res;
        }

        public bool AnularPagos(int idPagos)
        {
            using (MySqlConnection connection = new MySqlConnection(ConectionString))
            {
                var query =
                    @$"UPDATE Pagos 
                               SET {nameof(Pagos.Estado)} = 1 
                               WHERE {nameof(Pagos.IdPago)} = @IdPagos;";
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@IdPagos", idPagos);
                    connection.Open();
                    int result = command.ExecuteNonQuery();
                    return result > 0;
                }
            }
        }
    }
}
