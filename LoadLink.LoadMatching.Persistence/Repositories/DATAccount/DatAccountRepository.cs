using Dapper;
using LoadLink.LoadMatching.Application.DATAccount.Repository;
using LoadLink.LoadMatching.Domain.Procedures;
using LoadLink.LoadMatching.Persistence.Data;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace LoadLink.LoadMatching.Persistence.Repositories.DATAccount
{
    public class DatAccountRepository : IDatAccountRepository
    {
        private readonly IDbConnection _dbConnection;

        public DatAccountRepository(IConnectionFactory connectionFactory)
        {
            _dbConnection = new SqlConnection(connectionFactory.ConnectionString);
        }

        public async Task<UspGetDatAccountResult> GetAsync(string custCd)
        {
            var proc = "dbo.usp_GetDATAccount";

            var param = new DynamicParameters();
            param.Add("@CustCd", custCd);
            param.Add("@AccountId", 0);

            var result = await SqlMapper.QueryFirstOrDefaultAsync<UspGetDatAccountResult>(
                _dbConnection, sql: proc, param, commandType: CommandType.StoredProcedure);

            return result;
        }
    }
}
