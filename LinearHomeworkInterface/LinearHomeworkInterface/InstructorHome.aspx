<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="InstructorHome.aspx.cs" Inherits="LinearHomeworkInterface.InstructorHome" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Instructor Home</title>
    <link href="theme/bootstrap.css" rel="stylesheet" media="screen" />
    <link href="theme/jquery.dataTables.css" rel="stylesheet" media="screen" />
    <link href="theme/jquery-ui-1.10.3.custom.css" rel="stylesheet" media="screen" />
    <script src="javascript/jquery.js"></script>
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
                "iDisplayLength": 50,
                'sPaginationType': 'full_numbers',
                "bAutoWidth": false,
                "bJQueryUI": true,
                "oLanguage": {
                    "sInfoEmpty": "",
                    "sEmptyTable": "No Assignments have been created"
                }
            });

            $('.dataTable-student').dataTable({
                "bSort": false,
                "bFilter": false,
                "iDisplayLength": 50,
                'sPaginationType': 'full_numbers',
                "bAutoWidth": false,
                "bJQueryUI": true,
                "oLanguage": {
                    "sInfoEmpty": "",
                    "sEmptyTable": "Select a student from the dropdown to view grades..."
                }
            });


            $('#studentNameDropdown').change(function () {
                $(".overlay").show();
                $.ajax({
                    type: "POST",
                    url: "InstructorHome.aspx/UpdateStudentGradeTable",
                    data: "{'UserID': '" + $("#studentNameDropdown").val() + "'}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (rows) {
                        $(".overlay").hide();
                        $('#studentGradeTable').dataTable().fnClearTable();
                        $.each(rows, function (index, value) {
                            if (value != null) {
                                $.each(value, function (i, j) {
                                    $('#studentGradeTable').dataTable().fnAddData([j[0], j[1], j[2]]);
                                })
                            }
                            else {
                                $('#studentGradeTable').dataTable().fnClearTable();
                                $('#studentGradeTable').dataTable().fnAddData(["No homework assignments found for student",""]);
                            }
                        })
                    },
                    error: function (msg) {
                        $(".overlay").hide();
                        alert("Could not retrieve student grades!");
                    }
                });
            });
        });

    </script>
    <form id="form1" runat="server">
        <div class="container">
            <!--/possibly add class="container" -->
            <div class="navbar-inner" style="position: fixed; width: 900px; z-index: 1000;">
                <div class="nav-collapse collapse">
                    <ul class="nav" style="float: left; margin: 10px 0 0px 0;">
                        <li style="float: left; padding: 0 20px 0 0;"><a href="InstructorHome.aspx">Home</a></li>
                    </ul>
                    <div class="navbar-form pull-right" style="">
                        <button id="signOut" class="btn" style="margin-top: 5px;" type="submit">Sign Out</button>
                    </div>
                </div>
                <!--/.nav-collapse -->
            </div>

            <div id="content" style="padding-top: 50px;">
                <div id="header">
                    <h1>Instructor Home</h1>

                    <a href="CreateAssignment.aspx">Create New Assignment</a>
                </div>

                <div class="span6" style="margin-left: 5px;">
                    <h3>Assignments</h3>
                    <div id="table" style="box-shadow: 2px 2px 6px #666666; border-radius: 5px;">
                        <table class="dataTable-assignment">
                            <thead>
                                <tr>
                                    <th style="width: 65%;">Assignment</th>
                                    <th style="width: 20%;">Due Date</th>
                                    <th style="width: 15%;"></th>
                                </tr>
                            </thead>
                            <tbody>
                                <asp:Literal runat="server" ID="ltData"></asp:Literal>
                            </tbody>
                        </table>
                    </div>
                </div>

                <div class="span6" style="margin-left: 10px; margin-right: 5px;">
                    <h3 style="margin-left: 10px; float: left;">Students</h3>
                    <select id="studentNameDropdown" style="margin-top: 15px; float: right;">
			            <option value="0" selected="selected" disabled="disabled">-- Select Student --</option>
                        <asp:Literal runat="server" ID="StudentListLiteral"></asp:Literal>
		            </select>
		            <div class="span6" style="margin-left: 10px; margin-right: 5px;">
                    <div id="Div1" style="box-shadow: 2px 2px 6px #666666; border-radius: 5px;">
                        <table id="studentGradeTable" class="dataTable-student">
                            <thead>
                                <tr>
                                    <th style="text-align: right;">Assignment</th>
                                    <th style="text-align: right;">Status</th>
                                    <th style="text-align: right;">Grade</th>
                                </tr>
                            </thead>
                            <tbody id="gradeTableBody">

                            </tbody>
                        </table>
                    </div>
                </div>
              </div>
            </div>
            <div class="overlay" style="display: none;">
           <img src="theme/images/loading.gif" style="margin-top: 150px;" />
        </div>
            <asp:CheckBox id="refreshCheck" style="display: none;" runat="server"></asp:CheckBox>
        </div>
    </form>
    <script type="text/javascript">
        function validateNoInput(evt) {
            return false;
        }

        $(document).ready(function () {
            $(function () {
                if ($('#refreshCheck')[0].checked)
                    window.location.reload();

                $('#refreshCheck')[0].checked = true;
            });

            $(".datepicker").datepicker({
                dateFormat: 'yy-mm-dd'
            });

            $(".datepicker").change(function () {
                $(".overlay").show();
                $.ajax({
                    type: "POST",
                    url: "InstructorHome.aspx/UpdateDueDate",
                    data: "{'DueDate': '" + $(this).val() + "','homeworkid': '" + $(this).attr("id") + "'}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (url) {
                        $(".overlay").hide();
                    },
                    error: function (msg) {
                        $(".overlay").hide();
                        alert("Error Changing Due Date!");
                    }
                });
            });

            $("#signOut").click(function (e) {
                $(".overlay").show();
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
    <style type="text/css">
    .overlay {
            background-color: #FFFFFF;
            height: 100%;
            left: 0;
            opacity: 0.8;
            position: fixed;
            text-align: center;
            top: 0;
            vertical-align: middle;
            width: 100%;
            z-index: 2000;
        }
</style>
</body>
</html>
