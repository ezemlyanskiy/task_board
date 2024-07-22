using ErrorOr;

namespace Domain.Common.Errors;

public static partial class Errors
{
    public static class Sprint
    {
        public static Error DoesNotExist => Error.Conflict(
            code: "Sprint.DoesNotExist",
            description: "Sprint does not exist.");
    }
}
