<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CreateAssignment.aspx.cs" Inherits="LinearHomeworkInterface.CreateAssignment" %>

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
                "bSort": false,
                "bFilter": false,
                'sPaginationType': 'full_numbers',
                "bAutoWidth": false,
                "bJQueryUI": true,
                "oLanguage": {
                    "sEmptyTable": "No questions have been added to this assignment"
                },
            });
        });
</script>
    <form id="form1" runat="server">
        <div class="container">
            <!--/possibly add class="container" -->
            <div class="navbar-inner" style="position: fixed; width: 900px; z-index: 1000;">
                <div class="nav-collapse collapse">
                    <ul class="nav" style="float: left; margin: 10px 0 0px 0;">
                        <li style="float: left; padding: 0 20px 0 0;"><a href="instructorHome.html">Home</a></li>
                        <li style="float: left; padding: 0 20px 0 0;"><a href="#about">About</a></li>
                        <li style="float: left; padding: 0 20px 0 0;"><a href="#contact">Contact</a></li>
                        <li class="dropdown" style="float: left; padding: 0 20px 0 0;"><a data-toggle="dropdown" class="dropdown-toggle" href="#">Dropdown <b class="caret"></b></a>
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
                    <h1>New Assignment</h1>
                </div>
                <div class="span12">
                    <span>Question Type: </span>
                    <select id="questionType" style="margin-bottom: 0px;">
                        <option value="SoE">System of Equations</option>
                        <option value="I">Inverse</option>
                        <option value="D">Determinant</option>
                        <option value="EV">Eigen-Value</option>
                    </select>
                    <a id="assignHomework" class="btn btn-primary" href="#" onclick="assignQuestions()" style="margin-right: 20px; float: right;" type="submit">Assign</a>
		<a id="saveQuestions" class="btn" style="margin-right: 20px; float: right;" href="#" type="submit">Save Questions</a>
                </div>
                <div id="formHolder" class="well span4" style="margin-top: 10px; margin-left: 0px; padding-bottom: 10px;">
                    <div style="margin-bottom: 0px;">
                        <div id="SoE">
                            <span>Rows: </span>
                            <input id="rows" type="text" class="span1" placeholder="n" />
                            <span>Columns: </span>
                            <input id="cols" type="text" class="span1" placeholder="m" /><br />
                            <span>Coefficient Range: </span>
                            <input id="min" type="text" class="span1" placeholder="min" />
                            - 
			
                            <input id="max" type="text" class="span1" placeholder="max" /><br />
                            <span>Free Variables: </span>
                            <input id="freeVars" type="text" class="span2" placeholder="Free Variables" /><br />
                            <span>Inconsistent? </span>
                            <input type="checkbox" id="inconsistent" style="margin-top: 0px;" />
                        </div>
                        <a id="addQuestion" class="btn btn-primary" style="margin-top: 15px;" href="#" type="submit">Add Question</a>
                    </div>
                </div>
                <div id="currentQuestions" class="well span7" style="float: right; margin-top: 10px;">
                    <!-- The database will generate the previously posted questions -->
                    <h4 style="float: left; margin-top: 0px;">Current Questions</h4>
                    <a id="removeLastQuestion" href="#" style="float: right;">Remove Last Question Added</a>
                    <table id="addedQuestionTable" class="dataTable">
                        <thead>
                            <tr>
                                <th>#</th>
                                <th>Type</th>
                                <th id="rowstoadd">n</th>
                                <th id="colstoadd">m</th>
                                <th id="minstoadd">Min</th>
                                <th id="maxstoadd">Max</th>
                                <th id="freestoadd">Free</th>
                                <th id="inconstoadd">Inconsistent</th>
                            </tr>
                        </thead>
                    </table>
                </div>
            </div>
        </div>
    </form>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#questionType").change(function () {
                var questionType = $("#questionType option:selected").val();
                $("#questionType option:not(:selected)").each(function () {
                    $("#" + $(this).val()).hide();
                });
                $("#formHolder").show();
                $("#" + questionType).show();
            });

            $("#inconsistent").click(function () {
                if ($("#inconsistent").is(":checked")) {
                    $("#freeVars").val("");
                    $("#freeVars").attr("disabled", "true");
                } else {
                    $("#freeVars").removeAttr("disabled");
                }
            });

            var questionNumber = 0;

            $("#addQuestion").click(function () {
                if ($("#rows").val() != "" && $("#cols").val() != "" && $("#min").val() != "" && $("#max").val() != "") {
                    questionNumber++;
                    var questionType = $("#questionType option:selected").val();
                    var freeVars = $("#freeVars").val();
                    if (freeVars === "") { freeVars = "0"; }
                    var inconsistent;
                    if ($("#inconsistent").is(":checked")) { inconsistent = "yes"; } else { inconsistent = "no"; }
                    if (questionType === "SoE") {
                        $("#addedQuestionTable").dataTable().fnAddData([
							questionNumber,
							"System of Equations",
							$("#rows").val(),
							$("#cols").val(),
							$("#min").val(),
							$("#max").val(),
							freeVars,
							inconsistent]);
                    }
                }
            });


            $("#removeLastQuestion").click(function () {
                if (questionNumber > 0)
                    questionNumber--;
                $("#addedQuestionTable").dataTable().fnDeleteRow(questionNumber);
                $("#removeLastQuestion").attr("name", questionNumber);
            });

            //JQuery function activated when "Assign" is clicked
            $('a#assignHomework').click(function () {
                //if the user has created at least one question to assign
                if (questionNumber > 0) {
                    //create a variable to pass as the parameter for our grading controller
                    //in the code behind
                    var params = "";
                    var vin = new Array();
                    var tabler;
                    tabler = $('#addedQuestionTable').dataTable();
                    //for each answer text the user has created
                    for (var i = 0; i < questionNumber; i++) {
                            //add it to the params variable to pass into the grading controller
                            //the space in the end is supposed to be there to allow the grading
                            //controller to separate every answer we are passing to the controller
                        vin[i] = tabler.fnGetData(i);
                        params = params + vin[i] + "|";
                    }
                    //take off the extra break character at the end of our params variable
                    params = params.substring(0, params.length - 1);
                    //begin our AJAX call to our WebMethod in the controller
                    $.ajax({
                        //must be a POST type of call
                        type: "POST",
                        //pass this to the GradeAnswer controller in our code behind
                        url: "CreateAssignment.aspx/AddAssignment",
                        //input the params variable as the parameter for our WebMethod
                        data: "{'ListQuestions': '" + params + "'}",
                        //must have the following contentType details
                        contentType: "application/json; charset=utf-8",
                        //must have the JSON dataType
                        dataType: "json",
                        //the next two functions have debug purposes
                        //if the function executed successfully
                        success: function (msg) {
                            //give the result of this call as an alert for the user
                            alert(msg.d);
                        },
                        //if the function encountered an error
                        error: function (response) {
                            //replace the page with the stacktrace of the error
                            //(obviously this shouldn't happen)
                            $('body', document).html(response.responseText);
                            //also give an alert with accompanying error message
                            alert(response.d);
                        }
                    });
                }
            });
        });
	</script>
</body>
</html>
