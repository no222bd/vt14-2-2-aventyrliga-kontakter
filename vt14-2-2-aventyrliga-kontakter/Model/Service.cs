using System;
using System.Collections.Generic;
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

        public IEnumerable<Contact> GetContacts()
        {
            return ContactDAL.GetContacts();
        }
    }
}