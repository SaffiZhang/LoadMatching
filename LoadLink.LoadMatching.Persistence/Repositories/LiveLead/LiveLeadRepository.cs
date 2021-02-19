using Dapper;
using LoadLink.LoadMatching.Application.LiveLead.Models.Queries;
using LoadLink.LoadMatching.Application.LiveLead.Repository;
using LoadLink.LoadMatching.Domain.Procedures;
using LoadLink.LoadMatching.Persistence.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace LoadLink.LoadMatching.Persistence.Repositories.LiveLead
{
    public class LiveLeadRepository: ILiveLeadRepository
    {
        private readonly IDbConnection _dbConnection;

        public LiveLeadRepository(IConnectionFactory connectionFactory)
        {
            _dbConnection = new SqlConnection(connectionFactory.ConnectionString);
        }

        public async Task<IEnumerable<UspGetLiveLeadsListResult>> GetLiveLeads(GetLiveLeadQuery LLRequest)
        {
            var proc = "dbo.usp_GetLiveLeadsList";
            var param = new DynamicParameters(LLRequest);

            var result = await SqlMapper.QueryAsync<UspGetLiveLeadsListResult>(
               _dbConnection, sql: proc, param: param, commandType: CommandType.StoredProcedure);

            return result;
        }

        public async Task<DateTime> GetServerTime()
        {
            var proc = "dbo.usp_GetLiveLeadsServerTime";
            var param = new DynamicParameters();

            param.Add("@ServerTime", dbType: DbType.DateTime, direction: ParameterDirection.Output);

            await SqlMapper.ExecuteAsync(_dbConnection, sql: proc, param: param, commandType: CommandType.StoredProcedure);

            return param.Get<DateTime>("@ServerTime");
        }
    }
}
