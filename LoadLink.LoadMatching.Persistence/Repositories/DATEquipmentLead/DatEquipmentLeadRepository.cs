using Dapper;
using LoadLink.LoadMatching.Application.DATEquipmentLead.Repository;
using LoadLink.LoadMatching.Domain.Procedures;
using LoadLink.LoadMatching.Persistence.Data;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace LoadLink.LoadMatching.Persistence.Repositories.DATEquipmentLead
{
    public class DatEquipmentLeadRepository : IDatEquipmentLeadRepository
    {
        private readonly IDbConnection _dbConnection;

        public DatEquipmentLeadRepository(IConnectionFactory connectionFactory)
        {
            _dbConnection = new SqlConnection(connectionFactory.ConnectionString);

        }
        public async Task<IEnumerable<UspGetDatEquipmentLeadResult>> GetByPosting(string custCd, string mileageProvider, int postingId)
        {
            var proc = "usp_GetDATEquipmentLead";
            var param = new DynamicParameters();
            param.Add("@CustCD", custCd);
            param.Add("@MileageProvider", mileageProvider);
            param.Add("@EToken", postingId);

            var result = await SqlMapper.QueryAsync<UspGetDatEquipmentLeadResult>(
               _dbConnection, sql: proc, param, commandType: CommandType.StoredProcedure);

            return result;

        }

        public async Task<IEnumerable<UspGetDatEquipmentLeadResult>> GetList(string custCd, string mileageProvider)
        {
            var proc = "usp_GetDATEquipmentLead";
            var param = new DynamicParameters();
            param.Add("@CustCD", custCd);
            param.Add("@MileageProvider", mileageProvider);

            var result = await SqlMapper.QueryAsync<UspGetDatEquipmentLeadResult>(
               _dbConnection, sql: proc, param, commandType: CommandType.StoredProcedure);

            return result;
        }
    }
}
