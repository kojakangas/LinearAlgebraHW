﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LinearHomeworkInterface
{
    public partial class QuestionPage : System.Web.UI.Page
    {
        public static List<float> solution = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            int n = 2;
            int m = 3;
            int min = -2;
            int max = 2;
            int freeVars = 0;
            Boolean inconsistent = false;

            Random rand = new Random();
            
            solution = new List<float>();

            //Generate a random solution
            for (int rowSol = 0; rowSol < n; rowSol++)
            {
                solution.Add(rand.Next(min, max + 1));
            }

            float[,] matrix = new float[n, m];

            //Generates first equation randomly
            for (int col = 0; col < m - 1; col++)
            {
                //If it is the first postion, make sure a non zero number is inputted
                if (col == 0)
                {
                    matrix[0, col] = rand.Next(min, max);
                    if (matrix[0, col] == 0)
                        matrix[0, col] += 1;
                }
                else
                {
                    matrix[0, col] = rand.Next(min, max + 1);
                }

                //this if handles when n is smaller than m by more than 1. for example n = 2 and m = 4
                //ASSERT: will result in free variable
                if (!(col >= n))
                {
                    matrix[0, m - 1] += matrix[0, col] * solution.ElementAt(col);
                }
                else
                {
                    //INFO: This should never be the case in sprint 1
                    //This may actually not be needed and we can just not have an else condition
                    matrix[0, m - 1] += matrix[0, col] + rand.Next(min, max);
                }
            }

            //Generates second equation
            Boolean test = true;
            while (test)
            {
                //This loop sets the coefficients for the second row
                for (int col = 0; col < m - 1; col++)
                {
                    //If it is the first postion, make sure a non zero number is inputted
                    if (col == 1)
                    {
                        matrix[1, col] = rand.Next(min, max);
                        if (matrix[1, col] == 0)
                            matrix[1, col] += 1;
                    }
                    else
                    {
                        matrix[1, col] = rand.Next(min, max + 1);
                    }

                    //this if handles when n is smaller than m by more than 1. for example n = 2 and m = 4
                    //ASSERT: will result in free variable
                    if (!(col >= n))
                    {
                        matrix[1, m - 1] += matrix[1, col] * solution.ElementAt(col);
                    }
                    else
                    {
                        //INFO: This should never be the case in sprint 1
                        //This may actually not be needed and we can just not have an else condition
                        matrix[1, m - 1] += matrix[1, col] + rand.Next(min, max);
                    }
                }

                //Finds a number to check if the rows are multiples of each other
                float num = 0;
                if (matrix[1, 0] != 0)
                {
                    num = matrix[0, 0] / matrix[1, 0];
                }

                //Does the checking
                for (int col = 1; col < m - 1; col++)
                {
                    if (num * matrix[0, col] != matrix[1, col])
                    {
                        test = false;
                        break;
                    }
                }

                //If the rows are multiples, reset the last array position, generate a new row, and check it again
                if (test)
                {
                    matrix[1, m - 1] = 0;
                }
            }

            //Generate the third equation
            if (n > 2)
            {
                //This loop generates the 3rd to rth rows and checks that they are not multiples of each other
                //or any combination of the previously generated rows
                for (int r = 2; r < n; r++)
                {
                    //This nested loop does the generation and checking and only breaks when 
                    //the generated row is not a multiple or combination of other rows
                    test = true;
                    while (test)
                    {
                        //Generate a row of coefficients
                        for (int c = 0; c < m - 1; c++)
                        {
                            //If it is the rth postion, make sure a non zero number is inputted
                            if (c == r)
                            {
                                matrix[r, c] = rand.Next(min, max);
                                if (matrix[r, c] == 0)
                                    matrix[r, c] += 1;
                            }
                            else
                            {
                                matrix[r, c] = rand.Next(min, max + 1);
                            }

                            //this if handles when n is smaller than m by more than 1. for example n = 2 and m = 4
                            //ASSERT: will result in free variable
                            if (!(c >= n))
                            {
                                matrix[r, m - 1] += matrix[r, c] * solution.ElementAt(c);
                            }
                            else
                            {
                                //INFO: This should never be the case in sprint 1
                                //This may actually not be needed and we can just not have an else condition
                                matrix[r, m - 1] += matrix[r, c] + rand.Next(min, max);
                            }
                        }

                        //if the first constant of the equation is a 0 then
                        //to check if the generated row is any multiple of the other
                        //row this is going to add the first row down plus the current
                        //row and use the first element as the divider to get the 
                        //constant. Also it will save the position of all rows that
                        //will need the first row subtracted out before returning the array.
                        List<int> firstElZero = new List<int>();
                        for (int rowIndex = 0; rowIndex < r; rowIndex++)
                        {
                            if (matrix[rowIndex, 0] == 0)
                            {
                                firstElZero.Add(rowIndex);
                            }
                        }

                        //This is what adds the first row into the row with
                        //a zero as the first constant.
                        foreach (int row in firstElZero)
                        {
                            for (int colIndex = 0; colIndex < m; colIndex++)
                            {
                                matrix[row, colIndex] += matrix[0, colIndex];
                            }
                        }

                        //Make constants to check if the rows are any combination of the first rows
                        float addition = matrix[r, 0];
                        List<float> constants = new List<float>();
                        for (int i = 0; i < r; i++)
                        {
                            float div = matrix[i, 0];
                            for (int j = 0; j < r; j++)
                            {
                                if (i != j)
                                {
                                    addition -= matrix[j, 0];
                                }
                            }
                            constants.Add(addition / div);
                            addition = matrix[r, 0];
                        }

                        //Do the checking
                        Boolean[] check = new Boolean[m];
                        //For each row check if the generated constants above
                        //multiplied by a row and then added to the other coefficients
                        //in that column generate the row in question.
                        //If so, generate a new row
                        for (int rowPos = 0; rowPos < r; rowPos++)
                        {
                            int curCol = 0;
                            //initialize check array to false
                            for (int d = 0; d < m; d++)
                            {
                                check[d] = false;
                            }
                            while (curCol < m)
                            {
                                float x = matrix[rowPos, curCol];
                                float adder = 0;
                                for (int z = 0; z < r; z++)
                                {
                                    if (rowPos != z)
                                    {
                                        adder += matrix[z, curCol];
                                    }
                                }
                                if (x * constants.ElementAt(rowPos) + adder == matrix[r, curCol])
                                {
                                    check[curCol] = true;
                                }
                                curCol++;
                            }

                            //Check if the check array is all true
                            test = false;
                            if (Array.IndexOf(check, false) == (-1))
                            {
                                matrix[r, m - 1] = 0;
                                test = true;
                                break;
                            }
                        }

                        //After the checking subtract out the first row
                        //of the rows with first element 0
                        foreach (int row in firstElZero)
                        {
                            for (int h = 0; h < m; h++)
                            {
                                matrix[row, h] -= matrix[0, h];
                            }
                        }
                    }
                }
            }

            //This is if free variables were specified
            float[,] matrixFree = new float[n, m];
            int free = freeVars;
            int index = n;
            if (free > 0)
            {
                matrixFree = new float[n, m];
                Array.Copy(matrix, 0, matrixFree, 0, m - 1);
                while (free > 0)
                {
                    int row = rand.Next(freeVars - 1);
                    int multiplier = rand.Next(max) + 1;
                    index = index - 1;
                    for (int t = 0; t < m; t++)
                    {
                        matrixFree[index, t] = matrixFree[row, t] * multiplier;
                    }
                    free--;
                }
            }

            //This produces an inconsistent matrix
            if (inconsistent)
            {
                matrixFree = new float[n, m];
                Array.Copy(matrix, 0, matrixFree, 0, m - 1);
                int row = rand.Next(n - 1);
                int multiplier = rand.Next(max) + 1;
                float oldNumber = matrixFree[n - 1, m - 1];
                for (int t = 0; t < m - 1; t++)
                {
                    matrixFree[n - 1, t] = matrix[row, t] * multiplier;
                }
                Boolean sameSolution = true;
                while (sameSolution)
                {
                    float newNumber = oldNumber + rand.Next(max + 1);
                    if (oldNumber != newNumber)
                    {
                        matrixFree[row, m - 1] = newNumber;
                        sameSolution = false;
                    }
                    else
                    {
                        newNumber = 0;
                    }
                }
            }


            /*Currently unused code
            DataTable dt = new DataTable("matrix");

            for (int u = 0; u < m; u++)
            {
                DataColumn dc = new DataColumn();
                dt.Columns.Add(dc);
            }

            for (int i = 0; i < n; i++)
            {
                DataRow dr = dt.NewRow();
                for (int j = 0; j < m; j++)
                {
                   dr[j] = matrix[i, j];                   
                }
                dt.Rows.Add(dr);
            }

            DataGrid.DataSource = dt;
            DataGrid.DataBind();
            */

            //Displays the equations using MATHJAX by building a parsable string
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
       
        //our WebMethod for checking the user's solution(s)
        [WebMethod]
        public static string GradeAnswer(String ListPassingSolutions)
        {
            //create a String array of the answers submitted from the user page
            //splits by space
            string[] lines = ListPassingSolutions.Split(' ');

            //create a float array to represent our solutions being passed into
            //this method
            float[] UserSolutions = new float[lines.Length];

            //create a counter
            int x = 0;

            //for all the elements being passed in our array of user strings
            for (int i = 0; i < lines.Length; i++)
            {
                //convert each string into an integer and add it to our integer
                //array for the grading component
                UserSolutions[x] = System.Convert.ToInt32(lines[i]);

                //increment our counter
                x++;
            }

            //create our grading object to grade the user's answers
            GradeComponent.Grader grader = new GradeComponent.Grader();

            //have this created object grade the user's solutions against
            //the answers of our generated matrix
            return grader.Grade(solution, UserSolutions);
        } 
    }
}