<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Редактирование профилей принтеров.aspx.cs" Inherits="WebApplication1.WebForm6" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        .style1
        {
            text-align: justify;
        }
        .style2
        {
            text-align: justify;
            height: 28px;
        }
        .style3
        {
            height: 28px;
        }
        .style4
        {
            height: 27px;
        }
        .style5
        {
            color: #000000;
            font-weight: bold;
        }
        .style6
        {
            color: #000000;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
   <table width="98%" border ="1">
   <tr>
   <td class="style5">
    <asp:Label ID="Label1" runat="server" Text="Создать новый профиль принтера"></asp:Label>
</td>
<td>
    <b>
    <asp:Label ID="Label2" runat="server" Text="Выбрать профиль принтера:" 
        style="color: #000000"></asp:Label>
    </b>
    <asp:DropDownList ID="DropDownList1" runat="server" CssClass="bold">
    </asp:DropDownList>
</td>
</tr>
<tr>
<td class="style1"><asp:Label ID="Label3" runat="server" Text="Название профиля  " 
        CssClass="style6"></asp:Label>
    <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
    </td>
<td><asp:Label ID="Label14" runat="server" Text="Название профиля  " 
        CssClass="style6"></asp:Label>
    <asp:TextBox ID="TextBox12" runat="server"></asp:TextBox>
    </td>
</tr>
</tr>
<tr>
<td class="style1"><asp:Label ID="Label4" runat="server" 
        Text="Черный тонер OID    " CssClass="style6"></asp:Label>
    <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
    </td>
<td><asp:Label ID="Label15" runat="server" Text="Черный тонер OID    " 
        CssClass="style6"></asp:Label>
    <asp:TextBox ID="TextBox13" runat="server"></asp:TextBox>
    </td>
</tr>
</tr>
<tr>
<td class="style1"><asp:Label ID="Label5" runat="server" Text="Голый тонер OID" 
        CssClass="style6"></asp:Label>
    <asp:TextBox ID="TextBox3" runat="server" style="text-align: justify"></asp:TextBox>
    </td>
<td><asp:Label ID="Label16" runat="server" Text="Голый тонер OID" CssClass="style6"></asp:Label>
    <asp:TextBox ID="TextBox14" runat="server" style="text-align: justify"></asp:TextBox>
    </td>
</tr>
</tr>
<tr>
<td class="style1"><asp:Label ID="Label6" runat="server" Text="Пурпурный тонер OID" 
        CssClass="style6"></asp:Label>
    <asp:TextBox ID="TextBox4" runat="server"></asp:TextBox>
    </td>
<td><asp:Label ID="Label17" runat="server" Text="Пурпурный тонер OID" 
        CssClass="style6"></asp:Label>
    <asp:TextBox ID="TextBox15" runat="server"></asp:TextBox>
    </td>
</tr>
</tr>
<tr>
<td class="style2"><asp:Label ID="Label7" runat="server" Text="Желтый тонер OID   " 
        CssClass="style6"></asp:Label>
    <asp:TextBox ID="TextBox5" runat="server"></asp:TextBox>
    </td>
<td class="style3">
    <asp:Label ID="Label23" runat="server" 
        Text="Желтый тонер OID   " CssClass="style6"></asp:Label>
    <asp:TextBox ID="TextBox23" runat="server"></asp:TextBox>
    </td>
</tr>
<tr>
<td><asp:Label ID="Label8" runat="server" Text="Расположение OID   " 
        CssClass="style6"></asp:Label>
    <asp:TextBox ID="TextBox6" runat="server"></asp:TextBox>
    </td>
<td><asp:Label ID="Label18" runat="server" Text="Расположение OID   " 
        CssClass="style6"></asp:Label>
    <asp:TextBox ID="TextBox17" runat="server"></asp:TextBox>
    </td>
</tr>
</tr>
<tr>
<td><asp:Label ID="Label9" runat="server" Text="Описание OID   " CssClass="style6"></asp:Label>
    <asp:TextBox ID="TextBox7" runat="server"></asp:TextBox>
    </td>
<td><asp:Label ID="Label19" runat="server" Text="Описание OID   " CssClass="style6"></asp:Label>
    <asp:TextBox ID="TextBox18" runat="server"></asp:TextBox>
    </td>
</tr>
</tr>
<tr>
<td><asp:Label ID="Label10" runat="server" Text="MAC OID   " CssClass="style6"></asp:Label>
    <asp:TextBox ID="TextBox8" runat="server"></asp:TextBox>
    </td>
<td><asp:Label ID="Label20" runat="server" Text="MAC OID   " CssClass="style6"></asp:Label>
    <asp:TextBox ID="TextBox19" runat="server"></asp:TextBox>
    </td>
</tr>
</tr>
<tr>
<td class="style4"><asp:Label ID="Label11" runat="server" 
        Text="Серийный номер OID   " CssClass="style6"></asp:Label>
    <asp:TextBox ID="TextBox9" runat="server" Height="21px"></asp:TextBox>
    </td>
<td class="style4">
    <asp:Label ID="Label24" runat="server" 
        Text="Серийный номер OID   " CssClass="style6"></asp:Label>
    <asp:TextBox ID="TextBox24" runat="server" Height="21px"></asp:TextBox>
    </td>
</tr>
<tr>
<td><asp:Label ID="Label12" runat="server" Text="Имя хоста OID   " 
        CssClass="style6"></asp:Label>
    <asp:TextBox ID="TextBox10" runat="server"></asp:TextBox>
    </td>
<td><asp:Label ID="Label21" runat="server" Text="Имя хоста OID   " 
        CssClass="style6"></asp:Label>
    <asp:TextBox ID="TextBox21" runat="server"></asp:TextBox>
    </td>
</tr>
<tr>
<td><asp:Label ID="Label13" runat="server" Text="Счетчик страниц OID   " 
        CssClass="style6"></asp:Label>
    <asp:TextBox ID="TextBox11" runat="server"></asp:TextBox>
    </td>
<td><asp:Label ID="Label22" runat="server" Text="Счетчик страниц OID   " 
        CssClass="style6"></asp:Label>
    <asp:TextBox ID="TextBox22" runat="server"></asp:TextBox>
    </td>
</tr>
<tr>
<td>
</td>
<td>
</td>
</tr>
<tr>
<td>
</td>
<td>
</td>
</tr>
<tr>
<td>
</td>
<td>
</td>
</tr>
<tr>
<td>
</td>
<td>
</td>
</tr>
<tr>
<td>
</td>
<td>
</td>
</tr>
<tr>
<td>
    <asp:Button ID="Button1" runat="server" onclick="Button1_Click" 
        style="text-align: center" Text="Добавить" />
</td>
<td>
    <asp:Button ID="Button2" runat="server" style="text-align: center" 
        Text="Удалить" />
</td>
</tr>
</table>
</asp:Content>
