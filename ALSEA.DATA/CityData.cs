using ALSEA.Entities;
using ALSEA.Shared;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Transactions;

namespace ALSEA.DATA
{
    public class CityData
    {
        private readonly IConfiguration _configuration;
        private static string Connection { get; set; }

        private ErrorViewModel errorView;

        public CityData(IConfiguration configuration) {
            _configuration = configuration;
            Connection = _configuration["ConnectionStrings:DefaultConnection"];
        }

        /// <summary>
        /// Guarda una ciudad del usuario
        /// </summary>
        /// <param name="cityId">id de la ciudad</param>
        /// <param name="userId">id del usuario</param>
        /// <returns>retorna mensaje de respuesta</returns>
        public async Task<CitysDTO> SaveCitiesUser(int cityId, int userId)
        {
            CitysDTO citys = new CitysDTO();
            using (var transactionScope = new TransactionScope(TransactionScopeOption.RequiresNew, TimeSpan.FromMinutes(15), TransactionScopeAsyncFlowOption.Enabled))
            {
                using (var connection = new SqlConnection(Connection))
                {
                    using (var command = connection.CreateSpCommand(_configuration["Stores:SAVE_CITIES_BY_USER"]))
                    {
                        command.AddSqlParameter("@CityId", cityId);
                        command.AddSqlParameter("@UserId", userId);

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
                            
                            errorView = new ErrorViewModel() { ErrorMessage = row.ItemArray[0].ToString() };
                            citys.Validations.Add(errorView);
                            connection.Close();
                            return await Task.FromResult(citys);
                        }

                        //Mapea resultados
                        foreach (DataRow row in _dataSet.Tables[1].Rows)
                        {
                            citys.CityId = int.Parse(row.ItemArray[0].ToString());
                        }
                    }
                    connection.Close();
                }
                transactionScope.Complete();
            }
            return await Task.FromResult(citys);
        }

        /// <summary>
        /// Cosulta lista de ciuidades por usuario
        /// </summary>
        /// <param name="userId">id del usuario</param>
        /// <returns>Retorna lista de ciudades</returns>
        public async Task<IList<CitysDTO>> ConsultUserCities(int userId)
        {
            IList<CitysDTO> listCitys = new List<CitysDTO>();
            CitysDTO citys = new CitysDTO();
            using (var transactionScope = new TransactionScope(TransactionScopeOption.RequiresNew, TimeSpan.FromMinutes(15), TransactionScopeAsyncFlowOption.Enabled))
            {
                using (var connection = new SqlConnection(Connection))
                {
                    using (var command = connection.CreateSpCommand(_configuration["Stores:CONSULT_USERS_CITIES"]))
                    {
                        command.AddSqlParameter("@UserId", userId);

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
                            citys = new CitysDTO() {
                                CityId = int.Parse(row.ItemArray[0].ToString()),
                                State = row.ItemArray[1].ToString(),
                                Country = row.ItemArray[2].ToString(),
                                CoordLon = row.ItemArray[3].ToString(),
                                CoordLat = row.ItemArray[4].ToString()
                            };
                            listCitys.Add(citys);
                        }

                        
                    }
                    connection.Close();
                }
                transactionScope.Complete();
            }
            return await Task.FromResult(listCitys);
        }

        /// <summary>
        /// Consulta cuidades 
        /// </summary>
        /// <returns>Retorna lista de cuidades </returns>
        public async Task<IList<CitysDTO>> ConsultCities()
        {
            IList<CitysDTO> listCitys = new List<CitysDTO>();
            CitysDTO citys = new CitysDTO();
            using (var transactionScope = new TransactionScope(TransactionScopeOption.RequiresNew, TimeSpan.FromMinutes(15), TransactionScopeAsyncFlowOption.Enabled))
            {
                using (var connection = new SqlConnection(Connection))
                {
                    using (var command = connection.CreateSpCommand(_configuration["Stores:CONSULT_CITIES"]))
                    {

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
                            citys = new CitysDTO()
                            {
                                CityId = int.Parse(row.ItemArray[0].ToString()),
                                State = row.ItemArray[1].ToString(),
                                Country = row.ItemArray[2].ToString(),
                                CoordLon = row.ItemArray[3].ToString(),
                                CoordLat = row.ItemArray[4].ToString()
                            };
                            listCitys.Add(citys);
                        }


                    }
                    connection.Close();
                }
                transactionScope.Complete();
            }
            return await Task.FromResult(listCitys);
        }

        /// <summary>
        /// Consultar ciudades del usuario
        /// </summary>
        /// <param name="cityId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<CitysDTO> DeleteCituByUser(int cityId,int userId)
        {
            CitysDTO citys = new CitysDTO();
            using (var transactionScope = new TransactionScope(TransactionScopeOption.RequiresNew, TimeSpan.FromMinutes(15), TransactionScopeAsyncFlowOption.Enabled))
            {
                using (var connection = new SqlConnection(Connection))
                {
                    using (var command = connection.CreateSpCommand(_configuration["Stores:DELETE_CITY_USER"]))
                    {
                        command.AddSqlParameter("@CityId", cityId);
                        command.AddSqlParameter("@UserId", userId);

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

                            errorView = new ErrorViewModel() { ErrorMessage = row.ItemArray[0].ToString() };
                            citys.Validations.Add(errorView);
                            connection.Close();
                            return await Task.FromResult(citys);
                        }

                        //Mapea resultados
                        foreach (DataRow row in _dataSet.Tables[1].Rows)
                        {
                            citys.CityId = int.Parse(row.ItemArray[0].ToString());
                        }
                    }
                    connection.Close();
                }
                transactionScope.Complete();
            }
            return await Task.FromResult(citys);
        }



    }
}
