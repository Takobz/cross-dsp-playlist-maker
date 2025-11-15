using Microsoft.Extensions.Logging;

namespace CrossDSP.Infrastructure.HttpClientInfra.DelegatingHandlers
{
    public class LoggingDelegatingHandler : DelegatingHandler
    {
        private readonly ILogger<LoggingDelegatingHandler> _logger;

        public LoggingDelegatingHandler(
            ILogger<LoggingDelegatingHandler> logger
        )
        {
            _logger = logger;
        }

        protected override async Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request, 
            CancellationToken cancellationToken
        )
        {
            //Before Request
            var requestContent = await request.Content?.ReadAsStringAsync(cancellationToken)! ?? string.Empty;
            _logger.LogInformation(
                "Sending Request HTTP Request {ServiceRequesturi}, {ServiceHttpMethod}, {RequestBody}",
                request.RequestUri,
                request.Method,
                requestContent
            );

            // Sending Request to next Handler / internet
            var response = await base.SendAsync(request, cancellationToken);

            //Response from other handlers / internet
            var responseContent = await response.Content?.ReadAsStringAsync(cancellationToken)! ?? string.Empty;
            if (response.IsSuccessStatusCode)
            {
                _logger.LogInformation(
                    "Request {ServiceRequestUri} indicated a success response {ServiceResponseCode} with response body {ResponseBody}",
                    response.RequestMessage!.RequestUri,
                    response.StatusCode,
                    responseContent
                );
            }

            _logger.LogWarning(
                "Request {ServiceRequestUri} indicated a none success response {ServiceResponseCode} with response body {ResponseBody}",
                response.RequestMessage!.RequestUri,
                response.StatusCode,
                responseContent
            );

            //Sending the response to the handler above.
            return response;
        }
    }
}