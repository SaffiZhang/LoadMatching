using Dapper;
using LoadLink.LoadMatching.Application.LeadCount.Repository;
using LoadLink.LoadMatching.Domain.Procedures;
using LoadLink.LoadMatching.Persistence.Data;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace LoadLink.LoadMatching.Persistence.Repositories.LeadCount
{
    public class LeadsCountRepository : ILeadsCountRepository
    {
        private readonly IDbConnection _dbConnection;

        public LeadsCountRepository(IConnectionFactory connectionFactory)
        {
            _dbConnection = new SqlConnection(connectionFactory.ConnectionString);
        }

        public async Task<IEnumerable<UspGetLoadLeadsCountResult>> GetLoadLeadsCountAsync(int userId, int token, bool getDAT)
        {
            var proc = "dbo.usp_GetLoadLeadsCount";

            var param = new DynamicParameters();
            param.Add("@UserID", userId);
            param.Add("@LToken", token);
            param.Add("@GetDAT", getDAT);

            var result = await SqlMapper.QueryAsync<UspGetLoadLeadsCountResult>(
                _dbConnection, sql: proc, param, commandType: CommandType.StoredProcedure);

            return result;
        }

        public async Task<IEnumerable<UspGetEquipmentLeadsCountResult>> GetEquipLeadsCountAsync(int userId, int token, bool getDAT)
        {
            var proc = "dbo.usp_GetDATLoadLead";

            var param = new DynamicParameters();
            param.Add("@UserID", userId);
            param.Add("@EToken", token);
            param.Add("@GetDAT", getDAT);

            var result = await SqlMapper.QueryAsync<UspGetEquipmentLeadsCountResult>(
                _dbConnection, sql: proc, param, commandType: CommandType.StoredProcedure);

            return result;
        }
    }
}
