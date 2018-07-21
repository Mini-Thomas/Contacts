using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Hosting;

using Newtonsoft.Json;


namespace Contact.Models
{
    public class ContactRepository: IContactRepositiory
    {

        List<Contacts> contacts = new List<Contacts>();
        private ContactEntities _entities = new ContactEntities();

        public List<Contacts> GetContacts()
        {
            int ID = 0;
            contacts=FetchContacts(ID);
            return contacts;
        }

        public List<Contacts> GetContactsByID(int ID)
        {
            contacts = FetchContacts(ID);
            return contacts;
        }

        public List<Contacts> FetchContacts(int ID)
        {
            using (var dbCtx = _entities)
            {
                var contactList = dbCtx.spGetContactDetails(ID);
                var contact = from dtls in contactList
                              select new Contacts
                              {
                                  ContactID = dtls.ContactID,
                                  FirstName = dtls.FirstName,
                                  LastName = dtls.LastName,
                                  Email = dtls.Email,
                                  PhoneNumber = dtls.PhoneNumber,
                                  Status = dtls.Status == true ? "Active" : "InActive"
                              };
                contacts = new List<Contacts>(contact.ToList());
            }
            return contacts;
        }

        public void SaveContactDetails(int ID,Contacts contactDetails)
        {
            using (var dbCtx = _entities)
            {
                bool status = contactDetails.Status.ToString() == "Active" ? true : false;
                dbCtx.spSaveContact(ID,contactDetails.FirstName, contactDetails.LastName, contactDetails.PhoneNumber, contactDetails.Email, status, DateTime.Now);
            }
        }

        public void DeleteContactDetails(int ID)
        {
            using (var dbCtx = _entities)
            {
                dbCtx.spDeleteContact(ID, DateTime.Now);
            }
        }
    }
}
