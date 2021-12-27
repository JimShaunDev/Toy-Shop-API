using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Web.Http.ModelBinding;
using ToyShopAPI.Models;

namespace ToyShopAPI.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly UserManager<IdentityUser> userManager;

        public AuthController(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel userDetails)
        {
            if (!ModelState.IsValid || userDetails == null)
            {
                return new BadRequestObjectResult(new { Message = "User Registration Failed" });
            }

            var identityUser = new UserModel() { UserName = userDetails.Email, Email = userDetails.Email, FirstName = userDetails.FirstName, LastName = userDetails.LastName  };
            var result = await userManager.CreateAsync(identityUser, userDetails.Password);
            if (!result.Succeeded)
            {
                var dictionary = new ModelStateDictionary();
                foreach (IdentityError error in result.Errors)
                {
                    dictionary.AddModelError(error.Code, error.Description);
                }

                return new BadRequestObjectResult(new { Message = "User Registration Failed", Errors = dictionary });
            }

            return Ok(new { Message = "User Reigstration Successful" });
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] AuthenticateRequestModel credentials)
        {
            
            //https://thecodeblogger.com/2020/01/25/securing-net-core-3-api-with-cookie-authentication/

             if (!ModelState.IsValid || credentials == null)
             {
                 return new BadRequestObjectResult(new { Message = "Login failed" });
             }

             var identityUser = await userManager.FindByNameAsync(credentials.Email);
             if (identityUser == null)
             {
                 return new BadRequestObjectResult(new { Message = "Login failed" });
             }

             var result = userManager.PasswordHasher.VerifyHashedPassword(identityUser, identityUser.PasswordHash, credentials.Password);
             if (result == PasswordVerificationResult.Failed)
             {
                 return new BadRequestObjectResult(new { Message = "Login failed" });
             }

             /*
             var claims = new List<Claim>
             {
                 new Claim(ClaimTypes.Email, identityUser.Email),
                 new Claim(ClaimTypes.Name, identityUser.UserName)
             };

             var claimsIdentity = new ClaimsIdentity(
                 claims);

            await HttpContext.SignInAsync(
                new ClaimsPrincipal(claimsIdentity));
             */

             return Ok(new { Message = "You are logged in" });
            
        }

        [HttpPost]
        [Route("Logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return Ok(new { Message = "You are logged out" });
        }

    }
}

