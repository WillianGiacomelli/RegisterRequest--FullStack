using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using requestAPI.Interfaces.Services;
using requestpAPI.Data;
using requestpAPI.Models;

namespace requestpAPI.Controllers;

[ApiController]
[Route("api/request")]
public class RequestController(IRequestService requestService) : ControllerBase
{
    private readonly IRequestService _requestService = requestService;

    // POST: /api/request
    [HttpPost]
    public async Task<IActionResult> CreateRequest([FromBody] Request novoPedido)
    {
        try
        {
            var requestCreated = await _requestService.CreateRequestAsync(novoPedido);
            return CreatedAtAction(nameof(CreateRequest), new { id = requestCreated.Id }, requestCreated);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Erro interno ao criar o pedido: {ex.Message}");
        }
    }

    // GET: /api/request/last
    [HttpGet("last")]
    public async Task<IActionResult> CarregarUltimoPedido()
    {
        try
        {
            var lastRequest = await _requestService.GetLastRequestAsync();

            if (lastRequest == null)
            {
                return NotFound("Nenhum pedido encontrado.");
            }

            return Ok(lastRequest);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Erro interno ao carregar o pedido: {ex.Message}");
        }
    }
}
