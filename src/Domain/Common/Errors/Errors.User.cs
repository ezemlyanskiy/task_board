using ErrorOr;

namespace Domain.Common.Errors;

public static partial class Errors
{
    public static class User
    {
        public static Error DuplicateEmail => Error.Conflict(
            code: "User.DuplicateEmail",
            description: "Email is already in use.");
        
        public static Error DoesNotExist => Error.NotFound(
            code: "User.DoesNotExist",
            description: "User does not exist.");
        
        public static Error LockedOut => Error.Conflict(
            code: "User.LockedOut",
            description: "User is locked out.");

        public static Error MailIsNotConfirm => Error.Conflict(
            code: "User.MailIsNotConfirm",
            description: "Mail is not confirm.");

        public static Error InvalidRequest => Error.Validation(
            code: "User.InvalidRequest",
            description: "Invalid request.");
    }
}
