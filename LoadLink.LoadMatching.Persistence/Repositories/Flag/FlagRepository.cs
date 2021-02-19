using Dapper;
using LoadLink.LoadMatching.Application.Flag.Models.Commands;
using LoadLink.LoadMatching.Application.Flag.Repository;
using LoadLink.LoadMatching.Domain.Procedures;
using LoadLink.LoadMatching.Persistence.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace LoadLink.LoadMatching.Persistence.Repositories.Flag
{
    public class FlagRepository : IFlagRepository
    {
        private readonly IDbConnection _dbConnection;

        public FlagRepository(IConnectionFactory connectionFactory)
        {
            _dbConnection = new SqlConnection(connectionFactory.ConnectionString);
        }

        public async Task<UspGetFlagResult> GetAsync(string custCd, int Id)
        {
            var proc = "dbo.usp_GetFlag";

            var param = new DynamicParameters();
            param.Add("@Id", Id);
            param.Add("@CustCd", custCd); 

            var result = await SqlMapper.QueryFirstOrDefaultAsync<UspGetFlagResult>(
                _dbConnection, sql: proc, param, commandType: CommandType.StoredProcedure);

            return result;
        }

        public async Task<IEnumerable<UspGetFlagResult>> GetListAsync(string custCd)
        {
            var proc = "dbo.usp_GetFlagList";

            var param = new DynamicParameters();
            param.Add("@CustCd", custCd);

            var result = await SqlMapper.QueryAsync<UspGetFlagResult>(
                _dbConnection, sql: proc, param, commandType: CommandType.StoredProcedure);

            return result;
        }

        public async Task<CreateFlagCommand> CreateAsync(CreateFlagCommand createFlagCommand)
        {
            var proc = "dbo.usp_CreateFlag";

            var param = new DynamicParameters();
            param.Add("@CustCD", createFlagCommand.CustCD);
            param.Add("@ContactCustCD", createFlagCommand.ContactCustCD);
            param.Add("@LToken", createFlagCommand.LToken);
            param.Add("@EToken", createFlagCommand.EToken);
            param.Add("@LSrcCity", createFlagCommand.LSrcCity);
            param.Add("@LSrcSt", createFlagCommand.LSrcSt);
            param.Add("@LDestCity", createFlagCommand.LDestCity);
            param.Add("@LDestSt", createFlagCommand.LDestSt);
            param.Add("@LVType", createFlagCommand.LVType);
            param.Add("@LVSize", createFlagCommand.LVSize);
            param.Add("@LPostedDate", createFlagCommand.LPostedDate);
            param.Add("@LPAttrib", createFlagCommand.LPAttrib);
            param.Add("@LComment", createFlagCommand.LComment);
            param.Add("@PSrcCity", createFlagCommand.PSrcCity);
            param.Add("@PSrcSt", createFlagCommand.PSrcSt);
            param.Add("@PDestCity", createFlagCommand.PDestCity);
            param.Add("@PDestSt", createFlagCommand.PDestSt);
            param.Add("@PVType", createFlagCommand.PVType);
            param.Add("@PVSize", createFlagCommand.PVSize);
            param.Add("@PPostedDate", createFlagCommand.PPostedDate);
            param.Add("@PPAttrib", createFlagCommand.PPAttrib);
            param.Add("@PComment", createFlagCommand.PComment);
            param.Add("@PostType", createFlagCommand.PostType);
            param.Add("@CreatedBy", createFlagCommand.CreatedBy);

            param.Add("@ReturnValue", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);

            await SqlMapper.ExecuteAsync(
                _dbConnection, sql: proc, param: param, commandType: CommandType.StoredProcedure);

            var retValue = param.Get<int>("@ReturnValue").ToString();   // Returns Id or -1 in case of error during creation

            createFlagCommand.Id = Convert.ToInt32(retValue);

            return createFlagCommand;
        }

        public async Task DeleteAsync(int id)
        {
            var proc = "dbo.usp_DeleteFlag";

            var param = new DynamicParameters();
            param.Add("@Id", id);

            await SqlMapper.ExecuteAsync(
                _dbConnection, sql: proc, param, commandType: CommandType.StoredProcedure);
        }
    }
}
