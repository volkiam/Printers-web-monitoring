<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Edit_Sensors.aspx.cs" Inherits="WebApplication1.WebForm7" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        .style1
        {
            color: #000000;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<table width="85%">
<tr>
<td> <asp:Label ID="Label1" runat="server" style="color: #000000" 
        Text="Добавить новый датчик"></asp:Label>
</td>
<td>
    <asp:TextBox ID="TextBox1" runat="server" Height="18px" Width="202px"></asp:TextBox>
</td>
<td>
    <asp:DropDownList ID="DropDownList1" runat="server" 
        onselectedindexchanged="DropDownList1_SelectedIndexChanged">
        <asp:ListItem Selected="True">Текстовый</asp:ListItem>
        <asp:ListItem>Числовой</asp:ListItem>
        <asp:ListItem>MAC</asp:ListItem>
        <asp:ListItem>Статус устройства</asp:ListItem>
    </asp:DropDownList>
</td>
<td>
    <asp:Button ID="Button1" runat="server" onclick="Button1_Click" 
        Text="Добавить" />
    </td>
</tr>
</table>

    <br />
    <span class="style1"><strong>Список датчиков:<asp:GridView ID="GV_SensorsPrn" 
        runat="server" BackColor="White" 
    BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3" width="98%">
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
    
    </strong></span>
</asp:Content>
