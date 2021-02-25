using Dapper;
using LoadLink.LoadMatching.Application.EquipmentLead.Repository;
using LoadLink.LoadMatching.Domain.Procedures;
using LoadLink.LoadMatching.Persistence.Data;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace LoadLink.LoadMatching.Persistence.Repositories.EquipmentLead
{
    public class EquipmentLeadRepository : IEquipmentLeadRepository
    {
        private readonly IDbConnection _dbConnection;

        public EquipmentLeadRepository(IConnectionFactory connectionFactory)
        {
            _dbConnection = new SqlConnection(connectionFactory.ConnectionString);
        }

        public async Task<IEnumerable<UspGetEquipmentLeadResult>> GetListAsync(string custCD, string mileageProvider)
        {
            var proc = "dbo.usp_GetEquipmentLead";

            var param = new DynamicParameters();
            param.Add("@CustCD", custCD);
            param.Add("@MileageProvider", mileageProvider);

            var result = await SqlMapper.QueryAsync<UspGetEquipmentLeadResult>(
                _dbConnection, sql: proc, param, commandType: CommandType.StoredProcedure);

            return result;
        }

        public async Task<IEnumerable<UspGetEquipmentLeadResult>> GetByPostingAsync(string custCD, int postingId, string mileageProvider)
        {
            var proc = "dbo.usp_GetEquipmentLead";

            var param = new DynamicParameters();
            param.Add("@CustCD", custCD);
            param.Add("@MileageProvider", mileageProvider);
            param.Add("@EToken", postingId);

            var result = await SqlMapper.QueryAsync<UspGetEquipmentLeadResult>(
                _dbConnection, sql: proc, param, commandType: CommandType.StoredProcedure);

            return result;
        }

        public async Task<IEnumerable<UspGetEquipmentLeadCombinedResult>> GetCombinedAsync(string custCD, int postingId, string mileageProvider,
                                                                                            int leadsCap, bool getDAT)
        {
            var proc = "dbo.usp_GetEquipmentLead_Combined";

            var param = new DynamicParameters();
            param.Add("@CustCD", custCD);
            param.Add("@EToken", postingId);
            param.Add("@GetDAT", getDAT);
            param.Add("@MileageProvider", mileageProvider);
            param.Add("@LeadsCap", leadsCap);

            var result = await SqlMapper.QueryAsync<UspGetEquipmentLeadCombinedResult>(
                _dbConnection, sql: proc, param, commandType: CommandType.StoredProcedure);

            return result;
        }
    }
}
