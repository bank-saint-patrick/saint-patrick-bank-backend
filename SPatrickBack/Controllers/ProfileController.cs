using SPatrickBack.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IO;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace SPatrickBack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfileController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IConfiguration _configuration;

        public ProfileController(UserManager<ApplicationUser> userManager, IConfiguration configuration)
        {
            this.userManager = userManager;
            _configuration = configuration;
        }

        [Authorize]
        [HttpPost]
        [Route("UserUpdate")]
        public async Task<IActionResult> UserUpdate([FromBody] UserUpdateModel model)
        {
            var userExists = await userManager.FindByEmailAsync(model.Email);
            
            if (userExists == null)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User not exists!" });

            ApplicationUser user = new ApplicationUser();
          
            user = userExists;
            //user.UserName = model.Username;
            user.Email = model.Email;
            user.PhoneNumber = model.PhoneNumber;
            
            var result = await userManager.UpdateAsync(user);
            if (!result.Succeeded)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User update failed! Please check user details and try again." });

            return Ok(new Response { Status = "Success", Message = "User update successfully!" });
        }

       
        //[HttpPost]
        //[Route("login")]//username=email
        //public async Task<IActionResult> Login([FromBody] LoginModel model)
        //{
        //    var user = await userManager.FindByEmailAsync(model.Email);
        //    if (user != null && await userManager.CheckPasswordAsync(user, model.Password))
        //    {
        //        var userRoles = await userManager.GetRolesAsync(user);

        //        var authClaims = new List<Claim>
        //        {
        //            new Claim(ClaimTypes.Name, user.Email),
        //            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        //        };

        //        foreach (var userRole in userRoles)
        //        {
        //            authClaims.Add(new Claim(ClaimTypes.Role, userRole));
        //        }

        //        var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

        //        var token = new JwtSecurityToken(
        //            issuer: _configuration["JWT:ValidIssuer"],
        //            audience: _configuration["JWT:ValidAudience"],
        //            //expires: DateTime.Now.AddHours(3),
        //            expires: DateTime.Now.AddMinutes(1),
        //            claims: authClaims,
        //            signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
        //            );

        //        return Ok(new
        //        {
        //            token = new JwtSecurityTokenHandler().WriteToken(token),
        //            expiration = token.ValidTo
        //        });
        //    }
        //    return Unauthorized();
        //}

        //[HttpPost]
        //[Route("login2")]
        //public async Task<IActionResult> Login2([FromBody] LoginModel model)
        //{
        //    var user = await userManager.FindByNameAsync(model.Email);
        //    if (user != null && await userManager.CheckPasswordAsync(user, model.Password))
        //    {
        //        var userRoles = await userManager.GetRolesAsync(user);

        //        var authClaims = new List<Claim>
        //        {
        //            new Claim(ClaimTypes.Name, user.UserName),
        //            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        //        };

        //        foreach (var userRole in userRoles)
        //        {
        //            authClaims.Add(new Claim(ClaimTypes.Role, userRole));
        //        }

        //        var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

        //        var token = new JwtSecurityToken(
        //            issuer: _configuration["JWT:ValidIssuer"],
        //            audience: _configuration["JWT:ValidAudience"],
        //            expires: DateTime.Now.AddHours(3),
        //            claims: authClaims,
        //            signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
        //            );

        //        return Ok(new
        //        {
        //            token = new JwtSecurityTokenHandler().WriteToken(token),
        //            expiration = token.ValidTo
        //        });
        //    }
        //    return Unauthorized();
        //}

        //[Authorize]
        //[HttpPost]
        //[Route("PUpdate")]
        //public async Task<IActionResult> PUpdate([FromBody] PasswordUpdateModel model)
        //{
        //    var userExists = await userManager.FindByEmailAsync(model.Email);
        //    if (userExists == null)
        //        return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User no exist!" });

        //    var result = await userManager.ChangePasswordAsync(userExists, model.currentPassword, model.newPassword);
        //    if (!result.Succeeded)
        //        return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User Update password failed! Please check user details and try again." });

        //    return Ok(new Response { Status = "Success", Message = "User Password update successfully!" });
        //}

    }
}
