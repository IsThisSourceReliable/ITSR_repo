<%@ Page Title="" Language="C#" MasterPageFile="~/MainMaster.Master" AutoEventWireup="true" CodeBehind="AdminPanel.aspx.cs" Inherits="ITSR.AdminPanel" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="CSS/AdminPanelCSS.css" rel="stylesheet" />
    <script src="JS/AdminPanelJS.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentHolder" runat="server">

    <asp:HiddenField ID="RemovedOrNoAction" runat="server" />


    <div class="fullBox white-box padding-box margin-box">
        <div class="fullBox">
            <h2 class="panel-h2">ADMIN PANEL</h2>
            <p class="panel-txt"><strong>Welcome to the Admin panel!</strong></p>
            <p class="panel-txt">
                As a Admin this is where you can......... bla bla bla do stuff
            </p>
        </div>
        <div class="fullBox">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <ul class="panel-navbar">
                        <li>
                            <p>
                                <asp:LinkButton ID="lbShowComments" runat="server" OnClick="lbShowComments_Click">HISTORY</asp:LinkButton>
                            </p>
                        </li>
                        <li>
                            <p>
                                <asp:LinkButton ID="lbShowRoles" runat="server" OnClick="lbShowRoles_Click">ROLES</asp:LinkButton>
                            </p>
                        </li>
                    </ul>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
    <div class="fullBox white-box padding-box history-box margin-box">
        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
            <ContentTemplate>
                <asp:ListView ID="lvSolvedCommentReports" runat="server" OnItemDataBound="lvSolvedCommentReports_ItemDataBound" OnItemCommand="lvSolvedCommentReports_ItemCommand">
                    <ItemTemplate>
                        <asp:HiddenField ID="HiddenCommentID" runat="server" Value='<%# Eval("idcomment") %>' />
                        <asp:HiddenField ID="HiddenReportCommentID" runat="server" Value='<%# Eval("idreport_comment") %>' />
                        <div class="fullBox padding-fullBox action-fullBox">
                            <div class="reportContainer padding-box">
                                <p>
                                    <strong>Reported by</strong>
                                </p>
                                <p class="lbl-adminpanel">
                                    <asp:Label ID="lblCommenter" runat="server" Text='<%# Eval("usernamereport") %>'></asp:Label>
                                </p>
                                <p>
                                    <strong>Reason</strong>
                                </p>
                                <div class="commenttext-box">
                                    <p class="lbl-adminpanel">
                                        <asp:Label ID="lblComment" runat="server" Text='<%# Eval("reason") %>'></asp:Label>
                                    </p>
                                </div>
                            </div>
                            <div class="reportContainer padding-box">
                                <p>
                                    <strong>Posted by</strong>
                                </p>
                                <p class="lbl-adminpanel">
                                    <asp:Label ID="lblReporter" runat="server" Text='<%# Eval("usernamecomment") %>'></asp:Label>
                                </p>
                                <p>
                                    <strong>Text</strong>
                                </p>
                                <div class="commenttext-box">
                                    <p class="lbl-adminpanel">
                                        <asp:Label ID="lblText" runat="server" Text='<%# Eval("comment_text") %>'></asp:Label>
                                    </p>
                                </div>
                            </div>
                            <div class="reportContainer padding-box">
                                <p>
                                    <strong>Resolved by</strong>
                                </p>
                                <p class="lbl-adminpanel">
                                    <asp:Label ID="lblResolvedBy" runat="server" Text='<%# Eval("resolvedbyusername") %>'></asp:Label>
                                </p>
                                <p>
                                    <strong>Action</strong>
                                </p>
                                <p class="lbl-adminpanel">
                                    <asp:Label ID="lblAction" runat="server" Text='<%# Eval("removed") %>'></asp:Label>
                                </p>
                            </div>
                            <div class="btndiv-adminpanel padding-box">
                                <asp:Button ID="Button1" runat="server" CommandArgument='<%# Eval("idcomment") %>' CommandName="UndoRemovedResolvedReport" CssClass="btn-adminpanel itsr-button" Text="Undo" />
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:ListView>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <div class="white-box fullBox padding-box roles-box margin-box">
        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
            <ContentTemplate>
                <div class="fullBox margin-box">
                    <p class="lbl-adminpanel">
                        <strong>Serch on username</strong>
                    </p>
                    <asp:TextBox ID="tbSearch" runat="server" CssClass="txt-box"></asp:TextBox>
                </div>
                <asp:ListView ID="lvModerators" runat="server">
                    <ItemTemplate>
                        <div class="fullBox action-fullBox padding-box">
                            <div class="moderatorContainer">
                                <p>
                                    <strong>Username</strong>
                                </p>
                                <p class="lbl-adminpanel">
                                    <asp:Label ID="lblUsername" runat="server" Text='<%# Eval("username") %>'></asp:Label>
                                </p>
                            </div>
                            <div class="moderatorContainer">
                                <p>
                                    <strong>Email</strong>
                                </p>
                                <p class="lbl-adminpanel">
                                    <asp:Label ID="lblEmail" runat="server" Text='<%# Eval("email") %>'></asp:Label>
                                </p>
                            </div>
                            <div class="moderatorContainer moderatorContainer-Btns">
                                <div><asp:Button ID="Button2" CssClass="btn-adminpanel itsr-button" runat="server" Text="Promote" /></div>
                                <div><asp:Button ID="Button3" CssClass="btn-adminpanel itsr-button" runat="server" Text="Demote" /></div>
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:ListView>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
