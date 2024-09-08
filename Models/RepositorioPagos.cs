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
                                {nameof(Pagos.NroPago)}, 
                                {nameof(Pagos.FechaPago)}, 
                                {nameof(Pagos.Importe)}, 
                                {nameof(Pagos.Detalle)}, 
                                {nameof(Pagos.Estado)},
                                {nameof(Pagos.UsuarioCreacion)}) 
                               VALUES (@IdContrato, @NumeroPagos, @FechaPagos, @Importe, @Detalle, @Anulado, @UsuarioCreacion); 
                               SELECT LAST_INSERT_ID();";
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@IdContrato", Pagos.IdContrato);
                    command.Parameters.AddWithValue("@NumeroPagos", Pagos.NroPago);
                    command.Parameters.AddWithValue("@FechaPagos", Pagos.FechaPago);
                    command.Parameters.AddWithValue("@Importe", Pagos.Importe);
                    command.Parameters.AddWithValue("@Detalle", Pagos.Detalle);
                    command.Parameters.AddWithValue("@Anulado", Pagos.Estado);
                    command.Parameters.AddWithValue("@UsuarioCreacion", usuarioCreacion);

                    connection.Open();
                    res = Convert.ToInt32(command.ExecuteScalar());
                }
            }
            return res;
        }

        public bool ActualizarPago(Pagos pago)
        {
            using (MySqlConnection connection = new MySqlConnection(ConectionString))
            {
                var query =
                    @$"UPDATE Pagos 
                               SET {nameof(Pagos.IdContrato)} = @IdContrato,
                                   {nameof(Pagos.NroPago)} = @NroPago,
                                   {nameof(Pagos.FechaPago)} = @FechaPago,
                                   {nameof(Pagos.Importe)} = @Importe,
                                   {nameof(Pagos.Detalle)} = @Detalle,
                                   {nameof(Pagos.Estado)} = @Estado,
                                   {nameof(Pagos.UsuarioCreacion)} = @UsuarioCreacion,
                                   {nameof(Pagos.UsuarioAnulacion)} = @UsuarioAnulacion
                               WHERE {nameof(Pagos.IdPago)} = @IdPago;";
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@IdPago", pago.IdPago);
                    command.Parameters.AddWithValue("@IdContrato", pago.IdContrato);
                    command.Parameters.AddWithValue("@NroPago", pago.NroPago);
                    command.Parameters.AddWithValue("@FechaPago", pago.FechaPago);
                    command.Parameters.AddWithValue("@Importe", pago.Importe);
                    command.Parameters.AddWithValue("@Detalle", pago.Detalle);
                    command.Parameters.AddWithValue("@Estado", pago.Estado);
                    command.Parameters.AddWithValue("@UsuarioCreacion", pago.UsuarioCreacion);
                    command.Parameters.AddWithValue("@UsuarioAnulacion", pago.UsuarioAnulacion);

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
    }
}
