using Dapper;
using LoadLink.LoadMatching.Application.LoadLiveLead.Repository;
using LoadLink.LoadMatching.Domain.Procedures;
using LoadLink.LoadMatching.Persistence.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace LoadLink.LoadMatching.Persistence.Repositories.LoadLiveLead
{

    public class LoadLiveLeadRepository : ILoadLiveLeadRepository
    {
        private readonly IDbConnection _dbConnection;

        public LoadLiveLeadRepository(IConnectionFactory connectionFactory)
        {
            _dbConnection = new SqlConnection(connectionFactory.ConnectionString);

        }
        public async Task<IEnumerable<UspGetLoadLeadResult>> GetLeads(string custCd, string mileageProvider, DateTime? leadfrom, int? postingId)
        {
            var proc = "usp_GetLoadLead";
            var param = new DynamicParameters();
            param.Add("@CustCD", custCd);
            param.Add("@MileageProvider", mileageProvider);
            param.Add("@LToken", postingId);
            param.Add("@LeadFrom", leadfrom);

            var result = await SqlMapper.QueryAsync<UspGetLoadLeadResult>(
               _dbConnection, sql: proc, param, commandType: CommandType.StoredProcedure);

            return result;

        }

    }
}
