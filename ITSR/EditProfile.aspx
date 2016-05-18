<%@ Page Title="" Language="C#" MasterPageFile="~/MainMaster.Master" AutoEventWireup="true" CodeBehind="EditProfile.aspx.cs" Inherits="ITSR.EditProfile" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="CSS/EditProfileCSS.css" rel="stylesheet" />
    <script src="JS/EditProfileJS.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentHolder" runat="server">
    <div class="fullBox white-box">
        <div class="fullBox"><h2>Edit Your Profile</h2></div>
            <div class="fullBox">
                <div class="halfBox">
                    <asp:RequiredFieldValidator 
	                    ID="RequiredFieldValidator2" 
	                    ControlToValidate="tbEmail" 
	                    ForeColor ="Red" runat="server" 
	                    Font-Size="Medium" 
	                    ErrorMessage="This can not be empty" 
	                    display="Dynamic"
	                    ValidationGroup="SaveProfile"
                        CssClass="val">
                    </asp:RequiredFieldValidator>
                    <asp:TextBox ID="tbEmail" placeholder="Email" runat="server" CssClass="txt-box txt-box-editprofile"></asp:TextBox><br />
                    <div class="PasswordDiv">
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                        <asp:RequiredFieldValidator 
    	                    ID="RequiredFieldValidator3" 
    	                    ControlToValidate="tbOldPassword" 
    	                    ForeColor ="Red" runat="server" 
    	                    Font-Size="Medium" 
    	                    ErrorMessage="This can not be empty" 
    	                    display="Dynamic"
    	                    ValidationGroup="SavePassword"
                            CssClass="val">
                        </asp:RequiredFieldValidator>
                        <asp:CustomValidator
                        	ID="WrongPassWordValidator"
                        	ForeColor="Red"
                        	runat="server"
                        	Font-Size="Medium"
                        	display="Dynamic"
                        	ErrorMessage="Wrong password!"
                            CssClass="val">
                        </asp:CustomValidator>
                        <asp:TextBox ID="tbOldPassword" placeholder="Old Password" runat="server" CssClass="txt-box txt-box-editprofile" type="password"></asp:TextBox>
                        <asp:RequiredFieldValidator 
	                        ID="RequiredFieldValidator4" 
	                        ControlToValidate="tbPassword" 
	                        ForeColor ="Red" runat="server" 
	                        Font-Size="Medium" 
	                        ErrorMessage="This can not be empty" 
	                        display="Dynamic"
	                        ValidationGroup="SavePassword"
                            CssClass="val">
                        </asp:RequiredFieldValidator>
                        <asp:TextBox ID="tbPassword" placeholder="New Password" runat="server" CssClass="txt-box txt-box-editprofile" type="password"></asp:TextBox>
                        <asp:RequiredFieldValidator 
	                        ID="RequiredFieldValidator5" 
	                        ControlToValidate="tbConfirmPassword" 
	                        ForeColor ="Red" runat="server" 
	                        Font-Size="Medium" 
	                        ErrorMessage="This can not be empty" 
	                        display="Dynamic"
	                        ValidationGroup="SavePassword"
                            CssClass="val">
                        </asp:RequiredFieldValidator>
                        <asp:CompareValidator 
                        	ID="CompareValidator1" 
                        	ControlToValidate="tbPassword"
                        	ControlToCompare="tbConfirmPassword"
                        	ForeColor ="Red" runat="server" 
                        	Font-Size="Medium" 
                        	ErrorMessage="Passwords does not match!" 
                        	display="Dynamic"
                        	ValidationGroup="SavePassword"
                            CssClass="val">
                        </asp:CompareValidator>                        
                        <asp:TextBox ID="tbConfirmPassword" placeholder="Confirm New Password" runat="server" CssClass="txt-box txt-box-editprofile" type="password"></asp:TextBox>
                        <div class="fullBox">
                        <div class="halfBox"><input id="Button2" type="button" class="itsr-button itsr-button-editprofile" value="Cancel" onclick="hideDiv();" /></div>  
                        <div class="halfBox"><asp:Button ID="BtnNewPassword" runat="server" CssClass="itsr-button itsr-button-editprofile" Text="Save" OnClick="BtnNewPassword_Click" ValidationGroup="SavePassword" /></div>
                        </div>
                        </ContentTemplate>
                        </asp:UpdatePanel>                          
                    </div>                    
                    <input id="Button1" type="button" value="New Password" class="itsr-button itsr-button-editprofile" onclick="showDiv();" />
                </div>
                <div class="halfBox">
                    <asp:TextBox ID="tbFirstName" placeholder="First Name" runat="server" CssClass="txt-box txt-box-editprofile"></asp:TextBox>
                    <asp:TextBox ID="tbLastName" placeholder="Last Name" runat="server" CssClass="txt-box txt-box-editprofile"></asp:TextBox>
                    <asp:TextBox ID="tbCountry" placeholder="Country" runat="server" CssClass="txt-box txt-box-editprofile"></asp:TextBox>
                    <asp:TextBox ID="tbLocation" placeholder="Location" runat="server" CssClass="txt-box txt-box-editprofile"></asp:TextBox>
                    <asp:TextBox ID="tbOccupation" placeholder="Occupation" runat="server" CssClass="txt-box txt-box-editprofile"></asp:TextBox>
                    <asp:TextBox ID="tbAboutMe" placeholder="About me" TextMode="MultiLine" runat="server" CssClass="txt-box txt-box-editprofile"></asp:TextBox>      
                    <asp:Button ID="BtnSave" CssClass="itsr-button itsr-button-editprofile" runat="server" Text="Save" OnClick="BtnSave_Click" ValidationGroup="SaveProfile" />
                </div>
            </div>
    </div>
</asp:Content>