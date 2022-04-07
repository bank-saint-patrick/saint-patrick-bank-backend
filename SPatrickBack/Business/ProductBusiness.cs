using SPatrickBack.Authentication;
using SPatrickBack.Model;
using System.Collections.Generic;
using System.Linq;

namespace SPatrickBack.Business
{
    public class ProductBusiness
    {
        private readonly ApplicationDbContext _context;
        private readonly TransactionsBusiness _transactionsBusiness;
        public ProductBusiness(ApplicationDbContext context, TransactionsBusiness transactionsBusiness)
        {
            _context = context;
            _transactionsBusiness = transactionsBusiness;
        }

        public IEnumerable<Product> GetProductsByUser(string currentUser)
        {
            var prod = _context.Products.Where(w => w.idUser.Equals(currentUser));
            return prod;
        }

        public Product GetProductById(string currentUser, int productId)
        {
            var producX = _context.Products.Where(z => z.idUser.Equals(currentUser) && z.ProductID.Equals(productId)).FirstOrDefault();

            if (producX != null)
            {
                Product Pro = new Product();
                Pro.ProductID = producX.ProductID;
                Pro.ProductTypeID = producX.ProductTypeID;
                Pro.idUser = "0";
                Pro.saldoCupo = producX.saldoCupo;
                Pro.cardNumber = producX.cardNumber;
                Pro.startDate = producX.startDate;
                Pro.finishDate = (System.DateTime)producX.finishDate;
                Pro.state = producX.state;
                Pro.Transactions = _transactionsBusiness.GetTransactionByProductID(producX.ProductID);
                return Pro;
            }

            Product Pro1 = new Product();
            return Pro1;

        }

      



    }
}
