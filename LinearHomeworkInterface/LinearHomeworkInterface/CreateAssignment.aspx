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
