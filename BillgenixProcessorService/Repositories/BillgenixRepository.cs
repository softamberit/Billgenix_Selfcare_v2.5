using Common.Infrastructure.Common;
using System.Data;
using System.Data.Common;
using Dapper;
using TrafficProcessorService.Models;
using TrafficProcessorService.Repositories;

namespace TrafficProcessorService.Repositories;

public class BillgenixRepository : IBillgenixRepository
{
    IConnectionFactory _connectionFactory;
    public BillgenixRepository([FromKeyedServices("billgenix")] IConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public BillgenixRepository()
    {

    }

    public async Task<IEnumerable<CustomerTrafficRequestDto>> GetPendingTrafficRequest(string runServerName)
    {
        using var conn = _connectionFactory.CreateConnection();
        return await conn.QueryAsync<CustomerTrafficRequestDto>("sp_getPendingTrafficRequest",new { runServerName }, commandType: CommandType.StoredProcedure);

    }

    public async Task<int> UpdateTrafficRequestStatus(UpdateTrafficRequestDto request)
    {
        using var conn = _connectionFactory.CreateConnection();
        return await conn.ExecuteScalarAsync<int>("spUpdateTrafficRequestStatus", request, commandType: CommandType.StoredProcedure);
    }

    public async Task<CustomerTrafficRequestDto> GetPendingTrafficRequest(string ConnectionId,string runServerName)
    {
        using var conn = _connectionFactory.CreateConnection();
        return await conn.QueryFirstAsync<CustomerTrafficRequestDto>("sp_getPendingTrafficRequest",new { runServerName,ConnectionId }, commandType: CommandType.StoredProcedure);
    }
}
