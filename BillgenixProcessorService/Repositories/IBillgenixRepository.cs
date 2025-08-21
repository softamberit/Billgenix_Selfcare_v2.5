using BillgenixProcessorService.Models;

namespace BillgenixProcessorService.Repositories;

public interface IBillgenixRepository
{
    Task<IEnumerable<CustomerTrafficRequestDto>> GetPendingTrafficRequest();
    Task<CustomerTrafficRequestDto> GetPendingTrafficRequest(string connectionId);

    Task<int> UpdateTrafficRequestStatus(CustomerTrafficRequestDto request);
}
