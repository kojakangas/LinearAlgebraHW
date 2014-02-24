using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Data.Sql;
using System.Data.Linq;
using MySql.Data.MySqlClient;
using System.Web.Security;
using System.Configuration;
using System.Text;
using Newtonsoft.Json;

namespace LinearHomeworkInterface
{
    public partial class QuestionLinearDependence : System.Web.UI.Page
    {
        Random rand = new Random();
        String assId;
        String queId;
        int n;
        int m;
        int max;
        int min;
        public int numOfFreeVars;
        public Boolean inconsistent;
        String type;
        float[] vector1 = null;
        float[] vector2 = null;
        public float[] actualAnswer = null;
        public float[,] matrix = null;
        public int minNumOfRowOps = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            this.Check_User();
            instruction.Text = "";
            question.Text = "";

            assId = Request.QueryString["assign"];
            queId = Request.QueryString["question"];

            //set up connection, do stuff
            string connStr = ConfigurationManager.ConnectionStrings["linearhmwkdb"].ConnectionString;
            MySqlConnection msqcon = new MySqlConnection(connStr);
            //username is needed for assignment check
            String username = Context.User.Identity.Name;
            try
            {
                msqcon.Open();
                //grab assignments status
                String query = "SELECT ha.status From user JOIN hmwkassignment AS ha WHERE user.username=@username AND user.userId=ha.userId AND ha.assignmentId=@assignment";
                MySqlCommand msqcmd = new MySqlCommand(query, msqcon);
                msqcmd.Parameters.Add(new MySqlParameter("@username", username));
                msqcmd.Parameters.Add(new MySqlParameter("@assignment", Request.QueryString["assign"]));
                MySqlDataReader validateAssignment = msqcmd.ExecuteReader();
                String assignmentStatus = "";
                while (validateAssignment.Read())
                {
                    assignmentStatus = validateAssignment.GetString(0);
                }
                validateAssignment.Close();
                //check that the url parameter points to an assignment id associated with their user and is currently available to work
                if (assignmentStatus != "Assigned" && assignmentStatus != "In Progress")
                {
                    //redirect to home page if got a tampered parameter
                    Response.Redirect("StudentHome.aspx");
                }
                //now we need to check if the assignment is not late
                query = "SELECT h.status FROM homework AS h JOIN hmwkassignment AS ha WHERE ha.assignmentId=@assignment AND h.homeworkid = ha.homeworkId";
                msqcmd = new MySqlCommand(query, msqcon);
                msqcmd.Parameters.Add(new MySqlParameter("@assignment", Request.QueryString["assign"]));
                MySqlDataReader doubleValidate = msqcmd.ExecuteReader();
                doubleValidate.Read();
                assignmentStatus = System.Convert.ToString(doubleValidate["status"]);
                doubleValidate.Close();
                //check that the assignment is not late through the homework table
                if (assignmentStatus != "Assigned")
                {
                    //redirect to home page if the assignment is late
                    Response.Redirect("StudentHome.aspx");
                }
                //after this check we can update the status of this assignment
                else
                {
                    String command = "UPDATE hmwkassignment SET status = 'In Progress' WHERE assignmentId = @assignmentId";
                    msqcmd = new MySqlCommand(command, msqcon);
                    msqcmd.Parameters.Add(new MySqlParameter("@assignmentId", Request.QueryString["assign"]));
                    msqcmd.ExecuteNonQuery();
                }
                /* current question parameter may be uneeded, in which case this query will get the current question
                 * advantage of keeping parameter is if it needs to be referenced outside of page load */
                //fetch actual current question based on assignmentId as passed in url
                query = "SELECT ha.currentQuestion, ha.homeworkId FROM hmwkassignment AS ha WHERE ha.assignmentId=@assignment";
                msqcmd = new MySqlCommand(query, msqcon);
                msqcmd.Parameters.Add(new MySqlParameter("@assignment", Request.QueryString["assign"]));
                MySqlDataReader currentQuestion = null;
                currentQuestion = msqcmd.ExecuteReader();
                int curQuestion = 0;
                String homeworkID = "";
                currentQuestion.Read();
                curQuestion = currentQuestion.GetInt32(0);
                homeworkID = currentQuestion.GetString(1);
                currentQuestion.Close();
                /*this if would be uneccessary if parameter gets dropped*/
                //check url parameter against database result
                if (Convert.ToInt32(Request.QueryString["question"]) != curQuestion)
                {
                    //redirect them to the question they are actually on if didn't match
                    //however this solution will allow the user to get a new question at will by changing the url parameter
                    Response.Redirect("QuestionPage.aspx?assign=" + Request.QueryString["assign"] + "&question=" + curQuestion);
                }
                query = "SELECT * FROM question AS q WHERE @homework=q.homeworkId";
                msqcmd = new MySqlCommand(query, msqcon);
                msqcmd.Parameters.Add(new MySqlParameter("@homework", homeworkID));
                MySqlDataReader questions = null;
                questions = msqcmd.ExecuteReader();

                //build table
                StringBuilder sb = new StringBuilder();
                int count = 1;
                while (questions.Read())
                {
                    if (count < curQuestion)
                    {
                        sb.Append("<li class=\"disabled\">");
                    }
                    else if (count == curQuestion)
                    {
                        sb.Append("<li class=\"active\">");
                    }
                    else sb.Append("<li>");
                    sb.Append("<a>");
                    sb.Append(count.ToString());
                    sb.Append("</a>");
                    sb.Append("</li>");
                    count++;
                }
                paginationLiteral.Text = sb.ToString();
                questions.Close();
                //now read the data needed to generate our question
                query = "SELECT ha.assignmentId, que.* FROM hmwkassignment AS ha JOIN question AS que WHERE que.homeworkId=ha.homeworkid AND ha.assignmentId = " + assId + " AND que.number = " + queId;
                MySqlCommand msqcom = new MySqlCommand(query, msqcon);
                MySqlDataReader book = msqcom.ExecuteReader();
                book.Read();
                n = System.Convert.ToInt32(book["rows"]);
                m = System.Convert.ToInt32(book["columns"]);
                min = System.Convert.ToInt32(book["min"]);
                max = System.Convert.ToInt32(book["max"]);
                numOfFreeVars = System.Convert.ToInt32(book["freeVars"]);
                inconsistent = System.Convert.ToBoolean(book["inconsistent"]);
                type = System.Convert.ToString(book["type"]);

                msqcon.Close();
            }
            catch (Exception)
            {

                throw;
            }

