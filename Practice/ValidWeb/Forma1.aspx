<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Forma1.aspx.cs" Inherits="ValidWeb.Forma1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
				<asp:Label ID="Label5" runat="server" Text="Konkurso dalyvio registracija:"></asp:Label>
				<br />
				<asp:ValidationSummary ID="ValidationSummary1" runat="server" ForeColor="Red" />
				<br />
        <asp:Label ID="Label1" runat="server" Text="Vardas:"></asp:Label>
        <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="TextBox1" ErrorMessage="Vardas yra privalomas" ForeColor="Red"></asp:RequiredFieldValidator>
                <br />
                <br />
        <asp:Label ID="Label6" runat="server" Text="Pavardė:"></asp:Label>
        <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="TextBox2" ErrorMessage="Pavardė yra privaloma" ForeColor="Red"></asp:RequiredFieldValidator>
                <br />
                <br />
        <asp:Label ID="Label7" runat="server" Text="Mokykla:"></asp:Label>
        <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="TextBox3" ErrorMessage="Mokykla yra privaloma" ForeColor="Red"></asp:RequiredFieldValidator>
        <br />
				<br />
        <asp:Label ID="Label2" runat="server" Text="Amžius:"></asp:Label>
        <asp:DropDownList ID="DropDownList1" runat="server">
        </asp:DropDownList>
				<asp:RangeValidator ID="RangeValidator1" runat="server" ControlToValidate="DropDownList1" ErrorMessage="Neteisinga metų reikšmė" ForeColor="Red" MaximumValue="25" MinimumValue="14" Type="Integer"></asp:RangeValidator>
				<br />
        <br />
        <asp:Label ID="Label3" runat="server" Text="Programavimo kalba:"></asp:Label>
        <br />
                <asp:CheckBoxList ID="CheckBoxList1" runat="server" DataSourceID="Kalbos" DataTextField="title" DataValueField="title">
                </asp:CheckBoxList>
                <asp:XmlDataSource ID="Kalbos" runat="server" DataFile="~/App_Data/kalbos.xml"></asp:XmlDataSource>
                <br />
                <asp:Button ID="Button1" runat="server" Text="Registruotis" OnClick="Button1_Click" />
                <br />
                <br />
                <asp:Button ID="Button2" runat="server" Text="Išvalyti" OnClick="Button2_Click" />
                <br />
      <br />
                <asp:Table ID="Table1" runat="server" BackColor="#FFFFCC" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" GridLines="Both">
                </asp:Table>
				
    </form>
</body>
</html>
