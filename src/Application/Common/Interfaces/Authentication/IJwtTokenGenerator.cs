using Application.Authentication.Common;

namespace Application.Common.Interfaces.Authentication;

public interface IJwtTokenGenerator
{
    string GenerateToken(UserDto user, IList<string> roles);
}
