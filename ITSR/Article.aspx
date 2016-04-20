<%@ Page Title="" Language="C#" MasterPageFile="~/MainMaster.Master" AutoEventWireup="true" CodeBehind="Article.aspx.cs" Inherits="ITSR.Article" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentHolder" runat="server">
    <div class="fullBox white-box">
        <div class="fullBox">
            <h2><strong><asp:Label ID="lblArticleName" runat="server" Text="ArticleName"></asp:Label></strong></h2>
        </div>
        <div class="fullBox">
            <div class="halfBox">
                <p>"Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum."</p>
            </div>
            <div class="halfBox">
                <div class="fullBox">
                    <h3>LikeBox</h3>
                </div>
                
            </div>
        </div>
    </div>

</asp:Content>
