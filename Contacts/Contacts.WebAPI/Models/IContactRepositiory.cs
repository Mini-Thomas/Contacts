using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contact.Models
{
    public interface IContactRepositiory
    {
        List<Contacts> GetContacts();
        List<Contacts> GetContactsByID(int ID);
        void SaveContactDetails(int ID, Contacts contactDetails);
        void DeleteContactDetails(int ID);
    }
}
