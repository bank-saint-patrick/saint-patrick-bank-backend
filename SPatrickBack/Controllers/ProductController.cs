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

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SPatrickBack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ProductBusiness _ProductBusiness;

        public ProductController(ApplicationDbContext context, UserManager<ApplicationUser> userManager,
            IConfiguration configuration, ProductBusiness productBusiness)
        {
            _context = context;
            this.userManager = userManager;
            _ProductBusiness = productBusiness;
        }

        // GET: api/<ProductController>
        [Authorize]
        [HttpGet]
        [Route ("GetAllProductsByUser")]
        public IEnumerable<Product> GetAllProductsByUser()
        {
            var currentUser = userManager.GetUserName(HttpContext.User);
            //var prod = _context.Products.Where(w => w.idUser.Equals(currentUser));
            //return prod;

            return _ProductBusiness.GetProductsByUser(currentUser);
        }

        // GET api/<ProductController>/5
        [Authorize]
        [HttpGet("{productId}")]
        public Product Get(int productId)
        {
            var currentUser = userManager.GetUserName(HttpContext.User);
            //var producX = _context.Products.Where(z => z.idUser.Equals(currentUser) && z.ProductID.Equals(id)).FirstOrDefault();
            ////var ProdExists = _context.Products.Where(x => x.ProductID.Equals(id) && x.idUser.Equals(currentUser)).FirstOrDefault();

            //Product Pro = new Product();
            //Pro.ProductID = producX.ProductID;
            //Pro.ProductTypeID = producX.ProductTypeID;
            //Pro.idUser = "0";
            //Pro.saldoCupo = producX.saldoCupo;
            //Pro.cardNumber = producX.cardNumber;
            //Pro.startDate = producX.startDate;
            //Pro.finishDate = (System.DateTime)producX.finishDate;
            //Pro.state = producX.state;
            //return Pro;
            return _ProductBusiness.GetProductById(currentUser, productId);
        }

        // POST api/<ProductController>
        [Authorize]
        [HttpPost]
        public IActionResult CreateProduct([FromBody] ProductRequire model)
        {
            //var TypeExists = _context.ProductTypes.Where(x => x.nameProduct.Equals(model.)).FirstOrDefault();

            if (!ModelState.IsValid)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "Producto no posible!" });
            try
            {
                Product PT = new Product();
                PT.ProductTypeID = model.ProductTypeId;
                PT.idUser = userManager.GetUserName(HttpContext.User);
                PT.saldoCupo = model.saldoCupo;
                PT.cardNumber = model.cardNumber;
                PT.startDate = model.startDate;
                PT.finishDate = (System.DateTime)model.finishDate;
                PT.state = model.state;
                _context.Products.Add(PT);
                _context.SaveChanges();
                return Ok(new Response { Status = "Success", Message = "Producto agregado!" });
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response
                {
                    Status = "Error",
                    Message = "Agregar producto fallo! Por favor revise los detalles e intente de nuevo."
                });
            }
        }

        // PUT api/<ProductController>/5
        [Authorize]
        [HttpPut("{id}")]
        public IActionResult UpdateProduct(int id, [FromBody] ProductRequire model)
        {
            var currentUser = userManager.GetUserName(HttpContext.User);
            var ProdExists = _context.Products.Where(x => x.ProductID.Equals(id) && x.idUser.Equals(currentUser)).FirstOrDefault();

            if (ProdExists == null)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "El producto no existe!" });

            try
            {
                ProdExists.ProductTypeID = model.ProductTypeId;
                ProdExists.saldoCupo = model.saldoCupo;
                ProdExists.cardNumber = model.cardNumber;
                ProdExists.startDate = model.startDate;
                ProdExists.finishDate = (System.DateTime)model.finishDate;
                ProdExists.state = model.state;
                //ProdExists.Transactions = ;
                _context.Products.Update(ProdExists);
                _context.SaveChanges();
                return Ok(new Response { Status = "Success", Message = "Tipo de producto actualizado!" });
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response
                {
                    Status = "Error",
                    Message = "Actualizar Tipo de producto fallo! Por favor revise los detalles e intente de nuevo."
                });
            }
        }

        // DELETE api/<ProductController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
