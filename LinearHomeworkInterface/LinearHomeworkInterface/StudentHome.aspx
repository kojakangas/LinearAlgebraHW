﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StudentHome.aspx.cs" Inherits="LinearHomeworkInterface.StudentHome" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
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
    <div class="container">
        <!--/possibly add class="container" -->
        <div class="navbar-inner" style="position: fixed; width: 900px; z-index: 1000;">
			<div class="nav-collapse collapse">
				<ul class="nav" style="float: left; margin: 10px 0 0px 0;">
					<li style="float: left; padding: 0 20px 0 0;"><a href="/StudentHome.aspx">Home</a></li>
				</ul>
				<div class="navbar-form pull-right">
					<button id="signOut" class="btn" style="margin-top: 5px;">Sign Out</button>
				</div>
			</div><!--/.nav-collapse -->
		</div>
        <div id="content" style="padding-top:50px;">
            <div id="header">
                <asp:Literal runat="server" ID="headerltData"></asp:Literal>
            </div>

            <div id="table" style="box-shadow: 2px 2px 6px #666666; border-radius: 5px;">
                <table id="assignmentTable" class="dataTable">
                    <thead>
                        <tr>
                            <th style="text-align: left;">Assignment</th>
                            <th style="text-align: center; width: 200px;">Due Date</th>
                            <th style="text-align: center; width: 50px;">Grade</th>
                            <th style="text-align: center; width: 75px;">Status</th>
                        </tr>
                    </thead>
                    <tbody>
                        <asp:Literal runat="server" ID="ltData"></asp:Literal>
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
        $('#assignmentTable').dataTable({
            "bJQueryUI": true,
            "bSort": false,
            "bFilter": true,
            'sPaginationType': 'full_numbers',
            "bAutoWidth": false
        });
    });
</script>
</body>
</html>