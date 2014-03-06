<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="QuestionInverse.aspx.cs" Inherits="LinearHomeworkInterface.QuestionInverse" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Assignment Questions</title>
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
                    <li id="goHome" style="float: left; padding: 0 20px 0 0;"><a href="StudentHome.aspx">Home</a></li>
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
                                            <input id="rows" type="text" onkeypress="return validateNumericInputMatrixSize(event)" class="span4" style="float: left; margin-left: 20px" placeholder="rows" />
                                            <div style="display: inline; margin-left: 5px;">X</div>
                                            <input id="columns" type="text" onkeypress="return validateNumericInputMatrixSize(event)" class="span4" style="float: right; margin-right: 20px" placeholder="cols" />
                                        </li>
                                    <li><a id="makeMatrix" class="btn" style="margin: 0px 5px 5px 5px;">Create</a></li>
                                </ul>
                            </li>
                            <li><a id="copymatrix" style="cursor: pointer;">Copy Last Matrix</a></li>
                            <li class="dropdown">
                                <a data-toggle="dropdown" class="dropdown-toggle" href="#">Fraction <b class="caret"></b></a>
                                <ul class="dropdown-menu">
                                    <li style="margin-left: 5px;">Use the / for fractions:</li>
                                    <li style="margin-left: 5px;">Ex. 1/2</li>
                                    <li style="margin-left: 5px;">You must use improper fractions</li>
                                    <li style="margin-left: 5px;">Ex. 1 2/3 or 1.6666... must be written as 5/3</li>
                                </ul>
                            </li>
                            <li class="dropdown">
                                <a id="createAnsLink" data-toggle="dropdown" class="dropdown-toggle" href="#">Answer <b class="caret"></b></a>
                                <ul class="dropdown-menu">
                                    <li style="margin-left: 10px;">To submit an answer: Create a matrix of the correct size and input the inverse of the starting matrix then click Submit Answer.</li>
                                </ul>
                            </li>
                            <li><a id="resetQuestion" onclick="resetQuestion()" href="#">Reset Question</a></li>
                            <li><button id="instructionButton" class="btn btn-warning btn-small" style="" data-toggle="modal" data-target="#myModal">
                                  Sample Question
                                </button></li>
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
                            <div id="info" style="color: #888;">Instructions: Start by creating the initial matrix, then create a matrix that will include the initial matrix and identity, <br />then row reduce to the identity. Once the matrix is row reduced to the identity, create a matrix of the correct size and input the inverse as your answer. <br />Note: Only one row operation is allowed between matrices. Empty entries must contain 0.<br /> Use the tools on the left to work the problem. </div>
                        </div>


                        <!-- Modal -->
                        <div class="modal fade" id="myModal" style="width: 900px; left: 100%;" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
                          <div class="modal-dialog">
                            <div class="modal-content">
                              <div class="modal-body">
                                <img src="theme/images/InverseInstructions.png" alt="ImageName" />
                              </div>
                              <div class="modal-footer">
                                <button id="close" type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                              </div>
                            </div>
                          </div>
                        </div>

                        <hr style="margin-bottom: 0px; margin-top: 0px;" />
                        <button id="submitAnswer" class="btn btn-primary" disabled="disabled" type="button" style="margin-top: 5px; float: right; margin-bottom: 50px;">Submit Answer</button>
                        <button id="nextQuestion" class="btn btn-primary" type="button" style="display: none; margin-top: 5px; float: right; margin-bottom: 50px;">Next Question</button>
                        <asp:Label id="rowOpsNeeded" style="display: none;" runat="server">2</asp:Label>
                        <!--possibly unessarcary now-->
                        <asp:CheckBox id="refreshCheck" style="display: none;" runat="server"></asp:CheckBox>
                    </form>
                </div>
            </div>
            <div class="overlay" style="display: none;">
               <img src="theme/images/loading.gif" style="margin-top: 150px;" />
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
        function validateNumericInputMatrixSize(evt) {
            var charCode = (evt.which) ? evt.which : evt.keyCode
            if (charCode > 31 && (charCode < 48 || charCode > 57))
                return false;
            return true;
        }

        function validateNumericInput(evt) {
            var charCode = (evt.which) ? evt.which : evt.keyCode
            if (charCode > 31 && (charCode < 48 || charCode > 57) && charCode != 45 && charCode != 46 && charCode != 37 && charCode != 39 && !(charCode == 47 && evt.currentTarget.value != "" && evt.currentTarget.value.indexOf('/') === -1)) {
                return false;
            }
            else if (charCode == 13) $("#copymatrix").trigger('click');
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
                $("#matrixHolder").append("<div id=\"info\" style=\"color: #888;\">Instructions: Start by creating the initial matrix, then create a matrix that will include the initial matrix and identity, <br />then row reduce to the identity. Once the matrix is row reduced to the identity, create a matrix of the correct size and input the inverse as your answer. <br />Note: Only one row operation is allowed between matrices. Empty entries must contain 0.<br /> Use the tools on the left to work the problem. </div>");
            }
        }

        function removeLastMatrix(index) {
            if (confirm("Remove Last Matrix?")) {
                $("#row" + index).remove();
                $("#row" + (index - 1)).append("<a id=\"removeRow\" href=\"#\" onClick=\"removeLastMatrix(" + (index - 1) + ")\" style=\"display:flex; float: right;\">Remove Matrix</a>");
                matrixNumber = matrixNumber - 1;
                var numOfRowOpsNeeded = parseInt($("#rowOpsNeeded").text());
                return false;
            }
        }

        $(document).ready(function () {

            $("#instructionButton").click(function () {
                $("#myModal").css("left", "40%");
            });

            $(function () {
                //potentially unnessarcary now
                if ($('#refreshCheck')[0].checked)
                    window.location.reload();

                $('#refreshCheck')[0].checked = true;
            });

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
                            $('#matrix' + matrixNumber + 'row' + i).append("<td><input class=\"gradingInputs\" maxlength=\"10\" onkeypress=\"return validateNumericInput(event)\" style=\"width: 35px;\"></input></td>");
                        }
                    }
                    $('#row' + matrixNumber).append("<div id = \"feedback" + matrixNumber + "\" class=\"row-fluid\"></div>");
                    $("#removeRow").remove();
                    $('#row' + matrixNumber).append("<a id=\"removeRow\" tabindex=\"-1\" onClick=\"removeLastMatrix(" + matrixNumber + ")\" style=\"cursor: pointer; display:flex; float: right;\">Remove Matrix</a>");
                    matrixNumber = matrixNumber + 1;
                    var numOfRowOpsNeeded = parseInt($("#rowOpsNeeded").text());
                    
                    if (matrixNumber >= numOfRowOpsNeeded) {
                        $("#submitAnswer").removeAttr('disabled');
                    }
                }
            });

            $("#submitAnswer").click(function () {
                if (confirm("Submit Answer?")) {
                    var hasEmptyInput = false;

                    $(".gradingInputs").each(function (i, input) {
                        if (input.value === "") {
                            hasEmptyInput = true;
                        }
                        else {
                            try {
                                eval(input.value);
                                var temp = input.value;
                                var firstChar = temp.charAt(0);
                                // the g in the regular expression says to search the whole string 
                                // rather than just find the first occurrence
                                // match returns and array of -'s. Length will be the count for -.
                                var count = temp.match(/-/g);
                                if (count != null) {
                                    if (count.length > 1 || firstChar != '-') hasEmptyInput = true;
                                }
                            }
                            catch (err) {
                                hasEmptyInput = true;
                            }
                        }
                    });

                    if (hasEmptyInput) {
                        alert("Cannot leave inputs blank.\n\"-\" should only be placed at the beginning an input.\nEnsure all inputs have a number.");
                    } else {

                        $(".overlay").show();

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
                            url: "QuestionInverse.aspx/Grade",
                            data: "{'MatrixMapJSON': '" + JSON.stringify(matrixMap) + "','question': '" + vars['question'] + "','assignment': '" + vars['assign'] + "'}",
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            success: function (gradingMsg) {
                                $("#matrixHolder").append("<div id=\"answerDiv\"></div>");
                                $("#answerDiv").append("<h4>Results:<h4>");
                                $("#resultsDiv").remove();
                                $("#removeRow").remove();
                                $(".gradingInputs").attr("disabled", "true");
                                $("#submitAnswer").remove();
                                $("#nextQuestion").show();
                                $(".overlay").hide();
                                if (gradingMsg.d.indexOf("!") === -1) {
                                    $("#answerDiv").append("<div id=\"resultsDiv\" class=\"alert alert-danger\" style=\"display:flex;\">" + gradingMsg.d + "</div>");
                                } else {
                                    $("#answerDiv").append("<div id=\"resultsDiv\" class=\"alert alert-success\" style=\"display:flex;\"><div>Correct!<div><div>" + gradingMsg.d + "</div></div>");
                                }
                                nextQuestionUpdate();
                            },
                            error: function (msg) {
                                $(".overlay").hide();
                                alert("Grading Failed, don't panic");
                            }
                        });
                    }
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
                    url: "QuestionInverse.aspx/updateForNextQuestion",
                    data: "{'question': '" + vars['question'] + "','assignment': '" + vars['assign'] + "'}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (data) {
                        //the value returned from the POST AJAX is not immediately returned, so we must pass
                        //it from the AJAX call to another function
                        var statusAndQType = data.d.split(" ");
                        if (statusAndQType[0] == "incomplete") {
                            $('#nextQuestion').click(function () {
                                $(".overlay").show();
                                if (statusAndQType[1] === "SoE") {
                                    window.location = "QuestionPage.aspx?assign=" + vars['assign'] + "&question=" + (parseInt(vars['question'], 10) + 1);
                                }
                                else if (statusAndQType[1] === "I") {
                                    window.location = "QuestionInverse.aspx?assign=" + vars['assign'] + "&question=" + (parseInt(vars['question'], 10) + 1);
                                }
                                else if (statusAndQType[1] === "ID") {
                                   window.location = "QuestionLinearDependence.aspx?assign=" + vars['assign'] + "&question=" + (parseInt(vars['question'], 10) + 1);
                                }
                                else if (statusAndQType[1] === "RtI") {
                                    window.location = "ReducedRow.aspx?assign=" + vars['assign'] + "&question=" + (parseInt(vars['question'], 10) + 1);
                                } else {
                                    window.location.reload();
                                }
                            });
                        }
                        if (statusAndQType[0] == "complete") {
                            $('#nextQuestion').text('Finish');
                            alert("Well done! This assignment is now complete.");
                            $('#nextQuestion').click(function () {
                                $(".overlay").show();
                                window.location.href = "StudentHome.aspx";
                            });
                        }
                    },
                    error: function (msg) {
                        alert("Question Loading Failed, don't panic");
                    }
                });
            };

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
                        $(".overlay").hide();
                        alert("Sign Out Failed!");
                    }
                });
            });

            $("#goHome").click(function () {
                $('.overlay').show();
            });

            $("#copymatrix").click(function () {
                $("#makeMatrix").trigger('click');
                var index = 0;
                var values = [];
                if (matrixNumber > 1) {
                    $("#matrix" + (matrixNumber - 2)).find("tr").each(function (rowIndex, rowHTML) {
                        $(this).find("td").each(function (cellIndex, cell) {
                            values[index] = $(this).find("input").val();
                            index++;
                        });
                    });
                    index = 0;
                    $("#matrix" + (matrixNumber - 1)).find("tr").each(function (rowIndex, rowHTML) {
                        $(this).find("td").each(function (cellIndex, cell) {
                            $(this).find("input").val(values[index]);
                            index++;
                        });
                    });
                }
            });

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
