using requestpAPI.Models;

namespace requestAPI.Interfaces.Services;

public interface IRequestService
{
    Task<Request> CreateRequestAsync(Request request);
    Task<Request?> GetLastRequestAsync();
}
