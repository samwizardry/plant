namespace Plant.Logging;

public static class Constants
{
    public const string MessageTemplate = "{Protocol} {RequestMethod} {RequestPath}{QueryString} responded {StatusCode} in {Elapsed:0.0000} ms";
}
