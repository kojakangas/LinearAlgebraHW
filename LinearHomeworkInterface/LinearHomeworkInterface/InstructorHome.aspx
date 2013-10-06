<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="InstructorHome.aspx.cs" Inherits="LinearHomeworkInterface.InstructorHome" %>

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
            $('.dataTable-assignment').dataTable({
                "bSort": false,
                "bFilter": false,
                'sPaginationType': 'full_numbers',
                "bAutoWidth": false,
                "bJQueryUI": true
            });

            $('.dataTable-student').dataTable({
                "bSort": false,
                "bFilter": false,
                'sPaginationType': 'full_numbers',
                "bAutoWidth": false,
                "bJQueryUI": true
            });
        });
    </script>
    <form id="form1" runat="server">
        <div class="container">
            <!--/possibly add class="container" -->
            <div class="navbar-inner" style="position: fixed; width: 900px; z-index: 1000;">
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
                    <div class="navbar-form pull-right" style="">
                        <button class="btn" style="margin-top: 5px;" type="submit">Sign Out</button>
                    </div>
                </div>
                <!--/.nav-collapse -->
            </div>

            <div id="content" style="padding-top: 50px;">
                <div id="header">
                    <h1>Instructor Home</h1>

                    <a href="createAssignmentPage.html">Create New Assignment</a>
                </div>

                <div class="span6" style="margin-left: 5px;">
                    <h3>Assignments</h3>
                    <div id="table" style="box-shadow: 2px 2px 6px #666666; border-radius: 5px;">
                        <table class="dataTable-assignment">
                            <thead>
                                <tr>
                                    <th>Assignment</th>
                                    <th>Status</th>
                                </tr>
                            </thead>
                            <tbody>
                                <tr>
                                    <td>Homework 1</td>
                                    <td style="text-align: center;">Completed</td>
                                </tr>
                                <tr>
                                    <td><a href="#">Homework 2</a></td>
                                    <td style="text-align: center;">Assigned</td>
                                </tr>
                                <tr>
                                    <td><a href="#">Homework 3</a></td>
                                    <td style="text-align: center;">Not Assigned</td>
                                </tr>
                            </tbody>
                        </table>
                    </div>
                </div>

                <div class="span6" style="margin-left: 10px; margin-right: 5px;">
                    <h3>Students</h3>
                    <div id="Div1" style="box-shadow: 2px 2px 6px #666666; border-radius: 5px;">
                        <table class="dataTable-student">
                            <thead>
                                <tr>
                                    <th style="text-align: left;">Name</th>
                                </tr>
                            </thead>
                            <tbody>
                                <tr>
                                    <td><a>Brendan Birdsong</a></td>
                                </tr>
                                <tr>
                                    <td><a>Kieran Ojakangas</a></td>
                                </tr>
                                <tr>
                                    <td><a>Tyler Jenkins</a></td>
                                </tr>
                            </tbody>
                        </table>
                    </div>
                </div>

            </div>
        </div>
    </form>
</body>
</html>
