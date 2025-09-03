namespace MillionLuxury.RealEstate.Application.Consts;
public static class SortingOptions
{
    public const string Price = "price";
    public const string Year = "year";
    public const string Name = "name";
    public const string CreatedAt = "createdat";

    public static IReadOnlyList<string> All { get; } = new[]
    {
        Price,
        Year,
        Name,
        CreatedAt
    };

    public static string Default { get; } = CreatedAt;
}
