using SPatrickBack.Authentication;
using SPatrickBack.Controllers;
using SPatrickBack.Model;
using System.Collections.Generic;
using System.Linq;

namespace SPatrickBack.Business
{
    public class OperationLogBusiness
    {
        private readonly ApplicationDbContext _context;
        public OperationLogBusiness(ApplicationDbContext context)
        {
            _context = context;
        }

        public void LogsDeposit(int ProductId, int value)
        {
            OperationLog OpIngreso = new OperationLog();
            OpIngreso.OperationProductID = ProductId;
            OpIngreso.OperationFunction = "Ingreso";
            OpIngreso.OperationValue = value;
            _context.OperationsLogs.Add(OpIngreso);
            _context.SaveChanges();
        }
        public void LogsWithdraw(int ProductId, int value)
        {
            OperationLog OpRetiro = new OperationLog();
            OpRetiro.OperationProductID = ProductId;
            OpRetiro.OperationFunction = "Retiro";
            OpRetiro.OperationValue = value;
            _context.OperationsLogs.Add(OpRetiro);
            _context.SaveChanges();
        }

        public List<OperationLog> GetLogsByProduct(int ProdId)
        {
            var LogsProduct = _context.OperationsLogs.Where(z => z.OperationProductID.Equals(ProdId)).ToList();

            return LogsProduct;
        }

    }
}
