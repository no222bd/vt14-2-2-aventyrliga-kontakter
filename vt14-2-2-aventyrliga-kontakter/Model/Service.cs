using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using vt14_2_2_aventyrliga_kontakter.Model.DAL;

namespace vt14_2_2_aventyrliga_kontakter.Model
{
    public class Service
    {
        private ContactDAL _contactDAL;

        private ContactDAL ContactDAL
        {
            get { return _contactDAL ?? (_contactDAL = new ContactDAL()); }
        }

        // SELECT
        public IEnumerable<Contact> GetContacts()
        {
            return ContactDAL.GetContacts();
        }
        
        public Contact GetContact(int contactId)
        {
            return ContactDAL.GetContactById(contactId);
        }

        public IEnumerable<Contact> GetContactsPageWise(int maximumRows, int startPageIndex, out int totalRowCount)
        {
            return ContactDAL.GetContactsPageWise(maximumRows, startPageIndex, out totalRowCount);
        }
        
        // INSERT & UPDATE
        public void SaveContact(Contact contact)
        {
            var validationContext = new ValidationContext(contact);
            var validationResults = new List<ValidationResult>();
            
            // Check if data passes objectvalidation
            if (!Validator.TryValidateObject(contact, validationContext, validationResults, true))
            {
                var ex = new ValidationException("Inmatade värden uppfyllder ej valideringskraven");
                ex.Data.Add("ValidationResults", validationResults);
                throw ex;
            }

            if(contact.ContactID == 0)
                ContactDAL.InsertContact(contact);
            else
                ContactDAL.UpdateContact(contact);
        }

        // DELETE
        public void DeleteContact(int contactId)
        {
            ContactDAL.DeleteContact(contactId);
        }
    }
}