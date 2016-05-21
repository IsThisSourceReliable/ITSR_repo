<%@ Page Title="" Language="C#" MasterPageFile="~/MainMaster.Master" AutoEventWireup="true" CodeBehind="AdminPanel.aspx.cs" Inherits="ITSR.AdminPanel" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="CSS/AdminPanelCSS.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentHolder" runat="server">
    <div class="fullBox white-box padding-fullBox">
        <div class="fullBox">
            <h2 class="panel-h2">ADMIN PANEL</h2>
            <p class="panel-txt"><strong>Welcome to the Admin panel!</strong></p>
            <p class="panel-txt">
                As a Admin this is where you can......... bla bla bla do stuff
            </p>
        </div>
        <asp:ListView ID="lvSolvedCommentReports" runat="server">
            <ItemTemplate>
                <div class="fullBox">
                    <asp:Label ID="lblComment" runat="server" Text="<%# Eval("usernamereport") %>'"></asp:Label>
                    <asp:Label ID="lblResolvedBy" runat="server" Text="<%# Eval("usernamereport") %>'"></asp:Label>
                    <asp:Label ID="lblResolvedHow" runat="server" Text="<%# Eval("usernamereport") %>'"></asp:Label>
                </div>
            </ItemTemplate>
        </asp:ListView>
    </div>
</asp:Content>
