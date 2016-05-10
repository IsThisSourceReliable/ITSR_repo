<%@ Page Title="" Language="C#" MasterPageFile="~/MainMaster.Master" AutoEventWireup="true" CodeBehind="Article.aspx.cs" Inherits="ITSR.Article" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <!-- SPEC STYLE -->
    <link href="CSS/ArticleCSS.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentHolder" runat="server">
    
    <div class="page-overlay">
        <div class="overlay-message">
          <p class="my-glyph-close"><span class="glyphicon glyphicon-remove right " onclick="CloseOverlay();"></span></p>
            <p>Hej hej hej
                <br />
            LabelIndexListview = <asp:Label ID="lblIndexListView" runat="server" Text="Label"></asp:Label></p>
            <br />
            LabelIndexDatabas = <asp:Label ID="lblIndexDataBase" runat="server" Text="Label"></asp:Label>
            <br />            
            Label"UserName" = <asp:Label ID="lblUserName" runat="server" Text="Label"></asp:Label>
        </div>
    </div>
    
    <div class="fullBox white-box">
        <!-- TITLE ================================================== -->
        <div class="fullBox">
            <h2 class="article-heading"><strong><asp:Label ID="lblArticleName" runat="server" Text="ArticleName"></asp:Label></strong></h2>           
        </div>
        <!-- ARTICLE ================================================== -->
        <div class="fullBox">
            <div class="halfBox ">
                <div class="fullBox ">
                        <div class="like-box">
                            <div class="fullBox">
                                <div class="vote-bar upvote-bar" id="upvoteBar" runat="server"></div><div class="vote-bar downvote-bar" id="downvoteBar" runat="server"></div>
                            </div>
                            <div class="fullBox">
                                <div class="right"> 
                                    <div style="display:inline-block;">
                                        <p><asp:Label ID="lblTotalVotes" runat="server" Text="TotalVotes"></asp:Label> votes</p>
                                    </div>
                                    <div class="vote-btn upvote-btn">
                                        <span class="vote-glyph glyphicon glyphicon-arrow-up " onclick=""></span>
                                    </div>
                                    <div class="vote-btn downvote-btn">
                                        <span class="vote-glyph glyphicon glyphicon-arrow-down " onclick="();"></span>
                                    </div>
                                </div>

                            </div>
                        </div>
                </div>  
                <div class="fullBox">
                    <p class="article-text" id="articleText" runat="server">"Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum."</p>
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
            <div class="fullBox">
                <h4 class="info-titles">References</h4>
                <p class="ref-text"><asp:Label ID="lblRefText" runat="server" Text="Label"></asp:Label></p>
                <asp:ListView 
                    ID="ListViewReferences" runat="server">
                    <ItemTemplate>
                        <p class="ref-text">
                            <asp:Label 
                                ID="lblAuthor" 
                                runat="server" 
                                Text='<%# Eval("Author") %>'></asp:Label>. 
                            <asp:Label 
                                ID="lblYear" 
                                runat="server" 
                                Text='<%# Eval("Year") %>'></asp:Label>.
                            <i><asp:Label 
                                ID="lblTitle" 
                                runat="server" 
                                Text='<%# Eval("Title") %>'></asp:Label></i>.
                            <a href='http://<%# Eval("url") %>' target="_blank"><asp:Label ID="lblURL" runat="server" Text='<%# Eval("url") %>'></asp:Label></a>
                        </p>
                    </ItemTemplate>
                </asp:ListView>
            </div>
            <div class="fullBox">
                <br />
                <p class="edit-text">Last edited by <asp:LinkButton ID="linkBtnLastEdit" runat="server">LinkButton</asp:LinkButton> at <asp:Label ID="lblEditDate" runat="server" Text="Label"></asp:Label></p>                
                <p class="edit-text">Is the information about this source not correct? <asp:LinkButton ID="lBtnEdit" runat="server" OnClick="lBtnEdit_Click">Edit here</asp:LinkButton></p>
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
                        CommandName="ReportComment" 
                        CommandArgument='<%# Eval("iderik_table") %>'>Report</asp:LinkButton>
                    <br />
                    <asp:LinkButton 
                        ID="LinkButton2" 
                        runat="server" 
                        CommandName="DeleteComment" 
                        CommandArgument='<%# Eval("iderik_table") %>'>Delete</asp:LinkButton>
                </ItemTemplate>
            </asp:ListView>
        </div>
        <asp:HiddenField ID="hiddenArticleID" runat="server" />
    </div>

</asp:Content>
