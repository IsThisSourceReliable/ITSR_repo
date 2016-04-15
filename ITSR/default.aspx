<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="ITSR._default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8"/>
    <meta http-equiv="X-UA-Compatible" content="IE=edge"/>
    <meta name="description" content="IS THIS SOURCE RELIABLE, find out if the source you are refering to is reliable"/>

    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <!--<link rel="shortcut icon" href="img/favicon.ico" type="image/x-icon"/>
    <link rel="icon" href="img/favicon.ico" type="image/x-icon"/>-->

    <meta name="author" content="Drysén, Erik. Gustavsson, Martin"/>

    <!-- CSS -->
    <link href="CSS/font.css" rel="stylesheet" />
    <link href="CSS/Style.css" rel="stylesheet" />

    <link href="http://code.jquery.com/ui/1.10.4/themes/ui-lightness/jquery-ui.css" rel="stylesheet" />
    <script src="http://code.jquery.com/jquery-1.10.2.js"></script>
    <script src="http://code.jquery.com/ui/1.10.4/jquery-ui.js"></script>  

    <title>IS THIS SOURCE RELIABLE</title>
    
    <script src="JS/DefaultJS.js"></script>

</head>
<body>
    <form id="defaultForm" runat="server">
        <div class="menu-left">
            <div class="fullBox left-menu-bottom">
                <h2 class="left-menu-title"><strong>MENU</strong></h2>
            </div>
                            <div>
                                <ul class="side-left-navbar">
                                    <li><p>ABOUT</p></li>                                    
                                    <li><p>LINK</p></li>   
                                    <li><p>LINK</p></li> 
                                    <li><p>LINK</p></li> 
                                </ul>
                            </div>  
        </div>

        <div class="page-wrap">
        <!-- Main page ================================================== -->
        <section class="container navbar-section">
          <div class="content default-page-width">
            <div class="contentRow">        
                <ul class="main-navbar">
                  <li id="LeftMenu" onclick="OpenLeftMenu();"><p>☰ MENU</p></li>                                              
                </ul> 
                <ul class="main-navbar right">
                  <li id="RightMenu" onclick="OpenRightMenu();"><p>LOG IN</p></li>                                              
                </ul>                          
            </div>
          </div>
        </section> 


        <!-- Main page ================================================== -->
        <section class="container books-background">
            <div class="blue-overlay">
                <div class="content default-page-width fill-default-page">
                    <div class="contentRow"> 
                            <div class="fullBox center-text">
                                <div class="fullBox def-title-box ">
                                    <h1>IsThisSourceReliable.com</h1>
                                </div>

                                <div class="fullBox def-search-box">
                                    <asp:TextBox 
                                        ID="txtSearchField" 
                                        runat="server" 
                                        CssClass="round-txt-box">                                        
                                    </asp:TextBox>
                                </div>
                                <div class="fullBox">
                                    <asp:Button 
                                        ID="btnSearch" 
                                        CssClass="round-button" 
                                        runat="server" 
                                        Text="SEARCH" />
                                </div>
                                <p onclick="CloseLeftMenu();">CLOSE LEFT</p>
                                <p onclick="CloseRightMenu();">CLOSE RIGHT</p>
                            </div>

                    </div>
                </div>
            </div>
        </section>

        </div>

        <div class="menu-right">
            <div class="fullBox left-menu-bottom">
                <h2 class="right-menu-title"><strong>LOG IN</strong></h2>
            </div>
            <p>HEJ</p>
            <p>HEJ</p>
            <p>HEJ</p>
            <p>HEJ</p>
            <p>HEJ</p>
            <p>HEJ</p>
            <p>HEJ</p>
            <p>HEJ</p>
            <p>HEJ</p>
        </div>
    </form>
</body>
</html>
