using ErrorOr;

namespace Domain.Common.Errors;

public static partial class Errors
{
    public static class Task
    {
        public static Error DoesNotExist => Error.Conflict(
            code: "Task.DoesNotExist",
            description: "Task does not exist.");
    }
}
