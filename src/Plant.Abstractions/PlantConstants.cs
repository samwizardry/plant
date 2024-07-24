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
}
