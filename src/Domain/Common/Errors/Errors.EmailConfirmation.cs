using ErrorOr;

namespace Domain.Common.Errors;

public static partial class Errors
{
    public class EmailConfirmation
    {
        public static Error InvalidRequest => Error.Conflict(
            code: "EmailConfirmation.InvalidRequest",
            description: "Invalid email confirmation request.");
    }
}
