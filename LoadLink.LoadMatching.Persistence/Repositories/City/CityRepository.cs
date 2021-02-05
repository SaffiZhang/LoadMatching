using Dapper;
using LoadLink.LoadMatching.Application.City.Models.Commands;
using LoadLink.LoadMatching.Application.City.Repository;
using LoadLink.LoadMatching.Domain.Procedures;
using LoadLink.LoadMatching.Persistence.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace LoadLink.LoadMatching.Persistence.Repositories.City
{
    public class CityRepository : ICityRepository
    {

        private readonly IDbConnection _dbConnection;

        public CityRepository(IConnectionFactory connectionFactory)
        {
            _dbConnection = new SqlConnection(connectionFactory.ConnectionString);
        }


        public async Task<IEnumerable<UspGetCityListResult>> GetListAsync(string city, short sortType)
        {
            var proc = "usp_GetCityList";
            var param = new DynamicParameters();

            param.Add("@Cityname", city);
            param.Add("@SortType", sortType);

            return await  SqlMapper.QueryAsync<UspGetCityListResult>(
                _dbConnection, sql: proc, param: param, commandType: CommandType.StoredProcedure);
        }
    }
}
