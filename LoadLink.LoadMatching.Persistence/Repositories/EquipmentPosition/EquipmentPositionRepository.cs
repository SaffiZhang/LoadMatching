using Dapper;
using LoadLink.LoadMatching.Application.EquipmentPosition.Repository;
using LoadLink.LoadMatching.Domain.Procedures;
using LoadLink.LoadMatching.Persistence.Data;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace LoadLink.LoadMatching.Persistence.Repositories.EquipmentPosition
{
    public class EquipmentPositionRepository : IEquipmentPositionRepository
    {
        private readonly IDbConnection _dbConnection;

        public EquipmentPositionRepository(IConnectionFactory connectionFactory)
        {
            _dbConnection = new SqlConnection(connectionFactory.ConnectionString);
        }

        public async Task<IEnumerable<UspGetEquipmentPositionResult>> GetListAsync(int token)
        {
            var proc = "dbo.usp_GetEquipmentPosition";

            var param = new DynamicParameters();
            param.Add("@Token", token);

            var result = await SqlMapper.QueryAsync<UspGetEquipmentPositionResult>(
                _dbConnection, sql: proc, param, commandType: CommandType.StoredProcedure);

            return result;
        }

        public async Task CreateAsync(int token)
        {
            // Old API uses CreateLoadPosition
            var proc = "dbo.usp_CreateLoadPosition";

            var param = new DynamicParameters();
            param.Add("@Token", token);

            await SqlMapper.ExecuteAsync(
                _dbConnection, sql: proc, param: param, commandType: CommandType.StoredProcedure);
        }
    }
}
