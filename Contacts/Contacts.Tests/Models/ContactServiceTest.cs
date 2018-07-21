using System;
using Contact.Models;
using Contact.Models.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Web.Http.ModelBinding;

namespace Contacts.Tests.Models
{
    [TestClass]
    public class ContactServiceTest
    {
        private Mock<IContactRepositiory> _mockRepository;
        private ModelStateDictionary _modelState;
        private IContactService _service;

        [TestInitialize]
        public void Initialize()
        {
            _mockRepository = new Mock<IContactRepositiory>();
            _modelState = new ModelStateDictionary();
            _service = new ContactService(new ModelStateWrapper(_modelState), _mockRepository.Object);
        }

        [TestMethod]
        public void SaveContactDetails()
        {
            // Arrange
            int ID = 0;
            var contact = new Contact.Models.Contacts();
            contact.ContactID = 1;
            contact.FirstName = "Mini";
            contact.LastName = "Thomas";
            contact.PhoneNumber = "1234567890";
            contact.Email="mini@test.com";

            // Act
            var result = _service.SaveContactDetails(ID, contact);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void SaveContactRequiredFirstName()
        {
            // Arrange
            int ID = 0;
            var contact = new Contact.Models.Contacts();
            contact.ContactID = 1;
            contact.FirstName = string.Empty;
            contact.LastName = "Thomas";
            contact.PhoneNumber = "1234567890";
            contact.Email = "mini@test.com";

            // Act
            var result = _service.SaveContactDetails(ID,contact);

            // Assert
            Assert.IsFalse(result);
            var error = _modelState["FirstName"].Errors[0];
            Assert.AreEqual("First name is required.", error.ErrorMessage);
        }

        [TestMethod]
        public void SaveContactRequiredLastName()
        {
            // Arrange
            int ID = 0;
            var contact = new Contact.Models.Contacts();
            contact.ContactID = 1;
            contact.FirstName = "Mini";
            contact.LastName = string.Empty;
            contact.PhoneNumber = "1234567890";
            contact.Email = "mini@test.com";

            // Act
            var result = _service.SaveContactDetails(ID, contact);

            // Assert
            Assert.IsFalse(result);
            var error = _modelState["LastName"].Errors[0];
            Assert.AreEqual("Last name is required.", error.ErrorMessage);
        }

        [TestMethod]
        public void SaveContactInvalidPhone()
        {
            // Arrange
            int ID = 0;
            var contact = new Contact.Models.Contacts();
            contact.ContactID = -1;
            contact.FirstName = "Mini";
            contact.LastName = "Thomas";
            contact.PhoneNumber = "test";
            contact.Email = "mini@test.com";

            // Act
            var result = _service.SaveContactDetails(ID, contact);

            // Assert
            Assert.IsFalse(result);
            var error = _modelState["PhoneNumber"].Errors[0];
            Assert.AreEqual("Invalid phone number.", error.ErrorMessage);
        }

        [TestMethod]
        public void SaveContactInvalidEmail()
        {
            // Arrange
            int ID = 0;
            var contact = new Contact.Models.Contacts();
            contact.ContactID = 1;
            contact.FirstName = "Mini";
            contact.LastName = "Thomas";
            contact.PhoneNumber = "1234567890";
            contact.Email = "test";

            // Act
            var result = _service.SaveContactDetails(ID, contact);

            // Assert
            Assert.IsFalse(result);
            var error = _modelState["Email"].Errors[0];
            Assert.AreEqual("Invalid email address.", error.ErrorMessage);
        }
    }
}
