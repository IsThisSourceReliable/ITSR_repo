<%@ Page Title="" Language="C#" MasterPageFile="~/MainMaster.Master" AutoEventWireup="true" CodeBehind="EditProfile.aspx.cs" Inherits="ITSR.EditProfile" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="CSS/EditProfile/btnEditProfile.css" rel="stylesheet" />
    <link href="CSS/EditProfile/displaynoneEditProfile.css" rel="stylesheet" />
    <link href="CSS/EditProfile/marginEditProfile.css" rel="stylesheet" />
    <link href="CSS/EditProfile/tbEditProfile.css" rel="stylesheet" />
    <link href="CSS/EditProfile/paddingEditProfile.css" rel="stylesheet" />

    <script src="JS/EditProfileJS.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentHolder" runat="server">
    <div class="fullBox white-box">
        <div class="fullBox"><h2 class="margin">Edit Your Profile</h2></div>
            <div class="fullBox">
                <div class="halfBox padding paddingFix">
                    <asp:RequiredFieldValidator 
	                    ID="RequiredFieldValidator2" 
	                    ControlToValidate="tbEmail" 
	                    ForeColor ="Red" runat="server" 
	                    Font-Size="Medium" 
	                    ErrorMessage="This can not be empty" 
	                    display="Dynamic"
	                    ValidationGroup="SaveProfile"
                        CssClass="marginLeft">
                    </asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator 
	                    ID="regExValidatorEmail" 
	                    ForeColor="Red" 
                	    Font-Size="Medium" 
                	    ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" 
                	    ControlToValidate="tbEmail" 
                	    runat="server" 
                	    ErrorMessage="Hey! Thats not an Email!"
                	    display="Dynamic"
                	    ValidationGroup="SaveProfile"
                        CssClass="marginLeft"> 
                    </asp:RegularExpressionValidator>
                    <asp:CustomValidator
                         ID="CustomValidator2"
                         ForeColor="Red"
                         runat="server"
                         Font-Size="Medium"
                         display="Dynamic"
                         ErrorMessage="Email is already registered!"
                         CssClass="marginLeft">
                     </asp:CustomValidator>
                    <asp:TextBox ID="tbEmail" placeholder="Email" runat="server" CssClass="tb paddingFix"></asp:TextBox><br />
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
                            CssClass="marginLeft">
                        </asp:RequiredFieldValidator>
                        <asp:CustomValidator
                        	ID="WrongPassWordValidator"
                        	ForeColor="Red"
                        	runat="server"
                        	Font-Size="Medium"
                        	display="Dynamic"
                        	ErrorMessage="Wrong password!"
                            CssClass="marginLeft">
                        </asp:CustomValidator>
                        <asp:TextBox ID="tbOldPassword" placeholder="Old Password" runat="server" CssClass="tb paddingFix marginBot" type="password"></asp:TextBox>
                        <asp:RequiredFieldValidator 
	                        ID="RequiredFieldValidator4" 
	                        ControlToValidate="tbPassword" 
	                        ForeColor ="Red" runat="server" 
	                        Font-Size="Medium" 
	                        ErrorMessage="This can not be empty" 
	                        display="Dynamic"
	                        ValidationGroup="SavePassword"
                            CssClass="marginLeft">
                        </asp:RequiredFieldValidator>
                        <asp:TextBox ID="tbPassword" placeholder="New Password" runat="server" CssClass="tb paddingFix marginBot" type="password"></asp:TextBox>
                        <asp:RequiredFieldValidator 
	                        ID="RequiredFieldValidator5" 
	                        ControlToValidate="tbConfirmPassword" 
	                        ForeColor ="Red" runat="server" 
	                        Font-Size="Medium" 
	                        ErrorMessage="This can not be empty" 
	                        display="Dynamic"
	                        ValidationGroup="SavePassword"
                            CssClass="marginLeft">
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
                            CssClass="marginLeft">
                        </asp:CompareValidator>                        
                        <asp:TextBox ID="tbConfirmPassword" placeholder="Confirm New Password" runat="server" CssClass="tb paddingFix" type="password"></asp:TextBox>
                        <div class="fullBox">
                        <div class="halfBox padding paddingFix"><input id="Button2" type="button" class="Mainbtn" value="Cancel" onclick="hideDiv();" /></div>  
                        <div class="halfBox padding paddingFix"><asp:Button ID="BtnNewPassword" runat="server" CssClass="Mainbtn" Text="Save" OnClick="BtnNewPassword_Click" ValidationGroup="SavePassword" /></div>
                        </div>
                        </ContentTemplate>
                        </asp:UpdatePanel>                          
                    </div>                    
                    <input id="Button1" type="button" value="New Password" class="Mainbtn" onclick="showDiv();" />
                </div>
                <div class="halfBox padding paddingFix">
                    <asp:TextBox ID="tbFirstName" placeholder="First Name" runat="server" CssClass="tb paddingFix marginBot"></asp:TextBox>
                    <asp:TextBox ID="tbLastName" placeholder="Last Name" runat="server" CssClass="tb paddingFix marginBot"></asp:TextBox>
                    <asp:TextBox ID="tbCountry" placeholder="Country" runat="server" CssClass="tb paddingFix marginBot"></asp:TextBox>
                    <asp:TextBox ID="tbLocation" placeholder="Location" runat="server" CssClass="tb paddingFix marginBot"></asp:TextBox>
                    <asp:TextBox ID="tbOccupation" placeholder="Occupation" runat="server" CssClass="tb paddingFix marginBot"></asp:TextBox>
                    <asp:TextBox ID="tbAboutMe" placeholder="About me" TextMode="MultiLine" runat="server" CssClass="tb paddingFix marginBot"></asp:TextBox>      
                    <asp:Button ID="BtnSave" CssClass="Mainbtn" runat="server" Text="Save" OnClick="BtnSave_Click" ValidationGroup="SaveProfile" />
                </div>
            </div>
    </div>
</asp:Content>