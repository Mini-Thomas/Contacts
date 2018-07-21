using System;
using System.Collections.Generic;
using System.Linq;
using Contact.Models;

using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.OData;
using models = Contact.Models;
using Contact.Models.Validation;

namespace Contact.Controllers
{
    [EnableCorsAttribute("http://localhost:49685", "*", "*")]
    public class ContactsController : ApiController
    {
        private IContactService _service;


        public ContactsController()
        {
            _service = new ContactService(new ModelStateWrapper(this.ModelState));
        }

        public ContactsController(IContactService service)
        {
            _service = service;
        }

        // GET: api/contacts
        [EnableQuery()]
        public IQueryable<models.Contacts> Get()
        {
            return _service.GetContacts().AsQueryable();
        }
        // GET: api/contacts/1
        [EnableQuery()]
        public IQueryable<models.Contacts> Get([FromUri(Name = "id")]int ID)
        {
            return _service.GetContactsByID(ID).AsQueryable();
        }
        // POST api/contacts
        public void Post(models.Contacts contactDetails)
        {
            int ID = 0;
            _service.SaveContactDetails(ID, contactDetails);
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]models.Contacts contactDetails)
        {
            _service.SaveContactDetails(id, contactDetails);
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
            _service.DeleteContactDetails(id);
        }
    }
}