            actualAnswer = new float[n];
            for (int i = 0; i < n; i++)
            {
                actualAnswer[i] = rand.Next(min, max);
            }
            matrix = this.Create_Problem(n, m, min, max, numOfFreeVars, inconsistent, type, actualAnswer);

            if (type.Equals("DP"))
            {
                //Displays the two vectors for the Dot Product problem using MATHJAX
                buildQuestionDisplay(vector1, vector2);
            }
            else
            {
                //Displays the equations or matrices using MATHJAX by building a parsable string
                buildQuestionDisplay(matrix);
            }

            try
            {


                msqcon.Open();



                msqcon.Close();
            }
            catch (Exception)
            {
                throw;
            }

            HttpContext.Current.Session["matrix"] = matrix;
            HttpContext.Current.Session["actualAnswer"] = actualAnswer;
            HttpContext.Current.Session["minNumOfRowOps"] = minNumOfRowOps;
            HttpContext.Current.Session["inconsistent"] = inconsistent;
            HttpContext.Current.Session["numOfFreeVars"] = numOfFreeVars;
        }

        //method to dynamically load the question using MATHJAX
        public void buildQuestionDisplay(float[,] matrix)
        {
            //if we wish to generate a System of Equations question
            if (type == "SoE")
            {
                for (int i = 0; i < n; i++)
                {
                    char[] a = new char[1];
                    String expression = new String(a);
                    for (int j = 0; j < m; j++)
                    {
                        if (j == 0)
                        {
                            expression = "$${";
                            expression += matrix[i, j];
                            expression += "x_";
                            expression += j + 1;
                            expression += " ";
                        }
                        else if (j < (m - 2))
                        {
                            if (matrix[i, j] >= 0) expression += "+ ";
                            else expression += " ";
                            expression += matrix[i, j];
                            expression += "x_";
                            expression += j + 1;
                            expression += " ";
                        }
                        else if (j == (m - 2))
                        {
                            if (matrix[i, j] >= 0) expression += "+ ";
                            else expression += " ";
                            expression += matrix[i, j];
                            expression += "x_";
                            expression += j + 1;
                            expression += "} = ";
                        }
                        else if (j == (m - 1))
                        {
                            expression += matrix[i, j];
                            expression += "$$";
                        }
                    }
                    question.Text = question.Text + expression;
                }
            }
            //if this is a Reduce to Identity, Determinant, or Inverse problem
            else if (type == "RtI" || type == "D" || type == "I")
            {
                String expression;
                expression = "$$\\begin{bmatrix}";
                for (int i = 0; i < n; i++)
                {
                    for (int j = 0; j < m; j++)
                    {
                        if (j == (m - 1))
                        {
                            expression += matrix[i, j];
                            expression += " \\\\";
                        }
                        else
                        {
                            expression += matrix[i, j];
                            expression += " & ";
                        }
                    }
                }
                expression += "\\end{bmatrix}$$";
                question.Text = question.Text + expression;
            }
            //if this is a Linear Dependence/Independence Question
            else if (type == "ID")
            {
                String expression = "$$";
                for (int j = 0; j < m; j++)
                {
                    expression += "\\begin{pmatrix}";
                    for (int i = 0; i < n; i++)
                    {
                        expression += matrix[i,j];
                        expression += " \\\\";
                    }
                    expression += "\\end{pmatrix}";
                    if (j<m-1) expression += ",";
                }
                question.Text = question.Text + expression + "$$";
            }
            //fallback for if we are trying to display a question we haven't implemented
            else
            {
                question.Text = question.Text + "Question currently unsupported...";
            }
        }

