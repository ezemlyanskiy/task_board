using ErrorOr;

namespace Domain.Common.Errors;

public static partial class Errors
{
    public class ResetPassword
    {
        public static Error InvalidRequest => Error.Conflict(
            code: "ResetPassword.InvalidRequest",
            description: "Invalid reset password request.");
    }
}
