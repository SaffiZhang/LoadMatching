using Dapper;
using LoadLink.LoadMatching.Application.USMemberSearch.Models.Commands;
using LoadLink.LoadMatching.Application.USMemberSearch.Repository;
using LoadLink.LoadMatching.Domain.Procedures;
using LoadLink.LoadMatching.Persistence.Data;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace LoadLink.LoadMatching.Persistence.Repositories.USMemberSearch
{
    public class USMemberSearchRepository : IUSMemberSearchRepository
    {
        private readonly IDbConnection _dbConnection;

        public USMemberSearchRepository(IConnectionFactory connectionFactory)
        {
            _dbConnection = new SqlConnection(connectionFactory.ConnectionString);
        }

        public async Task<IEnumerable<UspGetUSMembersResult>> GetListAsync(GetUSMemberSearchCommand searchRequest)
        {
            var proc = "usp_GetUSMembers";

            var param = new DynamicParameters();
            param.Add("@CompanyName", searchRequest.CompanyName);
            param.Add("@ProvSt", searchRequest.ProvSt);
            param.Add("@Phone", searchRequest.Phone);
            param.Add("@ShowExcluded", (int)searchRequest.ShowExcluded);
            param.Add("@CustCD", searchRequest.CustCd);
            
            var result = await SqlMapper.QueryAsync<UspGetUSMembersResult>(
                _dbConnection, sql: proc, param: param, commandType: CommandType.StoredProcedure);

            return result;
        }

    }
}