        //additional overload method for generating dot product problem
        public void buildQuestionDisplay(float[] vector1, float[] vector2)
        {
            //form our vectors with dot in between
            String expression;
            expression = "$$\\begin{pmatrix}";
            for (int i = 0; i < n; i++)
            {
                expression += vector1[i];
                expression += " \\\\";
            }
            expression += "\\end{pmatrix}";
            expression += "\\cdot";
            expression += "\\begin{pmatrix}";
            for (int i = 0; i < n; i++)
            {
                expression += vector2[i];
                expression += " \\\\";
            }
            expression += "\\end{pmatrix}$$";
            question.Text = question.Text + expression;
        }

        public float[,] Create_Problem(int n, int m, int min, int max, int numOfFreeVars, bool inconsistent, string type, float[] answer)
        {
            MatrixBuilder.MatrixOperations mb = new MatrixBuilder.MatrixOperations();

            if (type.Equals("SoE"))
            {
                if ((n == m || (n + 1) == m) && !inconsistent && numOfFreeVars <= 0)
                {//has unique solution if n = m + 1; will have free var if n < m + 1
                    matrix = mb.generateUniqueSolutionMatrix(n, m, min, max, answer);
                }
                else if (n <= m && inconsistent && numOfFreeVars <= 0)
                {//inconsistent matrix
                    matrix = mb.generateInconsistentMatrix(n, m, min, max);
                }
                else if (n <= m && !inconsistent && numOfFreeVars > 0)
                {//free variable matrix
                    matrix = mb.generateMatrixWithFreeVariables(n, m, min, max, answer, numOfFreeVars);
                }
                else
                {
                    //This is the catch all. Not sure how accurate it is
                    matrix = mb.generateRandomMatrix(n, m, min, max);
                }
                int rowOpsCount = mb.countOperationsNeeded(matrix) - 2;
                rowOpsNeeded.Text = System.Convert.ToString(rowOpsCount);

                //Do the parsing and text adding for question
                instruction.Text = instruction.Text + "<h4 style=\"margin: 0px;\">Question " + queId + "</h4>\n"
                    + "<p style=\"margin: 0px; line-height: 25px; font-size: 14px;\">Solve the system of linear equations by using elementary row operations.</p>";
            }
            else if (type.Equals("RtI"))
            {
                matrix = mb.generateRandomIdentityMatrix(n, min, max);

                //Do the parsing and text adding for question
                instruction.Text = instruction.Text + "<h4 style=\"margin: 0px;\">Question " + queId + "</h4>\n"
                    + "<p style=\"margin: 0px; line-height: 25px; font-size: 14px;\">Reduce this matrix to an identity matrix.</p>";
            }
            else if (type.Equals("DP"))
            {
                vector1 = mb.generateRandomVector(n, min, max);
                vector2 = mb.generateRandomVector(n, min, max);

                //Do the parsing and text adding for question
                instruction.Text = instruction.Text + "<h4 style=\"margin: 0px;\">Question " + queId + "</h4>\n"
                    + "<p style=\"margin: 0px; line-height: 25px; font-size: 14px;\">Find the dot product between the following two vectors.</p>";
            }
            else if (type.Equals("D"))
            {
                matrix = mb.generateRandomIdentityMatrix(n, min, max);

                //Do the parsing and text adding for question
                instruction.Text = instruction.Text + "<h4 style=\"margin: 0px;\">Question " + queId + "</h4>\n"
                    + "<p style=\"margin: 0px; line-height: 25px; font-size: 14px;\">Find the determinant.</p>";
            }
            else if (type.Equals("I"))
            {
                matrix = mb.generateRandomIdentityMatrix(n, min, max);

                //Do the parsing and text adding for question
                instruction.Text = instruction.Text + "<h4 style=\"margin: 0px;\">Question " + queId + "</h4>\n"
                    + "<p style=\"margin: 0px; line-height: 25px; font-size: 14px;\">Find the inverse of the following matrix.</p>";
            }
            else if (type.Equals("ID"))
            {
                instruction.Text = instruction.Text + "<h4 style=\"margin: 0px;\">Question " + queId + "</h4>\n"
                    + "<p style=\"margin: 0px; line-height: 25px; font-size: 14px;\">Determing if the following vectors are linearly dependent or independent.</p>";
                if (inconsistent)
                {
                    matrix = mb.generateDependentMatrix(n, m, min, max);
                }
                else
                {
                    matrix = mb.generateIndependentMatrix(n, m, min, max);
                }
            }
            return matrix;
        }

