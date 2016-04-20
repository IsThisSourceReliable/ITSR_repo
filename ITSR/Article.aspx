<%@ Page Title="" Language="C#" MasterPageFile="~/MainMaster.Master" AutoEventWireup="true" CodeBehind="Article.aspx.cs" Inherits="ITSR.Article" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <!-- SPEC STYLE -->
    <link href="CSS/ArticleCSS.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentHolder" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div class="fullBox white-box">
        <!-- TITLE ================================================== -->
        <div class="fullBox">
            <h2 class="article-heading"><strong><asp:Label ID="lblArticleName" runat="server" Text="ArticleName"></asp:Label></strong></h2>
        </div>
        <!-- ARTICLE ================================================== -->
        <div class="fullBox">
            <div class="halfBox ">
                <div class="fullBox like-box-mobile">
                    <h3 style="margin-left: 0.5em; margin-bottom: 0.5em; margin-top: 0em;">LikeBox</h3>
                </div>  
                <div class="fullBox">
                    <p class="article-text">"Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum."</p>
                </div>
            </div>
            <div class="halfBox">
<%--                <div class="fullBox like-box-station">
                    <h3>LikeBox</h3>
                </div> --%>               
                <div class="fullbox info-box">
                    <h4 class="info-titles">Type of Organisation: </h4>
                    <p class="info-lbls">
                        <asp:Label ID="lblTypeOfOrg" runat="server" Text="TypeOfOrg"></asp:Label>
                    </p>
                </div>
                <div class="fullbox">
                    <h4 class="info-titles">Up house man: </h4>
                    <p class="info-lbls">
                        <asp:Label ID="lblUpHouseMan" runat="server" Text="UpHouseMan"></asp:Label>
                    </p>
                </div>
                <div class="fullbox">
                    <h4 class="info-titles">Domain owner: </h4>
                    <p class="info-lbls">
                        <asp:Label ID="lblDomainOwner" runat="server" Text="DomainOwner"></asp:Label>
                    </p>
                </div>
                <div class="fullbox">
                    <h4 class="info-titles">Financer:  </h4>
                    <p class="info-lbls">
                        <asp:Label ID="lblFinancer" runat="server" Text="Finnacer"></asp:Label>
                    </p>
                </div>
            </div>
        </div>
        <!-- COMMENT SECTION ================================================== -->
        <div class="fullBox">
            <div class="fullBox">
                <h3>COMMENTS </h3>
            </div>
            <asp:ListView ID="ListView1" runat="server">
                <ItemTemplate>
                    <p><%# Eval("iderik_table") %></p>
                    <br />
                    <p><%# Eval("erik_text") %></p>
                    <br />
                </ItemTemplate>
            </asp:ListView>
        </div>

    </div>

</asp:Content>
