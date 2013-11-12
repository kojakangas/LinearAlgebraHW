<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Help.aspx.cs" Inherits="LinearHomeworkInterface.Help" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>User Instructions for HW System</title>
    <link href="theme/bootstrap.css" rel="stylesheet" media="screen" />
    <link href="theme/jquery.dataTables.css" rel="stylesheet" media="screen" />
    <link href="theme/jquery-ui-1.10.3.custom.css" rel="stylesheet" media="screen" />
    <script src="javascript/jquery.js"></script>
    <script src="javascript/bootstrap.js"></script>
    <script src="javascript/jquery-ui-1.10.3.custom.min.js"></script>
    <script src="javascript/jquery.dataTables.min.js"></script>
    <style>
        body { 
            background: url(theme/images/PossibleCustomPage.png) no-repeat center center fixed; 
            -webkit-background-size: cover;
            -moz-background-size: cover;
            -o-background-size: cover;
            background-size: cover;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div id ="instructions" style ="display: none; background:silver; height: 200px; width: 400px; margin: auto; margin-top: 20px; box-shadow: 0px 0px 0px 8px rgba(0,0,0,0.3);">
    <p>Check this.</p>
    </div>
    </form>

    <div id="helpmenu" class="navbar-inner" style="display: none; position: absolute; width: 900px; z-index: 1000; bottom:20px; left:200px">
            <div class="nav-collapse collapse">
                <ul class="nav" style="float: left; margin: 10px 0 0px 0;">
                    <li style="float: left; padding: 0 20px 0 0;"><a href="StudentHome.aspx">Home</a></li>
                </ul>
                <form class="navbar-form pull-right" style="">
                    <button id="signOut" class="btn" style="margin-top: 5px;" type="submit">Sign Out</button>
                </form>
            </div>
        </div>
    <script type ="text/javascript">
        $(document).ready(function () {
            $('#instructions').delay("500").centerhorizontal().show("clip", "swing", 1000);
            $('#helpmenu').centerhorizontal().show("clip", "swing", 1000);
        });

        jQuery.fn.centerhorizontal = function () {
            this.css("position", "absolute");
            this.css("left", Math.max(0, (($(window).width() - $(this).outerWidth()) / 2) +
                                                        $(window).scrollLeft()) + "px");
            return this;
        }
    </script>
</body>
</html>
