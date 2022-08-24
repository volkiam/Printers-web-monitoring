<%@ Page Title="Редактирование групп" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Settings_group.aspx.cs" Inherits="WebApplication1.WebForm1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<table border=1 width="98%"> 
<tr>
<td align="center">Введите имя группы и нажмите добавить</td>
<td></td>
<td>Выберите группу и нажмите удалить</td>
</tr>
<tr>
<td valign="top">
    <asp:TextBox ID="TextBox1" runat="server" Width="100%"></asp:TextBox>
    <br />
    <asp:Button ID="Button1" runat="server" onclick="Button1_Click" 
        Text="Добавить группу" />
</td>
<td valign="top">

      
</td>
<td>
    <asp:DropDownList ID="DropDownList1" runat="server" Width="100%" 
        AutoPostBack="True">
    </asp:DropDownList>
    <br />
<asp:Button ID="Button2" runat="server" Text="Удалить группу" Width="143px" 
        onclick="Button2_Click" />
</td>
</tr>
</table>
<br />
    <asp:GridView ID="GV_listPrn" runat="server" BackColor="White" 
    BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3" width="98%"
        onselectedindexchanged="GV_listPrn_SelectedIndexChanged">
        <FooterStyle BackColor="White" ForeColor="#000066" />
        <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
        <RowStyle ForeColor="#000066" />
        <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
        <SortedAscendingCellStyle BackColor="#F1F1F1" />
        <SortedAscendingHeaderStyle BackColor="#007DBB" />
        <SortedDescendingCellStyle BackColor="#CAC9C9" />
        <SortedDescendingHeaderStyle BackColor="#00547E" />
</asp:GridView>
    
</asp:Content>
