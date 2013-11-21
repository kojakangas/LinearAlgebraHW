using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
using System.Configuration;

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
            int PKincrement = 0;
            int qid = 0;
            //first we establish our connection string to our database
            string connStr = ConfigurationManager.ConnectionStrings["linearhmwkdb"].ConnectionString;
            MySqlConnection msqcon = new MySqlConnection(connStr);
            try
            {
                //open the connection
                msqcon.Open();
                //we now check how many rows we have in our homework table
                MySqlCommand msqcheck = new MySqlCommand("SELECT COUNT(*) FROM homework", msqcon);
                MySqlDataReader numhwsbook = msqcheck.ExecuteReader();
                numhwsbook.Read();
                //set our Primary Key incrementer to the number of rows returned
                PKincrement = System.Convert.ToInt32(numhwsbook["COUNT(*)"]);
                //close our DataReader for this operation
                numhwsbook.Close();
                //if there are currently no rows of data in our homework table
                if(PKincrement == 0){
                    //we simply set our hwid to 1, inserting our first row of data later
                    hwid = 1;
                }
                //otherwise there are rows so the following statements are executed
                else{
                    //first we need to fetch the latest homeworkid so we can increment it properly later
                    msqcheck = new MySqlCommand("SELECT * FROM homework ORDER BY homeworkid DESC LIMIT 1", msqcon);
                    MySqlDataReader hwidbook = msqcheck.ExecuteReader();
                    hwidbook.Read();
                    //set hwid to the latest homeworkid from homework table
                    hwid = System.Convert.ToInt32(hwidbook["homeworkid"]);
                    //close our DataReader for this operation
                    hwidbook.Close();
                    //increment the homeworkId so that it reflects the NEXT homework we're about to assign rather than the previous already assigned HW
                    hwid++;
                }
                //we now check how many rows we have in our hmwkassignment table
                msqcheck = new MySqlCommand("SELECT COUNT(*) FROM hmwkassignment", msqcon);
                MySqlDataReader numassbook = msqcheck.ExecuteReader();
                numassbook.Read();
                //set our Primary Key incrementer to the number of rows returned
                PKincrement = System.Convert.ToInt32(numassbook["COUNT(*)"]);
                //close our DataReader for this operation
                numassbook.Close();
                //if there are currently no rows of data in our hmwkassignment table
                if (PKincrement == 0)
                {
                    //we simply set our aid to 1, inserting our first row of data later
                    aid = 1;
                }
                //otherwise there are rows so the following statements are executed
                else
                {
                    //we need to fetch the latest assignmentid so we can increment it properly later
                    msqcheck = new MySqlCommand("SELECT * FROM hmwkassignment ORDER BY assignmentId DESC LIMIT 1", msqcon);
                    MySqlDataReader aidbook = msqcheck.ExecuteReader();
                    aidbook.Read();
                    //set aid to the latest assignmentId found in the hmwkassignment table
                    aid = System.Convert.ToInt32(aidbook["assignmentId"]);
                    //close our DataReader for this operation
                    aidbook.Close();
                    //increment the assignmentId so that it reflects the NEXT assignment we're about to assign rather than the previous already assigned HW
                    aid++;
                }
                //we need to know how many students we have so that we can add the assignment to every student
                msqcheck = new MySqlCommand("SELECT COUNT(*) FROM user WHERE role = 'S'", msqcon);
                MySqlDataReader numstudentbook = msqcheck.ExecuteReader();
                numstudentbook.Read();
                //set numstudents to the number of student users returned
                numstudents = System.Convert.ToInt32(numstudentbook["COUNT(*)"]);
                //close our DataReader for this operation
                numstudentbook.Close();
                //now we can declare our userid array
                int[] studentid = new int[numstudents];
                //now we need to fetch the userid for each cell in our new array
                msqcheck = new MySqlCommand("SELECT userid FROM user WHERE role = 'S'", msqcon);
                MySqlDataReader studentidbook = msqcheck.ExecuteReader();
                i = 0;
                //while we're reading a non-empty row before moving on to the next row
                while (studentidbook.Read())
                {
                    //set our current index of studentid to the current row's userid
                    studentid[i] = System.Convert.ToInt32(studentidbook["userid"]);
                    //increment our index counter
                    i++;
                }
                //close our DataReader for this operation
                studentidbook.Close();
                //create our MySql command to handle everything else
                MySqlCommand msqcom = new MySqlCommand();
                //now assign the homework assignment to each student
                for (int j = 0; j < studentid.Length; j++)
                {
                    msqcom = new MySqlCommand("INSERT INTO hmwkassignment (assignmentId, homeworkId, grade, status, currentQuestion, userId) " +
                    "Values (" + aid + ", " + hwid + ", " + 0 + ", 'Assigned'," + 1 + ", " + studentid[j] + ")", msqcon);
                    msqcom.ExecuteNonQuery();
                    aid++;
                }
                //now we need to check what type of questions each of these are and insert them in the appropriate tables
                //for each question from our table on the CreateAssignment page...
                for (int j = 0; j < listOfQuestions.Length; j++)
                {
                    //if we are dealing with any of the currently supported questions
                    if (listOfQuestions[j].Contains("SoE") || listOfQuestions[j].Contains("RtI") || listOfQuestions[j].Contains("DP")
                        || listOfQuestions[j].Contains("D") || listOfQuestions[j].Contains("I"))
                    {
                        //split our question into separate strings and convert them to
                        //the appropriate parameters
                        string questionStr = listOfQuestions[j];
                        String[] variables = questionStr.Split(',');
                        //questionid will be decremented if a previous unsupported question was attempting
                        //to be included in the assignment
                        int questionid = System.Convert.ToInt32(variables[0])-questioniddecrement;
                        string type = variables[1];
                        int rows = System.Convert.ToInt32(variables[2]);
                        int cols = System.Convert.ToInt32(variables[3]);
                        int min = System.Convert.ToInt32(variables[4]);
                        int max = System.Convert.ToInt32(variables[5]);
                        int freeVar = System.Convert.ToInt32(variables[6]);
                        int inconsistent;
                        if (variables[7].Equals("Yes"))
                        {
                            inconsistent = 1;
                        }
                        else
                        {
                            inconsistent = 0;
                        }
                        //then we need to insert the question into the question table and give
                        //it the other appropriate assignment values
                        //we now check how many rows we have in our question table
                        msqcheck = new MySqlCommand("SELECT COUNT(*) FROM question", msqcon);
                        MySqlDataReader numquebook = msqcheck.ExecuteReader();
                        numquebook.Read();
                        //set our Primary Key incrementer to the number of rows returned
                        PKincrement = System.Convert.ToInt32(numquebook["COUNT(*)"]);
                        //close our DataReader for this operation
                        numquebook.Close();
                        //if there are currently no rows of data in our hmwkassignment table
                        if (PKincrement == 0)
                        {
                            //we simply set our aid to 1, inserting our first row of data later
                            qid = 1;
                        }
                        //otherwise there are rows so the following statements are executed
                        else
                        {
                            //we need to fetch the latest questionId so we can increment it properly
                            msqcheck = new MySqlCommand("SELECT * FROM question ORDER BY questionId DESC LIMIT 1", msqcon);
                            MySqlDataReader questionbook = msqcheck.ExecuteReader();
                            questionbook.Read();
                            qid = System.Convert.ToInt32(questionbook["questionId"]);
                            questionbook.Close();
                            //increment the questionId so that it reflects the current question we're about to assign
                            qid++;
                        }
                        //now insert the question into the question table
                        msqcom = new MySqlCommand("INSERT INTO question (questionId, homeworkId, number, pointValue, type, rows, columns, min, max, freeVars, inconsistent) " +
                        "Values (" + qid + ", " + hwid + ", " + questionid + ", " + 1 + ", '" + type + "', " + rows + ", " + cols + ", " + min + ", " + max + ", " + freeVar + ", " + inconsistent + ")", msqcon);
                        msqcom.ExecuteNonQuery();
                        //increment our questionId and total points counters
                        qid++;
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
                        //set our questioniddecrement to 1 since there was an attempt to assign an unsupported question
                        questioniddecrement = 1;
                    }
                }
                //insert the new assigned homework into the homework table with values passed from the instructor and the
                //total amount of points from the questions in the assignment
                msqcom = new MySqlCommand("INSERT INTO homework (homeworkid, title, points, dueDate, status) " +
                "Values (" + hwid + ", '" + title + "', " + points + ", '" + dueDate + " 23:59:59', 'Assigned')", msqcon);
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