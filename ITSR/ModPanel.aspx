<%@ Page Title="" Language="C#" MasterPageFile="~/MainMaster.Master" AutoEventWireup="true" CodeBehind="ModPanel.aspx.cs" Inherits="ITSR.ModPanel" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <link href="CSS/PanelStyle.css" rel="stylesheet" />

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentHolder" runat="server">
    <div class="fullBox white-box">
        <div class="fullBox">
            <h2 class="panel-h2">MODERATOR PANEL</h2>
            <p class="panel-txt"><strong>Welcome to the moderator panel!</strong></p>
            <p class="panel-txt">
                As a moderator this is where you can see comments that has been reported and decide wheter or not to remove them. 
                A comment should always be removed if it violates the community standards or breaks any laws.
            </p>
        </div>

        <asp:UpdatePanel ID="UpdatePanelComments" runat="server">
            <ContentTemplate>

                <div class="fullBox">
                    <ul class="panel-navbar">
                        <li>
                            <p>
                                <asp:LinkButton ID="lBtnShowComments" runat="server" OnClick="lBtnShowComments_Click">COMMENTS</asp:LinkButton>
                            </p>
                        </li>
                        <li>
                            <p>
                                <asp:LinkButton ID="lBtnShowArticles" runat="server">ARTICLES</asp:LinkButton>
                            </p>
                        </li>
                    </ul>
                </div>
                <div class="fullBox">
                    <p class="panel-txt">
                        There is currently 
                <asp:Label ID="lblTotalReports" runat="server" Text="Label"></asp:Label>
                        reports made by users.
                    </p>
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



            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
