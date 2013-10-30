<<<<<<< HEAD
﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CreateAssignment.aspx.cs" Inherits="LinearHomeworkInterface.CreateAssignment" %>

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
<body><div class="container"><!--/possibly add class="container" -->	<div class="navbar-inner" style="position: fixed; width: 900px; z-index: 1000;">			<div class="nav-collapse collapse">				<ul class="nav" style="float: left; margin: 10px 0 0px 0;">					<li style="float: left; padding: 0 20px 0 0;"><a href="instructorHome.html">Home</a></li>				</ul>				<form class="navbar-form pull-right" style="">					<button class="btn" style="margin-top: 5px;" type="submit">Sign Out</button>				</form>			</div><!--/.nav-collapse -->		</div>		<div id="content" style="padding-top:50px;">		<div id="header">			<h1>New Assignment</h1>		</div>			<div class="span12">		<a id="assignHomework" class="btn btn-primary" style="margin-right: 20px; float: right;" href="#" type="submit">Assign Homework</a>		</div>		<div id="formHolder" class="span4" style="margin-top:10px; margin-left: 0px; padding-bottom: 10px;">		<form class="well span4" style="margin-bottom: 10px; padding: 0px 20px 0px 10px;">			<h4>Homework Details</h4>			<span style="margin-right: 13px;">Title: </span><input id="title" data-placement="right" data-toggle="tooltip" type="text" placeholder="Homework Title" /><br />			<span>Points: </span><input id="points" data-placement="right" data-toggle="tooltip" type="text" placeholder="Points Possible" /><br />			<span style="margin-right: 13px;">Due: </span><input id="dueDate" data-placement="right" data-toggle="tooltip" type="text" placeholder="Due Date" />		</form>		<form class="well span4" style="padding-right:10px;">			<span>Question Type: </span>			<select id="questionType" style="margin-bottom: 5px;">				<option value="SoE">System of Equations</option>				<option value="RtI">Reduce to Identity</option>				<option value="DP">Dot Product</option>				<option value="I">Inverse</option>				<option value="D">Determinant</option>			</select>			<div id="SoE">				<span>Rows: </span>				<input id="rowsSoE" type="text" maxlength="2" class="span1"  onkeypress="return validateNumericInput(event)" placeholder="n"/>				<span>Columns: </span>				<input id="colsSoE" type="text" maxlength="2" class="span1"  onkeypress="return validateNumericInput(event)" placeholder="m"/><br />				<span>Coefficient Range: </span>				<input id="minSoE" type="text" maxlength="3" class="span1"  onkeypress="return validateNumericInputAllowMinus(event)" placeholder="min"/> - 				<input id="maxSoE" type="text" maxlength="3" class="span1"  onkeypress="return validateNumericInputAllowMinus(event)" placeholder="max"/><br />				<span>Free Variables: </span>				<input id="freeVarsSoE" type="text" maxlength="2" class="span2"  onkeypress="return validateNumericInput(event)" placeholder="Free Variables"/><br />				<span>Inconsistent: </span>				<input type="checkbox" id="inconsistentSoE" style="margin-top: 0px;" />			</div>				<div id="RtI" style="display:none;">				<span>Matrix Size: </span>				<input id="sizeRtI" type="text" maxlength="2" class="span1"  onkeypress="return validateNumericInput(event)" /><br />				<span>Coefficient Range: </span>				<input id="minRtI" type="text" maxlength="3" class="span1"  onkeypress="return validateNumericInputAllowMinus(event)" placeholder="min"/> - 				<input id="maxRtI" type="text" maxlength="3" class="span1"  onkeypress="return validateNumericInputAllowMinus(event)" placeholder="max"/>			</div>						<div id="DP" style="display:none;">				<span>Vector Size: </span>				<input id="sizeDP" type="text" maxlength="2" class="span1"  onkeypress="return validateNumericInput(event)" /><br />				<span>Coefficient Range: </span>				<input id="minDP" type="text" maxlength="3" class="span1"  onkeypress="return validateNumericInputAllowMinus(event)" placeholder="min"/> - 				<input id="maxDP" type="text" maxlength="3" class="span1"  onkeypress="return validateNumericInputAllowMinus(event)" placeholder="max"/>			</div>						<div id="D" style="display:none;">				<span>Matrix Size: </span>				<input id="sizeD" type="text" maxlength="2" class="span1"  onkeypress="return validateNumericInput(event)" /><br />				<span>Coefficient Range: </span>				<input id="minD" type="text" maxlength="3" class="span1"  onkeypress="return validateNumericInputAllowMinus(event)" placeholder="min"/> - 				<input id="maxD" type="text" maxlength="3" class="span1"  onkeypress="return validateNumericInputAllowMinus(event)" placeholder="max"/>			</div>						<div id="I" style="display:none;">				<span>Matrix Size: </span>				<input id="sizeI" type="text" maxlength="2" class="span1" /><br />				<span>Coefficient Range: </span>				<input id="minI" type="text" class="span1" maxlength="3"  onkeypress="return validateNumericInputAllowMinus(event)" placeholder="min"/> - 				<input id="maxI" type="text" class="span1" maxlength="3"  onkeypress="return validateNumericInputAllowMinus(event)" placeholder="max"/>			</div>						<a id="addQuestion" class="btn btn-primary" href="#" style="margin-top: 15px;" type="submit">Add Question</a>		</form>		</div>				<div id="currentQuestions" class="well span7" style="float: right; margin-top: 10px;">			<!-- The database will generate the previously posted questions -->			<h4 style="float: left; margin-top: 0px;">Current Questions</h4>			<a id="removeLastQuestion" href="#" style="float: right;">Remove Last Question Added</a>			<table id="addedQuestionTable" class="dataTable">				<thead>                    <tr>					    <th>#</th>					    <th>Type</th>					    <th>Rows</th>					    <th>Cols</th>					    <th>Min</th>					    <th>Max</th>					    <th>Free</th>					    <th>Inconsistent</th>                    </tr>				</thead>			</table>			</div>		</div>		</div><!-- Le javascript    ================================================== -->    <!-- Placed at the end of the document so the pages load faster -->    <script src="../assets/js/jquery.js"></script>    <script src="../assets/js/bootstrap-transition.js"></script>    <script src="../assets/js/bootstrap-alert.js"></script>    <script src="../assets/js/bootstrap-modal.js"></script>    <script src="../assets/js/bootstrap-dropdown.js"></script>    <script src="../assets/js/bootstrap-scrollspy.js"></script>    <script src="../assets/js/bootstrap-tab.js"></script>    <script src="../assets/js/bootstrap-tooltip.js"></script>    <script src="../assets/js/bootstrap-popover.js"></script>    <script src="../assets/js/bootstrap-button.js"></script>    <script src="../assets/js/bootstrap-collapse.js"></script>    <script src="../assets/js/bootstrap-carousel.js"></script>    <script src="../assets/js/bootstrap-typeahead.js"></script>	<script src="javascript/tooltip.js"></script>	<script type="text/javascript">	    function validateNumericInput(evt) {
	        var charCode = (evt.which) ? evt.which : evt.keyCode	        if (charCode > 31 && (charCode < 48 || charCode > 57))	            return false;	        return true;
	    }	    function validateNumericInputAllowMinus(evt) {
	        var charCode = (evt.which) ? evt.which : evt.keyCode	        if (charCode > 31 && (charCode < 48 || charCode > 57) && charCode != 45)	            return false;	        return true;
	    }	    $(document).ready(function () {
	        var oTable = $('.dataTable').dataTable({
	            "bSort": false,	            "bFilter": false,	            'sPaginationType': 'full_numbers',	            "bAutoWidth": false,	            "bJQueryUI": true,	            "oLanguage": {
	                "sEmptyTable": "No questions have been added to this assignment"
	            }
	        });	        var date = new Date();	        date.setDate(date.getDate() + 7);	        var dateString = (date.getMonth() + 1) + "/" + date.getDate() + "/" + date.getFullYear();	        $("#dueDate").val(dateString);	        $("#dueDate").datepicker({
	            changeMonth: true,	            changeYear: true,	            minDate: new Date()
	        });	        $("#questionType").change(function () {
	            var questionType = $("#questionType option:selected").val();	            $("#questionType option:not(:selected)").each(function () {
	                $("#" + $(this).val()).hide();
	            });	            $("#formHolder").show();	            $("#" + questionType).show();
	        });	        $("#inconsistentSoE").click(function () {
	            if ($("#inconsistentSoE").is(":checked")) {
	                $("#freeVarsSoE").val("");	                $("#freeVarsSoE").attr("disabled", "true");
	            } else {
	                $("#freeVarsSoE").removeAttr("disabled");
	            }
	        });	        var questionNumber = 0;	        $("#addQuestion").click(function () {
	            questionNumber++;	            var questionType = $("#questionType option:selected").val();	            if (questionType === "SoE") {
	                if ($("#minSoE").val() <= $("#maxSoE").val() && $("#rowsSoE").val() && $("#colsSoE").val() && $("#minSoE").val() && $("#maxSoE").val()) {
	                    var freeVars = $("#freeVarsSoE").val();	                    if (freeVars === "") { freeVars = "0"; }	                    var inconsistent;	                    if ($("#inconsistentSoE").is(":checked")) { inconsistent = "Yes"; } else { inconsistent = "No"; }	                    $("#addedQuestionTable").dataTable().fnAddData([							questionNumber,							"SoE",							$("#rowsSoE").val(),							$("#colsSoE").val(),							$("#minSoE").val(),							$("#maxSoE").val(),							freeVars,							inconsistent]);
	                } else {
	                    alert("Bad Question Parameters: \n1. Must specify Rows, Columns, Min and Max Coefficients\n2. Max Coefficient must be greater than Min Coefficient");
	                }
	            } else if (questionType === "RtI") {
	                if ($("#minRtI").val() <= $("#maxRtI").val() && $("#sizeRtI").val() && $("#minRtI").val() && $("#maxRtI").val()) {
	                    $("#addedQuestionTable").dataTable().fnAddData([							questionNumber,							"RtI",							$("#sizeRtI").val(),							$("#sizeRtI").val(),							$("#minRtI").val(),							$("#maxRtI").val(),							"0",							"No"]);
	                } else {
	                    alert("Bad Question Parameters: \n1. Must specify Size, Min and Max Coefficients\n2. Max Coefficient must be greater than Min Coefficient");
	                }
	            } else if (questionType === "DP") {
	                if ($("#minDP").val() <= $("#maxDP").val() && $("#sizeDP").val() && $("#minDP").val() && $("#maxDP").val()) {
	                    $("#addedQuestionTable").dataTable().fnAddData([							questionNumber,							"DP",							"1",							$("#sizeDP").val(),							$("#minDP").val(),							$("#maxDP").val(),							"0",							"No"]);
	                } else {
	                    alert("Bad Question Parameters: \n1. Must specify Size, Min and Max Coefficients\n2. Max Coefficient must be greater than Min Coefficient");
	                }
	            } else if (questionType === "D") {
	                if ($("#minD").val() <= $("#maxD").val() && $("#sizeD").val() && $("#minD").val() && $("#maxD").val()) {
	                    $("#addedQuestionTable").dataTable().fnAddData([							questionNumber,							"D",							$("#sizeD").val(),							$("#sizeD").val(),							$("#minD").val(),							$("#maxD").val(),							"0",							"No"]);
	                } else {
	                    alert("Bad Question Parameters: \n1. Must specify Size, Min and Max Coefficients\n2. Max Coefficient must be greater than Min Coefficient");
	                }
	            } else if (questionType === "I") {
	                if ($("#minI").val() <= $("#maxI").val() && $("#sizeI").val() && $("#minI").val() && $("#maxI").val()) {
	                    $("#addedQuestionTable").dataTable().fnAddData([							questionNumber,							"I",							$("#sizeI").val(),							$("#sizeI").val(),							$("#minI").val(),							$("#maxI").val(),							"0",							"No"]);
	                } else {
	                    alert("Bad Question Parameters: \n1. Must specify Size, Min and Max Coefficients\n2. Max Coefficient must be greater than Min Coefficient");
	                }
	            }
	        });	        $("#removeLastQuestion").click(function () {
	            if (questionNumber > 0)	                questionNumber--;	            $("#addedQuestionTable").dataTable().fnDeleteRow(questionNumber);	            $("#removeLastQuestion").attr("name", questionNumber);
	        });	        $("#title, #points").keyup(function () {
	            $(this).tooltip("destroy");	            $(this).removeClass("error");
	        });	        $("#dueDate").change(function () {
	            $(this).tooltip("destroy");	            $(this).removeClass("error");
	        });	        $("#assignHomework").click(function () {
	            if (!$("#title").val().trim()) {
	                $("#title").trigger("focus");	                $("#title").addClass("error");	                $("#title").tooltip({ trigger: "manual", title: "Required" });	                $("#title").tooltip("show");
	            } else if (!$("#points").val().trim()) {
	                $("#points").trigger("focus");	                $("#points").addClass("error");	                $("#points").tooltip({ trigger: "manual", title: "Required" });	                $("#points").tooltip("show");
	            } else if (!$("#dueDate").val().trim()) {
	                $("#dueDate").trigger("focus");	                $("#dueDate").addClass("error");	                $("#dueDate").tooltip({ trigger: "manual", title: "Required" });	                $("#dueDate").tooltip("show");
	            } else if (oTable.fnGetData().length == 0) {
	                alert("Homework must contain at least one\nquestion before assigning to students.");
	            } else if (confirm("Assign Homework to all students?")) {
	            }
	        });
	    });	</script>    <style type="text/css">	  	  .error {		border-color: rgba(255, 0, 0, 1) !important;	  }	  	  .tooltip.right {		width: 150px;		opacity: 1;	  }	  	  .tooltip.right .tooltip-arrow {		border-right-color: #FF0000;		opacity: 1;	  }	  	  .tooltip-inner {		background-color: #FF0000;		opacity: 1;	  }    </style></body>
</html>
=======
﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CreateAssignment.aspx.cs" Inherits="LinearHomeworkInterface.CreateAssignment" %>

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
    <style type="text/css">
        #GenConstraints {
            margin-top: 20px;
            height: 88px;
        }
    </style>
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
                    
                        <div id="GenConstraints">
                            <span>Assignment Name: </span>
                            <input id="assignmentname" type="text" class="span2" placeholder="Name"/>
                            <span>Points Possible: </span>
                            <input id="points" type="text" class="span2" placeholder="# points" />
                            
                            <!--<ASP:TextBox runat="server" ID="txtDate"></ASP:TextBox>--->

                            
                            <!---<asp:Calendar ID="Calendar1" runat="server"></asp:Calendar>--->

                            
                            <br />
                            <span>Due Date (Year): </span>
                            <input id="year" type="text" class="span1" placeholder="####" />
                            -
                            <span>Due Date (Month):</span>
                            <input id="month" type="text" class="span1" placeholder="##" />
                            -
                            <span>Due Date (Day):</span>
                            <input id="day" type="text" class="span1" placeholder="##" />
                        </div>
                    
                <div class="span12">
                    <span>Question Type: </span>
                    <select id="questionType" style="margin-bottom: 0px;" name="D1">
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
                //if the instructor has not assigned any questions yet
                if (questionNumber == 0) {
                    alert("You must assign at least one question!");
                }
                //if the instructor has not given a name for the new assignment
                else if (!$('#assignmentname').val()) {
                    alert("Nameless assignments are LAME. Don't be lame!");
                }
                //if the instructor has not assigned the number of points in the assignment
                else if (!$('#points').val()) {
                    alert("You really think students are motivated to complete POINTless assignments?");
                }
                //if the instructor has not entered the year due
                else if (!$('#year').val()) {
                    alert("Hmmm, this assignment is not due at any specific year?...");
                }
                //if the instructor has not entered the month due
                else if (!$('#month').val()) {
                    alert("Your students could complete the assignment at any point of the year...");
                }
                //if the instructor has not entered the day due
                else if (!$('#day').val()) {
                    alert("Really? There's no specific day in your month due? Try again.");
                }
                //if the instructor has entered a month that is not valid for the year
                else if ($('#month').val() > 12 || $('#month').val() < 1) {
                    alert("How hard is it to enter a valid month? You didn't pick between 1 and 12? Really?");
                }
                //if the instructor has entered a day that is not valid for the month
                else if ($('#day').val() > 31 || $('#day').val() < 1) {
                    alert("How hard is it to enter a valid day? You didn't pick between 1 and 31? Really?");
                }
                //if the instructor has entered all of the necessary values
                else {
                    //create a variable to pass as the parameter for our grading controller
                    //in the code behind
                    var params = "";
                    //first give our params variable the name, points, and due date for the assignment
                    params = params + $('#assignmentname').val() + "|" + $('#points').val() + "|" + $('#year').val() + "-" + $('#month').val() + "-" + $('#day').val() + "|";
                    //then create a variable in which we will pass our string of questions
                    var questions = "";
                    //then create an array to fetch our question data from the table
                    var vin = new Array();
                    var tabler;
                    tabler = $('#addedQuestionTable').dataTable();
                    //for each question in the question table
                    for (var i = 0; i < questionNumber; i++) {
                        //for (var j = 0; j < tabler.fnGetData(i).length; j++) {
                        vin[i] = tabler.fnGetData(i);
                        alert("Current row of data is:\n" + vin[i]);
                        questions = questions + vin[i] + "|";
                        //}
                    }
                    //take off the extra break character at the end of our params variable
                    params = params.substring(0, params.length - 1);
                    //do the same thing for our questions variable
                    questions = questions.substring(0, questions.length - 1);
                    alert("The questions are:\n" + questions);
                    //begin our AJAX call to our WebMethod in the controller
                    $.ajax({
                        //must be a POST type of call
                        type: "POST",
                        //pass this to the GradeAnswer controller in our code behind
                        url: "CreateAssignment.aspx/AddAssignment",
                        //input the params and questions variables as the parameters for our WebMethod
                        data: JSON.stringify({ ListConstraints: params, ListQuestions: questions }),
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
    <p>
        &nbsp;</p>
</body>
</html>
>>>>>>> 366f6977495386e23f00c98f62cbe801f33ea11b
