using TrafficProcessorService.Models;

namespace TrafficProcessorService.Repositories;

public interface IBillgenixRepository
{
    Task<IEnumerable<CustomerTrafficRequestDto>> GetPendingTrafficRequest(string runServerName);
    Task<CustomerTrafficRequestDto> GetPendingTrafficRequest(string connectionId,string runServerName);

    Task<int> UpdateTrafficRequestStatus(UpdateTrafficRequestDto request);
}
