<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="InsertContact.aspx.cs" Inherits="vt14_2_2_aventyrliga_kontakter.InsertContact" ViewStateMode="Disabled" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Äventyrliga kontakter | Ny kontakt</title>
    <link href="Content/style.css" rel="stylesheet" />
</head>
<body>
    <div id="container">

        <h1>Äventyrliga kontakter</h1>

        <h2>Registrera ny kontakt</h2>

        <form id="form2" runat="server">

            <asp:ValidationSummary ID="ValidationSummary" runat="server" />

            <asp:FormView ID="NewContactFormView" runat="server"
                ItemType="vt14_2_2_aventyrliga_kontakter.Model.Contact"
                DefaultMode="Insert"
                RenderOuterTable="false"
                InsertMethod="FormView_InsertItem"
                OnItemCommand="NewContactFormView_ItemCommand">
                <InsertItemTemplate>

                    <p class="formRow">
                        <span class="inputLabel">Förnamn</span>
                        <asp:TextBox ID="FirstName" runat="server" Text='<%# BindItem.FirstName %>' MaxLength="50" />
                        <asp:RequiredFieldValidator ID="FirstNameRequiredFieldValidator" runat="server" ErrorMessage="Ett förnamn måste anges" ControlToValidate="FirstName" Display="None" />
                    </p>

                    <p class="formRow">
                        <span class="inputLabel">Efternamn</span>
                        <asp:TextBox ID="LastName" runat="server" Text='<%# BindItem.LastName %>' MaxLength="50" />
                        <asp:RequiredFieldValidator ID="LastNameRequiredFieldValidator" runat="server" ErrorMessage="Ett efternamn måste anges" ControlToValidate="LastName" Display="None" />
                    </p>
                    
                    <p class="formRow">
                        <span class="inputLabel">E-postadress</span>
                        <asp:TextBox ID="EmailAddress" runat="server" Text='<%# BindItem.EmailAddress %>' MaxLength="50" />
                        <asp:RequiredFieldValidator ID="EmailAddressRequiredFieldValidator" runat="server" ErrorMessage="En e-postadress måste anges" ControlToValidate="EmailAddress" Display="None" />
                        <asp:RegularExpressionValidator ID="EmailAddressRegularExpressionValidator" runat="server" ErrorMessage="E-postadressen har ett ogiltigt format" Display="None" ControlToValidate="EmailAddress" ValidationExpression="^.+@.+\..+$" />
                    </p>

                    <p class="formRow">
                        <asp:LinkButton ID="InsertLinkButton" runat="server" CommandName="Insert" Text="Lägg till" />
                        <asp:LinkButton ID="CancelLinkButton" runat="server" CommandName="Cancel" Text="Avbryt" CausesValidation="false" />
                    </p>

                </InsertItemTemplate>
            </asp:FormView>
        </form>
    </div>
</body>
</html>
