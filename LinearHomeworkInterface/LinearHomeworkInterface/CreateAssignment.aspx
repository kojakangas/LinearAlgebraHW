<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CreateAssignment.aspx.cs" Inherits="LinearHomeworkInterface.CreateAssignment" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Create Assignment</title>
    <link href="theme/bootstrap.css" rel="stylesheet" media="screen" />
    <link href="theme/jquery.dataTables.css" rel="stylesheet" media="screen" />
    <link href="theme/jquery-ui-1.10.3.custom.css" rel="stylesheet" media="screen" />
    <script src="javascript/jquery.js"></script>
    <script src="javascript/bootstrap.min.js"></script>
    <script src="javascript/jquery-ui-1.10.3.custom.min.js"></script>
    <script src="javascript/jquery.dataTables.min.js"></script>
</head>
<body>
<div class="container"><!--/possibly add class="container" -->
	<div class="navbar-inner" style="position: fixed; width: 900px; z-index: 1000;">
			<div class="nav-collapse collapse">
				<ul class="nav" style="float: left; margin: 10px 0 0px 0;">
					<li id="goHome" style="float: left; padding: 0 20px 0 0;"><a href="InstructorHome.aspx">Home</a></li>
				</ul>
				<form class="navbar-form pull-right" style="">
                    <button id="signOut" class="btn" style="margin-top: 5px;" type="submit">Sign Out</button>
				</form>
			</div><!--/.nav-collapse -->
		</div>
	
	<div id="content" style="padding-top:50px;">
		<div id="header">
			<h1>New Assignment</h1>
		</div>	
		<div class="span12">
		<a id="assignHomework" class="btn btn-primary" style="margin-right: 20px; float: right;" href="#" type="submit">Assign Homework</a>
		</div>
		<div id="formHolder" class="span4" style="margin-top:10px; margin-left: 0px; padding-bottom: 10px;">
		<form class="well span4" style="margin-bottom: 10px; padding: 0px 20px 0px 10px; height: 130px;">
			<h4>Homework Details</h4>
			<span style="margin-right: 13px;">Title: </span><input id="title" maxlength="38" data-placement="right" data-toggle="tooltip" type="text" placeholder="Homework Title" /><br />
			<%--<span>Points: </span><input id="points" data-placement="right" data-toggle="tooltip" type="text" placeholder="Points Possible" /><br />--%>
			<span style="margin-right: 13px;">Due: </span><input id="dueDate" data-placement="right" data-toggle="tooltip" type="text" onkeypress="return validateNoInput(event)" placeholder="Due Date"/>
		</form>
		<form class="well span4" style="padding-right:10px;">
			<span>Question Type:  </span>
			<select id="questionType" style="margin-bottom: 5px;">
				<option value="SoE">System of Equations (SoE)</option>
			    <option value="RR">Row Reduction (RR)</option>
				<option value="I">Inverse (I)</option>
				<option value="ID">Independence/Dependence (ID)</option>
			</select>
			<div id="SoE">
				<span>Rows: </span>
				<input id="rowsSoE" type="text" maxlength="2" class="span1"  onkeypress="return validateNumericInput(event)" placeholder="n"/>
				<span>Columns: </span>
				<input id="colsSoE" type="text" maxlength="2" class="span1"  onkeypress="return validateNumericInput(event)" placeholder="m"/><br />
				<span>Coefficient Range: </span>
				<input id="minSoE" type="text" maxlength="3" class="span1"  onkeypress="return validateNumericInputAllowMinus(event)" placeholder="min"/> - 
				<input id="maxSoE" type="text" maxlength="3" class="span1"  onkeypress="return validateNumericInputAllowMinus(event)" placeholder="max"/><br />
				<span>Free Variables: </span>
				<input id="freeVarsSoE" type="text" maxlength="2" class="span2"  onkeypress="return validateNumericInput(event)" placeholder="Free Variables"/><br />
				<span>Inconsistent: </span>
				<input type="checkbox" id="inconsistentSoE" style="margin-top: 0px;" />
			</div>	

			<div id="RR" style="display:none;">
                <span>Rows: </span>
				<input id="rowsRR" type="text" maxlength="2" class="span1"  onkeypress="return validateNumericInput(event)" placeholder="n"/>
				<span>Columns: </span>
				<input id="colsRR" type="text" maxlength="2" class="span1"  onkeypress="return validateNumericInput(event)" placeholder="m"/><br />
				<span>Coefficient Range: </span>
				<input id="minRR" type="text" maxlength="3" class="span1"  onkeypress="return validateNumericInputAllowMinus(event)" placeholder="min"/> - 
				<input id="maxRR" type="text" maxlength="3" class="span1"  onkeypress="return validateNumericInputAllowMinus(event)" placeholder="max"/>
			</div>
						
			<div id="ID" style="display:none;">
                <span>Rows: </span>
				<input id="rowsID" type="text" maxlength="2" class="span1"  onkeypress="return validateNumericInput(event)" placeholder="n"/>
				<span>Columns: </span>
				<input id="colsID" type="text" maxlength="2" class="span1"  onkeypress="return validateNumericInput(event)" placeholder="m"/><br />
				<span>Coefficient Range: </span>
				<input id="minID" type="text" maxlength="3" class="span1"  onkeypress="return validateNumericInputAllowMinus(event)" placeholder="min"/> - 
				<input id="maxID" type="text" maxlength="3" class="span1"  onkeypress="return validateNumericInputAllowMinus(event)" placeholder="max"/>
                <span>Dependent: </span>
				<input type="checkbox" id="dependentID" style="margin-top: 0px;"/>
			</div>
			
			<div id="I" style="display:none;">
				<span>Matrix Size: </span>
				<input id="sizeI" type="text" maxlength="2" class="span1" /><br />
				<span>Coefficient Range: </span>
				<input id="minI" type="text" class="span1" maxlength="3"  onkeypress="return validateNumericInputAllowMinus(event)" placeholder="min"/> - 
				<input id="maxI" type="text" class="span1" maxlength="3"  onkeypress="return validateNumericInputAllowMinus(event)" placeholder="max"/>
			</div>
			<br />
			<a id="addQuestion" class="btn btn-primary" href="#" type="submit">Add Question</a>
		</form>
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
					    <th>Rows</th>
					    <th>Cols</th>
					    <th>Min</th>
					    <th>Max</th>
					    <th>Free</th>
					    <th>Other</th>
                    </tr>
				</thead>
			</table>
			</div>
		</div>	
            <div class="overlay" style="display: none;">
               <img src="theme/images/loading.gif" style="margin-top: 150px;" />
            </div>
	</div>

    <form id ="form1" runat="server" style="display: none">
        <asp:CheckBox id="refreshCheck" style="display: none;" runat="server"></asp:CheckBox>
    </form>
