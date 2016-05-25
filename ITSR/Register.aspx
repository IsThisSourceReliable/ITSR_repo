<%@ Page Title="" Language="C#" MasterPageFile="~/MainMaster.Master" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="ITSR.Register" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="CSS/RegisterCSS.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentHolder" runat="server">
    <div class="fullBox white-box">
        <!-------- REGISTER AREA ------>
        <div class="fullBox">
            <div class="padding paddingFix"><h2>Register</h2></div>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate> 
            <div class="halfBox padding paddingFix">
                <asp:RequiredFieldValidator 
                    ID="RequiredFieldValidator1" 
                    ControlToValidate="tbUserName" 
                    ForeColor ="Red" 
                    runat="server" 
                    Font-Size="Medium" 
                    ErrorMessage="You forgot this!" 
                    display="Dynamic"
                    ValidationGroup="RegisterGroup" >
                </asp:RequiredFieldValidator>
                <asp:CustomValidator
                    ID="CustomValidator1"
                    ForeColor="Red"
                    runat="server"
                    Font-Size="Medium"
                    display="Dynamic"
                    ErrorMessage="Username is already taken!">
                    </asp:CustomValidator>
                <asp:TextBox 
                    ID="tbUserName" 
                    CssClass="tb paddingFix marginBot" 
                    placeholder="Username" 
                    runat="server">
                </asp:TextBox>
                <asp:RequiredFieldValidator 
                    ID="RequiredFieldValidator2" 
                    ControlToValidate="tbEmail" 
                    ForeColor ="Red" 
                    runat="server" 
                    Font-Size="Medium" 
                    ErrorMessage="You forgot this!" 
                    display="Dynamic"
                    ValidationGroup="RegisterGroup">
                </asp:RequiredFieldValidator>
                <asp:CustomValidator
                    ID="CustomValidator2"
                    ForeColor="Red"
                    runat="server"
                    Font-Size="Medium"
                    display="Dynamic"
                    ErrorMessage="Email is already registered!">
                    </asp:CustomValidator>
                <asp:RegularExpressionValidator 
	                ID="regExValidatorEmail" 
	                ForeColor="Red" 
                	Font-Size="Medium" 
                	ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" 
                	ControlToValidate="tbEmail" 
                	runat="server" 
                	ErrorMessage="Hey! Thats not an Email!"
                	display="Dynamic"
                	ValidationGroup="RegisterGroup"> 
                </asp:RegularExpressionValidator>
                <asp:TextBox 
                    ID="tbEmail" 
                    CssClass="tb paddingFix marginBot" 
                    placeholder="Email" 
                    runat="server">
                </asp:TextBox>
                <asp:RequiredFieldValidator 
                    ID="RequiredFieldValidator3" 
                    ControlToValidate="tbPassword" 
                    ForeColor ="Red" 
                    runat="server" 
                    Font-Size="Medium" 
                    ErrorMessage="You forgot this!" 
                    display="Dynamic"
                    ValidationGroup="RegisterGroup">
                </asp:RequiredFieldValidator>
                <asp:TextBox 
                    ID="tbPassword" 
                    CssClass="tb paddingFix marginBot" 
                    placeholder="Password" 
                    runat="server" 
                    TextMode="Password">
                </asp:TextBox>
                <asp:RequiredFieldValidator 
                    ID="RequiredFieldValidator4" 
                    ControlToValidate="tbConfirmPassword" 
                    ForeColor ="Red" 
                    runat="server" 
                    Font-Size="Medium" 
                    ErrorMessage="You forgot this!" 
                    display="Dynamic"
                    ValidationGroup="RegisterGroup">
                </asp:RequiredFieldValidator>
                <asp:CompareValidator 
	                ID="RequiredFieldValidator5" 
	                ControlToValidate="tbPassword"
	                ControlToCompare="tbConfirmPassword"
	                ForeColor ="Red" runat="server" 
	                Font-Size="Medium" 
	                ErrorMessage="Passwords does not match!" 
	                display="Dynamic"
	                ValidationGroup="RegisterGroup">
                </asp:CompareValidator>
                <asp:TextBox 
                    ID="tbConfirmPassword" 
                    CssClass="tb paddingFix marginBot" 
                    placeholder="Confirm password" 
                    runat="server" 
                    TextMode="Password">
                </asp:TextBox>
 
                <asp:Button 
                    ID="btnRegister" 
                    CssClass="Mainbtn" 
                    runat="server" 
                    Text="Register"
                    ValidationGroup="RegisterGroup"
                    OnClick="btnRegister_Click" />
            </div>
            </ContentTemplate>     
            </asp:UpdatePanel>
            <!---- Welcome text or whatever ---->
            <div class="halfBox padding paddingFix">
                <h3>Welcome</h3><br />
                <p>"Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum."</p>
            </div>
        </div>
    </div>
</asp:Content>
