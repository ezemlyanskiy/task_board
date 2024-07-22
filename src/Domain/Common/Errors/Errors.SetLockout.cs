using ErrorOr;

namespace Domain.Common.Errors;

public static partial class Errors
{
    public static class SetLockedOut
    {
        public static Error InvalidRequest => Error.Conflict(
            code: "SetLockedOut.InvalidRequest",
            description: "Invalid SetLockedOut request.");
    }
}
