﻿using System;
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
    public partial class QuestionPage : System.Web.UI.Page
    {
        Random rand = new Random();
        String assId;
        String queId;
        int n;
        int m;
        int max;
        int min;
        public static int numOfFreeVars;
        public static Boolean inconsistent;
        String type;
        float[] vector1 = null;
        float[] vector2 = null;
        public static float[] actualAnswer = null;
        public static float[,] matrix = null;
        public static int minNumOfRowOps = 0;

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
                //if (book["inconsistent"] == "0")
                //{
                //    inconsistent = false;
                //}
                //else
                //{
                //    inconsistent = true;
                //}
                //type = "SoE";
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
            
            if(type.Equals("DP")){
                //Displays the two vectors for the Dot Product problem using MATHJAX
                buildQuestionDisplay(vector1, vector2);
            }
            else{
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
            //nextQuestion.Attributes.Add("href", "/QuestionPage?assign=" + assId + "&question=" + (System.Convert.ToInt32(queId) + 1));
        }

        //method to dynamically load the question using MATHJAX
        public void buildQuestionDisplay(float[,] matrix)
        {
            //if we wish to generate a System of Equations question
            if(type == "SoE")
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

            if (type.Equals("SoE")) {
                if ((n == m || (n + 1) == m) && !inconsistent && numOfFreeVars <= 0){//has unique solution if n = m + 1; will have free var if n < m + 1
                    matrix = mb.generateUniqueSolutionMatrix(n, m, min, max, answer);
                } else if (n <= m && inconsistent && numOfFreeVars <= 0) {//inconsistent matrix
                    matrix = mb.generateInconsistentMatrix(n, m, min, max);
                } else if (n <= m && !inconsistent && numOfFreeVars > 0) {//free variable matrix
                    matrix = mb.generateMatrixWithFreeVariables(n, m, min, max, answer, numOfFreeVars);
                } else if (n > m && !inconsistent) {//will have free variables = to n - m + 1 + # of free variables
                    matrix = mb.generateMatrixWithFreeVariables(n, m, min, max, answer, n - m + 1 + numOfFreeVars);
                } else if (n > m && inconsistent) {//not sure 
                    //Current does infinite loop
                    //matrix = generateInconsistentMatrix(n, m, min, max);
                }
                int rowOpsCount = mb.countOperationsNeeded(matrix) - 2;
                rowOpsNeeded.Text = System.Convert.ToString(rowOpsCount);

                //Do the parsing and text adding for question
                instruction.Text = instruction.Text + "<h4 style=\"margin: 0px;\">Question " + queId + "</h4>\n"
                    + "<p style=\"margin: 0px; line-height: 25px; font-size: 14px;\">Solve the system of linear equations by using elementary row operations.</p>";
            } else if (type.Equals("RtI")) {
                matrix = mb.generateRandomIdentityMatrix(n, min, max);

                //Do the parsing and text adding for question
                instruction.Text = instruction.Text + "<h4 style=\"margin: 0px;\">Question " + queId + "</h4>\n"
                    + "<p style=\"margin: 0px; line-height: 25px; font-size: 14px;\">Reduce this matrix to an identity matrix.</p>";
            } else if (type.Equals("DP")) {
                vector1 = mb.generateRandomVector(n, min, max);
                vector2 = mb.generateRandomVector(n, min, max);

                //Do the parsing and text adding for question
                instruction.Text = instruction.Text + "<h4 style=\"margin: 0px;\">Question " + queId + "</h4>\n"
                    + "<p style=\"margin: 0px; line-height: 25px; font-size: 14px;\">Find the dot product between the following two vectors.</p>";
            } else if (type.Equals("D")) {
                matrix = mb.generateRandomIdentityMatrix(n, min, max);

                //Do the parsing and text adding for question
                instruction.Text = instruction.Text + "<h4 style=\"margin: 0px;\">Question " + queId + "</h4>\n"
                    + "<p style=\"margin: 0px; line-height: 25px; font-size: 14px;\">Find the determinant.</p>";
            } else if (type.Equals("I")) {
                matrix = mb.generateRandomIdentityMatrix(n, min, max);

                //Do the parsing and text adding for question
                instruction.Text = instruction.Text + "<h4 style=\"margin: 0px;\">Question " + queId + "</h4>\n"
                    + "<p style=\"margin: 0px; line-height: 25px; font-size: 14px;\">Find the inverse of the following matrix.</p>";
            }

            return matrix;
        }

        [WebMethod]
        public static string Grade(String MatrixMapJSON, String AnswerJSON)
        {
            string feedback = "";
            Dictionary<int, float[,]> MatrixMap = JsonConvert.DeserializeObject<Dictionary<int, float[,]>>(MatrixMapJSON);
            MatrixBuilder.MatrixOperations mb = new MatrixBuilder.MatrixOperations();

            //should probably check if the first matrix is the actual first matrix
            float[,] augMatrix = null; 
            MatrixMap.TryGetValue(0, out augMatrix);
            //Not sure if this if works 
            if (!mb.checkMatrixEquality(matrix,augMatrix))
            {
               feedback += "The first matrix does not match the augmented matrix.";
            }

            if (AnswerJSON.Contains("I"))
            {
                feedback = mb.checkSingleRowOperation(MatrixMap);
                if (!inconsistent)
                {
                    feedback += "The matrix is actually consistent.";
                }
            }
            else if (AnswerJSON.Contains("F"))
            {
                feedback = mb.checkSingleRowOperation(MatrixMap);
                if (numOfFreeVars > 0)
                {
                    Dictionary<int, String> AnswersConverted = JsonConvert.DeserializeObject<Dictionary<int, String>>(AnswerJSON);
                    String[] Answers = new String[AnswersConverted.Count()];
                    AnswersConverted.Values.CopyTo(Answers, 0);
                    feedback += mb.checkFreeVariableAnswers(matrix, Answers);
                }
                else
                {
                    feedback += "The solution actually contains no free variables.";
                }
            }
            else
            {
                Dictionary<int, float> AnswersConverted = JsonConvert.DeserializeObject<Dictionary<int, float>>(AnswerJSON);
                float[] Answers = new float[AnswersConverted.Count()];
                AnswersConverted.Values.CopyTo(Answers, 0);

                feedback = mb.checkSingleRowOperation(MatrixMap);
                //Will need to also check answers here
                feedback += mb.checkAnswers(actualAnswer, Answers);
            }

            //Need to display points somehow
            //feedback += "<div><strong>Points Earned: </strong> 0.7 / 1.0</div>";

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
                    flag = "incomplete";
                }
                else
                {
                    book.Close();
                    String command = "UPDATE hmwkassignment SET status = 'Complete', currentQuestion = 0 WHERE assignmentId = " + assignment;
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