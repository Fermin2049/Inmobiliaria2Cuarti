using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace Inmobiliaria2Cuarti.Models
{
    public class RepositorioPagos
    {
        string ConectionString = "Server=localhost;User Id=root;Password=;Database=inmobiliaria2;";

        public List<Pagos> ObtenerTodos()
        {
            List<Pagos> pagos = new List<Pagos>();
            using (MySqlConnection connection = new MySqlConnection(ConectionString))
            {
                var query =
                    $@"SELECT {nameof(Pagos.IdPago)},
                              {nameof(Pagos.IdContrato)},
                              {nameof(Pagos.NroPago)},
                              {nameof(Pagos.FechaPago)},
                              {nameof(Pagos.Detalle)},
                              {nameof(Pagos.Importe)},
                              {nameof(Pagos.Estado)}
                        FROM pagos";
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    connection.Open();
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        pagos.Add(
                            new Pagos
                            {
                                IdPago = reader.GetInt32(nameof(Pagos.IdPago)),
                                IdContrato = reader.GetInt32(nameof(Pagos.IdContrato)),
                                NroPago = reader.GetInt32(nameof(Pagos.NroPago)),
                                FechaPago = reader.GetDateTime(nameof(Pagos.FechaPago)),
                                Detalle = reader.GetString(nameof(Pagos.Detalle)),
                                Importe = reader.GetDecimal(nameof(Pagos.Importe)),
                                Estado = reader.GetString(nameof(Pagos.Estado)),
                            }
                        );
                    }
                    connection.Close();
                }
                return pagos;
            }
        }

        public Pagos? Obtener(int id)
        {
            Pagos? res = null;
            using (MySqlConnection connection = new MySqlConnection(ConectionString))
            {
                var query =
                    $@"SELECT {nameof(Pagos.IdPago)},
                              {nameof(Pagos.IdContrato)},
                              {nameof(Pagos.NroPago)},
                              {nameof(Pagos.FechaPago)},
                              {nameof(Pagos.Detalle)},
                              {nameof(Pagos.Importe)},
                              {nameof(Pagos.Estado)}
                        FROM pagos
                        WHERE {nameof(Pagos.IdPago)} = @IdPago";
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@IdPago", id);
                    connection.Open();
                    var reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        res = new Pagos
                        {
                            IdPago = reader.GetInt32(nameof(Pagos.IdPago)),
                            IdContrato = reader.GetInt32(nameof(Pagos.IdContrato)),
                            NroPago = reader.GetInt32(nameof(Pagos.NroPago)),
                            FechaPago = reader.GetDateTime(nameof(Pagos.FechaPago)),
                            Detalle = reader.GetString(nameof(Pagos.Detalle)),
                            Importe = reader.GetDecimal(nameof(Pagos.Importe)),
                            Estado = reader.GetString(nameof(Pagos.Estado)),
                        };
                    }
                    connection.Close();
                }
                return res;
            }
        }

        public int CrearPago(Pagos pago)
        {
            int res = 0;
            using (MySqlConnection connection = new MySqlConnection(ConectionString))
            {
                var query =
                    @$"INSERT INTO pagos 
                            ({nameof(Pagos.IdContrato)}, 
                             {nameof(Pagos.NroPago)}, 
                             {nameof(Pagos.FechaPago)}, 
                             {nameof(Pagos.Detalle)}, 
                             {nameof(Pagos.Importe)}, 
                             {nameof(Pagos.Estado)}) 
                         VALUES (@IdContrato, @NroPago, @FechaPago, @Detalle, @Importe, @Estado); 
                         SELECT LAST_INSERT_ID();";
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@IdContrato", pago.IdContrato);
                    command.Parameters.AddWithValue("@NroPago", pago.NroPago);
                    command.Parameters.AddWithValue("@FechaPago", pago.FechaPago);
                    command.Parameters.AddWithValue("@Detalle", pago.Detalle);
                    command.Parameters.AddWithValue("@Importe", pago.Importe);
                    command.Parameters.AddWithValue("@Estado", pago.Estado);

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
                var sql =
                    @$"UPDATE pagos 
                         SET {nameof(Pagos.IdContrato)} = @IdContrato, 
                             {nameof(Pagos.NroPago)} = @NroPago, 
                             {nameof(Pagos.FechaPago)} = @FechaPago, 
                             {nameof(Pagos.Detalle)} = @Detalle, 
                             {nameof(Pagos.Importe)} = @Importe, 
                             {nameof(Pagos.Estado)} = @Estado
                         WHERE {nameof(Pagos.IdPago)} = @IdPago;";
                using (MySqlCommand command = new MySqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@IdPago", pago.IdPago);
                    command.Parameters.AddWithValue("@IdContrato", pago.IdContrato);
                    command.Parameters.AddWithValue("@NroPago", pago.NroPago);
                    command.Parameters.AddWithValue("@FechaPago", pago.FechaPago);
                    command.Parameters.AddWithValue("@Detalle", pago.Detalle);
                    command.Parameters.AddWithValue("@Importe", pago.Importe);
                    command.Parameters.AddWithValue("@Estado", pago.Estado);

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
                    @$"UPDATE pagos 
                            SET {nameof(Pagos.Estado)} = 'Eliminado' 
                            WHERE {nameof(Pagos.IdPago)} = @IdPago;";
                using (MySqlCommand command = new MySqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@IdPago", id);
                    connection.Open();
                    int result = command.ExecuteNonQuery();
                    connection.Close();
                    return result;
                }
            }
        }
    }
}
