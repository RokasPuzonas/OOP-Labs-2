<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Forma1.aspx.cs" Inherits="SesijaWeb.Forma1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">

        <asp:Label ID="Label1" runat="server" Text="Naujas įrašas:"></asp:Label>
        <br />
        <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
        
        <br /><br />
        <asp:Button ID="Button1" runat="server" Text="Įvesti" OnClick="Button1_Click" />
        
        <br /><br />
        <asp:Label ID="Label2" runat="server" Text="Egzistuojantys įrašai:"></asp:Label>
        <br />
        <asp:Table ID="Table1" runat="server" BorderColor="Black" BorderWidth="1px">
        </asp:Table>

    </form>
</body>
</html>
