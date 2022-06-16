using auth.Dtos;
using auth.Dtos.User;
using auth.Entities.Responses;

namespace auth.Services.Contractss
{
    public interface IUserService
    {
        Task<ApiOkResponse<AuthenticationResponseDto>> Login(LoginUserDto dto);
        Task<ApiBaseResponse> RegisterUser(RegisterUserDto dto);
        Task<ApiBaseResponse> RegisterAdmin(RegisterUserDto dto);
        Task<ApiBaseResponse> DeleteUser(DeleteUserDto dto);
        Task<ApiOkResponse<TokenDto>> RefreshToken(TokenDto dto);
        Task<ApiBaseResponse> SendVerificationEmail (
            SendVerificationEmailDto dto);
        Task<ApiBaseResponse> VerifyEmail(VerifyEmailDto dto);
        Task<ApiBaseResponse> SendForgotPasswordEmail(
            SendForgotPasswordEmailDto dto);
        Task<ApiBaseResponse> ResetForgottenPassword(ResetForgottenPasswordDto dto);
        Task<ApiBaseResponse> ChangePassword(ChangePasswordDto dto);
        Task<ApiOkPagedResponse<IEnumerable<UserDto>, MetaData>>
            SearchPersonsAsync(UserParameters personParameters, bool trackChanges);
        Task<ApiOkResponse<UserDto>> GetUserByName(string username);
    }
}
