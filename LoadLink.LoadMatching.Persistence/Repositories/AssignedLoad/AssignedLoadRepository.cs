
using Dapper;
using LoadLink.LoadMatching.Application.AssignedLoad.Models.Commands;
using LoadLink.LoadMatching.Application.AssignedLoad.Repository;
using LoadLink.LoadMatching.Domain.Procedures;
using LoadLink.LoadMatching.Persistence.Data;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace LoadLink.LoadMatching.Persistence.Repositories.AssignedLoad
{
    public class AssignedLoadRepository : IAssignedLoadRepository
    {
        private readonly IDbConnection _dbConnection;

        public AssignedLoadRepository(IConnectionFactory connectionFactory)
        {
            _dbConnection = new SqlConnection(connectionFactory.ConnectionString);
        }

        public async Task<UspGetAssignedLoadResult> GetAsync(int token, int userId)
        {
            var proc = "dbo.usp_GetAssignedLoad";

            var param = new DynamicParameters();
            param.Add("@Token", token);
            param.Add("@UserId", (token == 0)? userId:0); // If token is provided don't pass Userid to allow non driver to make a call

            var result = await SqlMapper.QueryFirstOrDefaultAsync<UspGetAssignedLoadResult>(
                _dbConnection, sql: proc, param, commandType: CommandType.StoredProcedure);

            return result;
        }

        public async Task<IEnumerable<UspGetAssignedLoadResult>> GetListAsync(int userId)
        {
            var proc = "dbo.usp_GetAssignedLoad";

            var param = new DynamicParameters();
            param.Add("@UserId", userId);

            var result = await SqlMapper.QueryAsync<UspGetAssignedLoadResult>(
                _dbConnection, sql: proc, param, commandType: CommandType.StoredProcedure);

            return result;
        }

        public async Task<string> CreateAsync(CreateAssignedLoadCommand createAssignedLoadCommand)
        {
            var proc = "dbo.usp_CreateAssignedLoad";
            var param = new DynamicParameters();

            param.Add("@UserId", createAssignedLoadCommand.UserId);
            param.Add("@Token", createAssignedLoadCommand.Token);
            param.Add("@EToken", createAssignedLoadCommand.EToken);
            param.Add("@Instruction", createAssignedLoadCommand.Instruction);
            param.Add("@CreatedBy", createAssignedLoadCommand.CreatedBy);

            param.Add("@ReturnValue", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);

            await SqlMapper.ExecuteAsync(
                _dbConnection, sql: proc, param: param, commandType: CommandType.StoredProcedure);

            var retValue = param.Get<int>("@ReturnValue").ToString();   // Returns PIN or -1 in case of error during creation
            if (retValue == "-1")
                throw new ValidationException($"There was an error creating the record. The return value is { retValue }.");

            return retValue;
        }

        public async Task<int> UpdateAsync(UpdateAssignedLoadCommand updateAssignedLoadCommand)
        {
            var proc = "dbo.usp_UpdateAssignedLoad";

            var param = new DynamicParameters();
            param.Add("@UserId", updateAssignedLoadCommand.UserId);
            param.Add("@PIN", updateAssignedLoadCommand.PIN);
            param.Add("@DriverID", updateAssignedLoadCommand.DriverID);
            param.Add("@UpdatedBy", updateAssignedLoadCommand.UpdatedBy);
            //Return value
            param.Add("@ReturnValue", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);

            await SqlMapper.ExecuteAsync(
                _dbConnection, sql: proc, param: param, commandType: CommandType.StoredProcedure);

            var token = param.Get<int>("@ReturnValue");

            //looking at the logic in SP, Updates ONLY if record's UserId is 0 - updateAssignedLoadCommand.UserId is used to get CustCD
            if (token <= 0)
            {
                throw new ValidationException($"There was an error updating the record.  The return value was {token}.");
            }

            return token;
        }

        public async Task<int> UpdateCustomerTrackingAsync(UpdateCustomerTrackingCommand updateCustomerTrackingCommand)
        {
            var proc = "dbo.usp_UpdateAssignedLoadCustTracking";

            var param = new DynamicParameters();
            param.Add("@UserId", updateCustomerTrackingCommand.UserId);
            param.Add("@ID", updateCustomerTrackingCommand.ID);
            param.Add("@CustTracking", updateCustomerTrackingCommand.CustTracking);
            param.Add("@UpdatedBy", updateCustomerTrackingCommand.UpdatedBy);
            //Return value
            param.Add("@ReturnValue", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);

            await SqlMapper.ExecuteAsync(
                _dbConnection, sql: proc, param: param, commandType: CommandType.StoredProcedure);

            var retValue = param.Get<int>("@ReturnValue");

            if (retValue <= 0)
            {
                throw new ValidationException($"There was an error updating the record.  The return value was {retValue}.");
            }

            return retValue;
        }

        public async Task<UspDeleteAssignedLoadResult> DeleteAsync(int token, int userId)
        {
            var proc = "dbo.usp_DeleteAssignedLoad";

            var param = new DynamicParameters();
            param.Add("@Token", token);
            param.Add("@UserId", userId);

            var result = await SqlMapper.QueryFirstOrDefaultAsync<UspDeleteAssignedLoadResult>(
                _dbConnection, sql: proc, param, commandType: CommandType.StoredProcedure);

            return result;
        }
    }
}
