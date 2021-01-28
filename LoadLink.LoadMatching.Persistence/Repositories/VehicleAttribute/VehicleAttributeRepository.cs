using Dapper;
using LoadLink.LoadMatching.Application.VehicleAttribute.Repository;
using LoadLink.LoadMatching.Domain.Procedures;
using LoadLink.LoadMatching.Persistence.Data;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace LoadLink.LoadMatching.Persistence.Repositories.VehicleAttribute
{
    public class VehicleAttributeRepository : IVehicleAttributeRepository
    {
        private readonly IDbConnection _dbConnection;

        public VehicleAttributeRepository(IConnectionFactory connectionFactory)
        {
            _dbConnection = new SqlConnection(connectionFactory.ConnectionString);
        }

        public async Task<IEnumerable<UspGetVehicleAttributeResult>> GetListAsync()
        {
            var proc = "usp_GetVehicleAttribute";

            var result = await SqlMapper.QueryAsync<UspGetVehicleAttributeResult>(
                _dbConnection, sql: proc, commandType: CommandType.StoredProcedure);

            return result;
        }

    }
}
