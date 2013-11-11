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
    public partial class QuestionPage : System.Web.UI.Page
    {
        public static List<float> solution = null;
        public static List<string> textSolution = null;
        Random rand = new Random();
        String assId;
        String queId;
        int n;
        int m;
        int max;
        int min;
        int numOfFreeVars;
        Boolean inconsistent;
        String type;
        float[,] matrix = null;
        float[] vector1 = null;
        float[] vector2 = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            this.Check_User();

            assId = Request.QueryString["assign"];
            queId = Request.QueryString["question"];

            //set up connection, do stuff
            string connStr = ConfigurationManager.ConnectionStrings["linearhmwkdb"].ConnectionString;
            MySqlConnection msqcon = new MySqlConnection(connStr);
            try
            {
                msqcon.Open();
                String query = "SELECT ha.assignmentId, que.* FROM hmwkassignment AS ha JOIN question AS que WHERE que.homeworkId=ha.homeworkid AND ha.assignmentId = " + assId + " AND que.number = " + queId;
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
            //Boolean inconsistent = false;
            
            solution = new List<float>();

            float[] answer = new float[n];
            for (int i = 0; i < n; i++)
            {
                answer[i] = rand.Next(min, max);
                solution.Add(answer[i]);
            }
            float[,] matrix = this.Create_Problem(n, m, min, max, numOfFreeVars, inconsistent, type, answer);
            
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
                String username = Context.User.Identity.Name;

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

                msqcon.Close();
            }
            catch (Exception)
            {
                throw;
            }
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
            //These should be moved out of method to be global

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
            Dictionary<int, float> Answers = JsonConvert.DeserializeObject<Dictionary<int, float>>(AnswerJSON);
            MatrixBuilder.MatrixOperations mb = new MatrixBuilder.MatrixOperations();

            feedback = mb.checkSingleRowOperation(MatrixMap);
            //Will need to also check answers here

            //return feedback.Equals("") ? null : feedback;

            //had to change return statement for feedback message purposes, can change later
            return "<h4 style=\"color:blue\" id = \"result\">Grade display successful</h4>";
        }

        ////our WebMethod for checking the user's solution(s)
        //[WebMethod]
        //public static string GradeAnswer(String ListPassingSolutions)
        //{
        //    //create a String array of the answers submitted from the user page
        //    //splits by space
        //    string[] lines = ListPassingSolutions.Split(' ');

        //    //create a float array to represent our solutions being passed into
        //    //this method
        //    float[] UserSolutions = new float[lines.Length];

        //    //create a counter
        //    int x = 0;

        //    //create a string array which will pass our free variables to the grader
        //    string[] freeVar = new string[lines.Length];

        //    //for all the elements being passed in our array of user strings
        //    for (int i = 0; i < lines.Length; i++)
        //    {
        //        //if our current element is a free variable or blank
        //        if(lines[i].Equals("f") || lines[i].Equals(null)) {
        //            //add it to the free variables string array
        //            freeVar[x] = lines[i];

        //            //increment our counter
        //            x++;
        //        }

        //        else {
        //            //convert each string into an integer and add it to our integer
        //            //array for the grading component
        //            UserSolutions[x] = System.Convert.ToInt32(lines[i]);

        //            //add an empty string to the free variables string array
        //            freeVar[x] = " ";

        //            //increment our counter
        //            x++;
        //        }
        //    }
        //    //create our grading object to grade the user's answers
        //    GradeComponent.Grader grader = new GradeComponent.Grader();

        //    //have this created object grade the user's solutions against
        //    //the answers of our generated matrix
        //    return grader.Grade(solution, UserSolutions, textSolution, freeVar);
        //}

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