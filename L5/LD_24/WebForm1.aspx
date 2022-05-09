<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="LD_24.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="~/Styles/main.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <asp:Label ID="ErrorLabel" runat="server" ForeColor="Red"></asp:Label>
        <br />
        <br />
        Norimas mėnuo:<br />
        <asp:TextBox ID="TextBox1" runat="server" TextMode="Number"></asp:TextBox>
        <br />
        <br />
        <asp:Button ID="Button1" runat="server" Text="Skaičiuoti pajamas" OnClick="Button1_Click" />
        <br />
        <br />
        <asp:Button ID="Button2" runat="server" Text="Vykdyti visa kita" OnClick="Button2_Click" />
        <br />
        <br />
        <div ID="ResultsDiv" runat="server"></div>
    </form>
</body>
</html>
