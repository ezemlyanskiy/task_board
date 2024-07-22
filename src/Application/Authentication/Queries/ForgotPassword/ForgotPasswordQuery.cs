using ErrorOr;
using MediatR;

namespace Application.Authentication.Queries.ForgotPassword;

public record ForgotPasswordQuery(
    string Email,
    string ClientUri) : IRequest<ErrorOr<bool>>;
