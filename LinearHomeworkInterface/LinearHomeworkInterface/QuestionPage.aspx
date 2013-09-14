<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="QuestionPage.aspx.cs" Inherits="LinearHomeworkInterface.QuestionPage" %>

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
                <form class="navbar-form pull-right" style="">
                    <button class="btn" style="margin-top: 5px;" type="submit">Sign Out</button>
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
                                    <li style="margin-bottom: 15px;">
                                        <input id="rows" type="text" onkeypress="return validateNumericInput(event)" class="span4" style="float: left; margin-left: 20px" placeholder="n" />
                                        <div style="display: inline; margin-left: 4px;">X</div>
                                        <input id="columns" type="text" onkeypress="return validateNumericInput(event)" class="span4" style="float: right; margin-right: 20px" placeholder="m" />
                                    </li>
                                    <li><a id="makeMatrix" class="btn" style="margin: 0px 5px 5px 5px;">Create</a></li>
                                </ul>
                            </li>
                            <li><a id="resetQuestion" onclick="resetQuestion()" href="#">Reset Question</a></li>
                            <li class="dropdown">
                                <a data-toggle="dropdown" class="dropdown-toggle" href="#">Answer <b class="caret"></b></a>
                                <ul class="dropdown-menu">
                                    <li style="margin-left: 5px;">
                                        <h5>Solution: </h5>
                                    </li>
                                    <li>
                                        <input id="variables" type="text" onkeypress="return validateNumericInput(event)" class="span10" style="float: left; margin-left: 13px" placeholder="# of elements" />
                                    </li>
                                    <li><a id="makeAnswers" class="btn" style="margin: 0px 5px 5px 5px;">Create</a></li>
                                </ul>
                            </li>
                        </ul>
                    </div>
                    <!--/.well -->
                </div>
                <!--/span-->
                <div class="span9">
                    <div class="hero-unit" style="padding: 10px; margin-bottom: 0px;">
                        <h3 style="margin: 0px;">Question 1</h3>
                        <p style="margin: 0px;">Find the solution.</p>
                        <p style="margin: 0px;">2x + 3y = 4</p>
                        <p style="margin: 0px;">x + y = 2</p>
                        <p>
                            $${2x_1 + 3x_2} = 4$$
                            $${x_1 + x_2} = 2$$
                        </p>
                        <asp:DataGrid ID="DataGrid" ShowHeader="False" RowHeadersVisible="false" GridLines="None" runat="server" AutoGenerateColumns="true"></asp:DataGrid>
                    </div>
                    <form>
                        <div id="matrixHolder" style="display: inline-block; width: 100%;">
                            <!-- jQuery appends the matrices here-->
                            <div id="info" style="color: #888;">Use the Tools to answer the question...</div>
                        </div>
                        <hr style="margin-bottom: 0px; margin-top: 0px;"/>
                        <a class="btn btn-primary" style="margin-top: 5px; float: right; margin-bottom: 50px;" href="#" type="submit">Submit Answer</a>
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
            if (charCode > 31 && (charCode < 48 || charCode > 57))
                return false;
            return true;
        }

        var matrixNumber = 0;
        var generatedAnswer = false;

        function resetQuestion() {
            if (confirm("Are you sure you want to reset this question?")) {
                matrixNumber = 0;
                generatedAnswer = false;
                $('#matrixHolder').empty();
            }
        }

        function addFreeVariable(index) {
            $('#var' + index).val("f");
            $('#var' + index).attr("disabled", "true");
            $('#freeLink' + index).text("Remove");
            $('#freeLink' + index).attr("onclick", "removeFreeVariable(" + index + ")");
        }

        function removeFreeVariable(index) {
            $('#var' + index).val("");
            $('#var' + index).removeAttr("disabled");
            $('#freeLink' + index).text("Set Free Variable");
            $('#freeLink' + index).attr("onclick", "addFreeVariable(" + index + ")");
        }

        $(document).ready(function () {
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
                            $('#matrix' + matrixNumber + 'row' + i).append("<td><input onkeypress=\"return validateNumericInput(event)\" style=\"width: 27px;\"></input></td>");
                        }
                    }
                    $('#row' + matrixNumber).append("<div class=\"alert alert-success\" style=\"display:flex;\">Correct!</div>");
                    matrixNumber = matrixNumber + 1;
                }
            });

            $('#makeAnswers').click(function () {
                var variables = $('#variables').val();
                if (!(variables === "") && generatedAnswer === false) {
                    generatedAnswer = true;
                    $('#matrixHolder').append("<h4>Answer: </h4>");
                    for (var i = 0; i < variables; i++) {
                        $('#matrixHolder').append("<strong>x<sub>" + (i + 1) + "</sub> = </strong><input id=\"var" + i + "\" onkeypress=\"return validateNumericInput(event)\" style=\"width: 27px; margin-right: 3px;\"></input><a id=\"freeLink" + i + "\" onclick=\"addFreeVariable(" + i + ")\" tabindex=\"-1\" href=\"#\">Set Free Variable</a></br>");
                    }
                }
            });
        });
    </script>
</body>
</html>
