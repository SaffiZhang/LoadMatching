using Dapper;
using LoadLink.LoadMatching.Application.EquipmentLead.Repository;
using LoadLink.LoadMatching.Domain.Procedures;
using LoadLink.LoadMatching.Persistence.Configuration;
using LoadLink.LoadMatching.Persistence.Data;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace LoadLink.LoadMatching.Persistence.Repositories.EquipmentLead
{
    public class EquipmentLeadRepository : IEquipmentLeadRepository
    {
        private readonly IDbConnection _dbConnection;
        private readonly AppSettings _settings;

        public EquipmentLeadRepository(IConnectionFactory connectionFactory,
                                        IOptions<AppSettings> settings)
        {
            _dbConnection = new SqlConnection(connectionFactory.ConnectionString);
            _settings = settings.Value;
        }

        public async Task<IEnumerable<UspGetEquipmentLeadResult>> GetListAsync(string custCD)
        {
            var proc = "dbo.usp_GetEquipmentLead";

            var param = new DynamicParameters();
            param.Add("@CustCD", custCD);
            param.Add("@MileageProvider", _settings.MileageProvider);

            var result = await SqlMapper.QueryAsync<UspGetEquipmentLeadResult>(
                _dbConnection, sql: proc, param, commandType: CommandType.StoredProcedure);

            return result;
        }

        public async Task<IEnumerable<UspGetEquipmentLeadResult>> GetByPostingAsync(string custCD, int postingId)
        {
            var proc = "dbo.usp_GetEquipmentLead";

            var param = new DynamicParameters();
            param.Add("@CustCD", custCD);
            param.Add("@MileageProvider", _settings.MileageProvider);
            param.Add("@EToken", postingId);

            var result = await SqlMapper.QueryAsync<UspGetEquipmentLeadResult>(
                _dbConnection, sql: proc, param, commandType: CommandType.StoredProcedure);

            return result;
        }

        public async Task<IEnumerable<UspGetEquipmentLeadCombinedResult>> GetCombinedAsync(string custCD, int postingId, bool getDAT)
        {
            var proc = "dbo.usp_GetEquipmentLead_Combined";

            var param = new DynamicParameters();
            param.Add("@CustCD", custCD);
            param.Add("@EToken", postingId);
            param.Add("@GetDAT", getDAT);
            param.Add("@MileageProvider", _settings.MileageProvider);
            param.Add("@LeadsCap", _settings.LeadsCap);

            var result = await SqlMapper.QueryAsync<UspGetEquipmentLeadCombinedResult>(
                _dbConnection, sql: proc, param, commandType: CommandType.StoredProcedure);

            return result;
        }
    }
}
