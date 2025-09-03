namespace MillionLuxury.RealEstate.Domain.Consts;

internal static class ErrorMessages
{
    public const string NotFound = "The requested entity was not found.";
    public const string DuplicateResource = "A duplicate resource was detected.";
    
    public static string DetailedNotFound(string resourceName, string key) => $"{resourceName} with key '{key}' was not found.";
}
