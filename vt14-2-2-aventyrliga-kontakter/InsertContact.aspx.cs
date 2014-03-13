using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using vt14_2_2_aventyrliga_kontakter.Model;


namespace vt14_2_2_aventyrliga_kontakter
{
    public partial class InsertContact : System.Web.UI.Page
    {
        private Service _service;

        private Service Service
        {
            get { return _service ?? (_service = new Service()); }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public void FormView_InsertItem(Contact contact)
        {
            // Check Validationcontrols on server
            if(IsValid)
            {
                // Check if data passes objectvalidation
                if (ModelState.IsValid)
                {
                    try
                    {
                        Service.SaveContact(contact);
                        Session["Post"] = true;

                        // Redirect to prevent dubbelposting
                        Response.Redirect("~/");
                    }
                    catch (Exception ex)
                    {
                        ModelState.AddModelError(String.Empty, ex.Message);
                    }
                }
            }
        }

        // Redirects to default.aspx if Cancelbutton is presssed
        protected void NewContactFormView_ItemCommand(object sender, FormViewCommandEventArgs e)
        {
            if (e.CommandName == "Cancel")
                Response.Redirect("~/");
        }
    }
}