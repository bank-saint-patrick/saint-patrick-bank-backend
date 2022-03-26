using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using SPatrickBack.Authentication;
using SPatrickBack.Model;
using System.Collections.Generic;
using System.Linq;

namespace SPatrickBack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionTypeController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public TransactionTypeController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/<TransactionTypeController>
        [HttpGet]
        public IEnumerable<TransactionType> Get()
        {
            var TransacType = _context.TransactionTypes;
            return TransacType;
        }

        // GET api/<TransactionTypeController>/5
        [HttpGet("{id}")]
        public TransactionType Get(int id)
        {
            var Transactionype = _context.TransactionTypes.Find(id);
            return Transactionype;
        }

        // POST api/<TransactionTypeController>
        [HttpPost]
        public IActionResult CreateTransactionType([FromBody] TransactionTypeRequire TransacT)
        {
            var TransacTypeExists = _context.TransactionTypes.Where(x => x.nameTransaction.Equals(TransacT.nameTransaction)).FirstOrDefault();

            if (TransacTypeExists != null)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { 
                    Status = "Error", Message = "El tipo de Transacción ya existe!" });

            try
            {
                TransactionType PT = new TransactionType();
                PT.nameTransaction = TransacT.nameTransaction;
                _context.TransactionTypes.Add(PT);
                _context.SaveChanges();
                return Ok(new Response { 
                    Status = "Success", Message = "Tipo de Transacción agregado!" });
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response
                {
                    Status = "Error",
                    Message = "Agregar Tipo de Transacción fallo! Por favor revise los detalles e intente de nuevo."
                });
            }
        }



        // PUT api/<TransactionTypeController>/5
        [HttpPut("{id}")]
        public IActionResult UpdateTransactionType(int id, [FromBody] TransactionTypeRequire TransacT)
        {
            var TypeExists = _context.TransactionTypes.Where(x => x.transactionTypeID.Equals(id)).FirstOrDefault();

            if (TypeExists == null)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { 
                    Status = "Error", Message = "EL tipo de Transacción no existe!" });

            try
            {
                TypeExists.nameTransaction = TransacT.nameTransaction;
                _context.TransactionTypes.Update(TypeExists);
                _context.SaveChanges();
                return Ok(new Response { Status = "Success", Message = "Tipo de Transacción actualizado!" });
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response
                {
                    Status = "Error",
                    Message = "Actualizar Tipo de Transacción fallo! Por favor revise los detalles e intente de nuevo."
                });
            }
        }



    }
}
