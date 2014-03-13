using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace vt14_2_2_aventyrliga_kontakter.Model.DAL
{
    // Handles connection to database table 'Contact'
    public class ContactDAL:DALBase
    {
        // DELETE
        public void DeleteContact(int contactId)
        {
            using (var conn = CreateConnection())
            {
                try
                {
                    var cmd = new SqlCommand("Person.uspRemoveContact", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@ContactID", SqlDbType.Int, 4).Value = contactId;

                    conn.Open();
                                       
                    cmd.ExecuteNonQuery();
                }
                catch
                {
                    throw new ApplicationException("Ett fel har uppstått i samband med borttagning av kontakt i databasen.");
                }
            }
        }

        // SELECT
        public Contact GetContactById(int contactId)
        {
            using (var conn = CreateConnection())
            {
                try
                {
                    var cmd = new SqlCommand("Person.uspGetContact", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@ContactID", SqlDbType.Int, 4).Value = contactId;

                    conn.Open();

                    using (var reader = cmd.ExecuteReader())
                    {
                        var contactIdIndex = reader.GetOrdinal("ContactId");
                        var firstnameIndex = reader.GetOrdinal("FirstName");
                        var lastnameIndex = reader.GetOrdinal("LastName");
                        var emailaddressIndex = reader.GetOrdinal("EmailAddress");

                        if (reader.Read())
                        {
                            return new Contact
                            {
                                ContactID = reader.GetInt32(contactIdIndex),
                                FirstName = reader.GetString(firstnameIndex),
                                LastName = reader.GetString(lastnameIndex),
                                EmailAddress = reader.GetString(emailaddressIndex)
                            };
                        }
                    }

                    return null;
                }
                catch
                {
                    throw new ApplicationException("Ett fel har uppstått i samband med hämtandet av kontakt i databasen.");
                }
            }
        }

        // SELECT
        public IEnumerable<Contact> GetContacts()
        {
            using (var conn = CreateConnection())
            {
                try
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
                catch
                {
                    throw new ApplicationException("Ett fel har uppstått vid hämtning av kontakter från databasen.");
                }
            }
        }

        // SELECT
        public IEnumerable<Contact> GetContactsPageWise(int maximumRows, int startPageIndex, out int totalRowCount)
        {
            using (var conn = CreateConnection())
            {
                try
                {
                    var contacts = new List<Contact>(100);

                    var cmd = new SqlCommand("Person.uspGetContactsPageWise", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@PageIndex", SqlDbType.Int, 4).Value = startPageIndex;
                    cmd.Parameters.Add("@PageSize", SqlDbType.Int, 4).Value = maximumRows;
                    
                    cmd.Parameters.Add("@RecordCount", SqlDbType.Int, 4).Direction = ParameterDirection.Output;

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

                    totalRowCount = (int)cmd.Parameters["@RecordCount"].Value;

                    contacts.TrimExcess();
                    
                    return contacts;
                }
                catch
                {
                    throw new ApplicationException("Ett fel har uppstått vid hämtning av kontakter från databasen.");
                }
            }
        }   

        // INSERT
        public void InsertContact(Contact contact)
        {
            using (var conn = CreateConnection())
            {
                try
                {
                    var cmd = new SqlCommand("Person.uspAddContact", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@FirstName", SqlDbType.NVarChar, 50).Value = contact.FirstName;
                    cmd.Parameters.Add("@LastName", SqlDbType.NVarChar, 50).Value = contact.LastName;
                    cmd.Parameters.Add("@EmailAddress", SqlDbType.NVarChar, 50).Value = contact.EmailAddress;

                    // Output parameter containing primary key of new 'Contact'
                    cmd.Parameters.Add("@ContactID", SqlDbType.Int, 4).Direction = ParameterDirection.Output;
                    
                    conn.Open();

                    cmd.ExecuteNonQuery();
                }
                catch
                {
                    throw new ApplicationException("Ett fel har uppstått vid skapandet av kontakt i databasen.");
                }
            }    
        }

        // UPDATE
        public void UpdateContact(Contact contact)
        {
            using (var conn = CreateConnection())
            {
                try
                {
                    var cmd = new SqlCommand("Person.uspUpdateContact", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@ContactID", SqlDbType.Int, 4).Value = contact.ContactID;
                    cmd.Parameters.Add("@FirstName", SqlDbType.NVarChar, 50).Value = contact.FirstName;
                    cmd.Parameters.Add("@LastName", SqlDbType.NVarChar, 50).Value = contact.LastName;
                    cmd.Parameters.Add("@EmailAddress", SqlDbType.NVarChar, 50).Value = contact.EmailAddress;

                    conn.Open();

                    cmd.ExecuteNonQuery();
                }
                catch
                {
                    throw new ApplicationException("Ett fel har uppstått vid uppdateringen av kontakt i databasen.");
                }
            }
        }
    }
}