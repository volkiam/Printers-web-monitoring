<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Find_Printers.aspx.cs" Inherits="WebApplication1.WebForm5" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        .style1
        {
            height: 38px;
        }
        .style2
        {
            height: 38px;
            width: 187px;
        }
        .style3
        {
            width: 187px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Label ID="Label1" runat="server" Text="Введите диапазон IP адресов:" 
        style="font-weight: 700; color: #000000"></asp:Label>
    <br />
    <table>
    <tr>
    <td class="style2">
    <asp:TextBox ID="TextBox1" runat="server" Width="30px">10</asp:TextBox>
    <asp:TextBox ID="TextBox2" runat="server" Width="30px">0</asp:TextBox>
    <asp:TextBox ID="TextBox3" runat="server" Width="30px">60</asp:TextBox>
    <asp:TextBox ID="TextBox4" runat="server" Width="30px">88</asp:TextBox>
    </td>
    <td class="style1">
      -  
        
    </td>
    <td class="style1">
    <asp:TextBox ID="TextBox5" runat="server" Width="30px">10</asp:TextBox>
    <asp:TextBox ID="TextBox6" runat="server" Width="30px">0</asp:TextBox>
    <asp:TextBox ID="TextBox7" runat="server" Width="30px">60</asp:TextBox>
    <asp:TextBox ID="TextBox8" runat="server" Width="30px">88</asp:TextBox>
    </td>
    <td class="style1">
        <asp:Button ID="Button1" runat="server" Text="Поиск" onclick="Button1_Click" />
    </td>
    <td class="style1">     </td>
    <td align="center" class="style1">

        

        </td>
   </tr>
   <tr>
   <td class="style3">
   
        <asp:Button ID="Button2" runat="server" Text="Добавить" Visible="False" 
            onclick="Button2_Click" />
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">        
       
            <ContentTemplate>
                <asp:Label ID="Label2" runat="server" Text="Label"></asp:Label>
                <br />
                <asp:Label ID="Label3" runat="server" Text="Label"></asp:Label>
                <asp:Timer ID="Timer1" runat="server" Interval="5000" ontick="Timer1_Tick">
                </asp:Timer>
                <asp:ScriptManager ID="ScriptManager1" runat="server">
                </asp:ScriptManager>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
            </Triggers>
        </asp:UpdatePanel>
   </td>

   </tr>
   </table>
    <asp:Table ID="Table1" runat="server"
            CellPadding = "10" 
            CellSpacing="0"
            GridLines="Both"
            ForeColor="Black"
            width="98%">
    </asp:Table>
    
   <br />
           </asp:Content>
