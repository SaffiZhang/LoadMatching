using Dapper;
using LoadLink.LoadMatching.Application.LoadLead.Repository;
using LoadLink.LoadMatching.Domain.Procedures;
using LoadLink.LoadMatching.Persistence.Configuration;
using LoadLink.LoadMatching.Persistence.Data;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace LoadLink.LoadMatching.Persistence.Repositories.LoadLead
{
    public class LoadLeadRepository : ILoadLeadRepository
    {
        private readonly IDbConnection _dbConnection;
        private readonly AppSettings _settings;

        public LoadLeadRepository(IConnectionFactory connectionFactory,
                                  IOptions<AppSettings> settings)
        {
            _dbConnection = new SqlConnection(connectionFactory.ConnectionString);
            _settings = settings.Value;
        }

        public async Task<IEnumerable<UspGetLoadLeadResult>> GetByPostingAsync(string custCd, int postingID)
        {
            var proc = "usp_GetLoadLead";
            var param = new DynamicParameters();

            param.Add("@LToken", postingID);
            param.Add("@CustCD", custCd);
            param.Add("@MileageProvider", _settings.MileageProvider);

            var result = await SqlMapper.QueryAsync<UspGetLoadLeadResult>(
                _dbConnection, sql: proc, param: param, commandType: CommandType.StoredProcedure);

            return result;
        }

        public async Task<IEnumerable<UspGetLoadLeadResult>> GetListAsync(string custCd)
        {
            var proc = "usp_GetLoadLead";
            var param = new DynamicParameters();

            param.Add("@CustCD", custCd);
            param.Add("@MileageProvider", _settings.MileageProvider);

            var result = await SqlMapper.QueryAsync<UspGetLoadLeadResult>(
                _dbConnection, sql: proc, param: param, commandType: CommandType.StoredProcedure);

            return result;
        }

        public async Task<IEnumerable<UspGetLoadLeadResult>> GetByPosting_CombinedAsync(string custCd, int postingID,  bool datStatus)
        {
            var proc = "usp_GetLoadLead_Combined";
            var param = new DynamicParameters();

            param.Add("@LToken", postingID);
            param.Add("@CustCD", custCd);
            param.Add("@MileageProvider", _settings.MileageProvider);
            param.Add("@GetDAT", datStatus);
            param.Add("@LeadsCap", _settings.LeadsCap);

            var result = await SqlMapper.QueryAsync<UspGetLoadLeadResult>(
                _dbConnection, sql: proc, param: param, commandType: CommandType.StoredProcedure);

            return result;
        }

    }
}
