using Dapper;
using LoadLink.LoadMatching.Application.DATLoadLead.Repository;
using LoadLink.LoadMatching.Domain.Procedures;
using LoadLink.LoadMatching.Persistence.Data;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace LoadLink.LoadMatching.Persistence.Repositories.DatLoadLead
{
    public class DatLoadLeadRepository : IDatLoadLeadRepository
    {
        private readonly IDbConnection _dbConnection;

        public DatLoadLeadRepository(IConnectionFactory connectionFactory)
        {
            _dbConnection = new SqlConnection(connectionFactory.ConnectionString);
        }

        public async Task<IEnumerable<UspGetDatLoadLeadResult>> GetListAsync(string custCD, string mileageProvider)
        {
            var proc = "dbo.usp_GetDATLoadLead";

            var param = new DynamicParameters();
            param.Add("@CustCD", custCD);
            param.Add("@MileageProvider", mileageProvider);

            var result = await SqlMapper.QueryAsync<UspGetDatLoadLeadResult>(
                _dbConnection, sql: proc, param, commandType: CommandType.StoredProcedure);

            return result;
        }

        public async Task<IEnumerable<UspGetDatLoadLeadResult>> GetByPostingAsync(string custCD, int postingId, string mileageProvider)
        {
            var proc = "dbo.usp_GetDATLoadLead";

            var param = new DynamicParameters();
            param.Add("@CustCD", custCD);
            param.Add("@MileageProvider", mileageProvider);
            param.Add("@LToken", postingId);

            var result = await SqlMapper.QueryAsync<UspGetDatLoadLeadResult>(
                _dbConnection, sql: proc, param, commandType: CommandType.StoredProcedure);

            return result;
        }
    }
}
