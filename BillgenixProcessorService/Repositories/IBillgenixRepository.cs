using BillgenixProcessorService.Models;

namespace BillgenixProcessorService.Repositories;

public interface IBillgenixRepository
{
    Task<IEnumerable<CustomerTrafficRequestDto>> GetPendingTrafficRequest();
    Task<int> UpdateTrafficRequestStatus(CustomerTrafficRequestDto request);
}
