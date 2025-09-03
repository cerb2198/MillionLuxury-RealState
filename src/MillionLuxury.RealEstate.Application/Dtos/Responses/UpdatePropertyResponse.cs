namespace MillionLuxury.RealEstate.Application.Dtos.Responses;

public class UpdatePropertyResponse
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Country { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string Street { get; set; } = string.Empty;
    public int ZipCode { get; set; }
    public decimal Price { get; set; }
    public int CodeInternal { get; set; }
    public int Year { get; set; }
    public int OwnerId { get; set; }
    public DateTime LastModifiedAt { get; set; }
}
