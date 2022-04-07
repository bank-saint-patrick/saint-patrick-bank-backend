using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using SPatrickBack.Authentication;
using SPatrickBack.Business;
using SPatrickBack.Model;
using SPatrickBack.ModelRequire;
using System.Collections.Generic;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SPatrickBack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserContactController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly UserContactBusiness _UserContactBusiness;

        public UserContactController(UserManager<ApplicationUser> userManager, UserContactBusiness userContactBusiness)
        {
            //_context = context;
            this.userManager = userManager;
            _UserContactBusiness = userContactBusiness;
        }

        // GET: api/<UserContactController>
        [Authorize]
        [HttpGet]
        [Route("GetContactByUser")]
        public List<UserContactRequire> GetContactByUser()
        {
            var currentUser = userManager.GetUserName(HttpContext.User);

            var Usercontacts = _UserContactBusiness.GetAllContactsByUserId(currentUser);

            return Usercontacts;
        }

        // POST api/<UserContactController>
        [Authorize]
        [HttpPost]
        public IActionResult SetContactByUser([FromBody] UserContactRequire model)
        {
            var currentUser = userManager.GetUserName(HttpContext.User);
            if (!ModelState.IsValid)
                return BadRequest();
            try
            {
                var contact = _UserContactBusiness.CreateContactsByUserId(currentUser, model);
                if (contact.Status == "Success")
                {
                    return Ok(contact);
                }
                else 
                {
                    return StatusCode(StatusCodes.Status500InternalServerError,
                    new Response { Status = "Error", Message = "Error al crear contacto" });
                }
               
            }
            catch 
            { 
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new Response { Status = "Error", Message = "Asignacion de contacto no posible!" });
            }

        }

        // DELETE api/<UserContactController>/5
        [Authorize]
        [HttpDelete("{id}")]
        public IActionResult DeleteContactByUser(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            try
            {
                var contact = _UserContactBusiness.DeleteContactsByUserId(id);
                if (contact.Status == "Success")
                {
                    return Ok("Contacto Eliminado");
                }
                else 
                {
                    return StatusCode(StatusCodes.Status500InternalServerError,
                    new Response { Status = "Error", Message = "No fue posible eliminar contacto" });
                }
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new Response { Status = "Error", Message = "Asignacion de contacto no posible!" });
            }
        }
    }
}
