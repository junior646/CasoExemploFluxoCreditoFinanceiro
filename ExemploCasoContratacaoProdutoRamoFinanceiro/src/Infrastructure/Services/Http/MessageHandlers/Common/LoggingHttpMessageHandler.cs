using System.Text;
using Microsoft.Extensions.Logging;

namespace ExemploCasoContratacaoProdutoRamoFinanceiro.Infrastructure.Services.Http.MessageHandlers.Common;
public class LoggingHttpMessageHandler : DelegatingHandler
{
    private readonly ILogger<LoggingHttpMessageHandler> _logger;

    public LoggingHttpMessageHandler(ILogger<LoggingHttpMessageHandler> logger)
    {
        _logger = logger;
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage requestMessage, CancellationToken cancellationToken)
    {
        var logContentBuilder = new StringBuilder($"Recurso: {requestMessage.RequestUri!.Host}{requestMessage.RequestUri.AbsolutePath}");

        var response = await base.SendAsync(requestMessage, cancellationToken);

        logContentBuilder.Append($"\r\nCodigo: {(int)response.StatusCode}");

        if (response.ReasonPhrase!.Replace(" ", string.Empty) != response.StatusCode.ToString())
            logContentBuilder.Append($"\r\nReason Phrase: {response.ReasonPhrase}");

        logContentBuilder.Append($"\r\nConteudo: {await response.Content.ReadAsStringAsync(cancellationToken)}");

        _logger.LogInformation(logContentBuilder.ToString());

        return response;
    }
}