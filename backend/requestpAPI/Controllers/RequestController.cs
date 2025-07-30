using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using requestpAPI.Data;
using requestpAPI.Models;

namespace requestpAPI.Controllers;

[ApiController]
[Route("api/request")]
public class RequestController : ControllerBase
{
    private readonly RequestContext _context;
    public RequestController(RequestContext context)
    {
        _context = context;
    }

    // POST: /api/request
    [HttpPost]
    public async Task<IActionResult> CreateRequest([FromBody] Request novoPedido)
    {
        if (novoPedido == null || !novoPedido.Items.Any())
        {
            return BadRequest("Dados do pedido inválidos ou sem itens.");
        }

        try
        {
            _context.Request.Add(novoPedido);
            await _context.SaveChangesAsync();

            return Ok(novoPedido);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Erro interno ao salvar o pedido: {ex.Message}");
        }
    }

    // GET: /api/request/last
    [HttpGet("last")]
    public async Task<IActionResult> CarregarUltimoPedido()
    {
        try
        {
            var ultimoPedido = await _context.Request
                .Include(p => p.Items)
                .OrderByDescending(p => p.Id)
                .FirstOrDefaultAsync();

            if (ultimoPedido == null)
            {
                return NotFound("Nenhum pedido encontrado.");
            }

            return Ok(ultimoPedido);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Erro interno ao carregar o pedido: {ex.Message}");
        }
    }
}
