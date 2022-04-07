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
using SPatrickBack.ModelRequire;

namespace SPatrickBack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfileController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> userManager;


        public ProfileController(UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
        }

        [Authorize]
        [HttpGet]
        [Route("GetUser")]
        public async Task<IActionResult> GetUser()
        {
            var currentUser = userManager.GetUserName(HttpContext.User);
            var userExists = await userManager.FindByNameAsync(currentUser);

            if (userExists == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response
                {
                    Status = "Error",
                    Message = "Usuario no existe!"
                });
            }
            else
            {
                ProfileRequire PR = new ProfileRequire();
                PR.FirstName = userExists.FirstName;
                PR.LastName = userExists.FirstName;
                PR.PhoneNumber = userExists.PhoneNumber;
                PR.Email = userExists.Email;
                PR.Dni = userExists.UserName;
                return Ok(PR);
            }
        }


        [Authorize]
        [HttpPost]
        [Route("UserUpdate")]
        public async Task<IActionResult> UserUpdate([FromBody] UserUpdateModel model)
        {
            var currentUser = userManager.GetUserName(HttpContext.User);
            var userExists = await userManager.FindByNameAsync(currentUser);

            if (userExists == null)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response
                {
                    Status = "Error",
                    Message = "Usuario no existe!"
                });

            ApplicationUser user = new ApplicationUser();

            user = userExists;
            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.Email = model.Email;
            user.PhoneNumber = model.PhoneNumber;

            var result = await userManager.UpdateAsync(user);
            if (!result.Succeeded)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response
                {
                    Status = "Error",
                    Message = "ACtualizacion de usuario fallida! VErifique datos y vuelvalo a intentar."
                });

            return Ok(new Response { Status = "Success", Message = "Usuario Actualizado Satisfactoriamente!" });
        }

        [Authorize]
        [HttpPost]
        [Route("PassUpdate")]
        public async Task<IActionResult> PUpdate([FromBody] PasswordUpdateModel model)
        {
            var currentUser = userManager.GetUserName(HttpContext.User);
            var userExists = await userManager.FindByNameAsync(currentUser);
            if (userExists == null)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response
                {
                    Status = "Error",
                    Message = "Usuario no existe!"
                });

            var result = await userManager.ChangePasswordAsync(userExists, model.currentPassword, model.newPassword);
            if (!result.Succeeded)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response
                {
                    Status = "Error",
                    Message = "Actualizacion de Password fallida! Verifique los detalles e intentelo de nuevo."
                });

            return Ok(new Response { Status = "Success", Message = "Password actualizado satisfactoriamente!" });
        }

        //[HttpPost("tokens/cancel")]
        //public async Task<IActionResult> CancelAccessToken()
        //{
        //    return NoContent();
        //}

    }
}
