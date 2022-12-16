using HotelListing.API.Models.Users;
using Microsoft.AspNetCore.Identity;

namespace HotelListing.API.Contracts
{
    public interface IAuthManager
    {
        /// <summary
        /// Initally the return type was Task<bool>. So we would just get a true or false
        /// depending on whether or not the registration was successful, but this 
        /// was change to return the error or list of errors to give detaials about 
        /// why the registration failed.
        /// 
        /// IdentityError is a class that was known to the instructor of this Udemy
        /// course. Apparently it is just built in and he just "knew" about it.
        /// </summary>
        /// <param name="userDto"></param>
        /// <returns></returns>
        Task<IEnumerable<IdentityError>> Register(ApiUserDto userDto);

        Task<IEnumerable<IdentityError>> RegisterAdminUser(ApiUserDto userDto);

        Task<AuthResponseDto> Login(LoginDto LoginDto);

        Task<string> CreateRefreshToken();

        Task<AuthResponseDto> VerifyRefreshToken(AuthResponseDto request);

    }
}
