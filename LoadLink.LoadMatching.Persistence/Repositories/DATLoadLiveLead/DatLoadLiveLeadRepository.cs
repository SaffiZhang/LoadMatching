using Dapper;
using LoadLink.LoadMatching.Application.DATLoadLiveLead.Repository;
using LoadLink.LoadMatching.Domain.Procedures;
using LoadLink.LoadMatching.Persistence.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace LoadLink.LoadMatching.Persistence.Repositories.DATLoadLead
{
    public class DatLoadLiveLeadRepository : IDatLoadLiveLeadRepository
    {
        private readonly IDbConnection _dbConnection;

        public DatLoadLiveLeadRepository(IConnectionFactory connectionFactory)
        {
            _dbConnection = new SqlConnection(connectionFactory.ConnectionString);

        }
        public async Task<IEnumerable<UspGetDatLoadLeadResult>> GetLeads(string custCd, string mileageProvider, DateTime? leadfrom, int? postingId)
        {
            var proc = "usp_GetDATLoadLead";
            var param = new DynamicParameters();
            param.Add("@CustCD", custCd);
            param.Add("@MileageProvider", mileageProvider);
            param.Add("@EToken", postingId);
            param.Add("@LeadFrom", leadfrom);

            var result = await SqlMapper.QueryAsync<UspGetDatLoadLeadResult>(
               _dbConnection, sql: proc, param, commandType: CommandType.StoredProcedure);

            return result;

        }

    }
}
