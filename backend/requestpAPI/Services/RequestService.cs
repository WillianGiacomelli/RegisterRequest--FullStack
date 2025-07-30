using requestAPI.Interfaces.Repositories;
using requestAPI.Interfaces.Services;
using requestpAPI.Models;

namespace requestAPI.Services;

public class RequestService(IRequestRepository requestRepository) : IRequestService
{
    private readonly IRequestRepository _requestRepository = requestRepository;

    public async Task<Request> CreateRequestAsync(Request request)
    {
        if (request == null || !request.Items.Any())
        {
            throw new ArgumentException("Dados do pedido inválidos ou sem itens.");
        }

        return await _requestRepository.AddAsync(request);
    }

    public async Task<Request?> GetLastRequestAsync()
    {
        return await _requestRepository.GetLastAsync();
    }
}
