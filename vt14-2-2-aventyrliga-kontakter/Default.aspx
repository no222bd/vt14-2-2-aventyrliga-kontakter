<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="vt14_2_2_aventyrliga_kontakter.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        
    <div>
        <%--Test--%>
        <asp:ListView ID="ContactListView" runat="server"
            ItemType ="vt14_2_2_aventyrliga_kontakter.Model.Contact"
            SelectMethod ="ContactListView_GetData"
            DataKeyNames="ContactID">
            
            <LayoutTemplate>
                <table>
                    <tr>
                        <th>Förnamn</th>
                        <th>Efternamn</th>
                        <th>Epostadress</th>
                    </tr>
                    <asp:PlaceHolder ID="itemPlaceHolder" runat="server"></asp:PlaceHolder>
                </table>
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
                </tr>
            </ItemTemplate>
            <EmptyDataTemplate>
                <p>Uppgifter saknas</p>
            </EmptyDataTemplate>
        
        </asp:ListView>
    </div>
    </form>
</body>
</html>
