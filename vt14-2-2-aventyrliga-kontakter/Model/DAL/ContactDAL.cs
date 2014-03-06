using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace vt14_2_2_aventyrliga_kontakter.Model.DAL
{
    public class ContactDAL
    {
        public IEnumerable<Contact> GetContacts()
        {
            string connectionString = WebConfigurationManager.ConnectionStrings["1dv406_AdventureWorksAssignmentConnectionString"].ConnectionString;

            using (var conn = new SqlConnection(connectionString))
            {
                var contacts = new List<Contact>(100);

                var cmd = new SqlCommand("Person.uspGetContacts", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                conn.Open();

                using (var reader = cmd.ExecuteReader())
                {
                    var contactIdIndex = reader.GetOrdinal("ContactId");
                    var firstnameIndex = reader.GetOrdinal("FirstName");
                    var lastnameIndex = reader.GetOrdinal("LastName");
                    var emailaddressIndex = reader.GetOrdinal("EmailAddress");

                    while (reader.Read())
                    {
                        contacts.Add(new Contact
                        {
                            ContactID = reader.GetInt32(contactIdIndex),
                            FirstName = reader.GetString(firstnameIndex),
                            LastName = reader.GetString(lastnameIndex),
                            EmailAddress = reader.GetString(emailaddressIndex)
                        });
                    }
                }

                contacts.TrimExcess();

                return contacts;
            }
        }
    }
}