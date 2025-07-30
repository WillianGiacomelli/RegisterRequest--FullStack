using Microsoft.EntityFrameworkCore;
using requestAPI.Interfaces.Repositories;
using requestpAPI.Data;
using requestpAPI.Models;

namespace requestAPI.Repositories;

public class RequestRepository(RequestContext context) : IRequestRepository
{
    private readonly RequestContext _context = context;

    public async Task<Request> AddAsync(Request request)
    {
        await _context.Request.AddAsync(request);
        await _context.SaveChangesAsync();
        return request;
    }

    public async Task<Request?> GetLastAsync()
    {
        return  await _context.Request
            .Include(p => p.Items)
            .OrderByDescending(p => p.Id)
            .FirstOrDefaultAsync();
    }
}
