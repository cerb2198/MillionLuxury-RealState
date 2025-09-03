using MillionLuxury.RealEstate.Domain.Consts;
using MillionLuxury.RealEstate.Domain.Exceptions.Common;
using System.Net;

namespace MillionLuxury.RealEstate.Domain.Exceptions;

public class DuplicateResourceException : BaseException
{
    public override HttpStatusCode StatusCode => HttpStatusCode.Conflict;
    
    public DuplicateResourceException() : base(ErrorMessages.DuplicateResource) { }
    public DuplicateResourceException(string message) : base(message) { }
    public DuplicateResourceException(string message, Exception innerException) : base(message, innerException) { }
    public DuplicateResourceException(string resourceName, string details)
        : base($"Duplicate {resourceName}: {details}") { }
}
