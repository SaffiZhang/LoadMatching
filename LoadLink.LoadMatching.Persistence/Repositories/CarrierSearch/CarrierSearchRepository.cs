using Dapper;
using LoadLink.LoadMatching.Application.CarrierSearch.Models.Queries;
using LoadLink.LoadMatching.Application.CarrierSearch.Repository;
using LoadLink.LoadMatching.Domain.Procedures;
using LoadLink.LoadMatching.Persistence.Data;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace LoadLink.LoadMatching.Persistence.Repositories.CarrierSearch
{
    public class CarrierSearchRepository: ICarrierSearchRepository
    {
        private readonly IDbConnection _dbConnection;
        public CarrierSearchRepository(IConnectionFactory connectionFactory)
        {
            _dbConnection = new SqlConnection(connectionFactory.ConnectionString);
        }

        public async Task<IEnumerable<UspGetCarrierResult>> GetListAsync(GetCarrierSearchQuery searchrequest)
        {
            var proc = "dbo.usp_GetCarrier";
            var param = new DynamicParameters(searchrequest);

            var result = await SqlMapper.QueryAsync<UspGetCarrierResult>(
                _dbConnection, sql: proc, param: param, commandType: CommandType.StoredProcedure);

            return result;
        }
    }
}
