<%@ Page Title="" Language="C#" MasterPageFile="~/MainMaster.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="ITSR.testdef" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="CSS/DefaultCSS.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentHolder" runat="server">

    <div class="fullBox center-text">
        <div class="fullBox def-title-box ">
            <h1 class="def-title">IsThisSourceReliable.com</h1>
        </div>

        <div class="fullBox def-search-box">
            <asp:TextBox 
                ID="txtSearchField" 
                runat="server" 
                CssClass="txt-box def-search-field">                                        
            </asp:TextBox>
        </div>
        <div class="fullBox">
            <asp:Button 
                ID="btnSearch" 
                CssClass="itsr-button def-search-button" 
                runat="server" 
                Text="SEARCH" />
        </div>
    </div>

</asp:Content>
