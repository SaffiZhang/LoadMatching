using Dapper;
using LoadLink.LoadMatching.Application.DATLoadLead.Repository;
using LoadLink.LoadMatching.Domain.Procedures;
using LoadLink.LoadMatching.Persistence.Configuration;
using LoadLink.LoadMatching.Persistence.Data;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace LoadLink.LoadMatching.Persistence.Repositories.DatLoadLead
{
    public class DatLoadLeadRepository : IDatLoadLeadRepository
    {
        private readonly IDbConnection _dbConnection;
        private readonly AppSettings _settings;

        public DatLoadLeadRepository(IConnectionFactory connectionFactory,
                                        IOptions<AppSettings> settings)
        {
            _dbConnection = new SqlConnection(connectionFactory.ConnectionString);
            _settings = settings.Value;
        }

        public async Task<IEnumerable<UspGetDatLoadLeadResult>> GetListAsync(string custCD)
        {
            var proc = "dbo.usp_GetDATLoadLead";

            var param = new DynamicParameters();
            param.Add("@CustCD", custCD);
            param.Add("@MileageProvider", _settings.MileageProvider);

            var result = await SqlMapper.QueryAsync<UspGetDatLoadLeadResult>(
                _dbConnection, sql: proc, param, commandType: CommandType.StoredProcedure);

            return result;
        }

        public async Task<IEnumerable<UspGetDatLoadLeadResult>> GetByPostingAsync(string custCD, int postingId)
        {
            var proc = "dbo.usp_GetDATLoadLead";

            var param = new DynamicParameters();
            param.Add("@CustCD", custCD);
            param.Add("@MileageProvider", _settings.MileageProvider);
            param.Add("@LToken", postingId);

            var result = await SqlMapper.QueryAsync<UspGetDatLoadLeadResult>(
                _dbConnection, sql: proc, param, commandType: CommandType.StoredProcedure);

            return result;
        }
    }
}