<!-- Le javascript
    ================================================== -->
    <!-- Placed at the end of the document so the pages load faster -->
    <script src="../assets/js/jquery.js"></script>
    <script src="../assets/js/bootstrap-transition.js"></script>
    <script src="../assets/js/bootstrap-alert.js"></script>
    <script src="../assets/js/bootstrap-modal.js"></script>
    <script src="../assets/js/bootstrap-dropdown.js"></script>
    <script src="../assets/js/bootstrap-scrollspy.js"></script>
    <script src="../assets/js/bootstrap-tab.js"></script>
    <script src="../assets/js/bootstrap-tooltip.js"></script>
    <script src="../assets/js/bootstrap-popover.js"></script>
    <script src="../assets/js/bootstrap-button.js"></script>
    <script src="../assets/js/bootstrap-collapse.js"></script>
    <script src="../assets/js/bootstrap-carousel.js"></script>
    <script src="../assets/js/bootstrap-typeahead.js"></script>
	<script src="javascript/tooltip.js"></script>
	<script type="text/javascript">

	    function validateNumericInput(evt) {
	        var charCode = (evt.which) ? evt.which : evt.keyCode
	        if (charCode > 31 && (charCode < 48 || charCode > 57))
	            return false;
	        return true;
	    }

	    function validateNumericInputAllowMinus(evt) {
	        var charCode = (evt.which) ? evt.which : evt.keyCode
	        if (charCode > 31 && (charCode < 48 || charCode > 57) && !(charCode == 45 && evt.currentTarget.value === ""))
	            return false;
	        return true;
	    }

	    function validateNoInput(evt) {
	        return false;
	    }

	    $(document).ready(function () {
	        $(function () {
	            if ($('#refreshCheck')[0].checked)
	                window.location.reload();

	            $('#refreshCheck')[0].checked = true;
	        });

	        var oTable = $('.dataTable').dataTable({
	            "bSort": false,
	            "bFilter": false,
	            'sPaginationType': 'full_numbers',
	            "bAutoWidth": false,
	            "bJQueryUI": true,
	            "oLanguage": {
	                "sEmptyTable": "No questions have been added to this assignment"
	            }
	        });

            //not 100% sure if this instance is needed or not
	        var date = new Date();
	        

	        $("#dueDate").datepicker({
	            changeMonth: true,
	            changeYear: true,
	            dateFormat: 'yy-mm-dd',
	            minDate: new Date()
	        });

	        $("#dateSet").click(function () {
	            $("#dueDate").focus();
	        });

	        $("#questionType").change(function () {
	            var questionType = $("#questionType option:selected").val();
	            $("#questionType option:not(:selected)").each(function () {
	                $("#" + $(this).val()).hide();
	            });
	            $("#formHolder").show();
	            $("#" + questionType).show();
	        });

	        $("#inconsistentSoE").click(function () {
	            if ($("#inconsistentSoE").is(":checked")) {
	                $("#freeVarsSoE").val("");
	                $("#freeVarsSoE").attr("disabled", "true");
	            } else {
	                $("#freeVarsSoE").removeAttr("disabled");
	            }
	        });

	        var questionNumber = 0;

	        $("#addQuestion").click(function () {
	            var questionType = $("#questionType option:selected").val();
	            if (questionType === "SoE") {
	                var freeVars = $("#freeVarsSoE").val();
	                if (freeVars === "") { freeVars = "0"; }
	                if (parseInt($("#minSoE").val()) < parseInt($("#maxSoE").val()) && $("#rowsSoE").val() && $("#colsSoE").val() && $("#minSoE").val() && $("#maxSoE").val() && (parseInt(freeVars) >= 0) && (parseInt(freeVars) < parseInt($("#rowsSoE").val())) && ((parseInt($("#rowsSoE").val()) + 1) == parseInt($("#colsSoE").val()))) {
	                    var inconsistent;
	                    if ($("#inconsistentSoE").is(":checked")) { inconsistent = "Inconsistent"; freeVars = "N/A";} else { inconsistent = "Consistent"; }
	                    questionNumber++;
	                    $("#addedQuestionTable").dataTable().fnAddData([
                            questionNumber,
                            "SoE",
                            $("#rowsSoE").val(),
                            $("#colsSoE").val(),
                            $("#minSoE").val(),
                            $("#maxSoE").val(),
                            freeVars,
                            inconsistent]);
	                } else {
	                    alert("Bad Question Parameters: \n1. Must specify Rows, Columns, Min and Max Coefficients\n2. Max Coefficient must be greater than Min Coefficient\n3. Must have Rows + 1 Columns (for now)\n4. Number of free variables must be from 0 to 1 less than the number of total variables");
	                }
	            } else if (questionType === "RR") {
	                if (parseInt($("#minRR").val()) < parseInt($("#maxRR").val()) && $("#rowsRR").val() && $("#colsRR").val() && $("#minRR").val() && $("#maxRR").val() && (parseInt($("#rowsRR").val()) > 0) && (parseInt($("#colsRR").val()) > 0)) {
	                    questionNumber++;
	                    $("#addedQuestionTable").dataTable().fnAddData([
							questionNumber,
							"RR",
							$("#rowsRR").val(),
							$("#colsRR").val(),
							$("#minRR").val(),
							$("#maxRR").val(),
							"N/A",
							"N/A"]);
	                } else {
	                    alert("Bad Question Parameters: \n1. Must specify Size > 0, Min and Max Coefficients\n2. Max Coefficient must be greater than Min Coefficient");
	                }
	            } else if (questionType === "ID") {
	                if (parseInt($("#minID").val()) < parseInt($("#maxID").val()) && parseInt($("#rowsID").val()) > 0 && parseInt($("#colsID").val()) > 0 && $("#minID").val() && $("#maxID").val()) {
	                    var dependency;
	                    if ($("#dependentID").is(":checked")) { dependency = "Dependent"; } else { dependency = "Independent"; }
	                    questionNumber++;
	                    $("#addedQuestionTable").dataTable().fnAddData([
							questionNumber,
							"ID",
							$("#rowsID").val(),
							$("#colsID").val(),
							$("#minID").val(),
							$("#maxID").val(),
							"N/A",
							dependency]);
	                } else {
	                    alert("Bad Question Parameters: \n1. Must specify Rows and Columns, Min and Max Coefficients\n2. Must have at least one row and column\n3. Max Coefficient must be greater than Min Coefficient");
	                }
	            } else if (questionType === "D") {
	                if (parseInt($("#minD").val()) < parseInt($("#maxD").val()) && $("#sizeD").val() && $("#minD").val() && $("#maxD").val() && parseInt($("#sizeD").val()) > 0) {
	                    questionNumber++;
	                    $("#addedQuestionTable").dataTable().fnAddData([
							questionNumber,
							"D",
							$("#sizeD").val(),
							$("#sizeD").val(),
							$("#minD").val(),
							$("#maxD").val(),
							"N/A",
							"N/A"]);
	                } else {
	                    alert("Bad Question Parameters: \n1. Must specify Size > 0, Min and Max Coefficients\n2. Max Coefficient must be greater than Min Coefficient");
	                }
	            } else if (questionType === "I") {
	                if (parseInt($("#minI").val()) < parseInt($("#maxI").val()) && $("#sizeI").val() && $("#minI").val() && $("#maxI").val() && parseInt($("#sizeI").val()) > 0) {
	                    questionNumber++;
	                    $("#addedQuestionTable").dataTable().fnAddData([
							questionNumber,
							"I",
							$("#sizeI").val(),
							$("#sizeI").val(),
							$("#minI").val(),
							$("#maxI").val(),
							"N/A",
							"N/A"]);
	                } else {
	                    alert("Bad Question Parameters: \n1. Must specify Size > 0, Min and Max Coefficients\n2. Max Coefficient must be greater than Min Coefficient");
	                }
	            }
	        });


	        $("#removeLastQuestion").click(function () {
	            if (questionNumber > 0)
	                questionNumber--;
	            $("#addedQuestionTable").dataTable().fnDeleteRow(questionNumber);
	            $("#removeLastQuestion").attr("name", questionNumber);
	        });

	        $("#title, #points").keyup(function () {
	            $(this).tooltip("destroy");
	            $(this).removeClass("error");
	        });

	        $("#dueDate").change(function () {
	            $(this).tooltip("destroy");
	            $(this).removeClass("error");
	        });

	        $("#assignHomework").click(function () {
	            if (!$("#title").val().trim()) {
	                $("#title").trigger("focus");
	                $("#title").addClass("error");
	                $("#title").tooltip({ trigger: "manual", title: "Required" });
	                $("#title").tooltip("show");
	            } /*else if (!$("#points").val().trim()) {
	            //    $("#points").trigger("focus");
	            //    $("#points").addClass("error");
	            //    $("#points").tooltip({ trigger: "manual", title: "Required" });
	            //    $("#points").tooltip("show");
	            }*/ else if (!$("#dueDate").val().trim()) {
	                $("#dueDate").trigger("focus");
	                $("#dueDate").addClass("error");
	                $("#dueDate").tooltip({ trigger: "manual", title: "Required" });
	                $("#dueDate").tooltip("show");
	            } else if (oTable.fnGetData().length == 0) {
	                alert("Homework must contain at least one\nquestion before assigning to students.");
	            } else if (confirm("Assign Homework to all students?")) {
                    $(".overlay").show();
	                //first we format the date given by the datepicker that the user understands
	                var dueDate = $('#dueDate').datepicker({ dateFormat: 'y-m-d' }).val();
	                //create a variable to pass as the parameter for our grading controller
	                //in the code behind
	                var params = "";
	                //first give our params variable the name and due date for the assignment
	                params = params + $('#title').val() + "|" + dueDate + "|";
	                //then create a variable in which we will pass our string of questions
	                var questions = "";
	                //then create an array to fetch our question data from the table
	                var vin = new Array();
                    //create our reference to manipulate the Datatable on the page
	                var tabler;
	                tabler = $('#addedQuestionTable').dataTable();
	                //for each question in the question table
	                for (var i = 0; i < questionNumber; i++) {
                        //get the current selected row of data from our JQuery Datatable
	                    vin[i] = tabler.fnGetData(i);
	                    //append the fetched row into our string to pass our questions for the
                        //assignment
	                    questions = questions + vin[i] + "|";
	                }
	                //take off the extra break character at the end of our params variable
	                params = params.substring(0, params.length - 1);
	                //do the same thing for our questions variable
	                questions = questions.substring(0, questions.length - 1);
	                //begin our AJAX call to our WebMethod in the controller
	                $.ajax({
	                    //must be a POST type of call
	                    type: "POST",
	                    //pass this to the AddAssignment controller in our code behind
	                    url: "CreateAssignment.aspx/AddAssignment",
	                    //input the params and questions variables as the parameters for our WebMethod
	                    data: JSON.stringify({ ListConstraints: params, ListQuestions: questions}),
	                    //must have the following contentType details
	                    contentType: "application/json; charset=utf-8",
	                    //must have the JSON dataType
	                    dataType: "json",
	                    //the next two functions have debug purposes
	                    //if the function executed successfully
	                    success: function (msg) {
	                        //give the result of this call as an alert for the instructor
                            //THIS WAS REMOVED BECAUSE IT WAS NOT NEEDED
	                        // alert(msg.d);
                            //redirect browser to the instructor's homepage
	                        window.location.href = "InstructorHome.aspx";
	                    },
	                    //if the function encountered an error
	                    error: function (response) {
	                        //replace the page with the stacktrace of the error
	                        //(obviously this shouldn't happen)
	                        $(".overlay").hide();
	                        $('body', document).html(response.responseText);
	                        //also give an alert with accompanying error message
	                        alert(response.d);
	                    }
	                });
	            }
	        });

	        $("#goHome").click(function () {
	            $('.overlay').show();
	        });

	        $("#rowsID, #colsID").change(function(){
	            if($("#rowsID").val() < $("#colsID").val()){
	                $("#dependentID").prop('checked', true);
	                $("#dependentID").attr('disabled','disabled');
	            } else {
	                $("#dependentID").removeAttr('disabled');
	            }
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

	  .error {
		border-color: rgba(255, 0, 0, 1) !important;
	  }
	  
	  .tooltip.right {
		width: 150px;
		opacity: 1;
	  }
	  
	  .tooltip.right .tooltip-arrow {
		border-right-color: #FF0000;
		opacity: 1;
	  }
	  
	  .tooltip-inner {
		background-color: #FF0000;
		opacity: 1;
	  }
    </style>
</body>
</html>