using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using SPatrickBack.Authentication;
using SPatrickBack.Business;
using SPatrickBack.Model;
using System.Collections.Generic;
using System.Linq;

namespace SPatrickBack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IConfiguration _configuration;
        private readonly TransactionsBusiness _transactionsBusiness;

        public TransactionController(ApplicationDbContext context, UserManager<ApplicationUser> userManager,
            IConfiguration configuration, TransactionsBusiness transactionsBusiness)
        {
            _context = context;
            this.userManager = userManager;
            _configuration = configuration;
            _transactionsBusiness = transactionsBusiness;
        }

        // GET: api/<TransactionController>
        [Authorize]
        [HttpGet]
        public IEnumerable<TransactionRequire> Get()
        {
            //var currentUser = userManager.GetUserName(HttpContext.User);
            //var prod = _context.Transactions;
            //return prod;
            return _transactionsBusiness.GetAll();
        }

        // GET api/<TransactionController>/5
        //[Authorize]
        //[HttpGet("{id}")]
        //public TransactionRequire GetById(int id)
        //{
        //    ////var currentUser = userManager.GetUserName(HttpContext.User);
        //    //var producX = _context.Transactions.Where(z => z.transactionID.Equals(id)).FirstOrDefault();

        //    //Transaction Tran = new Transaction();
        //    //Tran.transactionID = producX.transactionID;
        //    //Tran.transactionTypeID = producX.transactionTypeID;
        //    //Tran.productIDOrigin = producX.productIDOrigin;
        //    //Tran.productIDDestination = producX.productIDDestination;
        //    //Tran.transactionValue = producX.transactionValue;
        //    //Tran.transactionDate = producX.transactionDate;

        //    //return Tran;
        //    return _transactionsBusiness.GetTransactionByID(id);
        //}

        [Authorize]
        [HttpGet("{productid}")]
        //[Route ("[action]")]
        public List<TransactionRequire> GetByProducId(int productid)
        {
            ////var currentUser = userManager.GetUserName(HttpContext.User);
            //var producX = _context.Transactions.Where(z => z.productIDOrigin.Equals(id)).ToList();
            //List<TransactionRequire> Ltran = new List<TransactionRequire>();
            //foreach (var item in producX)
            //{
            //    TransactionRequire Tran = new TransactionRequire();
            //    Tran.transactionID = item.transactionID;
            //    Tran.transactionTypeID = item.transactionTypeID;
            //    Tran.transactionTypeName = item.transacType.nameTransaction;
            //    Tran.productIDOrigin = item.productIDOrigin;
            //    Tran.productIDDestination = item.productIDDestination;
            //    Tran.transactionValue = item.transactionValue;
            //    Tran.transactionDate = item.transactionDate;
            //    Ltran.Add(Tran);
            //}
            //return Ltran;
            return _transactionsBusiness.GetTransactionRequireByProductID(productid);

        }

        // POST api/<TransactionController>
        [Authorize]
        [HttpPost]
        public IActionResult CreateTransaction([FromBody] TransactionRequire model)
        {
            //var TypeExists = _context.TransactionTypes.Where(x => x.nameTransaction.Equals(model.)).FirstOrDefault();

            if (!ModelState.IsValid)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response
                {
                    Status = "Error",
                    Message = "Transacción no posible!"
                });
            try
            {
                Transaction Tran = new Transaction();
                Tran.transactionID = (int)model.transactionID;
                Tran.transactionTypeID = model.transactionTypeID;
                Tran.productIDOrigin = (int)model.productIDOrigin;
                Tran.productIDDestination = (int)model.productIDDestination;
                Tran.transactionValue = model.transactionValue;
                Tran.transactionDate = System.DateTime.Now;

                var tranComplete = _transactionsBusiness.MakeTransaction(Tran);

                if (tranComplete.Status == "Success")
                {
                    var Save = _transactionsBusiness.SaveTransaction(Tran);
                    if (Save.Status == "Success")
                    {
                        return Ok(new Response{Status = "Success", Message = "Transacción Exitosa!" }); 
                    }
                    else
                    {
                        return StatusCode(StatusCodes.Status500InternalServerError, new Response
                        {
                            Status = "Success",
                            Message = "Transacción Exitosa! Atender registro ."
                        });
                    }
                }
                return StatusCode(StatusCodes.Status500InternalServerError, tranComplete);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response
                {
                    Status = "Error",
                    Message = "Transacción fallo! Por favor revise los detalles e intente de nuevo."
                });
            }
        }

    }
}
