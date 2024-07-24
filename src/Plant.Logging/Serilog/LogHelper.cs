using Microsoft.AspNetCore.Http;
using Serilog;

namespace Plant.Serilog;

public static partial class LogHelper
{
    public static void EnrichFromRequest(IDiagnosticContext diagnosticContext, HttpContext httpContext)
    {
        diagnosticContext.Set("Protocol", httpContext.Request.Protocol);

        if (httpContext.Request.QueryString.HasValue)
        {
            diagnosticContext.Set("QueryString", Uri.UnescapeDataString(httpContext.Request.QueryString.Value!));
        }
        else
        {
            diagnosticContext.Set("QueryString", "");
        }
    }
}
