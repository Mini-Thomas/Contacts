using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contact.Models
{
    public interface IContactService
    {
        List<Contacts> GetContacts();
        List<Contacts> GetContactsByID(int ID);
        bool SaveContactDetails(int ID, Contacts contactDetails);
        bool DeleteContactDetails(int ID);
    }
}
