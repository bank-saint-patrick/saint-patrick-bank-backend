using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using SPatrickBack.Authentication;
using System.Collections.Generic;
using SPatrickBack.Model;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SPatrickBack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductTypeController : ControllerBase
    {

        private readonly ApplicationDbContext _context;
        public ProductTypeController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, IConfiguration configuration)
        {
            _context = context;
        }

        // GET: api/<productTypeController>
        [HttpGet]
        public IEnumerable<ProducType> Get()
        {
            var prodType = _context.ProductTypes;
            return prodType;
        }

        // GET api/<productTypeController>/5
        [HttpGet("{id}")]
        public ProducType Get(int id)
        {
            var producType = _context.ProductTypes.Find(id);
            return producType;
        }

        // POST api/<productTypeController>
        [HttpPost]
        public IActionResult CreateProductType([FromBody] ProductTypeRequire ProdT)
        {
            var TypeExists = _context.ProductTypes.Where(x => x.nameProduct.Equals(ProdT.nameProduct)).FirstOrDefault();
            
            if (TypeExists != null)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "EL tipo de producto ya existe!" });
            
            try
            {
                ProducType PT = new ProducType();
                PT.nameProduct = ProdT.nameProduct;
                _context.ProductTypes.Add(PT);
                _context.SaveChanges();
                return Ok(new Response { Status = "Success", Message = "Tipo de producto agregado!" });
            }
            catch { 
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { 
                    Status = "Error", 
                    Message = "Agregar Tipo de producto fallo! Por favor revise los detalles e intente de nuevo." });
            }
        }



        // PUT api/<productTypeController>/5
        [HttpPut("{id}")]
        public IActionResult UpdateProductType(int id, [FromBody] ProductTypeRequire ProdT)
        {
            var TypeExists = _context.ProductTypes.Where(x => x.ProductTypeID.Equals(id)).FirstOrDefault();

            if (TypeExists == null)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "EL tipo de producto no existe!" });

            try
            {
                TypeExists.nameProduct = ProdT.nameProduct;
                _context.ProductTypes.Update(TypeExists);
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

        
    }
}
