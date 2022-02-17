<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Forma1.aspx.cs" Inherits="LD_24.Forma1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <asp:Table ID="Table1" runat="server" BackColor="#FFFFCC" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True">
        </asp:Table>
        <br />
        <br />
        <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Skaičiuoti" />
        <br />
        <br />
        <asp:Label ID="Label1" runat="server" Text="Draugų koordinatės:" Visible="False"></asp:Label>
        <asp:BulletedList ID="BulletedList1" runat="server" Visible="False">
        </asp:BulletedList>
        <asp:Label ID="Label2" runat="server" Text="Susitikimas:" Visible="False"></asp:Label>
      <br />
        <asp:Label ID="Label3" runat="server" Visible="False"></asp:Label>
    </form>
</body>
</html>
