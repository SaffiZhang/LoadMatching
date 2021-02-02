using Dapper;
using LoadLink.LoadMatching.Application.AssignedEquipment.Models.Commands;
using LoadLink.LoadMatching.Application.AssignedEquipment.Repository;
using LoadLink.LoadMatching.Domain.Procedures;
using LoadLink.LoadMatching.Persistence.Data;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace LoadLink.LoadMatching.Persistence.Repositories.AssignedEquipment
{
    public class AssignedEquipmentRepository : IAssignedEquipmentRepository
    {
        private readonly IDbConnection _dbConnection;

        public AssignedEquipmentRepository(IConnectionFactory connectionFactory)
        {
            _dbConnection = new SqlConnection(connectionFactory.ConnectionString);
        }

        public async Task<UspGetAssignedLoadResult> GetAsync(int token)
        {
            var proc = "dbo.usp_GetAssignedLoad";

            var param = new DynamicParameters();
            param.Add("@EToken", token);

            var result = await SqlMapper.QuerySingleOrDefaultAsync<UspGetAssignedLoadResult>(
                _dbConnection, sql: proc, param, commandType: CommandType.StoredProcedure);

            return result;
        }

        public async Task<int> UpdateAsync(UpdateAssignedEquipmentCommand updateAssignedEquipmentCommand)
        {
            var proc = "dbo.usp_UpdateAssignedEquipment";

            var param = new DynamicParameters();
            param.Add("@UserID", updateAssignedEquipmentCommand.UserId);
            param.Add("@PIN", updateAssignedEquipmentCommand.PIN);
            param.Add("@EToken", updateAssignedEquipmentCommand.EToken);
            param.Add("@DriverID", updateAssignedEquipmentCommand.DriverID);
            param.Add("@EquipmentID", updateAssignedEquipmentCommand.EquipmentID);
            param.Add("@UpdatedBy", updateAssignedEquipmentCommand.UpdatedBy);
            //Return value
            param.Add("@ReturnValue", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);

            await SqlMapper.ExecuteAsync(
                _dbConnection, sql: proc, param: param, commandType: CommandType.StoredProcedure);

            var token = param.Get<int>("@ReturnValue");

            if (token <= 0)
            {
                throw new ValidationException($"There was an error updating the record.  The return value was {token}.");
            }

            return token;
        }
    }
}
