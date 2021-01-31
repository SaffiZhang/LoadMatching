using Dapper;
using LoadLink.LoadMatching.Application.USCarrierSearch.Models.Commands;
using LoadLink.LoadMatching.Application.USCarrierSearch.Repository;
using LoadLink.LoadMatching.Domain.Procedures;
using LoadLink.LoadMatching.Persistence.Data;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace LoadLink.LoadMatching.Persistence.Repositories.USCarrierSearch
{
    public class USCarrierSearchRepository : IUSCarrierSearchRepository
    {
        private readonly IDbConnection _dbConnection;

        public USCarrierSearchRepository(IConnectionFactory connectionFactory)
        {
            _dbConnection = new SqlConnection(connectionFactory.ConnectionString);
        }

        public async Task<IEnumerable<UspGetUSCarrierResult>> GetListAsync(GetUSCarrierSearchCommand searchRequest)
        {
            var proc = "usp_GetUSCarrier";
            var param = new DynamicParameters();

            param.Add("@userId", searchRequest.UserId);
            param.Add("@SrceSt", searchRequest.SrceSt);
            param.Add("@SrceCity", searchRequest.SrceCity);
            param.Add("@SrceRadius", searchRequest.SrceRadius);
            param.Add("@DestSt", searchRequest.DestSt);
            param.Add("@DestCity", searchRequest.DestCity);
            param.Add("@DestRadius", searchRequest.DestRadius);
            param.Add("@VType", searchRequest.VehicleType);
            param.Add("@VSize", searchRequest.VehicleSize);
            param.Add("@CompanyName", searchRequest.CompanyName);
            param.Add("@GetMexico", searchRequest.GetMexico);

            var result = await SqlMapper.QueryAsync<UspGetUSCarrierResult>(
                _dbConnection, sql: proc, param: param, commandType: CommandType.StoredProcedure);

            return result;
        }

    }
}
