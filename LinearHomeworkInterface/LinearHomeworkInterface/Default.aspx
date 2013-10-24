<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="LinearHomeworkInterface.Default" %>

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
<body style="background-color: #F5F5F5;">
        <div class="container">
            <div class="navbar-inner" style="position: fixed; width: 900px; z-index: 1000;">
                <div class="nav-collapse collapse">
                    <ul class="nav" style="float: left; margin: 10px 0 0px 0;">
                        <li class="dropdown" style="float: left; padding: 0 20px 0 0;"><a id="createAcctLink" data-toggle="dropdown" class="dropdown-toggle" href="#">Create Account <b class="caret"></b></a>
                            <ul class="dropdown-menu">
                                <form id="createAccount" class="signin" runat="server" autocomplete="off">
                                    <h4>New Account</h4>                                    
                                    <input id="username" data-placement="right" data-toggle="tooltip" type="text" maxlength="20" placeholder="Username" />
                                    <input id="first" data-placement="right" data-toggle="tooltip" type="text" maxlength="20" placeholder="First Name" />
                                    <input id="last" data-placement="right" data-toggle="tooltip" type="text" maxlength="20" placeholder="Last Name" />
                                    <input id="password" data-placement="right" data-toggle="tooltip" type="password" maxlength="20" placeholder="Password" />
                                    <input id="retypedpass" data-placement="right" data-toggle="tooltip" type="password" maxlength="20" placeholder="Re-enter Password" />
                                    <input id="accessCode" data-placement="right" data-toggle="tooltip" type="text" maxlength="20" placeholder="Student Access Code" />
                                    <button id="create" class="btn btn-primary">Create</button>
                                    <button id="clearForm" style="float: right;" class="btn">Clear</button>
                                </form>
                            </ul>
                        </li>
                    </ul>
                    <form id="signInForm" class="navbar-form pull-right">
                        <input id="usernameSignIn" data-placement="bottom" data-toggle="tooltip" maxlength="20" type="text" placeholder="Username" class="span2" />
                        <input id="passwordSignIn" maxlength="20" type="password" placeholder="Password" class="span2" />
                        <button id="signIn" class="btn btn-success" type="submit">Sign in</button>
                    </form>
                </div>
            </div>
            <div id="content" style="background: white; margin-top: 50px; height: 550px; border: 1px solid #e5e5e5;">
                <!-- This div background style will be set to a picture that represents the application -->
                <div id="createAcctAlert" class="alert alert-success fade in" style="display: none; width: 400px; margin-top: 5px; margin-left: auto; margin-right: auto;">
                    <a class="close" data-dismiss="alert" href="#">&times;</a>
				    Account Created! It is time to enter the matrix...		
                </div>
                <div id="background" style="display: none; background-image: url('theme/images/axiomz.bmp'); height: 115px; width: 380px; margin: auto; margin-top: 200px;" />                                              
            </div>
        </div>
        <script src="javascript/tooltip.js"></script>
        <script type="text/javascript">
            //If you try to paste bad characters into the input fields then you will be 
            //disabled from pasting into input fields. This will prevent sql injection attacks
            var badInputPaste = false;

            //Do Stuffs
            $(document).ready(function () {
                $("#background").delay("500").fadeIn(5000);

                $("#signIn").click(function (e) {
                    e.preventDefault();
                    var RegEx = /^[a-zA-Z0-9\s]*$/;
                    if (RegEx.test($("#usernameSignIn").val()) && $("#passwordSignIn").val().indexOf(";") && $("#passwordSignIn").val().indexOf("'") && $("#passwordSignIn").val().indexOf("/")) {
                        if (!$("#usernameSignIn").val()) {
                            $("#usernameSignIn").trigger("focus");
                            $("#usernameSignIn").addClass("error");
                        } else if (!$("#passwordSignIn").val()) {
                            $("#passwordSignIn").trigger("focus");
                            $("#passwordSignIn").addClass("error");
                        } else {
                            //This is the call to the controller that handles logging in
                            $.ajax({
                                type: "POST",
                                url: "Default.aspx/SignIn",
                                data: "{'Username': '" + $("#usernameSignIn").val() + "','Password': '" + $("#passwordSignIn").val() + "'}",
                                contentType: "application/json; charset=utf-8",
                                dataType: "json",
                                success: function (url) {
                                    if (url.d === "") {
                                        $("#usernameSignIn").trigger("focus");
                                        $("#usernameSignIn").addClass("error");
                                        $("#passwordSignIn").addClass("error");
                                        $("#usernameSignIn").tooltip({ trigger: "manual", title: "Incorrect Username or Password!" });
                                        $("#usernameSignIn").tooltip("show");
                                    } else {
                                        window.location = url.d;
                                    }
                                },
                                error: function (msg) {
                                    alert("Authentication Failed!");
                                }
                            });
                        }
                    } else {
                        alert("Invalid Characters were entered \nin username and password fields!");
                    }
                });

                $("#create").click(function (e) {
                    e.preventDefault();
                    if (!$("#username").val()) {
                        $("#username").trigger("focus");
                        $("#username").addClass("error");
                        $("#username").tooltip({ trigger: "manual", title: "Don't forget a username" });
                        $("#username").tooltip("show");
                    } else if (!$("#first").val().trim()) {
                        $("#first").trigger("focus");
                        $("#first").addClass("error");
                        $("#first").tooltip({ trigger: "manual", title: "First name is required" });
                        $("#first").tooltip("show");
                    } else if (!$("#last").val().trim()) {
                        $("#last").trigger("focus");
                        $("#last").addClass("error");
                        $("#last").tooltip({ trigger: "manual", title: "" + $("#first").val().trim() + " who?" });
                        $("#last").tooltip("show");
                    } else if (!$("#password").val() || $("#password").val() != $("#retypedpass").val()) {
                        $("#password").trigger("select");
                        $("#password").addClass("error");
                        $("#retypedpass").addClass("error");
                        $("#password").tooltip({ trigger: "manual", title: "Passwords do not match!" });
                        $("#password").tooltip("show");
                    } else if (!$("#accessCode").val() || $("#accessCode").val() != "DU2014") {
                        $("#accessCode").trigger("select");
                        $("#accessCode").addClass("error");
                        $("#accessCode").tooltip({ trigger: "manual", title: "Incorrect Access Code!" });
                        $("#accessCode").tooltip("show");
                    } else {
                        //This call will create the account
                        $.ajax({
                            type: "POST",
                            url: "Default.aspx/CheckUsername",
                            data: "{'username': '" + $("#username").val() + "'}",
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            success: function (available) {
                                if (!available.d) {
                                    $("#username").trigger("select");
                                    $("#username").addClass("error");
                                    $("#username").tooltip({ trigger: "manual", title: "Username is already taken!" });
                                    $("#username").tooltip("show");
                                } else {
                                    var details = $("#username").val() + " " + $("#first").val() + " " + $("#last").val() + " "
                                        + $("#password").val();
                                    $.ajax({
                                        type: "POST",
                                        url: "Default.aspx/CreateAccount",
                                        data: "{'UserDetails': '" + details + "'}",
                                        contentType: "application/json; charset=utf-8",
                                        dataType: "json",
                                        success: function (available) {
                                            $("#createAcctAlert").show();
                                            $(".dropdown").removeClass("open");
                                            $("#clearForm").trigger("click");
                                        },
                                        error: function (response) {
                                            alert("Account Creation Failed");
                                        }
                                    });
                                }
                            },
                            error: function (response) {
                                alert("Account Creation Failed");
                            }
                        });
                    }
                });

                $("#username,#accessCode,#usernameSignIn").keypress(function (e) {
                    var charCode = (e.which) ? e.which : e.keyCode
                    var RegEx = /^[a-zA-Z0-9\s]*$/;
                    var newVal = $(this).val() + String.fromCharCode(charCode);
                    var test = !RegEx.test(newVal);
                    var ctrlKey = e.ctrlKey;
                    if (ctrlKey && charCode == 118 && badInputPaste) {
                        return false;
                    } else {
                        if ((charCode != 32 && RegEx.test(newVal)) || charCode == 8 || (charCode == 46 && e.which == 0) || (!e.shiftKey && (charCode == 37 || (charCode == 39 && e.which == 0)))) {
                            $(this).tooltip("destroy");
                            $(this).removeClass("error");
                        } else {
                            return false;
                        }
                    }
                });

                $("#username,#accessCode,#usernameSignIn").keyup(function (e) {
                    var RegEx = /^[a-zA-Z0-9\s]*$/;
                    if (!RegEx.test($(this).val())) {
                        alert("Pasting has been disabled due to pasting potentially harmful characters...");
                        badInputPaste = true;
                        $(this).val("");
                    }
                });

                $("#usernameSignIn,#username,#accessCode").change(function (e) {
                    var RegEx = /^[a-zA-Z0-9\s]*$/;
                    if (!RegEx.test($(this).val())) {
                        alert("Pasting has been disabled due to pasting potentially harmful characters...");
                        badInputPaste = true;
                        $(this).val("");
                    }
                });

                $("#first,#last").keypress(function (e) {
                    var charCode = (e.which) ? e.which : e.keyCode
                    var curTar = e.currentTarget.id
                    var RegEx = /^[a-zA-Z\s]*$/;
                    var newVal = $(this).val() + String.fromCharCode(charCode);
                    if (((((charCode != 32 || ($(this).val().trim() && $(this).val() == $(this).val().replace(" ", ""))) && RegEx.test(newVal)) || charCode == 8 || (charCode == 46 && e.which == 0)) && !badInputPaste) || (!e.shiftKey && (charCode == 37 || (charCode == 39 && e.which == 0)))) {
                        $(this).tooltip("destroy");
                        $(this).removeClass("error");
                    } else {
                        return false;
                    }
                });

                $("#first,#last").keyup(function (e) {
                    var RegEx = /^[a-zA-Z\s]*$/;
                    if (!RegEx.test($(this).val())) {
                        alert("Pasting has been disabled due to pasting potentially harmful characters...");
                        badInputPaste = true;
                        $(this).val("");
                    }
                });

                $("#first,#last").change(function (e) {
                    var RegEx = /^[a-zA-Z\s]*$/;
                    if (!RegEx.test($(this).val())) {
                        alert("Pasting has been disabled due to pasting potentially harmful characters...");
                        badInputPaste = true;
                        $(this).val("");
                    }
                });

                $("#password,#retypedpass,#passwordSignIn").keypress(function (e) {
                    var charCode = (e.which) ? e.which : e.keyCode
                    var ctrlKey = e.ctrlKey;
                    if (charCode == 118 && ctrlKey == true) {
                        //Do nothing because they are trying to paste the password and they shouldn't be able to
                        return false;
                    } else {
                        if (charCode != 32 && charCode != 39 && charCode != 59 && charCode != 47) {
                            return true;
                        } else {
                            return false;
                        }
                    }
                });

                $("#password,#retypedpass,#passwordSignIn").keyup(function (e) {
                    var charCode = (e.which) ? e.which : e.keyCode
                    if (charCode != 13) {
                        if ($("#password").val() == $("#retypedpass").val() || $(this).attr("id") === $("#passwordSignIn").attr("id")) {
                            $("#password,#retypedpass,#passwordSignIn").tooltip("destroy");
                            $("#password,#retypedpass,#passwordSignIn").removeClass("error");
                        }
                    }
                });

                $("#usernameSignIn").trigger("focus");

                $("#clearForm").click(function (e) {
                    e.preventDefault();
                    $("#username,#first,#last,#password,#retypedpass,#accessCode").tooltip("destroy");
                    $("#username,#first,#last,#password,#retypedpass,#accessCode").removeClass("error");
                    $("#username,#first,#last,#password,#retypedpass,#accessCode").val("");
                    $("#username").focus();
                });

            });
</script>
        <style type="text/css">
            body {
                background-color: #f5f5f5;
            }

            .signin {
                max-width: 200px;
                padding: 5px 35px 10px 15px;
                margin: 0 auto 0px;
                background-color: #fff;
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
