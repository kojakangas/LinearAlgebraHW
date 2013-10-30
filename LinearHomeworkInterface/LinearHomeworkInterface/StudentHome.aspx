﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StudentHome.aspx.cs" Inherits="LinearHomeworkInterface.StudentHome" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="theme/bootstrap.css" rel="stylesheet" media="screen" />
    <link href="theme/jquery.dataTables.css" rel="stylesheet" media="screen" />
    <link href="theme/jquery-ui-1.10.3.custom.css" rel="stylesheet" media="screen" />
    <script src="http://code.jquery.com/jquery.js"></script>
    <script src="javascript/bootstrap.js"></script>
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
				<div class="navbar-form pull-right">
					<button id="signOut" class="btn" style="margin-top: 5px;">Sign Out</button>
				</div>
			</div><!--/.nav-collapse -->
		</div>
        <div id="content" style="padding-top:50px;">
            <div id="header">
                <h1>Welcome, Student</h1>
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
<script type="text/javascript">

    $(document).ready(function () {

        $("#signOut").click(function (e) {
            e.preventDefault();
            $.ajax({
                type: "POST",
                url: "Default.aspx/SignOut",
                data: "",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    window.location = "Default.aspx";
                },
                error: function (msg) {
                    alert("Sign Out Failed!");
                }
            });
        });
    });
</script>
</body>
</html>
