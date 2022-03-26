using SPatrickBack.Authentication;
using SPatrickBack.Model;
using System.Collections.Generic;
using System.Linq;

namespace SPatrickBack.Business
{
    public class TransactionsBusiness
    {
        private readonly ApplicationDbContext _context;
        public TransactionsBusiness(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<TransactionRequire> GetAll()
        {
            var tranTypeC = _context.TransactionTypes;
            var transac = _context.Transactions;

            List<TT2> LtranType = new List<TT2>();
            foreach (var item in tranTypeC){
                TT2 transactionType = new TT2();
                transactionType.transactionTypeID = item.transactionTypeID;
                transactionType.nameTransaction = item.nameTransaction;
                LtranType.Add(transactionType);
            }

                List<TransactionRequire> Ltran = new List<TransactionRequire>();

            foreach (var item in transac)
            {
                TransactionRequire Tran = new TransactionRequire();
                Tran.transactionID = item.transactionID;
                Tran.transactionTypeID = item.transactionTypeID;
                Tran.transactionTypeName = LtranType.Find(z=> z.transactionTypeID.Equals(item.transactionTypeID)).nameTransaction;
                    //_context.TransactionTypes.Find(item.transactionTypeID).nameTransaction;
                Tran.productIDOrigin = item.productIDOrigin;
                Tran.productIDDestination = item.productIDDestination;
                Tran.transactionValue = item.transactionValue;
                Tran.transactionDate = item.transactionDate;
                Ltran.Add(Tran);
            }
            return Ltran;
        }
        public TransactionRequire GetTransactionByID(int id) 
        {
            var producX = _context.Transactions.Where(z => z.transactionID.Equals(id)).FirstOrDefault();
            var tranTypeC = _context.TransactionTypes;
          
            List<TT2> LtranType = new List<TT2>();
            foreach (var item in tranTypeC)
            {
                TT2 transactionType = new TT2();
                transactionType.transactionTypeID = item.transactionTypeID;
                transactionType.nameTransaction = item.nameTransaction;
                LtranType.Add(transactionType);
            }

            TransactionRequire Tran = new TransactionRequire();
            Tran.transactionID = producX.transactionID;
            Tran.transactionTypeID = producX.transactionTypeID;
            Tran.transactionTypeName = LtranType.Find(z => z.transactionTypeID.Equals(producX.transactionTypeID)).nameTransaction;
            Tran.productIDOrigin = producX.productIDOrigin;
            Tran.productIDDestination = producX.productIDDestination;
            Tran.transactionValue = producX.transactionValue;
            Tran.transactionDate = producX.transactionDate;

            return Tran;
        }
        public List<TransactionRequire> GetTransactionRequireByProductID(int id)
        {
            var producX = _context.Transactions.Where(z => z.productIDOrigin.Equals(id)).ToList();
            var tranTypeC = _context.TransactionTypes;

            List<TT2> LtranType = new List<TT2>();
            foreach (var item in tranTypeC)
            {
                TT2 transactionType = new TT2();
                transactionType.transactionTypeID = item.transactionTypeID;
                transactionType.nameTransaction = item.nameTransaction;
                LtranType.Add(transactionType);
            }

            List<TransactionRequire> Ltran = new List<TransactionRequire>();

            foreach (var item in producX)
            {
                TransactionRequire Tran = new TransactionRequire();
                Tran.transactionID = item.transactionID;
                Tran.transactionTypeID = item.transactionTypeID;
                Tran.transactionTypeName = LtranType.Find(z => z.transactionTypeID.Equals(item.transactionTypeID)).nameTransaction;
                Tran.productIDOrigin = item.productIDOrigin;
                Tran.productIDDestination = item.productIDDestination;
                Tran.transactionValue = item.transactionValue;
                Tran.transactionDate = item.transactionDate;
                Ltran.Add(Tran);
            }
            return Ltran;
        }

        public List<Transaction> GetTransactionByProductID(int id)
        {
            var producX = _context.Transactions.Where(z => z.productIDOrigin.Equals(id)).ToList();
            var tranTypeC = _context.TransactionTypes;

            List<TT2> LtranType = new List<TT2>();
            foreach (var item in tranTypeC)
            {
                TT2 transactionType = new TT2();
                transactionType.transactionTypeID = item.transactionTypeID;
                transactionType.nameTransaction = item.nameTransaction;
                LtranType.Add(transactionType);
            }

            List<Transaction> Ltran = new List<Transaction>();

            foreach (var item in producX)
            {
                Transaction Tran = new Transaction();
                Tran.transactionID = item.transactionID;
                Tran.transactionTypeID = item.transactionTypeID;
                Tran.productIDOrigin = item.productIDOrigin;
                Tran.productIDDestination = item.productIDDestination;
                Tran.transactionValue = item.transactionValue;
                Tran.transactionDate = item.transactionDate;
                Ltran.Add(Tran);
            }
            return Ltran;
        }

        public Response MakeTransaction(Transaction transaction)
        {
            Response result;
            switch ((TransactionType)transaction.transactionTypeID)
            {
                case TransactionType.Transferencia:
                    result = MakeTransfer(transaction.productIDOrigin, transaction.productIDDestination, transaction.transactionValue);
                    break;
                case TransactionType.Retiro:
                    result = MakeWithdraw(transaction.productIDOrigin, transaction.productIDDestination, transaction.transactionValue);
                    break;
                case TransactionType.Consignacion:
                    result = MakeDeposit(transaction.productIDOrigin, transaction.productIDDestination, transaction.transactionValue);
                    break;
                default:
                    result = (new Response{Status = "Error", Message = "Opción no valida"});
                    break;
            }
            return result;            
        }
        public Response SaveTransaction(Transaction transaction)
        {
            try
            {
                _context.Transactions.Add(transaction);
                _context.SaveChanges();
                return (new Response { Status = "Success", Message = " Registro de Transaccion Guardada" });
            }
            catch 
            {
                return (new Response { Status = "Error", Message = "Registro deTransaccion NO Guardada" });
            }

        }
        public Response MakeTransfer(int origin, int destiny, int value)
        {
            var origen = _context.Products.Where(z => z.ProductID.Equals(origin)).FirstOrDefault();
            var destino = _context.Products.Where(r => r.ProductID.Equals(destiny)).FirstOrDefault();

            if (origen != null && destino != null && value > 0)
            {
                if (origen.saldoCupo > value)
                {
                    origen.saldoCupo -= value;
                    destino.saldoCupo += value;
                    try
                    {
                        _context.Products.Update(origen);
                        _context.Products.Update(destino);
                        _context.SaveChanges();
                        return (new Response
                        {
                            Status = "Success",
                            Message = "Trasferencia realizada."
                        });
                    }
                    catch
                    {
                        return (new Response
                        {
                            Status = "Error",
                            Message = ""
                        });
                    }
                }
                else
                {
                    return (new Response
                    {
                        Status = "Error",
                        Message = "Cupo no disponible."
                    });
                }
            }

            return (new Response
            {
                Status = "Error",
                Message = "Cuenta no disponible."
            });

        }
        public Response MakeDeposit(int origin, int destiny, int value)
        {
            var origen = _context.Products.Find(1);  //Cuenta establecida para cajero
            var destino = _context.Products.Where(r => r.ProductID.Equals(destiny)).FirstOrDefault();

            if (destino != null && value > 0)
            {
                destino.saldoCupo += value;
                try
                {
                    _context.Products.Update(destino);
                    _context.SaveChanges();
                    return (new Response
                    {
                        Status = "Success",
                        Message = "Deposito realizada."
                    });
                }
                catch
                {
                    return (new Response
                    {
                        Status = "Error",
                        Message = ""
                    });
                }
            }

            return (new Response
            {
                Status = "Error",
                Message = "Cuenta no disponible."
            });

        }
        public Response MakeWithdraw(int origin, int destiny, int value)
        {
            var origen = _context.Products.Where(r => r.ProductID.Equals(origin)).FirstOrDefault();
            var destino = _context.Products.Find(1);  //Cuenta establecida para cajero

            if (origen != null && value > 0)
            {
                if (origen.saldoCupo > value)
                {
                    origen.saldoCupo -= value;
                    try
                    {
                        _context.Products.Update(origen);
                        _context.SaveChanges();
                        return (new Response
                        {
                            Status = "Success",
                            Message = "Retiro realizado."
                        });
                    }
                    catch
                    {
                        return (new Response
                        {
                            Status = "Error",
                            Message = ""
                        });
                    }
                }
                else
                {
                    return (new Response
                    {
                        Status = "Error",
                        Message = "Cupo no disponible."
                    });
                }

            }

            return (new Response
            {
                Status = "Error",
                Message = "Cuenta no disponible."
            });
        }

    }

    public enum TransactionType
    {
        Transferencia=1,
        Consignacion,
        Retiro

    }

    public class TT2
    {
       public int transactionTypeID { get; set; }
        public string nameTransaction { get; set; }
    }
}
