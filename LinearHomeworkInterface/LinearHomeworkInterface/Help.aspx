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

    <script type ="text/javascript">
        $(document).ready(function () {
            $('#instructions').centerhorizontal().show("clip", "swing", 1000);
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
