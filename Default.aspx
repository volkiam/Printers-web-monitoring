<%@ Page Title="Статистика печати" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="Default.aspx.cs" Inherits="WebApplication1._Default" %>

  
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <style type="text/css">
        .style1
        {
            color: #000000;
        }
    </style>
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <span class="style1"><strong>Выберите промежуток за который необходимо получить информацию:</strong></span><br />
    <table>
    <tr>
    <td><asp:Calendar ID="Calendar1" runat="server" BackColor="White" 
            BorderColor="#3366CC" BorderWidth="1px" Caption="ОТ" CaptionAlign="Top" 
            CellPadding="1" DayNameFormat="Shortest" FirstDayOfWeek="Monday" 
            Font-Names="Verdana" Font-Size="8pt" ForeColor="#003399" Height="200px" 
            Width="220px">
        <DayHeaderStyle BackColor="#99CCCC" ForeColor="#336666" Height="1px" />
        <NextPrevStyle Font-Size="8pt" ForeColor="#CCCCFF" />
        <OtherMonthDayStyle ForeColor="#999999" />
        <SelectedDayStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
        <SelectorStyle BackColor="#99CCCC" ForeColor="#336666" />
        <TitleStyle BackColor="#003399" BorderColor="#3366CC" BorderWidth="1px" 
            Font-Bold="True" Font-Size="10pt" ForeColor="#CCCCFF" Height="25px" />
        <TodayDayStyle BackColor="#99CCCC" ForeColor="White" />
        <WeekendDayStyle BackColor="#CCCCFF" />
        </asp:Calendar></td>
    <td><asp:Calendar ID="Calendar2" runat="server" BackColor="White" 
            BorderColor="#3366CC" BorderWidth="1px" Caption="ДО" CellPadding="1" 
            DayNameFormat="Shortest" Font-Names="Verdana" Font-Size="8pt" 
            ForeColor="#003399" Height="200px" Width="220px">
        <DayHeaderStyle BackColor="#99CCCC" ForeColor="#336666" Height="1px" />
        <NextPrevStyle Font-Size="8pt" ForeColor="#CCCCFF" />
        <OtherMonthDayStyle ForeColor="#999999" />
        <SelectedDayStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
        <SelectorStyle BackColor="#99CCCC" ForeColor="#336666" />
        <TitleStyle BackColor="#003399" BorderColor="#3366CC" BorderWidth="1px" 
            Font-Bold="True" Font-Size="10pt" ForeColor="#CCCCFF" Height="25px" />
        <TodayDayStyle BackColor="#99CCCC" ForeColor="White" />
        <WeekendDayStyle BackColor="#CCCCFF" />
        </asp:Calendar></td>
   
    </tr>
    <tr>
    
    <td>
    <span class="style1"><strong>Выберите группу:
    </strong></span>
    <br />
        <asp:DropDownList ID="DDL_main" runat="server" Width="99%" AutoPostBack="True">
        </asp:DropDownList>
        <asp:Label ID="Label1" runat="server" Text="Label" Visible="False"></asp:Label>
    </td>
    </tr>
    <tr>
    <td>
    <br />
    <asp:Button ID="Button1" runat="server" Text="Выполнить" 
    onclick="Button1_Click" Width="99%" />
    </td>
    </tr>
    </table>
    <br />
    <asp:GridView ID="GV_main" runat="server" 
    Caption="Данные" CellPadding="4" 
    ForeColor="#333333" GridLines="None" Width="98%">
        <AlternatingRowStyle BackColor="White" />
        <EditRowStyle BackColor="#2461BF" />
        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
        <RowStyle BackColor="#EFF3FB" />
        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
        <SortedAscendingCellStyle BackColor="#F5F7FB" />
        <SortedAscendingHeaderStyle BackColor="#6D95E1" />
        <SortedDescendingCellStyle BackColor="#E9EBEF" />
        <SortedDescendingHeaderStyle BackColor="#4870BE" />
    </asp:GridView>
    <br />
    <br />
  
</asp:Content>
