using SPatrickBack.Authentication;
using SPatrickBack.Model;
using SPatrickBack.ModelRequire;
using System.Collections.Generic;
using System.Linq;

namespace SPatrickBack.Business
{
    public class UserContactBusiness
    {
        private readonly ApplicationDbContext _context;
        public UserContactBusiness(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<UserContactRequire> GetAllContactsByUserId(string currentUser)
        {
            List<UserContactRequire> LUC = new List<UserContactRequire>();
            if (currentUser == "" || currentUser == null)
            {
                return LUC;
            }
            try
            {
                var contacts = _context.UserContacts.Where(z => z.idUser.Equals(currentUser));
                foreach (var item in contacts)
                {
                    UserContactRequire contact = new UserContactRequire();
                    contact.idUser = item.idUser;
                    contact.ContactName = item.ContactName;
                    contact.ContactProductId = item.ContactProductId;
                    contact.Image = item.Image;
                    contact.UserContactID = item.UserContactID;
                    LUC.Add(contact);
                }
                return LUC;
            }
            catch
            {
                return LUC;
            }
        }

        public Response CreateContactsByUserId(string currentUser, UserContactRequire contact)
        {

            if (currentUser == "" || currentUser == null || contact == null)
            {
                return (new Response
                {
                    Status = "Error",
                    Message = "Modelo no valido"
                });
            }

            try
            {
                UserContact con = new UserContact();
                con.idUser = contact.idUser;
                con.ContactName = contact.ContactName;
                con.ContactProductId = contact.ContactProductId;
                con.Image = contact.Image;
                _context.UserContacts.Add(con);
                _context.SaveChanges();
                return (new Response
                {
                    Status = "Success",
                    Message = "Contacto Creado."
                });
            }
            catch
            {
                return (new Response
                {
                    Status = "Error",
                    Message = "Internal Error"
                });
            }
        }

        public Response DeleteContactsByUserId(int id)
        {
            if (id == 0)
            {
                return (new Response
                {
                    Status = "Error",
                    Message = "Modelo no valido"
                });
            }

            try
            {
                var contact = _context.UserContacts.Find(id);
                if (contact != null)
                {
                    _context.UserContacts.Remove(contact);
                    _context.SaveChanges();
                    return (new Response
                    {
                        Status = "Success",
                        Message = "Contacto Creado."
                    });
                }
                else
                {
                    return (new Response
                    {
                        Status = "Error",
                        Message = "Contacto no encontrado."
                    });
                }
            }
            catch
            {
                return (new Response
                {
                    Status = "Error",
                    Message = "Internal Error"
                });
            }
        }


    }
}
