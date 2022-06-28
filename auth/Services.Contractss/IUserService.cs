using auth.Dtos;
using auth.Dtos.User;
using common.Models.Responses;

namespace auth.Services.Contractss
{
    public interface IUserService
    {
        Task<ApiOkResponse<AuthenticationResponseDto>> Login(LoginUserDto dto);
        Task<ApiOkResponse<AuthenticationResponseDto>> RegisterUser(RegisterUserDto dto);
        Task<ApiBaseResponse> RegisterAdmin(RegisterUserDto dto);
        Task<ApiBaseResponse> DeleteUser(DeleteUserDto dto);
        Task<ApiOkResponse<TokenDto>> RefreshToken(TokenDto dto);
        Task<ApiBaseResponse> SendVerificationEmail ();
        Task<ApiBaseResponse> VerifyEmail(VerifyEmailDto dto);
        Task<ApiBaseResponse> SendForgotPasswordEmail(
            SendForgotPasswordEmailDto dto);
        Task<ApiBaseResponse> ResetPassword(ResetPasswordDto dto);
        Task<ApiBaseResponse> ChangePassword(ChangePasswordDto dto);
        Task<ApiOkPagedResponse<IEnumerable<UserDto>, MetaData>>
            SearchPersonsAsync(UserParameters personParameters, bool trackChanges);
        Task<ApiOkResponse<UserDto>> GetLoggedInUser();
        Task<ApiOkResponse<UserDto>> GetUser(GetUserRequestParams dto);
        Task<ApiBaseResponse> UpdateProfilePicture(IFormFile formFile);
    }
}
