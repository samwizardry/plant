namespace Plant.Abstractions;

public static class PlantConstants
{
    public static class Errors
    {
        public const string ProblemDetailsErrors = "errors";
        public const string DetailMessage = "Sorry, an error occurred while processing your request.";
        public const string StandardExceptionOccurred = "A standard exception occurred.";
        public const string ExceptionOccurred = "An exception occurred.";
    }

    public static class Serilog
    {
        public const string MessageTemplate = "{Protocol} {RequestMethod} {RequestPath}{QueryString} responded {StatusCode} in {Elapsed:0.0000} ms";
    }

    public static class ConfigureOrder
    {
        public const int Default = 0;

        // The configuration for authentication should be set up early, prior to any non-security modules.
        public const int Authentication = -150;

        // The reverse proxy should always be configured before the 'Authentication' and security initialization logic.
        public const int ReverseProxy = Authentication * 2;

        // The CORS module should be registered after the reverse proxy module to ensure that the correct host is used.
        public const int Cors = ReverseProxy + 10;

        // The Security module should be registered after the reverse proxy module.
        public const int Security = ReverseProxy + 10;

        public const int Media = Default;

        // Image cache overrides Media configurations and services.
        // The order number should always be greater than Media module. 
        public const int ImageSharpCache = Media + 5;

        // Image cache overrides Media configurations and services.
        // The order number should always be greater than Media module. 
        public const int AzureImageSharpCache = Media + 5;

        // Azure media storage overrides Media configurations and services.
        // The order number should always be greater than Media module.
        public const int AzureMediaStorage = Media + 10;

        public const int DataProtection = Default;

        // Azure DataProtection will override default data-protection configurations.
        // The order number should always be greater than data protection modules. 
        public const int AzureDataProtection = DataProtection + 10;

        public const int Autoroute = -100;

        public const int HomeRoute = -150;

        public const int AdminPages = 1000;

        // Services that should always be registered before everything else.
        public const int InfrastructureService = int.MinValue + 100;
    }
}
