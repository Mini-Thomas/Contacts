using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace Contact.Models
{
    public class ContactService: IContactService
    {
        private IValidationDictionary _validationDictionary;
        private IContactRepositiory _repository;

        public ContactService(IValidationDictionary validationDictionary) 
            : this(validationDictionary, new ContactRepository())
        { }

        public ContactService(IValidationDictionary validationDictionary, IContactRepositiory repository)
        {
            _validationDictionary = validationDictionary;
            _repository = repository;
        }

        public bool ValidateContact(Contacts contactToValidate)
        {
            if (contactToValidate.FirstName.Trim().Length == 0)
                _validationDictionary.AddError("FirstName", "First name is required.");
            if (contactToValidate.LastName.Trim().Length == 0)
                _validationDictionary.AddError("LastName", "Last name is required.");
            if (contactToValidate.PhoneNumber.Length > 0 && !Regex.IsMatch(contactToValidate.PhoneNumber, @"^[\+]?[(]?[0-9]{3}[)]?[-\s\.]?[0-9]{3}[-\s\.]?[0-9]{4,6}$"))
                _validationDictionary.AddError("PhoneNumber", "Invalid phone number.");
            if (contactToValidate.Email.Length > 0 && !Regex.IsMatch(contactToValidate.Email, @"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$"))
                _validationDictionary.AddError("Email", "Invalid email address.");
            return _validationDictionary.IsValid;
        }

        public bool SaveContactDetails(int ID, Contacts contactToCreate)
        {
            // Validation logic
            if (!ValidateContact(contactToCreate))
                return false;

            // Database logic
            try
            {
                _repository.SaveContactDetails(ID,contactToCreate);
            }
            catch
            {
                return false;
            }
            return true;
        }
        public bool DeleteContactDetails(int ID)
        {
            try
            {
                _repository.DeleteContactDetails(ID);
            }
            catch
            {
                return false;
            }
            return true;
        }

        public List<Contacts> GetContacts()
        {
            return _repository.GetContacts();
        }
        public List<Contacts> GetContactsByID(int ID)
        {
            return _repository.GetContactsByID(ID);
        }
    }
}