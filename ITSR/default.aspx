<%@ Page Title="" Language="C#" MasterPageFile="~/MainMaster.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="ITSR.testdef" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <link href="CSS/DefaultCSS.css" rel="stylesheet" />

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentHolder" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="fullBox center-text">
                <div class="fullBox def-title-box ">
                    <h1 class="def-title">IsThisSourceReliable.com</h1>
                </div>

                <asp:Panel ID="Panel1" runat="server" DefaultButton="linkBtnSearch">
                    <asp:RequiredFieldValidator
                        ID="RequiredFieldValidator1"
                        ControlToValidate="txtSearchField"
                        ForeColor="Red" runat="server"
                        Font-Size="Medium"
                        ErrorMessage=""
                        Display="Dynamic"
                        ValidationGroup="SearchGroup">
                    </asp:RequiredFieldValidator>
                    <div class="fullBox def-search-box">
                        <asp:TextBox
                            ID="txtSearchField"
                            runat="server"
                            CssClass="txt-box def-search-field">                                        
                        </asp:TextBox>
                        <ajaxToolkit:AutoCompleteExtender
                            ID="AutoCompleteExtender1"
                            runat="server"
                            TargetControlID="txtSearchField"
                            ServiceMethod="GetArticles"
                            MinimumPrefixLength="1"
                            EnableCaching="false"
                            CompletionSetCount="1"
                            CompletionInterval="1000"
                            CompletionListCssClass="completionList"
                            CompletionListItemCssClass="listItem"
                            CompletionListHighlightedItemCssClass="itemHighlighted">
                        </ajaxToolkit:AutoCompleteExtender>
                    </div>
                    <div class="fullBox">
                        <asp:LinkButton ID="linkBtnSearch" ValidationGroup="SearchGroup" OnClick="linkBtnSearch_Click" CssClass="itsr-button def-search-button" runat="server"><span class="glyphicon glyphicon-search" style="font-size: 1em; vertical-align: text-bottom; font-weight: lighter;" aria-hidden="true"></span> SEARCH </asp:LinkButton>
                    </div>
                </asp:Panel>
            </div>
            </div>      
             <div class="fullBox white-box big-search-box" id="messageDiv" runat="server">
                 <div class="search-message-box center-text">
                     <p id="searchMessage" runat="server">
                         We could not find the source you were looking for. Click
                         <asp:LinkButton ID="lbNewARticle" runat="server" OnClick="lbNewARticle_Click">here</asp:LinkButton>
                         to add it!
                     </p>
                     <p id="searchMessage2" runat="server">
                         No? Click
                         <asp:LinkButton ID="lbNewARticle2" runat="server" OnClick="lbNewARticle_Click">here</asp:LinkButton>
                         to add it!
                     </p>
                 </div>
                 <div class="fullBox center-text">
                     <asp:ListView ID="lvSearchResult" OnItemCommand="lvSearchResult_ItemCommand" runat="server">
                         <ItemTemplate>
                             <div class="fullBox">
                                 <asp:LinkButton ID="lbArticle" CssClass="SearchResult border-bottom" CommandName="GoToArticle" CommandArgument='<%# Eval("idarticle") %>' runat="server">
                                     <asp:Label ID="lblTitle" runat="server" Text='<%# Eval("title") %>'></asp:Label>
                                     <asp:Label ID="lblSpace" runat="server" Text=" "></asp:Label>
                                     <asp:Label ID="lblUrl" runat="server" Text='<%# Eval("url") %>'></asp:Label>
                                 </asp:LinkButton>
                             </div>
                         </ItemTemplate>
                     </asp:ListView>
                 </div>
             </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
