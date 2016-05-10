<%@ Page Title="" Language="C#" MasterPageFile="~/MainMaster.Master" AutoEventWireup="true" CodeBehind="Search.aspx.cs" Inherits="ITSR.Search" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="CSS/SearchCSS.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentHolder" runat="server">
    <div class="fullBox center-text">
        <div class="fullBox Search-search-box">
            <asp:TextBox 
                ID="txtSearchField" 
                runat="server" 
                CssClass="txt-box Search-search-field">                                        
            </asp:TextBox>
        </div>
        <div class="fullBox">
            <asp:LinkButton ID="linkBtnSearch" CssClass="itsr-button Search-search-button" runat="server"><span class="glyphicon glyphicon-search" style="font-size: 1em; vertical-align: text-bottom; font-weight: lighter;" aria-hidden="true"></span> SEARCH </asp:LinkButton>
        </div>
    </div>
    <div class="fullBox white-box Search-gridview-box">
        <asp:UpdatePanel runat="server">
            <ContentTemplate>
            <asp:GridView ID="gvArticles" runat="server" AutoGenerateColumns="false" CssClass="gridview" GridLines="None" DataKeyNames="idarticle" OnSelectedIndexChanged="gvArticles_SelectedIndexChanged" OnRowDataBound="gvArticles_RowDataBound">
                <Columns>
                    <asp:BoundField DataField="title" HeaderText="Title"/>
                    <asp:BoundField DataField="publisher" HeaderText="Publisher"/>
                    <asp:BoundField DataField="domainowner" HeaderText="Downain owner"/>
                    <asp:CommandField ShowSelectButton="true" ControlStyle-CssClass="OPEN-btn" SelectText="OPEN"/>
                </Columns>
            </asp:GridView>
            </ContentTemplate>        
        </asp:UpdatePanel>
    </div>
</asp:Content>
