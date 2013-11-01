using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;

namespace AssignComponent
{
    class Assigner
    {
        public Assigner() {
            //no need for code
        }

        //Here is the logic to assign the problems through SQL connection strings
        public String Assign(String title, String dueDate, String[] listOfQuestions)
        {
            //instance variables we will need
            int hwid = 0;
            int i = 0;
            int aid = 0;
            int numstudents = 0;
            Boolean unsupportedQuestionAttempt = false;
            int questioniddecrement = 0;
            int points = 0;
            //first we establish our connection string to our database
            MySqlConnection msqcon = new MySqlConnection("server=localhost;User Id=root;Password=root;database=linearhmwkdb");
            try
            {
                //open the connection
                msqcon.Open();
                //first we need to fetch the latest homeworkid so we can increment it properly later
                MySqlCommand msqcheck = new MySqlCommand("SELECT * FROM homework ORDER BY homeworkid DESC LIMIT 1", msqcon);
                MySqlDataReader hwidbook = msqcheck.ExecuteReader();
                hwidbook.Read();
                hwid = System.Convert.ToInt32(hwidbook["homeworkid"]);
                hwidbook.Close();
                //increment the homeworkId so that it reflects the NEXT homework we're about to assign rather than the previous already assigned HW
                hwid++;
                //then we need to fetch the latest assignmentid so we can increment it properly later
                msqcheck = new MySqlCommand("SELECT * FROM hmwkassignment ORDER BY assignmentId DESC LIMIT 1", msqcon);
                MySqlDataReader aidbook = msqcheck.ExecuteReader();
                aidbook.Read();
                aid = System.Convert.ToInt32(aidbook["assignmentId"]);
                aidbook.Close();
                //we need to know how many students we have so that we can add the assignment to every student
                msqcheck = new MySqlCommand("SELECT COUNT(*) FROM user WHERE role = 'S'", msqcon);
                MySqlDataReader numstudentbook = msqcheck.ExecuteReader();
                numstudentbook.Read();
                numstudents = System.Convert.ToInt32(numstudentbook["COUNT(*)"]);
                numstudentbook.Close();
                //now we can declare our userid array
                int[] studentid = new int[numstudents];
                //now we need to fetch the userid for each cell in our new array
                msqcheck = new MySqlCommand("SELECT userid FROM user WHERE role = 'S'", msqcon);
                MySqlDataReader studentidbook = msqcheck.ExecuteReader();
                i = 0;
                while (studentidbook.Read())
                {
                    studentid[i] = System.Convert.ToInt32(studentidbook["userid"]);
                    i++;
                }
                studentidbook.Close();
                //create our MySql command to handle everything else
                MySqlCommand msqcom = new MySqlCommand();
                //now assign the homework assignment to each student
                for (int j = 0; j < studentid.Length; j++)
                {
                    msqcom = new MySqlCommand("INSERT INTO hmwkassignment (assignmentId, homeworkId, grade, status, currentQuestion, userId) " +
                    "Values (" + (aid + 1) + ", " + hwid + ", " + 0 + ", 'Assigned'," + 1 + ", " + studentid[j] + ")", msqcon);
                    msqcom.ExecuteNonQuery();
                    aid++;
                }
                //now we need to check what type of questions each of these are and insert them in the appropriate tables
                //for each question from our table on the Create Assignment page...
                for (int j = 0; j < listOfQuestions.Length; j++)
                {
                    //if we are dealing with a System of Equations question
                    if (listOfQuestions[j].Contains("SoE") || listOfQuestions[j].Contains("RtI") || listOfQuestions[j].Contains("DP")
                        || listOfQuestions[j].Contains("D") || listOfQuestions[j].Contains("I"))
                    {
                        //split our question into separate strings and convert them to
                        //the appropriate parameters
                        string questionStr = listOfQuestions[j];
                        String[] variables = questionStr.Split(',');
                        int questionid = System.Convert.ToInt32(variables[0])-questioniddecrement;
                        string type = variables[1];
                        int rows = System.Convert.ToInt32(variables[2]);
                        int cols = System.Convert.ToInt32(variables[3]);
                        int min = System.Convert.ToInt32(variables[4]);
                        int max = System.Convert.ToInt32(variables[5]);
                        int freeVar = System.Convert.ToInt32(variables[6]);
                        int inconsistent;
                        if (variables[7].Equals("yes"))
                        {
                            inconsistent = 1;
                        }
                        else
                        {
                            inconsistent = 0;
                        }
                        //then we need to insert the question into the soe table and give
                        //it the other appropriate assignment values
                        //start with reading latest SoEindex to put question into SoE table
                        int qid;
                        msqcheck = new MySqlCommand("SELECT * FROM question ORDER BY questionId DESC LIMIT 1", msqcon);
                        MySqlDataReader questionbook = msqcheck.ExecuteReader();
                        questionbook.Read();
                        qid = System.Convert.ToInt32(questionbook["questionId"]);
                        questionbook.Close();
                        //increment the questionId so that it reflects the current question we're about to assign
                        qid++;
                        //now we need to decrement the assignmentid back for each assignment we processed for each student
                        aid = aid-studentid.Length;
                        //now insert the question into the question table
                        msqcom = new MySqlCommand("INSERT INTO question (questionId, homeworkId, number, pointValue, type, rows, columns, min, max, freeVars, inconsistent) " +
                        "Values (" + qid + ", " + hwid + ", " + questionid + ", " + 1 + ", '" + type + "', " + rows + ", " + cols + ", " + min + ", " + max + ", " + freeVar + ", " + inconsistent + ")", msqcon);
                        msqcom.ExecuteNonQuery();
                        aid++;
                        qid++;
                        questionid++;
                        points++;
                        //ensure our questioniddecrementer remains at 0 for the next question
                        questioniddecrement = 0;
                        //now we've assigned everything, so we move out of our if and let the loop continue to the last question
                    }
                    else
                    {
                        //currently our implementation for the other types of questions are
                        //not supported, so they will come in due time.
                        unsupportedQuestionAttempt = true;
                        questioniddecrement = 1;
                    }
                }
                //insert the new assigned homework into the homework table with values passed from the instructor
                msqcom = new MySqlCommand("INSERT INTO homework (homeworkid, title, points, dueDate, status) " +
                "Values (" + hwid + ", '" + title + "', " + points + ", '" + dueDate + "', 'Assigned')", msqcon);
                msqcom.ExecuteNonQuery();
                //close our connection since we're now finished with assignment insertion
                msqcon.Close();
            }
            catch (Exception error)
            {
                return "An error occurred while creating the assignment: " + error
                    + "The out of bound index was: " + (i);
            }
            if (unsupportedQuestionAttempt == false)
            {
                return "Homework successfully assigned.";
            }
            else
            {
                return "The homework was successfully assigned, but the attempt to assign currently unsupported questions was detected. "
                    + "In the future you will, in fact, be able to assign this type of question. We apologize for the inconvenience.";
            }
        }
    }
}