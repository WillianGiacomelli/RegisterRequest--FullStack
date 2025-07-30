using requestpAPI.Models;

namespace requestAPI.Interfaces.Repositories;

public interface IRequestRepository
{
    Task<Request> AddAsync(Request pedido);
    Task<Request?> GetLastAsync();
}
