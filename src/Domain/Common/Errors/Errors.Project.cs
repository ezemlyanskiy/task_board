using ErrorOr;

namespace Domain.Common.Errors;

public static partial class Errors
{
    public static class Project
    {
        public static Error DoesNotExist => Error.Conflict(
            code: "Project.DoesNotExist",
            description: "Project does not exist.");
    }
}
