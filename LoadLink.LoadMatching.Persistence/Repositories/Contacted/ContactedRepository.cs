using Dapper;
using LoadLink.LoadMatching.Application.Contacted.Models.Commands;
using LoadLink.LoadMatching.Application.Contacted.Repository;
using LoadLink.LoadMatching.Persistence.Data;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace LoadLink.LoadMatching.Persistence.Repositories.Contacted
{
    public class ContactedRepository : IContactedRepository
    {
        private readonly IDbConnection _dbConnection;

        public ContactedRepository(IConnectionFactory connectionFactory)
        {
            _dbConnection = new SqlConnection(connectionFactory.ConnectionString);
        }

        public async Task<int> UpdateAsync(UpdateContactedCommand updateContactedCommand)
        {
            var proc = "dbo.usp_UpdateContacted";

            var param = new DynamicParameters();
            param.Add("@UserId", updateContactedCommand.UserId);
            param.Add("@CnCustCd", updateContactedCommand.CnCustCd);
            param.Add("@LToken", updateContactedCommand.LToken);
            param.Add("@EToken", updateContactedCommand.EToken);

            var result = await SqlMapper.ExecuteAsync(
                _dbConnection, sql: proc, param: param, commandType: CommandType.StoredProcedure);

            return result;
        }
    }
}
