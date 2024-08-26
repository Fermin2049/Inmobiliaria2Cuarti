using MySql.Data.MySqlClient;

namespace Inmobiliaria2Cuatri.Models;
    public class RepositorioPropietario
    {
        string ConectionString = "Server=localhost;User Id=root;Password=;Database=inmobiliaria2;";

        public List<Propietario> ObtenerTodos()
        {
            List<Propietario> propietarios = new List<Propietario>();
            using(MySqlConnection connection = new MySqlConnection(ConectionString))
            {
                var query = $@"SELECT {nameof(Propietario.idPropietario)}, {nameof(Propietario.Nombre)}, {nameof(Propietario.Apellido)},
                {nameof(Propietario.Dni)}, {nameof(Propietario.Email)}, {nameof(Propietario.Telefono)}, {nameof(Propietario.Estado)}
                FROM propietario"; 
                using(MySqlCommand command = new MySqlCommand(query, connection))
                {
                    connection.Open();
                    var reader = command.ExecuteReader();
                    while(reader.Read())
                    {
                        propietarios.Add(new Propietario
                        {
                            idPropietario = reader.GetInt32(nameof(Propietario.idPropietario)),
                            Nombre = reader.GetString(nameof(Propietario.Nombre)),
                            Apellido = reader.GetString(nameof(Propietario.Apellido)),
                            Dni = reader.GetInt32(nameof(Propietario.Dni)),
                            Email = reader.GetString(nameof(Propietario.Email)),
                            Telefono = reader.GetString(nameof(Propietario.Telefono)),
                            Estado = reader.GetBoolean(nameof(Propietario.Estado))
                        });
                    }
                    connection.Close();
                }
                return propietarios;
            }
        }
    }
    
    