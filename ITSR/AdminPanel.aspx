<%@ Page Title="" Language="C#" MasterPageFile="~/MainMaster.Master" AutoEventWireup="true" CodeBehind="AdminPanel.aspx.cs" Inherits="ITSR.AdminPanel" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="CSS/AdminPanelCSS.css" rel="stylesheet" />
    <script src="JS/AdminPanelJS.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentHolder" runat="server">

    <asp:HiddenField ID="RemovedOrNoAction" runat="server" />


    <div class="fullBox white-box padding paddingFix marginBot">
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
    <div class="history-box fullBox white-box padding paddingFix marginBot">
        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
            <ContentTemplate>
                <asp:ListView ID="lvSolvedCommentReports" runat="server" OnItemDataBound="lvSolvedCommentReports_ItemDataBound" OnItemCommand="lvSolvedCommentReports_ItemCommand">
                    <ItemTemplate>
 
                        <asp:HiddenField ID="HiddenCommentID" runat="server" Value='<%# Eval("idcomment") %>' />
                        <asp:HiddenField ID="HiddenReportCommentID" runat="server" Value='<%# Eval("idreport_comment") %>' />
                        <div class="fullBox border-bottom">
                            <div class="div25 padding paddingFix">
                                <p>
                                    <strong>Reported by</strong>
                                </p>
                                <p class="lbl">
                                    <asp:Label ID="lblCommenter" runat="server" Text='<%# Eval("usernamereport") %>'></asp:Label>
                                </p>
                                <p>
                                    <strong>Reason</strong>
                                </p>
                                <div>
                                    <p class="lbl">
                                        <asp:Label ID="lblComment" runat="server" Text='<%# Eval("reason") %>'></asp:Label>
                                    </p>
                                </div>
                            </div>
                            <div class="div25 padding paddingFix">
                                <p>
                                    <strong>Posted by</strong>
                                </p>
                                <p class="lbl">
                                    <asp:Label ID="lblReporter" runat="server" Text='<%# Eval("usernamecomment") %>'></asp:Label>
                                </p>
                                <p>
                                    <strong>Text</strong>
                                </p>
                                <div>
                                    <p class="lbl">
                                        <asp:Label ID="lblText" runat="server" Text='<%# Eval("comment_text") %>'></asp:Label>
                                    </p>
                                </div>
                            </div>
                            <div class="div25 padding paddingFix">
                                <p>
                                    <strong>Resolved by</strong>
                                </p>
                                <p class="lbl">
                                    <asp:Label ID="lblResolvedBy" runat="server" Text='<%# Eval("resolvedbyusername") %>'></asp:Label>
                                </p>
                                <p>
                                    <strong>Action</strong>
                                </p>
                                <p class="lbl">
                                    <asp:Label ID="lblAction" runat="server" Text='<%# Eval("removed") %>'></asp:Label>
                                </p>
                            </div>
                            <div class="btndiv-floatright padding paddingFix">
                                <asp:Button ID="Button1" runat="server" CommandArgument='<%# Eval("idcomment") %>' CommandName="UndoRemovedResolvedReport" CssClass="Secondarybtn" Text="Undo" />
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:ListView>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <div class="roles-box white-box fullBox padding paddingFix margin-bottom-box">
        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
            <ContentTemplate>
                <div class="fullBox padding paddingFix">
                    <div class="fullBox marginTop marginLeft">
                        <p>
                            <strong>Search on username</strong>
                            <asp:RequiredFieldValidator
                                ID="RequiredFieldValidator1"
                                ControlToValidate="tbSearch"
                                ForeColor="Red" runat="server"
                                Font-Size="Medium"
                                ErrorMessage="Did you forget something?"
                                Display="Dynamic"
                                ValidationGroup="SearchGroup">
                            </asp:RequiredFieldValidator>
                        </p>
                    </div>
                    <div class="twothirdBox paddingLeft paddingRight paddingFix">
                        <asp:TextBox ID="tbSearch" runat="server" CssClass="tbSearch tb paddingFix marginBot"></asp:TextBox>
                    </div>
                    <div class="thirdBox">
                        <div class="fullBox MakeHalfBox">
                            <asp:Button ID="btnSearch" runat="server" ValidationGroup="SearchGroup" Text="Search" CssClass="Mainbtn marginLeft" OnClick="btnSearch_Click" />
                        </div>
                        <div class="CustomDiv25 marginLeft">
                            <input id="btnClearSearch" type="button" class="HiddenBtn Mainbtn marginLeft" value="X" onclick="MakeBtnBigger();" />
                        </div>
                    </div>
                </div>
                <div class="listView">
                    <asp:ListView ID="lvSearchMember" OnItemDataBound="lvSearchMember_ItemDataBound" OnItemCommand="lvSearchMember_ItemCommand" runat="server">
                        <ItemTemplate>
                         <asp:HiddenField ID="HiddenField1" runat="server" value="0"/>
                            <asp:HiddenField ID="HiddenUserID" Value='<%# Eval("iduser") %>' runat="server" />
                            <div class="fullBox marginTop border-bottom padding paddingFix">
                                <div class="div25">
                                    <p>
                                        <strong>Username</strong>
                                    </p>
                                    <p class="lbl">
                                        <asp:Label ID="lblUsername" runat="server" Text='<%# Eval("username") %>'></asp:Label>
                                    </p>
                                </div>
                                <div class="div25">
                                    <p>
                                        <strong>Email</strong>
                                    </p>
                                    <p class="lbl">
                                        <asp:Label ID="lblEmail" runat="server" Text='<%# Eval("email") %>'></asp:Label>
                                    </p>
                                </div>
                                <div class="btndiv-floatright marginLeft">
                                    <asp:Button ID="BtnMakeModerator" CssClass="Secondarybtn" runat="server" CommandName="MakeModerator" Text="Make Moderator" />
                                </div>
                            </div>
                        </ItemTemplate>
                    </asp:ListView>
                </div>
                <asp:ListView ID="lvModerators" runat="server" OnItemCommand="lvModerators_ItemCommand" OnItemDataBound="lvModerators_ItemDataBound">
                    <ItemTemplate>
                        <asp:HiddenField ID="HiddenUserID" Value='<%# Eval("iduser") %>' runat="server" />
                        <div class="fullBox border-bottom padding paddingFix">
                            <div class="div25">
                                <p>
                                    <strong>Username</strong>
                                </p>
                                <p class="lbl">
                                    <asp:Label ID="lblUsername" runat="server" Text='<%# Eval("username") %>'></asp:Label>
                                </p>
                            </div>
                            <div class="div25">
                                <p>
                                    <strong>Email</strong>
                                </p>
                                <p class="lbl">
                                    <asp:Label ID="lblEmail" runat="server" Text='<%# Eval("email") %>'></asp:Label>
                                </p>
                            </div>
                            <div class="btndiv-floatright marginLeft">
                                <asp:Button ID="BtnSaveRole" CssClass="Secondarybtn" runat="server" CommandName="SaveNewRole" Text="Save" />
                            </div>
                            <div class="btndiv-floatright">
                                <asp:DropDownList ID="ddlRoles" CssClass="DropDown DropDown-AdminPanel" runat="server"></asp:DropDownList>
                            </div>
                        </div>

                    </ItemTemplate>
                </asp:ListView>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
