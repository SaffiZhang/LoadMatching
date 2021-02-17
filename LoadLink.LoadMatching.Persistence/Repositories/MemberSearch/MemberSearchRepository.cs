using Dapper;
using LoadLink.LoadMatching.Application.MemberSearch.Models.Queries;
using LoadLink.LoadMatching.Application.MemberSearch.Repository;
using LoadLink.LoadMatching.Domain.Procedures;
using LoadLink.LoadMatching.Persistence.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace LoadLink.LoadMatching.Persistence.Repositories.MemberSearch
{
    public class MemberSearchRepository : IMemberSearchRepository
    {

        private readonly IDbConnection _dbConnection;

        public MemberSearchRepository(IConnectionFactory connectionFactory)
        {
            _dbConnection = new SqlConnection(connectionFactory.ConnectionString);
        }
        public async Task<IEnumerable<UspGetMembersResult>> GetListAsync(GetMemberSearchQuery searchrequest)
        {
            var proc = "dbo.usp_GetMembers";
            var param = new DynamicParameters(searchrequest);

            var result = await SqlMapper.QueryAsync<UspGetMembersResult>(
             _dbConnection, sql: proc, param: param, commandType: CommandType.StoredProcedure);

            return result;
        }
    }
}
