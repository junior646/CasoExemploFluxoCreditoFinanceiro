using MediatR;
using NUnit.Framework;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;

namespace ExemploCasoContratacaoProdutoRamoFinanceiro.Application.IntegrationTests;

[SetUpFixture]
public class Testing
{
    private static IServiceScopeFactory _scopeFactory = null!;
    private static WebApplicationFactory<Program> _factory = null!;

    [OneTimeSetUp]
    public void RunBeforeAnyTests()
    {
        _factory = new CustomWebApplicationFactory();
        _scopeFactory = _factory.Services.GetRequiredService<IServiceScopeFactory>();
    }

    public static async Task<TResponse> SendAsync<TResponse>(IRequest<TResponse> request)
    {
        using var scope = _scopeFactory.CreateScope();

        var mediator = scope.ServiceProvider.GetRequiredService<ISender>();

        return await mediator.Send(request);
    }

    [OneTimeTearDown]
    public void RunAfterAnyTests() { }
}