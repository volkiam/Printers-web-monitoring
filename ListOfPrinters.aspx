<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ListOfPrinters.aspx.cs" Inherits="WebApplication1.WebForm3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        .style1
        {
            color: #000000;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<table width="98%">
<tr>
<td>
    <strong><span class="style1">Фильтр по группам:
</span>
<br />
    <asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="True" 
        Height="16px" onselectedindexchanged="DropDownList1_SelectedIndexChanged" 
        Width="278px">
    </asp:DropDownList>
    </strong>
</td>
</tr>
</table>
<br />
    <asp:GridView ID="GV_fulllistprn" runat="server" BackColor="White" 
    BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3" Width="98%" 
        AllowSorting="True">
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