        [WebMethod]
        public static string Grade(String MatrixMapJSON, String AnswerJSON, String question, String assignment)
        {
            //initialize floats for total point value and their grade so far on assignment
            float questionValue = 0;
            float currentGrade = 0;

            //Get session variables
            float[,] sessionMatrix = (float[,])HttpContext.Current.Session["matrix"];
            float[] sessionActualAnswer = (float[])HttpContext.Current.Session["actualAnswer"];
            int sessionMinNumOfRowsOps = (int)HttpContext.Current.Session["minNumOfRowOps"];
            Boolean sessionInconsistent = (Boolean)HttpContext.Current.Session["inconsistent"];
            int sessionNumOfFreeVars = (int)HttpContext.Current.Session["numOfFreeVars"];

            //query to fetch question's point value and their current grade on the assignment
            string connStr = ConfigurationManager.ConnectionStrings["linearhmwkdb"].ConnectionString;
            MySqlConnection msqcon = new MySqlConnection(connStr);
            try
            {
                msqcon.Open();
                String query = "SELECT q.pointValue, ha.grade FROM question AS q JOIN hmwkassignment AS ha WHERE ha.homeworkId=q.homeworkId AND ha.assignmentId = " + assignment + " AND q.number = " + question;
                MySqlCommand msqcmd = new MySqlCommand(query, msqcon);
                MySqlDataReader points = msqcmd.ExecuteReader();
                points.Read();
                questionValue = points.GetFloat(0);
                currentGrade = points.GetFloat(1);
                points.Close();
            }
            catch (Exception)
            {
                throw;
            }


            string feedback = "";
            Dictionary<int, float[,]> MatrixMap = JsonConvert.DeserializeObject<Dictionary<int, float[,]>>(MatrixMapJSON);
            MatrixBuilder.MatrixOperations mb = new MatrixBuilder.MatrixOperations();

            //get the amount to deduct by dividing the total points by our approximate number of steps to solve (+1 for first matrix) (+1 for copying the answer correctly)
            //shifted then rounded to keep it to 2 decimal points, then shifted back
            float deductValue = (float)Math.Round((questionValue / ((float)mb.countOperationsNeeded(sessionMatrix) + 2F)) * 100F) / 100F;
            //if above gives a value of 0 (a rediculously large number of steps), correct to .01 so points are actually deducted
            if (deductValue == 0) deductValue = .01F;

            //should probably check if the first matrix is the actual first matrix
            float[,] augMatrix = null;
            MatrixMap.TryGetValue(0, out augMatrix);
            //Not sure if this if works 
            if (!mb.checkColumnEquality(sessionMatrix, augMatrix))
            {
                feedback += "<div>The first matrix does not contain the correct set of vectors.<div>";
            }

            if (AnswerJSON.Contains("I"))
            {
                feedback += mb.checkSingleRowOperation(MatrixMap);
                if (sessionInconsistent)
                {
                    feedback += "<div>The matrix is actually dependent.<div>";
                }
            }
            else if (AnswerJSON.Contains("D"))
            {
                feedback += mb.checkSingleRowOperation(MatrixMap);
                if (!sessionInconsistent)
                {
                    feedback += "<div>The matrix is actually independent.<div>";
                }
            }
            else
            {
                Dictionary<int, float> AnswersConverted = JsonConvert.DeserializeObject<Dictionary<int, float>>(AnswerJSON);
                float[] Answers = new float[AnswersConverted.Count()];
                AnswersConverted.Values.CopyTo(Answers, 0);

                feedback += mb.checkSingleRowOperation(MatrixMap);
                //Will need to also check answers here
                feedback += mb.checkAnswers(sessionActualAnswer, Answers);
            }

            float grade = questionValue;
            //ensure there was feedback, if not, will retain value of the question's total points
            if (!feedback.Equals(""))
            {
                //Run through feedback, create array of statements based on . to count number of point deductions
                String[] deductionStrings = feedback.Split('.');
                //set grade to the total minus the deduct amount times number of mistakes
                grade = questionValue - deductValue * (deductionStrings.Length - 1);
                //correct any odd values from results close to 0 by re-rounding to 2 decimal points
                grade = (float)Math.Round(grade * 100F) / 100F;
            }
            //make their grade go no lower than 0 (no negative points)
            if (grade < 0) grade = 0;
            //create grade to write back to database
            float updatedGrade = grade + currentGrade;

            //Need to display points somehow
            if (grade == 1)
            {
                feedback += "<!-- -->";
            }
            feedback += "<div><strong>Points Earned: </strong>" + grade + " / " + questionValue + "<div>";

            //Update grade in database
            msqcon = new MySqlConnection(connStr);
            try
            {
                msqcon.Open();
                String query = "UPDATE hmwkassignment AS ha SET ha.grade = " + updatedGrade + " WHERE assignmentId = " + assignment;
                MySqlCommand msqcmd = new MySqlCommand(query, msqcon);
                msqcmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }

            return String.IsNullOrEmpty(feedback) ? null : feedback;
        }

