using ErrorOr;

namespace Domain.Common.Errors;

public static partial class Errors
{
    public class Authentication
    {
        public static Error InvalidCredentials => Error.Validation(
            code: "Auth.InvalidCred",
            description: "Invalid credentials.");
    }
}
