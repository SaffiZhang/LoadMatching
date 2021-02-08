using Dapper;
using LoadLink.LoadMatching.Application.LoadLead.Repository;
using LoadLink.LoadMatching.Domain.Procedures;
using LoadLink.LoadMatching.Persistence.Data;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace LoadLink.LoadMatching.Persistence.Repositories.LoadLead
{
    public class LoadLeadRepository : ILoadLeadRepository
    {
        private readonly IDbConnection _dbConnection;

        public LoadLeadRepository(IConnectionFactory connectionFactory)
        {
            _dbConnection = new SqlConnection(connectionFactory.ConnectionString);
        }

        public async Task<IEnumerable<UspGetLoadLeadResult>> GetByPostingAsync(int postingID, string custCd, string mileageProvider)
        {
            var proc = "usp_GetLoadLead";
            var param = new DynamicParameters();

            param.Add("@LToken", postingID);
            param.Add("@CustCD", custCd);
            param.Add("@MileageProvider", mileageProvider);

            var result = await SqlMapper.QueryAsync<UspGetLoadLeadResult>(
                _dbConnection, sql: proc, param: param, commandType: CommandType.StoredProcedure);

            return result;
        }

        public async Task<IEnumerable<UspGetLoadLeadResult>> GetListAsync(string custCd, string mileageProvider)
        {
            var proc = "usp_GetLoadLead";
            var param = new DynamicParameters();

            param.Add("@CustCD", custCd);
            param.Add("@MileageProvider", mileageProvider);

            var result = await SqlMapper.QueryAsync<UspGetLoadLeadResult>(
                _dbConnection, sql: proc, param: param, commandType: CommandType.StoredProcedure);

            return result;
        }

        public async Task<IEnumerable<UspGetLoadLeadResult>> GetByPosting_CombinedAsync(int postingID, string custCd, string mileageProvider, bool datStatus, int leadsCap)
        {
            var proc = "usp_GetLoadLead_Combined";
            var param = new DynamicParameters();

            param.Add("@LToken", postingID);
            param.Add("@CustCD", custCd);
            param.Add("@MileageProvider", mileageProvider);
            param.Add("@GetDAT", datStatus);
            param.Add("@LeadsCap", leadsCap);

            var result = await SqlMapper.QueryAsync<UspGetLoadLeadResult>(
                _dbConnection, sql: proc, param: param, commandType: CommandType.StoredProcedure);

            return result;
        }

    }
}
