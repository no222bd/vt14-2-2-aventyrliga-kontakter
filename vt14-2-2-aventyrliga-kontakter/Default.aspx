<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="vt14_2_2_aventyrliga_kontakter.Default" ViewStateMode="Disabled" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Äventyrliga kontakter</title>
    <link href="Content/style.css" rel="stylesheet" />
</head>
<body>
    <div id="container">

        <h1>Äventyrliga kontakter</h1>

        <form id="form1" runat="server">

            <asp:Panel ID="MessagePanel" runat="server" Visible="false" ViewStateMode="Disabled">
                <p>Kunduppgiften har
                    <asp:Literal ID="MessageLiteral" runat="server" /></p>
                <asp:LinkButton ID="CloseButton" runat="server">[X]</asp:LinkButton>
            </asp:Panel>

            <asp:ValidationSummary ID="ValidationSummary" runat="server" />
            
            <asp:Button ID="NewContactButton" runat="server" Text="Registrera ny kontakt" OnClick="NewContactButton_Click" />

            <asp:ListView ID="ContactListView" runat="server"
                ItemType="vt14_2_2_aventyrliga_kontakter.Model.Contact"
                SelectMethod="ContactListView_GetDataPageWise"
                UpdateMethod="ContactListView_UpdateItem"
                DeleteMethod="ContactListView_DeleteItem"
                DataKeyNames="ContactID"
                OnItemDataBound="ContactListView_ItemDataBound">

                <LayoutTemplate>
                    <table>
                        <tr>
                            <th>Förnamn</th>
                            <th>Efternamn</th>
                            <th>Epostadress</th>
                            <th></th>
                        </tr>
                        <asp:PlaceHolder ID="itemPlaceHolder" runat="server"></asp:PlaceHolder>
                    </table>
                    <div id="Pager">
                        <asp:DataPager ID="DataPager1" runat="server" PageSize="15">
                            <Fields>
                                <asp:NextPreviousPagerField ShowFirstPageButton="True" ShowLastPageButton="False" FirstPageText="Första" ShowNextPageButton="False" ShowPreviousPageButton="False" />
                                <asp:NumericPagerField ButtonCount="25" />
                                <asp:NextPreviousPagerField ShowLastPageButton="True" LastPageText="Sista" ShowNextPageButton="False" ShowPreviousPageButton="False" />
                            </Fields>
                        </asp:DataPager>
                    </div>
                </LayoutTemplate>
                <ItemTemplate>
                    <tr>
                        <td>
                            <asp:Label ID="FirstNameLabel" runat="server" Text='<%#: Item.FirstName %>'></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="LastNameLabel" runat="server" Text='<%#: Item.LastName %>'></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="EmailLabel" runat="server" Text='<%#: Item.EmailAddress %>'></asp:Label>
                        </td>
                        <td>
                            <asp:LinkButton ID="DeleteLinkButton" runat="server" CommandName="Delete" Text="Ta bort" CausesValidation="false" />
                            <asp:LinkButton ID="EditLinkButton" runat="server" CommandName="Edit" Text="Redigera" CausesValidation="false" />
                        </td>
                    </tr>
                </ItemTemplate>
                <EmptyDataTemplate>
                    <p>Uppgifter saknas</p>
                </EmptyDataTemplate>
                <EditItemTemplate>
                    <tr>
                        <td>
                            <asp:TextBox ID="FirstName" runat="server" Text='<%# BindItem.FirstName %>' MaxLength="50" />
                            <asp:RequiredFieldValidator ID="FirstNameRequiredFieldValidator" runat="server" ErrorMessage="Ett förnamn måste anges" ControlToValidate="FirstName" Display="None" />
                        </td>
                        <td>
                            <asp:TextBox ID="LastName" runat="server" Text='<%# BindItem.LastName %>' MaxLength="50" />
                            <asp:RequiredFieldValidator ID="LastNameRequiredFieldValidator" runat="server" ErrorMessage="Ett efternamn måste anges" ControlToValidate="LastName" Display="None" />
                        </td>
                        <td>
                            <asp:TextBox ID="EmailAddress" runat="server" Text='<%# BindItem.EmailAddress %>' MaxLength="50" />
                            <asp:RequiredFieldValidator ID="EmailAddressRequiredFieldValidator" runat="server" ErrorMessage="En e-postadress måste anges" ControlToValidate="EmailAddress" Display="None" />
                            <asp:RegularExpressionValidator ID="EmailAddressRegularExpressionValidator" runat="server" ErrorMessage="E-postadressen har ett ogiltigt format" Display="None" ControlToValidate="EmailAddress" ValidationExpression="^.+@.+\..+$" />
                        </td>
                        <td>
                            <asp:LinkButton ID="UpdateLinkButton" runat="server" CommandName="Update" Text="Spara" />
                            <asp:LinkButton ID="CancelLinkButton" runat="server" CommandName="Cancel" Text="Avbryt" CausesValidation="false" />
                        </td>
                    </tr>
                </EditItemTemplate>
            </asp:ListView>
        </form>
    </div>
    
    <script type="text/javascript">
        function ConfirmDeletion(name) {
            return confirm("Ta bort kontakten '" + name + "' permanent?");
        }

        function closeMessage() {
            var element = document.getElementById("MessagePanel");
            element.parentNode.removeChild(element);
            return false;
        }
    </script>
</body>
</html>
