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
                    <asp:TextBox ID="tbUserName" placeholder="Username" runat="server" CssClass="txt-box txt-box-editprofile"></asp:TextBox>
                    <asp:TextBox ID="tbEmail" placeholder="Email" runat="server" CssClass="txt-box txt-box-editprofile"></asp:TextBox><br />
                    <div class="PasswordDiv">
                        <asp:TextBox ID="tbOldPassword" placeholder="Old Password" runat="server" CssClass="txt-box txt-box-editprofile" type="password"></asp:TextBox>
                        <asp:TextBox ID="tbPassword" placeholder="New Password" runat="server" CssClass="txt-box txt-box-editprofile" type="password"></asp:TextBox>
                        <asp:TextBox ID="tbConfirmPassword" placeholder="Confirm New Password" runat="server" CssClass="txt-box txt-box-editprofile" type="password"></asp:TextBox>
                        <div class="fullBox">
                        <div class="halfBox"><input id="Button2" type="button" class="itsr-button itsr-button-editprofile" value="Cancel" onclick="hideDiv();" /></div>  
                        <div class="halfBox"><asp:Button ID="BtnNewPassword" runat="server" CssClass="itsr-button itsr-button-editprofile" Text="Save" OnClick="BtnNewPassword_Click" /></div>
                        </div>
                           
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
                    <asp:Button ID="BtnSave" CssClass="itsr-button itsr-button-editprofile" runat="server" Text="Save" OnClick="BtnSave_Click" />
                </div>
            </div>
    </div>
</asp:Content>