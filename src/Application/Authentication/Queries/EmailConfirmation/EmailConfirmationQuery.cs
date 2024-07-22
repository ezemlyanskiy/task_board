using ErrorOr;
using MediatR;

namespace Application.Authentication.Queries.EmailConfirmation;

public record EmailConfirmationQuery(
    string Email,
    string Token) : IRequest<ErrorOr<bool>>;
