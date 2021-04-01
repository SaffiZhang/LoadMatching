using Dapper;
using LoadLink.LoadMatching.Application.EquipmentLiveLead.Repository;
using LoadLink.LoadMatching.Domain.Procedures;
using LoadLink.LoadMatching.Persistence.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace LoadLink.LoadMatching.Persistence.Repositories.EquipmentLiveLead
{

    public class EquipmentLiveLeadRepository : IEquipmentLiveLeadRepository
    {
        private readonly IDbConnection _dbConnection;

        public EquipmentLiveLeadRepository(IConnectionFactory connectionFactory)
        {
            _dbConnection = new SqlConnection(connectionFactory.ConnectionString);

        }
        public async Task<IEnumerable<UspGetEquipmentLeadResult>> GetLeads(string custCd, string mileageProvider, DateTime? leadfrom, int? postingId)
        {
            var proc = "usp_GetEquipmentLead";
            var param = new DynamicParameters();
            param.Add("@CustCD", custCd);
            param.Add("@MileageProvider", mileageProvider);
            param.Add("@EToken", postingId == null ? 0 : postingId);
            param.Add("@LeadFrom", leadfrom);

            var result = await SqlMapper.QueryAsync<UspGetEquipmentLeadResult>(
               _dbConnection, sql: proc, param, commandType: CommandType.StoredProcedure);

            return result;

        }

    }
}
