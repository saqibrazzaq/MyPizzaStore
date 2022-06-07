using auth.Dtos.User;
using auth.Entities.Responses;

namespace auth.Services.Contractss
{
    public interface IUserService
    {
        Task<ApiOkResponse<AuthenticationResponseDto>> Login(LoginUserDto dto);
        Task<ApiBaseResponse> RegisterUser(RegisterUserDto dto);
        Task<ApiBaseResponse> SendVerificationEmail (
            SendVerificationEmailDto dto);
        Task<ApiBaseResponse> VerifyEmail(VerifyEmailDto dto);
        Task<ApiBaseResponse> SendForgotPasswordEmail(
            SendForgotPasswordEmailDto dto);
        Task<ApiBaseResponse> ResetForgottenPassword(ResetForgottenPasswordDto dto);
        Task<ApiBaseResponse> ChangePassword(ChangePasswordDto dto);
    }
}
