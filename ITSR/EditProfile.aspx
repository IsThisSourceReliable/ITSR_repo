<%@ Page Title="" Language="C#" MasterPageFile="~/MainMaster.Master" AutoEventWireup="true" CodeBehind="EditProfile.aspx.cs" Inherits="ITSR.EditProfile" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="CSS/EditProfileCSS.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentHolder" runat="server">
    <div class="fullBox white-box">
        <div class="fullBox"><h2>Edit Your Profile</h2></div>
        <div class="fullBox">
        <div class="halfBox">
        <asp:TextBox ID="tbUserName" placeholder="Username" runat="server" CssClass="txt-box txt-box-editprofile"></asp:TextBox>
        <asp:TextBox ID="tbEmail" placeholder="Email" runat="server" CssClass="txt-box txt-box-editprofile"></asp:TextBox>
        <asp:TextBox ID="tbPassword" placeholder="Password" runat="server" CssClass="txt-box txt-box-editprofile"></asp:TextBox>
        <asp:TextBox ID="tbConfirmPassword" placeholder="Confirm Password" runat="server" CssClass="txt-box txt-box-editprofile"></asp:TextBox>
        </div>
        <div class="halfBox">
        <asp:TextBox ID="tbFirstName" placeholder="First Name" runat="server" CssClass="txt-box txt-box-editprofile"></asp:TextBox>
        <asp:TextBox ID="tbLastName" placeholder="Last Name" runat="server" CssClass="txt-box txt-box-editprofile"></asp:TextBox>
        <asp:TextBox ID="tbCountry" placeholder="Country" runat="server" CssClass="txt-box txt-box-editprofile"></asp:TextBox>
        <asp:TextBox ID="tbLocation" placeholder="Location" runat="server" CssClass="txt-box txt-box-editprofile"></asp:TextBox>
        <asp:TextBox ID="tbOccupation" placeholder="Occupation" runat="server" CssClass="txt-box txt-box-editprofile"></asp:TextBox>
        <asp:TextBox ID="tbAboutMe" placeholder="About me" TextMode="MultiLine" runat="server" CssClass="txt-box txt-box-editprofile"></asp:TextBox>      
        <asp:Button ID="BtnSave" CssClass="itsr-button itsr-button-editprofile" runat="server" Text="Save" />
        </div>
        </div>
    </div>
</asp:Content>
