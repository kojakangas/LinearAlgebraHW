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
</head>
<body>
    <div class="container">
    <div class="navbar-inner">
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
				<a class="btn" style="margin-top: 5px;" type="submit">Sign Out</a>
			</form>
		</div><!--/.nav-collapse -->
	</div>

    <div style="padding-top:10px;">
      <div class="row-fluid">
        <div class="span3">
          <div class="well sidebar-nav" style="height: 700px;">
            <ul class="nav nav-list">
              <li class="nav-header">Tools</li>
              <li><a href="#">Create Matrix</a></li>
              <li><a href="#">Link</a></li>
              <li><a href="#">Link</a></li>
              <li><a href="#">Link</a></li>
            </ul>
          </div><!--/.well -->
        </div><!--/span-->
        <div class="span9">
          <div class="hero-unit" style="padding: 10px; margin-bottom: 0px;">
            <h3 style="margin: 0px;">Question 1</h3>
            <p style="margin: 0px;">Find the solution.</p>
			<p style="margin: 0px;">2x + 3y = 4</p>
			<p style="margin: 0px;">x + y = 2</p>
          </div>
          <div class="row-fluid">
            <div class="span4">
              <h2>Matrix Spot</h2>
              <p><a class="btn" href="#">View details »</a></p>
            </div><!--/span-->
            <div class="span4">
              <h2>Heading</h2>
              <p><a class="btn" href="#">View details »</a></p>
            </div><!--/span-->
            <div class="span4">
              <h2>Heading</h2>
              <p><a class="btn" href="#">View details »</a></p>
            </div><!--/span-->
          </div><!--/row-->
          <div class="row-fluid">
            <div class="span4">
              <h2>Heading</h2>
              <p><a class="btn" href="#">View details »</a></p>
            </div><!--/span-->
            <div class="span4">
              <h2>Heading</h2>
              <p><a class="btn" href="#">View details »</a></p>
            </div><!--/span-->
            <div class="span4">
              <h2>Heading</h2>
              <p><a class="btn" href="#">View details »</a></p>
            </div><!--/span-->
          </div><!--/row-->
        </div><!--/span-->
      </div><!--/row-->

      <hr>

      <footer>
        <p>&copy; Company 2013</p>
      </footer>

    </div><!--/.fluid-container-->
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
</body>
</html>
