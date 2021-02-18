using Dapper;
using LoadLink.LoadMatching.Application.LegacyDeleted.Repository;
using LoadLink.LoadMatching.Domain.Procedures;
using LoadLink.LoadMatching.Persistence.Data;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace LoadLink.LoadMatching.Persistence.Repositories.LegacyDeleted
{
    public class LegacyDeletedRepository : ILegacyDeletedRepository
    {
        private readonly IDbConnection _dbConnection;

        public LegacyDeletedRepository(IConnectionFactory connectionFactory)
        {
            _dbConnection = new SqlConnection(connectionFactory.ConnectionString);
        }

        public async Task<IEnumerable<UspGetUserLegacyDeletedResult>> GetListAsync(char leadType, string custCd)
        {
            var proc = "dbo.usp_GetUserLegacyDeleted";

            var param = new DynamicParameters();
            param.Add("@LeadType", leadType);
            param.Add("@CustCD", custCd);

            var result = await SqlMapper.QueryAsync<UspGetUserLegacyDeletedResult>(
                _dbConnection, sql: proc, param, commandType: CommandType.StoredProcedure);

            return result;
        }

        public async Task UpdateAsync(char leadType, string custCd)
        {
            var proc = "dbo.usp_UpdateUserLegacyDeleted";

            var param = new DynamicParameters();
            param.Add("@LeadType", leadType);
            param.Add("@CustCD", custCd);

            await SqlMapper.ExecuteAsync(
                _dbConnection, sql: proc, param, commandType: CommandType.StoredProcedure);
        }
    }
}
