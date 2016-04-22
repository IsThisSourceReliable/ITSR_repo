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
                    <div class="halfBox">
                        <h3 style="margin-left: 0.5em; margin-bottom: 0.5em; margin-top: 0em;">LikeBox</h3>
                    </div>
                    <div class="halfBox right">
                        <div style="width:70%; height:4px; background-color: green; display:inline-block;"></div><div style="width:30%; height:4px; background-color: red; display:inline-block;"></div>
                    </div>
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
                <h3>COMMENTS <asp:Label ID="Label3" runat="server" Text="Label"></asp:Label></h3>
            </div>
            <asp:ListView 
                ID="ListView1" 
                DataKeyNames="iderik_table" 
                runat="server" 
                OnItemCommand="ListView1_ItemCommand" 
                OnSelectedIndexChanged="ListView1_SelectedIndexChanged">
                <ItemTemplate>
                    <p><asp:Label 
                        ID="Label1" 
                        runat="server" 
                        Text='<%# Eval("iderik_table") %>'></asp:Label></p>
                    
                    <br />
                    <p><asp:Label 
                        ID="Label2" 
                        runat="server" 
                        Text='<%# Eval("erik_text") %>'></asp:Label></p>
                    
                    <br />
                    <asp:LinkButton 
                        ID="LinkButton1" 
                        runat="server" 
                        CommandName="Sel" 
                        CommandArgument='<%# Eval("iderik_table") %>'>LinkButton</asp:LinkButton>
                    <br />
                </ItemTemplate>
            </asp:ListView>
        </div>

    </div>

</asp:Content>
