<%@ Page Title="" Language="C#" MasterPageFile="~/MainMaster.Master" AutoEventWireup="true" CodeBehind="ModPanel.aspx.cs" Inherits="ITSR.ModPanel" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="CSS/PanelStyle.css" rel="stylesheet" />
    <link href="CSS/ArticleCSS.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentHolder" runat="server">

    <div class="fullBox white-box padding paddingFix">
        <div class="fullBox">
            <h2 class="panel-h2">MODERATOR PANEL</h2>
            <div class="fullBox">
                <ul class="panel-navbar">
                    <li>
                        <p>
                            <asp:LinkButton ID="lBtnShowComments" runat="server" OnClick="lBtnShowComments_Click">COMMENTS</asp:LinkButton>
                        </p>
                    </li>
                    <li>
                        <p>
                            <asp:LinkButton ID="lBtnShowArticles" runat="server" OnClick="lBtnShowArticles_Click">ARTICLES</asp:LinkButton>
                        </p>
                    </li>
                </ul>
            </div>
            <div class="halfBox">
                <p class="panel-txt"><strong>Welcome to the moderator panel!</strong></p>
                <p class="panel-txt">
                    As a moderator this is where you can see comments and articles that has been reported and decide wheter or not to remove them. 
                    <br />
                    <br />
                    And when it comes to articles you can choose to revert to an older version, edit or decide that the report was unecessary.
                    <br />
                    <br />
                    A comment should always be removed if it violates the community standards or breaks any laws.
                </p>
            </div>
            <div class="halfBox">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <p class="panel-txt">
                            <strong>Current status: </strong>
                            <br />
                            <asp:Label ID="lblTotalCommentReport" runat="server" Text="Label"></asp:Label>
                            <br />
                            <asp:Label ID="lblTotalArticleReport" runat="server" Text="Label"></asp:Label>
                        </p>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>

        <asp:UpdatePanel ID="UpdatePanelReports" runat="server">
            <ContentTemplate>

                <div class="fullBox comment" style="width: 97%; margin-left: 1%;">
                    <h2>
                        <asp:Label ID="lblSection" runat="server" Text=""></asp:Label></h2>
                </div>

                <asp:ListView
                    ID="listViewReports"
                    runat="server"
                    OnItemCommand="listViewReports_ItemCommand">
                    <ItemTemplate>
                        <div class="fullBox">
                            <div class="report-holder">
                                <div class="report-box">
                                    <p>
                                        <strong>Reported by</strong>
                                    </p>
                                    <p class="lbl-style">
                                        <asp:Label ID="lblReportUser" runat="server" Text='<%# Eval("usernamereport") %>'></asp:Label>
                                    </p>

                                    <p>
                                        <strong>Reason</strong>
                                    </p>
                                    <p class="lbl-style">
                                        <asp:Label ID="lblReason" runat="server" Text='<%# Eval("reason") %>'></asp:Label>
                                    </p>
                                </div>
                                <div class="report-box big-report-box">
                                    <p>
                                        <strong>Posted by</strong>
                                    </p>
                                    <p class="lbl-style">
                                        <asp:Label ID="lblCommentUser" runat="server" Text='<%# Eval("usernamecomment") %>'></asp:Label>
                                    </p>
                                    <p>
                                        <strong>Text</strong>
                                    </p>
                                    <p class="lbl-style">
                                        <asp:Label ID="lblCommentText" runat="server" Text='<%# Eval("comment_text") %>'></asp:Label>
                                        <asp:HiddenField ID="HiddenCommentID" runat="server" Value='<%# Eval("idcomment") %>' />
                                        <asp:HiddenField ID="HiddenReportCommentID" runat="server" Value='<%# Eval("idreport_comment") %>' />
                                    </p>
                                </div>
                                <div class="report-box">
                                    <asp:LinkButton
                                        ID="lBtnDelete"
                                        CssClass="itsr-button btn-panel"
                                        runat="server"
                                        CommandName="DeleteComment"
                                        CommandArgument='<%# Eval("idcomment") %>'>DELETE</asp:LinkButton>
                                    <asp:LinkButton
                                        ID="lBtnNoAction"
                                        CssClass="itsr-button btn-panel"
                                        runat="server"
                                        CommandName="NoAction"
                                        CommandArgument="">NO ACTION</asp:LinkButton>
                                </div>
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:ListView>

                <asp:ListView
                    ID="ListViewReportArticles"
                    runat="server"
                    OnItemCommand="ListViewReportArticles_ItemCommand">
                    <ItemTemplate>
                        <div class="fullBox">
                            <div class="report-holder">
                                <div class="report-box">
                                    <p>
                                        <asp:HiddenField ID="HiddenArticleID" runat="server" Value='<%# Eval("idarticle") %>' />
                                        <asp:HiddenField ID="HiddenArticleReportIDListView" runat="server" Value='<%# Eval("idreport_article") %>' />
                                        <strong>
                                            <asp:Label ID="lblArticleName" runat="server" Text='<%# Eval("title") %>'></asp:Label></strong>
                                    </p>
                                </div>
                                <div class="report-box">
                                    <p>
                                        <strong>Reason </strong>
                                        <asp:Label ID="lblArticleReason" runat="server" Text='<%# Eval("reason") %>'></asp:Label>
                                    </p>
                                </div>
                                <div class="report-box">
                                    <p>
                                        <strong>Reported by </strong>
                                        <asp:Label ID="lblArticleReportUser" runat="server" Text='<%# Eval("username") %>'></asp:Label>
                                    </p>

                                </div>
                                <div>
                                    <asp:LinkButton
                                        ID="lBtnDetails"
                                        runat="server"
                                        CssClass="itsr-button btn-panel"
                                        CommandName="ShowDetails"
                                        CommandArgument="">SHOW DETAILS</asp:LinkButton>
                                </div>
                            </div>
                        </div>

                    </ItemTemplate>
                </asp:ListView>

                <div id="ArticleBox" class="fullBox" runat="server">
                    <asp:HiddenField ID="HiddenArticleIDBox" runat="server" />
                    <asp:HiddenField ID="HiddenReportArticleID" runat="server" />
                    <div class="fullBox">
                        <div class=" fullBox comment" style="width: 97%; margin-left: 1%;">
                            <div class="halfBox">
                                <h2>Review article details</h2>
                                <p class="fail-text">
                                    <asp:Label ID="lblFail" runat="server" Text=""></asp:Label>
                                </p>
                            </div>

                            <div class="halfBox" id="dropDownDiv" runat="server">
                                <div class="fullBox">
                                    <p style="padding-top: 1.8em; padding-bottom: 0.5em;"><strong>Choose an older version to preview revert</strong></p>
                                    <asp:DropDownList ID="dropDownOldVersion" CssClass="DropDown dropdown-panel" Style="margin-bottom: 0.5em;" AutoPostBack="true" OnSelectedIndexChanged="dropDownOldVersion_SelectedIndexChanged" runat="server"></asp:DropDownList>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="fullBox">
                        <h2 class="article-heading"><strong>
                            <asp:Label ID="lblArticleName" runat="server" Text="ArticleName"></asp:Label></strong></h2>
                    </div>
                    <div class="fullBox">
                        <div class="halfBox">
                            <div class="fullBox">
                                <p class="article-text" id="articleText" runat="server"></p>
                            </div>
                        </div>
                        <div class="halfBox">
                            <div class="fullbox">
                                <h4 class="info-titles">Type of Organisation: </h4>
                                <p class="info-lbls">
                                    <asp:Label ID="lblTypeOfOrg" runat="server" Text=""></asp:Label>
                                </p>
                            </div>
                            <div class="fullbox">
                                <h4 class="info-titles">Publisher/Author: </h4>
                                <p class="info-lbls">
                                    <asp:Label ID="lblUpHouseMan" runat="server" Text=""></asp:Label>
                                </p>
                            </div>
                            <div class="fullbox">
                                <h4 class="info-titles">Domain owner: </h4>
                                <p class="info-lbls">
                                    <asp:Label ID="lblDomainOwner" runat="server" Text=""></asp:Label>
                                </p>
                            </div>
                            <div class="fullbox">
                                <h4 class="info-titles">Main Owner:  </h4>
                                <p class="info-lbls">
                                    <asp:Label ID="lblFinancer" runat="server" Text=""></asp:Label>
                                </p>
                            </div>
                        </div>

                    </div>

                    <div class="fullBox">
                        <h4 class="info-titles">References</h4>
                        <p class="ref-text">
                            <asp:Label ID="lblRefText" runat="server" Text="Label"></asp:Label>
                        </p>
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
                            <i>
                                <asp:Label
                                    ID="lblTitle"
                                    runat="server"
                                    Text='<%# Eval("Title") %>'></asp:Label></i>.
                            <a href='http://<%# Eval("url") %>' target="_blank">
                                <asp:Label ID="lblURL" runat="server" Text='<%# Eval("url") %>'></asp:Label></a>
                                </p>
                            </ItemTemplate>
                        </asp:ListView>
                    </div>
                    <div class="fullBox">
                        <div style="margin-left: 1em;">
                            <asp:LinkButton ID="lBtnRevertArticle" CssClass="itsr-button btn-panel" runat="server" OnClick="lBtnRevertArticle_Click">REVERT</asp:LinkButton>
                            <asp:LinkButton ID="lBtnEditArticle" CssClass="itsr-button btn-panel" runat="server" OnClick="lBtnEditArticle_Click">EDIT</asp:LinkButton>
                            <asp:LinkButton ID="lBtnNoActionArticle" CssClass="itsr-button btn-panel" runat="server" OnClick="lBtnNoActionArticle_Click">NO ACTION</asp:LinkButton>
                            <div class="right" style="padding-right: 1em;">
                                <asp:LinkButton ID="lBtnCancelRevert" CssClass="itsr-button btn-panel" runat="server" OnClick="lBtnCancelRevert_Click">CANCEL</asp:LinkButton>
                                <asp:LinkButton ID="lBtnConfirmRevert" CssClass="itsr-button btn-panel" runat="server" OnClick="lBtnConfirmRevert_Click">CONFIRM</asp:LinkButton>
                            </div>
                        </div>
                    </div>
                </div>

            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
