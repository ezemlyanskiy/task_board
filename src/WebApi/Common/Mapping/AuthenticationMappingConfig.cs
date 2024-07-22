using Mapster;
using Application.Authentication.Commands.Register;
using Application.Authentication.Common;
using Application.Authentication.Queries.Login;
using Application.Authentication.Queries.EmailConfirmation;
using Application.Authentication.Queries.ForgotPassword;
using Application.Authentication.Commands.ResetPassword;
using WebApi.Contracts.Accounts;
using Application.Authentication.Commands.AddRole;
using Application.Authentication.Commands.UnblockUser;

namespace WebApi.Common.Mapping;

public class AuthenticationMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<RegisterRequest, RegisterCommand>();
        config.NewConfig<AuthenticationResult, RegisterResponse>();

        config.NewConfig<LoginRequest, LoginQuery>();
        config.NewConfig<AuthenticationResult, LoginResponse>();
   
        config.NewConfig<(string email, string token), EmailConfirmationQuery>()
            .Map(dest => dest.Email, src => src.email)
            .Map(dest => dest.Token, src => src.token);
        
        config.NewConfig<ForgotPasswordRequest, ForgotPasswordQuery>();

        config.NewConfig<ResetPasswordRequest, ResetPasswordCommand>();

        config.NewConfig<UnblockUserRequest, UnblockUserCommand>();

        config.NewConfig<AddRoleRequest, AddRoleCommand>();
    }
}
