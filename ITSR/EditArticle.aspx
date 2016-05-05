<%@ Page Title="" Language="C#" MasterPageFile="~/MainMaster.Master" AutoEventWireup="true" CodeBehind="EditArticle.aspx.cs" Inherits="ITSR.EditArticle" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="CSS/CreateArticleCSS.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentHolder" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

    <div class="page-overlay">
        <div class="overlay-message">
            <p class="close-glyph"><span class="glyphicon glyphicon-remove right " onclick="CloseOverlay();"></span></p>
            <div class="fullBox">
                <h2>Add reference</h2>
                <asp:Label
                    ID="lblID"
                    runat="server"
                    Text="Label"
                    CssClass="hiddenCol">
                </asp:Label>
                <h4 class="title-h4"><strong>Author</strong></h4>
                <asp:TextBox
                    ID="txtAuthor"
                    CssClass="txt-box ca-txt-box"
                    runat="server">
                </asp:TextBox>
                <h4 class="title-h4"><strong>Year</strong></h4>
                <asp:TextBox
                    ID="txtYear"
                    CssClass="txt-box ca-txt-box"
                    runat="server">
                </asp:TextBox>
                <h4 class="title-h4"><strong>Title</strong></h4>
                <asp:TextBox
                    ID="txtTitle"
                    CssClass="txt-box ca-txt-box"
                    runat="server">
                </asp:TextBox>
                <h4 class="title-h4"><strong>URL</strong></h4>
                <asp:TextBox
                    ID="txtURL"
                    CssClass="txt-box ca-txt-box"
                    runat="server">
                </asp:TextBox>

                <asp:Button
                    ID="btnAddRef"
                    runat="server"
                    CssClass="itsr-button ref-btn"
                    Text="Add Reference"
                    OnClick="btnAddRef_Click" />
            </div>
            .
        </div>
        .
    </div>


    <div class="fullBox white-box">
        <div class="fullBox">
            <h2 class="ca-heading">Edit A Source</h2>
        </div>
        <div class="fullBox">
            <div class="halfBox">
                <div class="fullBox">
                    <h4 class="title-h4">Name of source: </h4>
                    <p class="ca-text">
                        <span style="color: red;">
                            <asp:Label
                                ID="lblTitleFail"
                                runat="server"
                                Text="Label">
                            </asp:Label>
                        </span>
                        <asp:RequiredFieldValidator
                            ID="ValidatorTitle"
                            ControlToValidate="txtArticleTitle"
                            ValidationGroup="Required"
                            runat="server"
                            ErrorMessage="Please add a name to the source."
                            Display="Dynamic"
                            ForeColor="Red">
                        </asp:RequiredFieldValidator>
                    </p>
                    <asp:TextBox
                        ID="txtArticleTitle"
                        CssClass="txt-box ca-txt-box"
                        runat="server">
                    </asp:TextBox>
                </div>
                <div class="fullBox">
                    <p class="ca-text">Give a brief explanation of the source.</p>
                    <p class="ca-text">
                        <asp:RequiredFieldValidator
                            ID="ValidatorInfoText"
                            ControlToValidate="txtInfo"
                            ValidationGroup="Required"
                            runat="server"
                            ErrorMessage="Please add some information about the source"
                            Display="Dynamic"
                            ForeColor="Red">
                        </asp:RequiredFieldValidator>
                    </p>
                    <asp:TextBox
                        ID="txtInfo"
                        runat="server"
                        TextMode="MultiLine"
                        CssClass="multiline-txt-box ca-multi-txt">
                    </asp:TextBox>
                </div>
            </div>
            <div class="halfBox">
                <div class="fullBox">
                    <h4 class="title-h4">Type of organisation: </h4>
                    <p class="ca-text">
                        <asp:RequiredFieldValidator
                            ID="ValidatorORG"
                            ControlToValidate="dropDownTypeOfOrg"
                            ValidationGroup="Required"
                            InitialValue="Choose"
                            runat="server"
                            ErrorMessage="Please choose what type of organisation"
                            Display="Dynamic"
                            ForeColor="Red">
                        </asp:RequiredFieldValidator>
                    </p>
                    <asp:DropDownList
                        ID="dropDownTypeOfOrg"
                        runat="server"
                        CssClass="DropDown ca-dropdown">
                    </asp:DropDownList>
                </div>
                <div class="fullBox">
                    <h4 class="title-h4">URL: </h4>
                    <p class="ca-text">
                        <span style="color: red;">
                            <asp:Label
                                ID="lblURLFail"
                                runat="server"
                                Text="Label">
                            </asp:Label>
                        </span>
                        <asp:RequiredFieldValidator
                            ID="ValidatorURL"
                            ControlToValidate="txtArticleURL"
                            ValidationGroup="Required"
                            runat="server"
                            ErrorMessage="Please add a URL to the source"
                            Display="Dynamic"
                            ForeColor="Red">
                        </asp:RequiredFieldValidator>
                    </p>
                    <asp:TextBox
                        ID="txtArticleURL"
                        runat="server"
                        CssClass="txt-box ca-txt-box">
                    </asp:TextBox>
                </div>
                <div class="fullBox">
                    <h4 class="title-h4">Up house man: </h4>
                    <asp:TextBox
                        ID="txtUpHouseMan"
                        runat="server"
                        CssClass="txt-box ca-txt-box">
                    </asp:TextBox>
                </div>
                <div class="fullBox">
                    <h4 class="title-h4">Domain owner: </h4>
                    <asp:TextBox
                        ID="txtDomainOwner"
                        runat="server"
                        CssClass="txt-box ca-txt-box">
                    </asp:TextBox>
                </div>
                <div class="fullBox">
                    <h4 class="title-h4">Financer: </h4>
                    <asp:TextBox
                        ID="txtFinancer"
                        runat="server"
                        CssClass="txt-box ca-txt-box">
                    </asp:TextBox>
                </div>
                <div class="itsr-button ref-btn" onclick="OpenOverlay();">Add Reference</div>

            </div>
        </div>

        <div class="fullBox">
            <div class="fullBox">
                <h4 class="title-h4"><strong>References</strong></h4>
            </div>
            <div class="fullBox">
                <p>
                    <asp:Label
                        ID="lblRef"
                        runat="server"
                        CssClass="ca-text"
                        Text="Label"></asp:Label>
                </p>
                <asp:GridView
                    ID="gridViewReferences"
                    runat="server"
                    CssClass="Grid"
                    AutoGenerateColumns="false"
                    OnRowCommand="gridViewReferences_RowCommand">
                    <Columns>
                        <asp:BoundField
                            DataField="ID"
                            HeaderText="ID"
                            ItemStyle-CssClass="hiddenCol"
                            HeaderStyle-CssClass="hiddenCol" />
                        <asp:BoundField
                            DataField="Author"
                            HeaderText="Author" />
                        <asp:BoundField
                            DataField="Year"
                            HeaderText="Year" />
                        <asp:BoundField
                            DataField="Title"
                            HeaderText="Title" />
                        <asp:BoundField
                            DataField="URL"
                            HeaderText="URL" />
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:LinkButton
                                    ID="lbEdit"
                                    CommandArgument='<%# Eval("ID") %>'
                                    CommandName="EditRow"
                                    runat="server" OnClientClick="OpenOverlay();">Edit</asp:LinkButton>
                                <asp:LinkButton ID="lbDelete"
                                    CommandArgument='<%# Eval("ID") %>'
                                    CommandName="DeleteRow"
                                    runat="server"
                                    CausesValidation="false">Delete</asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>

        </div>
        <div class="fullBox">
            <div class="halfBox">
                <p style="color: white;">.</p>
            </div>
            <div class="halfBox">
                <asp:Button
                    ID="btnUpdate"
                    runat="server"
                    CssClass="itsr-button ref-btn"
                    Text="Save Changes"
                    OnClick="btnUpdate_Click"
                    ValidationGroup="Required" />
            </div>
        </div>
        <asp:HiddenField ID="hiddenArticleID" runat="server" />
    </div>

</asp:Content>
