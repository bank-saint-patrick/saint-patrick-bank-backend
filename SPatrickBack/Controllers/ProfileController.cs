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

        [Authorize]
        [HttpPost]
        [Route("PassUpdate")]
        public async Task<IActionResult> PUpdate([FromBody] PasswordUpdateModel model)
        {
            var userExists = await userManager.FindByEmailAsync(model.Dni);
            if (userExists == null)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User no exist!" });

            var result = await userManager.ChangePasswordAsync(userExists, model.currentPassword, model.newPassword);
            if (!result.Succeeded)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User Update password failed! Please check user details and try again." });

            return Ok(new Response { Status = "Success", Message = "User Password update successfully!" });
        }

        [HttpPost("tokens/cancel")]
        public async Task<IActionResult> CancelAccessToken()
        {
            return NoContent();
        }

    }
}
