using Dapper;
using LoadLink.LoadMatching.Application.DATEquipmentLiveLead.Repository;
using LoadLink.LoadMatching.Domain.Procedures;
using LoadLink.LoadMatching.Persistence.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace LoadLink.LoadMatching.Persistence.Repositories.DATEquipmentLead
{
    public class DatEquipmentLiveLeadRepository : IDatEquipmentLiveLeadRepository
    {
        private readonly IDbConnection _dbConnection;

        public DatEquipmentLiveLeadRepository(IConnectionFactory connectionFactory)
        {
            _dbConnection = new SqlConnection(connectionFactory.ConnectionString);
        }

        public async Task<IEnumerable<UspGetDatEquipmentLeadResult>> GetLeads(string custCd, string mileageProvider, DateTime? leadfrom, int? postingId)
        {
            var proc = "usp_GetDATEquipmentLead";
            var param = new DynamicParameters();
            param.Add("@CustCD", custCd);
            param.Add("@MileageProvider", mileageProvider);
            param.Add("@EToken", postingId == null ? 0 : postingId);
            param.Add("@LeadFrom", leadfrom);

            var result = await SqlMapper.QueryAsync<UspGetDatEquipmentLeadResult>(
               _dbConnection, sql: proc, param, commandType: CommandType.StoredProcedure);

            return result;
        }
    }
}
