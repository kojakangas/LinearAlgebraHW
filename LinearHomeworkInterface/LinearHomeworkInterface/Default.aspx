<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="LinearHomeworkInterface.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="theme/bootstrap.css" rel="stylesheet" media="screen" />
    <link href="theme/jquery.dataTables.css" rel="stylesheet" media="screen" />
    <link href="theme/jquery-ui-1.10.3.custom.css" rel="stylesheet" media="screen" />
    <script src="http://code.jquery.com/jquery.js"></script>
    <script src="javascript/bootstrap.min.js"></script>
    <script src="javascript/jquery-ui-1.10.3.custom.min.js"></script>
    <script src="javascript/jquery.dataTables.min.js"></script>
</head>
<body>
    <script>
        $(document).ready(function () {
            $('.dataTable').dataTable({
                "bJQueryUI": true,
                "bSort": false,
                "bFilter": true,
                'sPaginationType': 'full_numbers',
                "bAutoWidth": false
            });
        });
    </script>
    <div class="container">
        <!--/possibly add class="container" -->
        <div class="navbar-inner">
            <div class="nav-collapse collapse">
                <ul class="nav" style="float: left; margin: 10px 0 0px 0;">
                    <li style="float: left; padding: 0 20px 0 0;"><a href="#">Home</a></li>
                    <li style="float: left; padding: 0 20px 0 0;"><a href="#about">About</a></li>
                    <li style="float: left; padding: 0 20px 0 0;"><a href="#contact">Contact</a></li>
                    <li class="dropdown" style="float: left; padding: 0 20px 0 0;">
                        <a data-toggle="dropdown" class="dropdown-toggle" href="#">Dropdown <b class="caret"></b></a>
                        <ul class="dropdown-menu">
                            <li><a href="#">Action</a></li>
                            <li><a href="#">Another action</a></li>
                            <li><a href="#">Something else here</a></li>
                            <li class="divider"></li>
                            <li class="nav-header">Nav header</li>
                            <li><a href="#">Separated link</a></li>
                            <li><a href="#">One more separated link</a></li>
                        </ul>
                    </li>
                </ul>
                <form class="navbar-form pull-right" style="">
                    <a class="btn" href="#" style="margin-top: 5px;" type="submit">Sign Out</a>
                </form>
            </div>
            <!--/.nav-collapse -->
        </div>
        <div id="content">
            <div id="header">
                <h1>Welcome, User user</h1>
            </div>

            <div id="table" style="box-shadow: 2px 2px 6px #666666; border-radius: 5px;">
                <table class="dataTable">
                    <thead>
                        <tr>
                            <th>Assignment</th>
                            <th>Due Date</th>
                            <th>Grade</th>
                            <th>Status</th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr>
                            <td>Homework 1</td>
                            <td style="text-align: center;">09/03/2013</td>
                            <td style="text-align: center;">8/10</td>
                            <td style="text-align: center;">Complete</td>
                        </tr>
                        <tr>
                            <td><a href="QuestionPage.aspx">Homework 2</a></td>
                            <td style="text-align: center;">09/05/2013</td>
                            <td style="text-align: center;">--/10</td>
                            <td style="text-align: center;">Not Started</td>
                        </tr>
                        <tr>
                            <td><a href="#">Homework 3</a></td>
                            <td style="text-align: center;">09/05/2013</td>
                            <td style="text-align: center;">--/10</td>
                            <td style="text-align: center;">Not Started</td>
                        </tr>
                    </tbody>
                </table>
            </div>
        </div>
    </div>
</body>
</html>
