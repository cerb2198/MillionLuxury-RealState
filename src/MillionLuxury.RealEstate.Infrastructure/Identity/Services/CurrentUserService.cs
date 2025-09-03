using Microsoft.AspNetCore.Http;
using MillionLuxury.RealEstate.Infrastructure.Identity.Interfaces;

namespace MillionLuxury.RealEstate.Infrastructure.Identity.Services;
public sealed class CurrentUserService(
    IHttpContextAccessor _httpContextAccessor
) : ICurrentUserService
{
    private const string SystemUser = "system";
    private const string EmailClaim =
        "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress";

    public string GetUserEmail()
    {
        var user = _httpContextAccessor.HttpContext?.User;

        if (user == null)
            return SystemUser;

        var emailClaim = user.FindFirst(EmailClaim);

        return emailClaim?.Value ?? SystemUser;
    }
}
