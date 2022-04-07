using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SPatrickBack.Authentication;
using SPatrickBack.Business;
using SPatrickBack.Model;
using System.Collections.Generic;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SPatrickBack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OperationLogController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly OperationLogBusiness _OperationLogBusiness;

        public OperationLogController(UserManager<ApplicationUser> userManager, OperationLogBusiness OperationLogBusiness)
        {
            this.userManager = userManager;
            _OperationLogBusiness = OperationLogBusiness;
        }


        // GET: api/<OperationLog>
        [Authorize]
        [HttpGet]
        [Route ("GetLogsByProduct")]        
        public List<OperationLog> GetLogsProduct(int ProdId)
        {
            //var currentUser = userManager.GetUserName(HttpContext.User);
            return _OperationLogBusiness.GetLogsByProduct(ProdId);
        }

        
    }
}
