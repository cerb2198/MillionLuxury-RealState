using System.Net;

namespace MillionLuxury.RealEstate.Domain.Exceptions.Common;
public abstract class BaseException : Exception
{
    public abstract HttpStatusCode StatusCode { get; }
    public virtual string ErrorCode => GetType().Name;
    protected BaseException() { }
    protected BaseException(string message) : base(message) { }
    protected BaseException(string message, Exception innerException) : base(message, innerException) { }
}
