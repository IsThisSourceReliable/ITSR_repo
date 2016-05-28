<%@ Page Title="" Language="C#" MasterPageFile="~/MainMaster.Master" AutoEventWireup="true" CodeBehind="Profile.aspx.cs" Inherits="ITSR.Profile" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="CSS/ProfileCSS.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentHolder" runat="server">

    <div class="fullBox white-box">
        <div class="fullBox center-text">
            <h2>
                <asp:Label ID="lbluserName" runat="server" Text=""></asp:Label></h2>
        </div>
        <div class="thirdBox padding paddingFix Profile">
            <img src="IMG/profilepic.jpg" style="width: 100%; border-radius: 10px;" />
        </div>
        <div class="thirdBox padding paddingFix Profile">
            <h3>
                <asp:Label ID="lblFullNAme" CssClass="border-bottom marginBot" runat="server" Text="FullName"></asp:Label>
            </h3>
            <h3>
                <asp:Label ID="lblCountry" CssClass="border-bottom marginBot" runat="server" Text="Country"></asp:Label>
            </h3>
            <h3>
                <asp:Label ID="lblLocation" CssClass="border-bottom marginBot" runat="server" Text="Location"></asp:Label>
            </h3>
            <h3>
                <asp:Label ID="lblOccupation" CssClass="border-bottom marginBot" runat="server" Text="Occupation"></asp:Label>
            </h3>
            <br />
            <h4>
                <asp:Label ID="lblAboutme" runat="server" Text="Aboutme"></asp:Label>
            </h4>
        </div>
        <div class="thirdBox padding paddingFix floatRight">
            <div class="fullBox marginBot minHeight">
                <div class="fullBox center-text border-bottom marginBot">
                    <h3>Created</h3>
                </div>
                <div>
                    <asp:CustomValidator
                        ID="NoCreatedArticles"
                        ForeColor="Black"
                        runat="server"
                        Font-Size="Medium"
                        Display="Dynamic"
                        ErrorMessage="This user has not yet created any articles">
                    </asp:CustomValidator>
                </div>
                <asp:ListView ID="lvCreatedArticles" OnItemDataBound="lvCreatedArticles_ItemDataBound" OnItemCommand="lvCreatedArticles_ItemCommand" runat="server">
                    <ItemTemplate>
                        <div class="fullBox marginBot center-text">
                            <asp:LinkButton ID="btnCreatedArticle" CssClass="ArticleLink" CommandArgument='<%# Eval("idarticle") %>' text='<%# Eval("title") %>'  CommandName="GoToArticle" runat="server"></asp:LinkButton>                           
                        </div>
                    </ItemTemplate>
                </asp:ListView>
            </div>
            <div class="fullBox marginBot minHeight">
                <div class="fullBox center-text border-bottom marginBot">
                    <h3>Commented</h3>
                </div>
                <div>
                    <asp:CustomValidator
                        ID="NoComments"
                        ForeColor="Black"
                        runat="server"
                        Font-Size="Medium"
                        Display="Dynamic"
                        ErrorMessage="This user has not yet commented on any articles">
                    </asp:CustomValidator>
                </div>
                <asp:ListView ID="lvLastCommented" OnItemDataBound="lvLastCommented_ItemDataBound" OnItemCommand="lvLastCommented_ItemCommand" runat="server">
                    <ItemTemplate>
                        <div class="fullBox marginBot center-text">
                            <asp:LinkButton ID="btnLastCommented" CssClass="ArticleLink" CommandArgument='<%# Eval("idarticle") %>' text='<%# Eval("title") %>'  CommandName="GoToArticle" runat="server"></asp:LinkButton> 
                        </div>
                    </ItemTemplate>
                </asp:ListView>
            </div>
            <div class="fullBox marginBot minHeight">
                <div class="fullBox center-text border-bottom marginBot">
                    <h3>Voted</h3>
                </div>
                <div>
                    <asp:CustomValidator
                        ID="NoVotes"
                        ForeColor="Black"
                        runat="server"
                        Font-Size="Medium"
                        Display="Dynamic"
                        ErrorMessage="This user has not yet voted on any articles">
                    </asp:CustomValidator>
                </div>
                <asp:ListView ID="lvLastVoted" OnItemDataBound="lvLastVoted_ItemDataBound" OnItemCommand="lvLastVoted_ItemCommand" runat="server">
                    <ItemTemplate>
                        <div class="fullBox marginBot center-text">
                            <asp:LinkButton ID="btnLastVoted" CssClass="ArticleLink" CommandArgument='<%# Eval("idarticle") %>' text='<%# Eval("title") %>'  CommandName="GoToArticle" runat="server"></asp:LinkButton> 
                        </div>
                    </ItemTemplate>
                </asp:ListView>
            </div>
        </div>
    </div>
</asp:Content>
