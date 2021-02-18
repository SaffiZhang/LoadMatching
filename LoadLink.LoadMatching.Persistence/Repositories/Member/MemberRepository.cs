using Dapper;
using LoadLink.LoadMatching.Application.Member.Models.Commands;
using LoadLink.LoadMatching.Application.Member.Repository;
using LoadLink.LoadMatching.Domain.Procedures;
using LoadLink.LoadMatching.Persistence.Data;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace LoadLink.LoadMatching.Persistence.Repositories.Member
{
    public class MemberRepository : IMemberRepository
    {
        private readonly IDbConnection _dbConnection;

        public MemberRepository(IConnectionFactory connectionFactory)
        {
            _dbConnection = new SqlConnection(connectionFactory.ConnectionString);
        }

        public async Task<UspGetMemberResult> GetAsync(string custCd, string memberCustCd)
        {
            var proc = "dbo.usp_GetMember";

            var param = new DynamicParameters();
            param.Add("@CustCD", custCd);
            param.Add("@MemberCustCD", memberCustCd);

            var result = await SqlMapper.QueryFirstOrDefaultAsync<UspGetMemberResult>(
                _dbConnection, sql: proc, param, commandType: CommandType.StoredProcedure);

            return result;
        }

        public async Task<CreateMemberCommand> CreateAsync(CreateMemberCommand createMemberCommand)
        {
            var proc = "dbo.usp_CreateMember";
            var param = new DynamicParameters();

            param.Add("@CustCD", createMemberCommand.CustCd);
            param.Add("@MemberCustCD", createMemberCommand.MemberCustCd);
            param.Add("@DispatchNote", createMemberCommand.DispatchNote);

            await SqlMapper.ExecuteAsync(
                _dbConnection, sql: proc, param: param, commandType: CommandType.StoredProcedure);

            return createMemberCommand;
        }

        public async Task UpdateAsync(UpdateMemberCommand updateMemberCommand)
        {
            var proc = "dbo.usp_UpdateMember";

            var param = new DynamicParameters();
            param.Add("@MemberId", updateMemberCommand.MemberId);
            param.Add("@DispatchNote", updateMemberCommand.DispatchNote);

            await SqlMapper.ExecuteAsync(
                _dbConnection, sql: proc, param: param, commandType: CommandType.StoredProcedure);
        }

        public async Task DeleteAsync(int memberId)
        {
            var proc = "dbo.usp_DeleteMember";

            var param = new DynamicParameters();
            param.Add("@MemberId", memberId);

            await SqlMapper.ExecuteAsync(
                _dbConnection, sql: proc, param, commandType: CommandType.StoredProcedure);
        }
    }
}
