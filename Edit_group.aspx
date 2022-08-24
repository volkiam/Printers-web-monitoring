<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Edit_group.aspx.cs" Inherits="WebApplication1.WebForm4" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <table border="1">
<tr>
<td align ="center">Выберите принтеры</td>
<td align ="center">Выберите группу</td>
<td align ="center">Добавьте в группу</td>
</tr>
<tr>
<td valign="top">
    <asp:DropDownList ID="DDL2" runat="server" 
        onselectedindexchanged="DDL2_SelectedIndexChanged" AutoPostBack="True">
    </asp:DropDownList>
    <br />
    <asp:CheckBoxList ID="CBL1" runat="server"  AutoPostBack="True">
    </asp:CheckBoxList>

</td>

<td valign="top">
    <asp:DropDownList ID="DDL1" runat="server">
    </asp:DropDownList>
</td>
<td valign="top"; align="center">
    <asp:Button ID="Button1" runat="server" Text="Добавить" 
        style="text-align: center" onclick="Button1_Click" />
    </td>
    </tr>
</table>
</asp:Content>
