using Dapper;
using LoadLink.LoadMatching.Application.VehicleType.Repository;
using LoadLink.LoadMatching.Domain.Procedures;
using LoadLink.LoadMatching.Persistence.Data;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace LoadLink.LoadMatching.Persistence.Repositories.VehicleType
{
    public class VehicleTypeRepository : IVehicleTypeRepository
    {
        private readonly IDbConnection _dbConnection;

        public VehicleTypeRepository(IConnectionFactory connectionFactory)
        {
            _dbConnection = new SqlConnection(connectionFactory.ConnectionString);
        }

        public async Task<IEnumerable<UspGetVehicleTypeResult>> GetListAsync()
        {
            var proc = "usp_GetVehicleType";

            var result = await SqlMapper.QueryAsync<UspGetVehicleTypeResult>(
                _dbConnection, sql: proc, commandType: CommandType.StoredProcedure);

            return result;
        }

    }
}
