using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using vt14_2_2_aventyrliga_kontakter.Model;

namespace vt14_2_2_aventyrliga_kontakter
{
    public partial class Default : System.Web.UI.Page
    {
        private Service _service;
        
        private Service Service
        {
            get { return _service ?? (_service = new Service()); }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            // Show message after INSERT
            if(Session["Post"] as bool? == true)
                ShowMessage("lagts till");
            Session.Remove("Post");
        }

        // SELECT
        public IEnumerable<vt14_2_2_aventyrliga_kontakter.Model.Contact> ContactListView_GetData()
        {
            return Service.GetContacts();
        }
    
        // SELECT PAGE
        public IEnumerable<vt14_2_2_aventyrliga_kontakter.Model.Contact> ContactListView_GetDataPageWise(int maximumRows, int startRowIndex, out int totalRowCount)
        {
            var startPageIndex = startRowIndex/15+1;
            
            return Service.GetContactsPageWise(maximumRows, startPageIndex, out totalRowCount);
        }

        // UPDATE
        public void ContactListView_UpdateItem(int ContactID)
        {
            // Check Validationcontrols on server
            if (IsValid)
            {
                try
                {
                    var contact = Service.GetContact(ContactID);

                    if (contact == null)
                    {
                        ModelState.AddModelError(String.Empty, String.Format("Kontakt med ID {0} fanns ej", ContactID));

                        return;
                    }

                    // Check if new data passes validation
                    if (TryUpdateModel(contact))
                    {
                        Service.SaveContact(contact);
                        ShowMessage("uppdaterats");
                    }
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(String.Empty, ex.Message);
                }
            }
        }

        // DELETE
        public void ContactListView_DeleteItem(int ContactID)
        {
            try
            {
                Service.DeleteContact(ContactID);
                ShowMessage("tagits bort");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(String.Empty, ex.Message);
            }
        }

        // Add JavaScript confirmation for delete link
        protected void ContactListView_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            var contact = (Contact)e.Item.DataItem;
            LinkButton DeleteLinkButton = (LinkButton)e.Item.FindControl("DeleteLinkButton");
            if (DeleteLinkButton != null)
            {
                DeleteLinkButton.OnClientClick = string.Format("return ConfirmDeletion('{0} {1}');", contact.FirstName, contact.LastName);
            }
        }

        // Show custom successmessage after INSERT, UPDATE & DELETE
        private void ShowMessage(string message)
        {
            MessageLiteral.Text = message;
            MessagePanel.Visible = true;
            CloseButton.OnClientClick = "return closeMessage();";
        }

        protected void NewContactButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/InsertContact.aspx");
        }

    }
}