<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Forma1.aspx.cs" Inherits="LD_24.Forma1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="~/Styles/main.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        
        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ForeColor="Red" />
        <asp:Label ID="Label9" runat="server" Text="Produktai:"></asp:Label>
        <br />
        <asp:FileUpload ID="FileUpload1" runat="server" />
        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="FileUpload1" Display="Dynamic" ErrorMessage="Privaloma nurodyti produktus" ForeColor="Red"></asp:RequiredFieldValidator>
        <br />
        <asp:Label ID="Label10" runat="server" Text="Pirkėjai:"></asp:Label>
        <br />
        <asp:FileUpload ID="FileUpload2" runat="server" />
        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="FileUpload2" Display="Dynamic" ErrorMessage="Privaloma nurodyti pirkėjus" ForeColor="Red"></asp:RequiredFieldValidator>
        <br />
        <asp:Label ID="Label5" runat="server" Text="n:"></asp:Label>
        <br />
        <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="TextBox1" ErrorMessage="&quot;n&quot; gali būti tiktais teigiamas sveikas skaičius" ForeColor="Red" ValidationExpression="\d+" Display="Dynamic"></asp:RegularExpressionValidator>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="TextBox1" Display="Dynamic" ErrorMessage="Privaloma nurodyti &quot;n&quot;" ForeColor="Red"></asp:RequiredFieldValidator>
        <br />
        <asp:Label ID="Label6" runat="server" Text="k:"></asp:Label>
        <br />
        <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="TextBox2" ErrorMessage="&quot;k&quot; gali būti tiktais teigiamas sveikas skaičius" ForeColor="Red" ValidationExpression="\d+(\.\d+)?" Display="Dynamic"></asp:RegularExpressionValidator>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="TextBox2" Display="Dynamic" ErrorMessage="Privaloma nurodyti &quot;k&quot;" ForeColor="Red"></asp:RequiredFieldValidator>
        <br />
        <br />
        <asp:Button ID="Button1" runat="server" Text="Atrinkti" OnClick="Button1_Click" />
        <div id="ResultsDiv" runat="server">
            <br />
            <hr />
            <br />
            <asp:Label ID="Label1" runat="server" Text="Įtaisai:"></asp:Label>
            <asp:Table ID="Table1" runat="server">
            </asp:Table>
            <asp:Label ID="Label2" runat="server" Text="Pirkėjai:"></asp:Label>
            <asp:Table ID="Table2" runat="server">
            </asp:Table>
            <asp:Label ID="Label8" runat="server" Text="Populiariausi įtaisai:"></asp:Label>
            <asp:Table ID="Table5" runat="server">
            </asp:Table>
            <asp:Label ID="Label4" runat="server" Text="Vienos rūšies pirkėjai:"></asp:Label>
            <asp:Table ID="Table3" runat="server">
            </asp:Table>
            <asp:Label ID="Label7" runat="server" Text="Atrinkti įtaisai:"></asp:Label>
            <asp:Table ID="Table4" runat="server">
            </asp:Table>
        </div>
    </form>
</body>
</html>
