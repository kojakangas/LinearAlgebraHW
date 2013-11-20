<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="QuestionPage.aspx.cs" Inherits="LinearHomeworkInterface.QuestionPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Student Assignment Questions</title>
    <link href="theme/bootstrap.css" rel="stylesheet" media="screen" />
    <link href="theme/jquery.dataTables.css" rel="stylesheet" media="screen" />
    <link href="theme/jquery-ui-1.10.3.custom.css" rel="stylesheet" media="screen" />
    <script src="javascript/jquery.js"></script>
    <script src="javascript/bootstrap.js"></script>
    <script src="javascript/jquery-ui-1.10.3.custom.min.js"></script>
    <script src="javascript/jquery.dataTables.min.js"></script>
    <script type="text/javascript"
        src="http://cdn.mathjax.org/mathjax/latest/MathJax.js?config=TeX-AMS-MML_HTMLorMML">
    </script>
    <script type="text/x-mathjax-config">
MathJax.Hub.Config({
  tex2jax: {inlineMath: [['$','$'], ['\\(','\\)']]}
});
    </script>
</head>
<body>
    <div class="container">
        <div class="navbar-inner" style="position: fixed; width: 900px; z-index: 1000;">
            <div class="nav-collapse collapse">
                <ul class="nav" style="float: left; margin: 10px 0 0px 0;">
                    <li style="float: left; padding: 0 20px 0 0;"><a href="StudentHome.aspx">Home</a></li>
                </ul>
                <form class="navbar-form pull-right" style="">
                    <button id="signOut" class="btn" style="margin-top: 5px;" type="submit">Sign Out</button>
                </form>
            </div>
            <!--/.nav-collapse -->
        </div>

        <div style="padding-top: 50px;">
            <div class="row-fluid">
                <div class="span3">
                    <div class="well sidebar-nav" style="height: 780px; width: 185px; position: fixed;">
                        <ul class="nav nav-list">
                            <li class="nav-header">Tools</li>
                            <li class="dropdown">
                                <a data-toggle="dropdown" class="dropdown-toggle" href="#">Create Matrix <b class="caret"></b></a>
                                <ul class="dropdown-menu">
                                    <li style="margin-left: 5px;">
                                        <h5>Matrix Size: </h5>
                                    </li>
                                        <li style="line-height: 25px;">
                                            <input id="rows" type="text" onkeypress="return validateNumericInput(event)" class="span4" style="float: left; margin-left: 20px" placeholder="n" />
                                            <div style="display: inline; margin-left: 5px;">X</div>
                                            <input id="columns" type="text" onkeypress="return validateNumericInput(event)" class="span4" style="float: right; margin-right: 20px" placeholder="m" />
                                        </li>
                                    <li><a id="makeMatrix" class="btn" style="margin: 0px 5px 5px 5px;">Create</a></li>
                                </ul>
                            </li>
                            <li class="dropdown">
                                <a data-toggle="dropdown" class="dropdown-toggle" href="#">Fraction <b class="caret"></b></a>
                                <ul class="dropdown-menu">
                                    <li style="margin-left: 5px;">Use the / for fractions:</li>
                                    <li style="margin-left: 5px;">Ex. 1/2</li>
                                </ul>
                            </li>
                            <li class="dropdown">
                                <a id="createAnsLink" data-toggle="dropdown" class="dropdown-toggle" href="#">Answer <b class="caret"></b></a>
                                <ul class="dropdown-menu">
                                    <li style="margin-left: 5px;">
                                        <h5>Solution: </h5>
                                    </li>
                                        <li>
                                            <input id="variables" type="text" onkeypress="return validateNumericInput(event)" class="span10" style="float: left; margin-left: 13px" placeholder="# of elements" />
                                        </li>
                                        <li style="margin-bottom: 5px;">
                                            <span style="margin-left: 13px;">Inconsistent: </span>
                                            <input id="inconsistent" type="checkbox" style="margin-bottom: 5px;" />
                                        </li>
                                    <li><a id="makeAnswers" class="btn" style="margin: 0px 5px 5px 5px;">Create</a></li>
                                </ul>
                            </li>
                            <li><a id="resetQuestion" onclick="resetQuestion()" href="#">Reset Question</a></li>
                        </ul>
                    </div>
                    <!--/.well -->
                </div>
                <!--/span-->
                <div class="span9">
                    <div class="pagination pagination-centered" style="margin-top: 3px; margin-bottom: 3px;">
                        <ul>
                            <asp:Literal runat="server" ID="paginationLiteral"></asp:Literal>
                        </ul>
                    </div>
                    <div class="hero-unit" style="padding: 10px; margin-bottom: 0px; font-size: 14px;">
                        <asp:Label ID="instruction" runat="server" />
                        <div id="questiondisplay">
                            <asp:Label ID="question" runat="server" />
                        </div>
                    </div>
                    <form id="form1" runat="server">
                        <div id="matrixHolder" style="display: inline-block; width: 100%;">
                            <!-- jQuery appends the matrices here-->
                            <div id="info" style="color: #888;">Use the Tools to answer the question...</div>
                        </div>
                        <hr style="margin-bottom: 0px; margin-top: 0px;" />
                        <button id="submitAnswer" disabled="disabled" class="btn btn-primary" title="Note: Must create an answer to submit." type="button" style="margin-top: 5px; float: right; margin-bottom: 50px;">Submit Answer</button>
                        <button id="nextQuestion" class="btn btn-primary" type="button" style="display: none; margin-top: 5px; float: right; margin-bottom: 50px;">Next Question</button>
                        <asp:Label id="rowOpsNeeded" style="display: none;" runat="server"></asp:Label>
                    </form>
                </div>
            </div>
            <!--/.fluid-container-->
        </div>
    </div>

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
    <script type="text/javascript">
        function validateNumericInput(evt) {
            var charCode = (evt.which) ? evt.which : evt.keyCode
            if (charCode > 31 && (charCode < 48 || charCode > 57) && charCode != 45 && charCode != 47)
                return false;
            return true;
        }

        var matrixNumber = 0;
        var generatedAnswer = false;
        var numAnswers = 0;

        function resetQuestion() {
            if (confirm("Are you sure you want to reset this question?")) {
                matrixNumber = 0;
                generatedAnswer = false;
                $('#matrixHolder').empty();
            }
        }

        function addFreeVariable(index) {
            $('#var' + index).val("F");
            $('#var' + index).attr("disabled", "true");
            $('#freeLink' + index).remove();
            var variables = $("div[id^='variable']");
            var c = variables.length;
            variables.each(function (i, el) {
                if (i != index) {
                    if ($("#var" + i).val() != "F") {
                        el.innerHTML += "+ ";
                        var input = document.createElement("input");
                        input.id = "free" + index;
                        input.name = index;
                        input.style.width = "27px";
                        input.className = "gradingInputs";
                        el.appendChild(input);
                        el.innerHTML += "x<sub>" + (index + 1) + "</sub>";
                    } else {
                        var input = document.getElementById("var" + i);
                        el.innerHTML = "";
                        el.appendChild(input);
                    }
                } else {
                    var input = document.getElementById("var" + i);
                    el.innerHTML = "";
                    el.appendChild(input);
                }
            });
        }

        function removeFreeVariable(index) {
            $('#var' + index).val("");
            $('#var' + index).removeAttr("disabled");
            $('#freeLink' + index).text("Set Free Variable ");
            $('#freeLink' + index).attr("onclick", "addFreeVariable(" + index + ")");
        }

        function removeLastMatrix(index) {
            if (confirm("Remove Last Matrix?")) {
                $("#row" + index).remove();
                $("#row" + (index - 1)).append("<a id=\"removeRow\" href=\"#\" onClick=\"removeLastMatrix(" + (index - 1) + ")\" style=\"display:flex; float: right;\">Remove Matrix</a>");
                matrixNumber = matrixNumber - 1;
                var numOfRowOpsNeeded = parseInt($("#rowOpsNeeded").text());
                if (matrixNumber < numOfRowOpsNeeded) {
                    $("#createAnsLink").click(function () { return false; });
                }
                return false;
            }
        }

        function removeAnswer() {
            if (confirm("Remove Answer?")) {
                $("#answerDiv").remove();
                $("#row" + (matrixNumber - 1)).append("<a id=\"removeRow\" href=\"#\" onClick=\"removeLastMatrix(" + (matrixNumber - 1) + ")\" style=\"display:flex; float: right;\">Remove Matrix</a>");
                $("#createAnsLink").off("click");
                generatedAnswer = false;
                $("#submitAnswer").attr("disabled", true);
            }
        }

        $(document).ready(function () {
            $("#createAnsLink").click(function () { return false; });

            $('.dropdown-menu input, #makeMatrix').click(function (e) {
                e.stopPropagation();
            });

            $('#makeMatrix').click(function () {
                var rows = $("#rows").val();
                var cols = $("#columns").val();
                if ((!(rows === "") && !(cols === "")) && generatedAnswer === false) {
                    $('#matrixHolder').append("<div id=\"row" + matrixNumber + "\" class=\"row-fluid\"></div>");
                    $('#row' + matrixNumber).append("<div style=\"font-size: 25px; margin-bottom: 5px;\"> &rarr; </div>");
                    $('#row' + matrixNumber).append("<table id=\"table" + matrixNumber + "\" class=\"span12\" style=\"margin-left: 0px; width: auto;\"><tbody id=\"matrix" + matrixNumber + "\"></tbody></table>");
                    for (var i = 0; i < rows; i++) {
                        $('#table' + matrixNumber).append("<tr id=\"matrix" + matrixNumber + "row" + i + "\"></tr>");
                        for (var j = 0; j < cols; j++) {
                            $('#matrix' + matrixNumber + 'row' + i).append("<td><input class=\"gradingInputs\" onkeypress=\"return validateNumericInput(event)\" style=\"width: 27px;\"></input></td>");
                        }
                    }
                    $('#row' + matrixNumber).append("<div id = \"feedback" + matrixNumber + "\" class=\"row-fluid\"></div>");
                    $("#removeRow").remove();
                    $('#row' + matrixNumber).append("<a id=\"removeRow\" tabindex=\"-1\" onClick=\"removeLastMatrix(" + matrixNumber + ")\" style=\"cursor: pointer; display:flex; float: right;\">Remove Matrix</a>");
                    matrixNumber = matrixNumber + 1;
                    var numOfRowOpsNeeded = parseInt($("#rowOpsNeeded").text());
                    if (matrixNumber >= numOfRowOpsNeeded) {
                        $("#createAnsLink").off("click");
                    }
                }
            });

            $("#submitAnswer").click(function () {
                var hasEmptyInput = false;

                $(".gradingInputs").each(function (i, input) {
                    if (input.value === "") {
                        hasEmptyInput = true;
                    }
                });

                if (hasEmptyInput) {
                    alert("Cannot leave inputs blank.");
                } else {

                    //Puts all matrices in a javascript object
                    var matrixMap = new Object();

                    $("tbody[id^='matrix']").each(function (index, matrixHTML) {
                        var matrix = [];
                        $("#" + matrixHTML.id).find("tr").each(function (rowIndex, rowHTML) {
                            var row = [];
                            $(this).find("td").each(function (cellIndex, cell) {
                                row[cellIndex] = eval($(this).find("input").val());
                            });
                            matrix[rowIndex] = row;
                        });
                        matrixMap[index] = matrix;
                    });
                  
                    //Gets the answer
                    var answer = new Object();

                    var inconsistentAnswer = $("#answerDiv:has(input#inconsistentAnswer)");
                    if (inconsistentAnswer.length == 0) {
                        $("#answerDiv > div[id^='variable']").each(function (index, div) {
                            var answerString = "";
                            var firstInput = $(this).find("#var" + index).val();
                            answerString = firstInput;//if F it is a free var
                            if (firstInput != "F") {
                                $(this).find("input[id^='free']").each(function (inputIndex, input) {
                                    var value = eval($(this).val());
                                    answerString += "," + value + "@" + $(this).attr("name");//Parsing can be done differently
                                });
                            }
                            answer[index] = answerString;
                        });
                    } else {
                        answer = "I";//I is for inconsistent. possible to use boolean or 0 and 1
                    }

                    //Then there will be an ajax call to grade this
                    //It will need both the matrixMap and answer variables
					var complete = "";
					var vars = [], hash;
					var q = document.URL.split('?')[1];
					if (q != undefined) {
						q = q.split('&');
						for (var i = 0; i < q.length; i++) {
							hash = q[i].split('=');
							vars.push(hash[1]);
							vars[hash[0]] = hash[1];
							vars[hash[0]] = vars[hash[0]].replace("#", "");
						}
					}
                    $.ajax({
                        type: "POST",
                        url: "QuestionPage.aspx/Grade",
                        data: "{'MatrixMapJSON': '" + JSON.stringify(matrixMap) + "','AnswerJSON': '" + JSON.stringify(answer) + "','question': '" + vars['question'] + "','assignment': '" + vars['assign'] + "'}",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (gradingMsg) {
                            $("#answerDiv").append("<h4>Results:<h4>");
                            $("#resultsDiv").remove();
                            $("#removeRow").remove();
                            $("#removeAnswer").remove();
                            $(".freeLinks").remove();
                            $(".gradingInputs").attr("disabled", "true");
                            $("#submitAnswer").remove();
                            $("#nextQuestion").show();
                            if (gradingMsg.d != null) {
                                $("#answerDiv").append("<div id=\"resultsDiv\" class=\"alert alert-danger\" style=\"display:flex;\">" + gradingMsg.d + "</div>");
                            } else {
                                $("#answerDiv").append("<div id=\"resultsDiv\" class=\"alert alert-success\" style=\"display:flex;\">Correct!</div>");
                            }
                            nextQuestionUpdate();
                        },
                        error: function (msg) {
                            alert("Grading Failed, don't panic");
                        }
                    });
                }
            });

            function nextQuestionUpdate() {
                var complete = "";
                var vars = [], hash;
                var q = document.URL.split('?')[1];
                if (q != undefined) {
                    q = q.split('&');
                    for (var i = 0; i < q.length; i++) {
                        hash = q[i].split('=');
                        vars.push(hash[1]);
                        vars[hash[0]] = hash[1];
                        vars[hash[0]] = vars[hash[0]].replace("#", "");
                    }
                }
                $.ajax({
                    type: "POST",
                    url: "QuestionPage.aspx/updateForNextQuestion",
                    data: "{'question': '" + vars['question'] + "','assignment': '" + vars['assign'] + "'}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (data) {
                        //the value returned from the POST AJAX is not immediately returned, so we must pass
                        //it from the AJAX call to another function
                        complete = data.d;
                        nextStep(complete);
                    },
                    error: function (msg) {
                        alert("Question Loading Failed, don't panic");
                    }
                });

                function nextStep(data) {
                    if (complete == "incomplete") {
                        $('#nextQuestion').click(function () {
                            window.location.href = "QuestionPage.aspx?assign=" + vars['assign'] + "&question=" + (parseInt(vars['question'], 10) + 1);
                        });
                    }
                    if (complete == "complete") {
                        $('#nextQuestion').text('Finish');
                        alert("Well done! This assignment is now complete.");
                        $('#nextQuestion').click(function () {
                            window.location.href = "StudentHome.aspx";
                        });
                    }
                }
            };

            ////JQuery function activated when "Submit Answer" is clicked
            //$('#submitAnswer').click(function () {
            //        //if the user has generated one or more answers
            //    if (generatedAnswer === true) {
            //        if (confirm("Submit Answer?")) {
            //            //create a variable to pass as the parameter for our grading controller
            //            //in the code behind
            //            var params = "";
            //            //create counter for number of answers that are filled in
            //            var anscount = 0;
            //            //for each answer text the user has created
            //            for (var i = 0; i < numAnswers; i++) {
            //                if ($('#var' + i).val()) {
            //                    //add it to the params variable to pass into the grading controller
            //                    //the space in the end is supposed to be there to allow the grading
            //                    //controller to separate every answer we are passing to the controller
            //                    var params = params + ($('#var' + i).val()) + " ";
            //                    anscount++;
            //                }
            //            }
            //            //take off the extra space at the end of our params variable
            //            params = params.substring(0, params.length - 1);
            //            //if we have no blank answer boxes
            //            if (anscount == numAnswers) {
            //                //begin our AJAX call to our WebMethod in the controller
            //                $.ajax({
            //                    //must be a POST type of call
            //                    type: "POST",
            //                    //pass this to the GradeAnswer controller in our code behind
            //                    url: "QuestionPage.aspx/GradeAnswer",
            //                    //input the params variable as the parameter for our WebMethod
            //                    data: "{'ListPassingSolutions': '" + params + "'}",
            //                    //must have the following contentType details
            //                    contentType: "application/json; charset=utf-8",
            //                    //must have the JSON dataType
            //                    dataType: "json",
            //                    //the next two functions have debug purposes
            //                    //if the function executed successfully
            //                    success: function (msg) {
            //                        //give the result of this call as an alert for the user
            //                        alert(msg.d);
            //                    },
            //                    //if the function encountered an error
            //                    error: function (response) {
            //                        //replace the page with the stacktrace of the error
            //                        //(obviously this shouldn't happen)
            //                        $('body', document).html(response.responseText);
            //                        //also give an alert with accompanying error message
            //                        alert(response.d);
            //                    }
            //                });
            //            }
            //                //otherwise we notify the student of their blank answer
            //            else {
            //                alert("One of your answer boxes is blank... fix it!");
            //            }
            //        }
            //    }
            //});

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

            $('#inconsistent').click(function () {
                if ($("#inconsistent").is(":checked")) {
                    $("#variables").val("");
                    $("#variables").attr("disabled", "true");
                } else {
                    $("#variables").removeAttr("disabled");
                }
            });


            $('#makeAnswers').click(function () {
                var variables = $('#variables').val();
                $('#matrixHolder').append("<div id=\"answerDiv\"></div>");
                if (!(variables === "") && generatedAnswer === false) {
                    generatedAnswer = true;
                    $('#answerDiv').append("<h4>Answer: </h4>");
                    $('#answerDiv').append("<a id=\"removeAnswer\" tabindex=\"-1\" onClick=\"removeAnswer()\" style=\"cursor: pointer; display:flex; float: right;\">Remove Answer</a>");
                    for (var i = 0; i < variables; i++) {
                        $('#answerDiv').append("<strong>x<sub>" + (i + 1) + "</sub> = </strong>" +
                            "<div style=\"display: inline;\" id=\"variable" + i + "\"><input id=\"var" + i + "\" class=\"gradingInputs\" onkeypress=\"return validateNumericInput(event)\" style=\"width: 27px; margin-right: 3px;\" /></div>" +
                            "<a id=\"freeLink" + i + "\" class=\"freeLinks\" onclick=\"addFreeVariable(" + i + ")\" style=\"cursor: pointer;\">Set Free Variable</a></br>");
                    }
                } else if ($("#inconsistent").is(":checked")) {
                    generatedAnswer = true;
                    $('#answerDiv').append("<h4>Answer: </h4>");
                    $('#answerDiv').append("<a id=\"removeAnswer\" tabindex=\"-1\" onClick=\"removeAnswer()\" style=\"cursor: pointer; display:flex; float: right;\">Remove Answer</a>");
                    $("#answerDiv").append("<div style=\"margin-bottom: 10px;\"><span>The matrix is inconsistent.</span><input id=\"inconsistentAnswer\" type=\"checkbox\" checked=\"true\" style=\"display:none;\" /></div>");

                }
                $("#removeRow").remove();
                $("#createAnsLink").click(function () { return false; });
                $("#submitAnswer").removeAttr('disabled');
            });

            $("#createAnsLink").attr("title", "You must complete at least " + $("#rowOpsNeeded").text() + " row operations before creating an answer.");

        });
    </script>
    <style type="text/css">
        .MathJax_Display {
            display: block;
            margin: 0;
            position: relative;
            text-align: center;
            width: 100%;
        }
        </style>
</body>
</html>
