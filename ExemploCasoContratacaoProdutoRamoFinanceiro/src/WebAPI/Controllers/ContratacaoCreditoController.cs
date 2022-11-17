using WebAPI.Common.Base;
using Microsoft.AspNetCore.Mvc;
using ExemploCasoContratacaoProdutoRamoFinanceiro.Application.UseCases.ContratacaoCredito.Queries.ConsultarCredito;

namespace WebAPI.Controllers;

public class ContratacaoCreditoController : ApiControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Consultar([FromBody] ConsultarCreditoQuery query)
    {
        return query is null ? BadRequest() : Ok(await Mediator.Send(query));
    }
}