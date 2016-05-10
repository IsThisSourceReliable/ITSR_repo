<%@ Page Title="" Language="C#" MasterPageFile="~/MainMaster.Master" AutoEventWireup="true" CodeBehind="Profile.aspx.cs" Inherits="ITSR.Profile" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="CSS/ProfileCSS.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentHolder" runat="server">

    <div class="fullBox white-box white-box-profile">
        <div class="fullBox center-text"><h2><asp:Label ID="lbluserName" runat="server" Text=""></asp:Label></h2></div>
        <div class="thirdBox">
            <img src="IMG/profilepic.jpg" class="profilepic" />
        </div>
        <div class="thirdBox">
            <h3><asp:Label ID="lblFullNAme" runat="server" Text="FullName"></asp:Label></h3>
            <h3><asp:Label ID="lblCountry" runat="server" Text="Country"></asp:Label></h3>
            <h3><asp:Label ID="lblLocation" runat="server" Text="Location"></asp:Label></h3>
            <h3><asp:Label ID="lblOccupation" runat="server" Text="Occupation"></asp:Label></h3>
            <h3><asp:Label ID="lblAboutme" runat="server" Text="Aboutme"></asp:Label></h3>
        </div>
        <div class="thirdBox thirdBox-floatright">
            <div class="fullBox">        
            <h3 class="underline"><asp:Label ID="lblAdded" runat="server" Text="Added"></asp:Label></h3>
            <asp:GridView ID="gvMyArticles" runat="server" AutoGenerateColumns="false" CssClass="gridview" GridLines="None" DataKeyNames="idarticle" OnRowDataBound="gvMyArticles_RowDataBound" OnSelectedIndexChanged="gvMyArticles_SelectedIndexChanged">
                <Columns>
                    <asp:BoundField DataField="title"/>
                    <asp:CommandField ShowSelectButton="true" ControlStyle-CssClass="OPEN-btn" SelectText="OPEN"/>
                </Columns>
            </asp:GridView>
            </div>
            <div class="fullBox">  
            <h3 class="underline"><asp:Label ID="lblVotes" runat="server" Text="Votes"></asp:Label></h3>          
            <asp:GridView ID="gvMyVotes" runat="server" AutoGenerateColumns="false" CssClass="gridview" GridLines="None" DataKeyNames="idarticle" OnRowDataBound="gvMyVotes_RowDataBound" OnSelectedIndexChanged="gvMyVotes_SelectedIndexChanged">
                <Columns>
                    <asp:BoundField DataField="title"/>
                    <asp:CommandField ShowSelectButton="true" ControlStyle-CssClass="OPEN-btn" SelectText="OPEN"/>
                </Columns>
            </asp:GridView>
            </div>
            <div class="fullBox"> 
            <h3 class="underline"><asp:Label ID="lblComments" runat="server" Text="Comments"></asp:Label></h3>           
            <asp:GridView ID="gvMyComments" runat="server" AutoGenerateColumns="false" CssClass="gridview" GridLines="None" DataKeyNames="idarticle" OnRowDataBound="gvMyComments_RowDataBound" OnSelectedIndexChanged="gvMyComments_SelectedIndexChanged">
                <Columns>
                    <asp:BoundField DataField="title"/>
                    <asp:CommandField ShowSelectButton="true" ControlStyle-CssClass="OPEN-btn" SelectText="OPEN"/>
                </Columns>
            </asp:GridView>
            </div>
         </div>      
    </div>
</asp:Content>
