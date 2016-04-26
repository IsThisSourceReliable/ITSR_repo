<%@ Page Title="" Language="C#" MasterPageFile="~/MainMaster.Master" AutoEventWireup="true" CodeBehind="CreateArticle.aspx.cs" Inherits="ITSR.CreateArticle" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="CSS/CreateArticleCSS.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentHolder" runat="server">
    <div class="fullBox white-box">
        <div class="fullBox">
            <h2 class="ca-heading">Create A Source</h2>
        </div>
        <div class="fullBox">
            <div class="halfBox">
                <p class="ca-text">Give a brief explanation of the source.</p>
                <asp:TextBox ID="txtInfo"
                    runat="server"
                    TextMode="MultiLine"
                    CssClass="multiline-txt-box ca-multi-txt">
                </asp:TextBox>
            </div>
            <div class="halfBox">
                <div class="fullBox">
                    <h4 class="title-h4">Type of organisation: </h4>
                    <asp:DropDownList
                        ID="dropDownTypeOfOrg"
                        runat="server"
                        CssClass="DropDown ca-dropdown">
                    </asp:DropDownList>
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

            </div>
        </div>
        <div class="fullBox">
            <div class="fullBox">
                <h4 class="title-h4"><strong>References</strong></h4>
            </div>
            <div class="halfBox">
                <asp:GridView
                    ID="gridViewReferences"
                    runat="server" AutoGenerateColumns="false" EmptyDataText="No references, add some references!" OnRowCommand="gridViewReferences_RowCommand">
                    <Columns>
                        <asp:BoundField DataField="ID" HeaderText="ID" ItemStyle-Width="200" /> <%--TO BE HIDDEN --%>
                        <asp:BoundField DataField="Author" HeaderText="Author" ItemStyle-Width="200" />
                        <asp:BoundField DataField="Year" HeaderText="Year" ItemStyle-Width="200" />
                        <asp:BoundField DataField="Title" HeaderText="Title" ItemStyle-Width="200" />
                        <%--                        <asp:BoundField DataField="TypeOfReference" HeaderText="TypeOfReference" ItemStyle-Width="200"/>--%>
                        <asp:BoundField DataField="URL" HeaderText="URL" ItemStyle-Width="200" />
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:LinkButton ID="lbEdit" CommandArgument='<%# Eval("Author") %>' CommandName="EditRow" ForeColor="#8C4510" runat="server">Edit</asp:LinkButton>
                                <asp:LinkButton ID="lbDelete" CommandArgument='<%# Eval("Author") %>' CommandName="DeleteRow" ForeColor="#8C4510" runat="server" CausesValidation="false">Delete</asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
                <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
                <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox>
                <asp:TextBox ID="TextBox4" runat="server"></asp:TextBox>
                <asp:TextBox ID="TextBox5" runat="server"></asp:TextBox>
                <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
                <asp:Button ID="Button2" runat="server" Text="Button" OnClick="Button2_Click" />
            </div>

            <div class="halfBox">
            </div>
        </div>
    </div>

</asp:Content>
