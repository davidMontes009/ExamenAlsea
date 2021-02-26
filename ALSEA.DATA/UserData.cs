using ALSEA.Entities;
using ALSEA.Shared;
using Microsoft.Extensions.Configuration;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Transactions;

namespace ALSEA.DATA
{
    public class UserData
    {
        private readonly IConfiguration _configuration;
        private static string Connection { get; set; }

        private ErrorViewModel errorView;

        public UserData(IConfiguration configuration)
        {
            _configuration = configuration;
            Connection = _configuration["ConnectionStrings:DefaultConnection"];
        }

        /// <summary>
        /// Metodo para registrar un usuario 
        /// </summary>
        /// <param name="user">objeto con la informacion del usuario </param>
        /// <returns>Retorna resultado de la accion </returns>
        public async Task<UserDTO> RegisterUser(UserDTO user)
        {
            using (var transactionScope = new TransactionScope(TransactionScopeOption.RequiresNew, TimeSpan.FromMinutes(15), TransactionScopeAsyncFlowOption.Enabled))
            {
                using (var connection = new SqlConnection(Connection))
                {
                    using (var command = connection.CreateSpCommand(_configuration["Stores:REGISTER_USER"]))
                    {
                        command.AddSqlParameter("@Name"     , user.Name);
                        command.AddSqlParameter("@Email"    , user.Email);
                        command.AddSqlParameter("@Password" , user.Password);

                        ///Valida que la conexion este abierta 
                        if (connection.State == ConnectionState.Closed)
                            connection.Open();

                        ///Inicializa nueva instancia de la clase SqlDataAdapter con el objeto SqlCommand como parametro 
                        var _adapter = new SqlDataAdapter(command);
                        var _dataSet = new DataSet();
                        _adapter.Fill(_dataSet);

                        //Mapea errores
                        foreach (DataRow row in _dataSet.Tables[0].Rows)
                        {
                            user = new UserDTO();
                            errorView = new ErrorViewModel() { ErrorMessage = row.ItemArray[0].ToString() };
                            user.Validations.Add(errorView);
                            connection.Close();
                            return await Task.FromResult(user);
                        }

                        //Mapea resultados
                        foreach (DataRow row in _dataSet.Tables[1].Rows)
                        {
                            user.UserId = int.Parse(row.ItemArray[0].ToString());
                        }
                    }
                    connection.Close();
                }
                transactionScope.Complete();
            }
            return await Task.FromResult(user);
        }

        /// <summary>
        /// Metodo para validar el inicio de sesión 
        /// </summary>
        /// <param name="email">correo del usuario</param>
        /// <param name="password">contraseña del usuario </param>
        /// <returns>Retorna mensaje de la acción </returns>
        public async Task<UserDTO> LogIn(string email, string password)
        {
            UserDTO user = new UserDTO();
            using (var transactionScope = new TransactionScope(TransactionScopeOption.RequiresNew, TimeSpan.FromMinutes(15), TransactionScopeAsyncFlowOption.Enabled))
            {
                using (var connection = new SqlConnection(Connection))
                {
                    using (var command = connection.CreateSpCommand(_configuration["Stores:LOG_IN"]))
                    {
                        command.AddSqlParameter("@Email"    , email);
                        command.AddSqlParameter("@Password" , password);

                        ///Valida que la conexion este abierta 
                        if (connection.State == ConnectionState.Closed)
                            connection.Open();

                        ///Inicializa nueva instancia de la clase SqlDataAdapter con el objeto SqlCommand como parametro 
                        var _adapter = new SqlDataAdapter(command);
                        var _dataSet = new DataSet();
                        _adapter.Fill(_dataSet);

                        //Mapea errores
                        foreach (DataRow row in _dataSet.Tables[0].Rows)
                        {
                            user = new UserDTO();
                            errorView = new ErrorViewModel() { ErrorMessage = row.ItemArray[0].ToString() };
                            user.Validations.Add(errorView);
                            connection.Close();
                            return await Task.FromResult(user);
                        }

                        //Mapea resultados
                        foreach (DataRow row in _dataSet.Tables[1].Rows)
                        {
                            user.UserId = int.Parse(row.ItemArray[0].ToString());
                        }
                    }
                    connection.Close();
                }
                transactionScope.Complete();
            }
            return await Task.FromResult(user);
        }



    }
}
