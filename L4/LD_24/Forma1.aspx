<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Forma1.aspx.cs" Inherits="LD_24.Forma1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="~/Styles/main.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Label ID="Label3" runat="server" Text="Minimalus herojaus intelekto kiekis:"></asp:Label>
            <br />
            <asp:TextBox ID="TextBox1" runat="server" TextMode="Number"></asp:TextBox>
            <br />
            <asp:Label ID="Label4" runat="server" Text="Maksimalus NPC žalos kiekis:"></asp:Label>
            <br />
            <asp:TextBox ID="TextBox2" runat="server" TextMode="Number"></asp:TextBox>
            <br />
            <br />
            <asp:Label ID="Label5" runat="server" ForeColor="Red"></asp:Label>
            <br />
            <br />
            <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Vykdyti" />
            <br />
            <br />
            <asp:Label ID="Label1" runat="server" Text="Pradiniai duomenys:"></asp:Label>
            <asp:Table ID="Table1" runat="server">
            </asp:Table>
            <asp:Label ID="Label2" runat="server" Text="Daugiausiai gyvybės taškų pagal klases:"></asp:Label>
            <asp:Table ID="Table2" runat="server">
            </asp:Table>
        </div>
    </form>
</body>
</html>
