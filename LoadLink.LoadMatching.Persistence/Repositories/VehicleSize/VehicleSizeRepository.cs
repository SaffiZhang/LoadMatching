using Dapper;
using LoadLink.LoadMatching.Application.VehicleSize.Repository;
using LoadLink.LoadMatching.Domain.Procedures;
using LoadLink.LoadMatching.Persistence.Data;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace LoadLink.LoadMatching.Persistence.Repositories.VehicleSize
{
    public class VehicleSizeRepository : IVehicleSizeRepository
    {
        private readonly IDbConnection _dbConnection;

        public VehicleSizeRepository(IConnectionFactory connectionFactory)
        {
            _dbConnection = new SqlConnection(connectionFactory.ConnectionString);
        }

        public async Task<IEnumerable<UspGetVehicleSizeResult>> GetListAsync()
        {
            var proc = "usp_GetVehicleSize";

            var result = await SqlMapper.QueryAsync<UspGetVehicleSizeResult>(
                _dbConnection, sql: proc, commandType: CommandType.StoredProcedure);

            return result;
        }

        public async Task<IEnumerable<UspGetVehicleSizeResult>> GetListByPostTypeAsync(string postType)
        {
            var proc = "usp_GetVehicleSize";
            var param = new DynamicParameters();
            param.Add("@PostType", postType);

            var result = await SqlMapper.QueryAsync<UspGetVehicleSizeResult>(
                _dbConnection, sql: proc, param: param, commandType: CommandType.StoredProcedure);

            return result;
        }

    }
}
