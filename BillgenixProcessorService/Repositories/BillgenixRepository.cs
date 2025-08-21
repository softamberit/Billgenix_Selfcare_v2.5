using BillgenixProcessorService.Models;
using Common.Infrastructure.Common;
using System.Data;
using System.Data.Common;
using Dapper;

namespace BillgenixProcessorService.Repositories;

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

    public async Task<IEnumerable<CustomerTrafficRequestDto>> GetPendingTrafficRequest()
    {
        using var conn = _connectionFactory.CreateConnection();
        return await conn.QueryAsync<CustomerTrafficRequestDto>("sp_getPendingTrafficRequest", commandType: CommandType.StoredProcedure);

    }

    public async Task<int> UpdateTrafficRequestStatus(CustomerTrafficRequestDto request)
    {
        using var conn = _connectionFactory.CreateConnection();
        return await conn.ExecuteScalarAsync<int>("spUpdateTrafficRequestStatus", new { request.Id, request.ProcessStatus }, commandType: CommandType.StoredProcedure);
    }

    public async Task<CustomerTrafficRequestDto> GetPendingTrafficRequest(string ConnectionId)
    {
        using var conn = _connectionFactory.CreateConnection();
        return await conn.QueryFirstAsync<CustomerTrafficRequestDto>("sp_getPendingTrafficRequest",new { ConnectionId }, commandType: CommandType.StoredProcedure);
    }
}