        [WebMethod]
        public static String updateForNextQuestion(String question, String assignment)
        {
            String flag;
            //set up connection, do stuff
            string connStr = ConfigurationManager.ConnectionStrings["linearhmwkdb"].ConnectionString;
            MySqlConnection msqcon = new MySqlConnection(connStr);
            try
            {
                msqcon.Open();
                String query = "SELECT ha.assignmentId, ha.currentQuestion, h.points FROM hmwkassignment AS ha JOIN homework AS h WHERE h.homeworkId=ha.homeworkid AND ha.assignmentId = " + assignment;
                MySqlCommand msqcheck = new MySqlCommand(query, msqcon);
                MySqlDataReader book = msqcheck.ExecuteReader();
                book.Read();
                if (System.Convert.ToInt32(book["currentQuestion"]) < System.Convert.ToInt32(book["points"]))
                {
                    book.Close();
                    String command = "UPDATE hmwkassignment SET currentQuestion = " + (System.Convert.ToInt32(question) + 1) + " WHERE assignmentId = " + assignment;
                    MySqlCommand msqcom = new MySqlCommand(command, msqcon);
                    msqcom.ExecuteNonQuery();

                    command = "SELECT q.type FROM question AS q JOIN hmwkassignment AS h WHERE h.assignmentId=" + assignment + " AND q.homeworkid = h.homeworkid AND q.number = " + (System.Convert.ToInt32(question) + 1);
                    msqcom = new MySqlCommand(command, msqcon);
                    MySqlDataReader qType = msqcom.ExecuteReader();
                    qType.Read();

                    flag = "incomplete ";
                    flag += qType.GetString(0);

                }
                else
                {
                    book.Close();
                    String command = "UPDATE hmwkassignment SET status = 'Complete' WHERE assignmentId = " + assignment;
                    MySqlCommand msqcom = new MySqlCommand(command, msqcon);
                    msqcom.ExecuteNonQuery();
                    flag = "complete";
                }
                msqcon.Close();
            }
            catch (Exception)
            {
                throw;
            }
            return flag;
        }

        protected void Check_User()
        {
            HttpCookie cookie = Request.Cookies["LINALGHW"];
            FormsAuthenticationTicket t = null;
            string[] user = null;

            if (cookie != null)
            {
                t = FormsAuthentication.Decrypt(cookie.Value);
                user = t.UserData.ToString().Split(' ');
            }

            if (t == null)
            {
                Server.Transfer("Default.aspx", true);
            }
            else if (user[0].Equals("I"))
            {
                Server.Transfer("InstructorHome.aspx", true);
            }
        }
    }
}