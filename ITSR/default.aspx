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
            <h2><strong>MENU</strong></h2>
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

        <div class="page-wrap" style="position: fixed; min-width: 100%;">
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
                                <br />
                                                            <br />
                                                            <br />
                                                            <br />
                                                            <br />
                                                            <br />
                                                            <br />
                                <br />
                                <br />
                                                                                        <br /><br />
                                <p onclick="CloseLeftMenu();">CLOSE LEFT</p>
                                <p onclick="CloseRightMenu();">CLOSE RIGHT</p>
                            </div>

                    </div>
                </div>
            </div>
        </section>

        </div>

        <div class="menu-right">
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
