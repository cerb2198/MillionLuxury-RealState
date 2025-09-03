using MillionLuxury.RealEstate.Domain.Consts;
using MillionLuxury.RealEstate.Domain.Exceptions.Common;
using System.Net;

namespace MillionLuxury.RealEstate.Domain.Exceptions;
public class NotFoundException : BaseException
{
    public override HttpStatusCode StatusCode => HttpStatusCode.NotFound;
    public NotFoundException() : base(ErrorMessages.NotFound) { }
    public NotFoundException(string message) : base(message) { }
    public NotFoundException(string message, Exception innerException) : base(message, innerException) { }
    public NotFoundException(string resourceName, string key)
        : base(ErrorMessages.DetailedNotFound(resourceName, key)) { }
}
