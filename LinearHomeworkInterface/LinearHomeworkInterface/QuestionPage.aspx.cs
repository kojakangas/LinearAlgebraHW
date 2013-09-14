using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LinearHomeworkInterface
{
    public partial class QuestionPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int n = 3;
            int m = 4;
            int max = 2;
            int freeVars = 1;
            Boolean inconsistent = false;

            Random rand = new Random();
            List<float> solution = new List<float>();
            for (int u = 0; u < n; u++)
            {
                solution.Add(rand.Next(max + 1));
            }
            float[,] matrix = new float[n, m];

            //Generates first equation
            for (int a = 0; a < m - 1; a++)
            {
                if (a == 0)
                {
                    matrix[0, a] = rand.Next(max) + 1;
                }
                else
                {
                    matrix[0, a] = rand.Next(max + 1);
                }
                //this if handles when n is smaller than m by more than 1. for example n = 2 and m = 4
                //will result in free variable
                if (!(a >= n))
                {
                    matrix[0, m - 1] += matrix[0, a] * solution.ElementAt(a);
                }
                else
                {
                    matrix[0, m - 1] += matrix[0, a] + rand.Next(max);
                }
            }

            //Generates second equation
            Boolean test = true;
            while (test)
            {
                for (int a = 0; a < m - 1; a++)
                {
                    if (a == 1)
                    {
                        matrix[1, a] = rand.Next(max) + 1;
                    }
                    else
                    {
                        matrix[1, a] = rand.Next(max + 1);
                    }
                    //this if handles when n is smaller than m by more than 1. for example n = 2 and m = 4
                    //will result in free variable
                    if (!(a >= n))
                    {
                        matrix[1, m - 1] += matrix[1, a] * solution.ElementAt(a);
                    }
                    else
                    {
                        matrix[1, m - 1] += matrix[1, a] + rand.Next(max);
                    }
                }
                float num = 0;
                if (matrix[1, 0] != 0)
                {
                    num = matrix[0, 0] / matrix[1, 0];
                }

                for (int b = 1; b < m - 1; b++)
                {
                    if (num * matrix[0, b] != matrix[1, b])
                    {
                        test = false;
                        break;
                    }
                }

                if (test)
                {
                    matrix[1, m - 1] = 0;
                }
            }

            //Generate the third equation
            if (n > 2)
            {
                for (int r = 2; r < n; r++)
                {
                    test = true;
                    while (test)
                    {
                        for (int a = 0; a < m - 1; a++)
                        {
                            if (a == r)
                            {
                                matrix[r, a] = rand.Next(max) + 1;
                            }
                            else
                            {
                                matrix[r, a] = rand.Next(max + 1);
                            }
                            if (!(a >= n))
                            {
                                matrix[r, m - 1] += matrix[r, a] * solution.ElementAt(a);
                            }
                            else
                            {
                                matrix[r, m - 1] += matrix[r, a] + rand.Next(max);
                            }
                        }

                        //if the first constant of the equation is a 0 then
                        //to check if the generated row is any multiple of the other
                        //row this is going to add the first row down plus the current
                        //row and use the first element as the divider to get the 
                        //constant. Also it will save the position of all rows that
                        //will need the first row subtracted out before returning the array.
                        List<int> firstElZero = new List<int>();
                        for (int u = 0; u < r; u++)
                        {
                            if (matrix[u, 0] == 0)
                            {
                                firstElZero.Add(u);
                            }
                        }
                        //This is what adds the first row into the row with
                        //a zero as the first constant.
                        foreach (int row in firstElZero)
                        {
                            for (int g = 0; g < m; g++)
                            {
                                matrix[row, g] += matrix[0, g];
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
                            //                        if (div != 0) {
                            //                            constants.add(addition / div);
                            //                        } else {
                            //                            //if the first constant of the equation is a 0 then
                            //                            //to check if the generated row is any multiple of the other
                            //                            //row this is going to add the first row down plus the current
                            //                            //row and use the first element as the divider to get the 
                            //                            //constant. Also it will save the position of all rows that
                            //                            //will need the first row subtracted out before returning the array.
                            //                            for (int b = 0; b < i; b++) {
                            //                                if (constants.get(b) == 0) {
                            //                                    constants.set(b, -1f);
                            //                                } else {
                            //                                    if (constants.get(b) > 0) {
                            //                                        constants.set(b, constants.get(b) * (-1));
                            //                                    }
                            //                                }
                            //                            }
                            //                            constants.add(addition / matrix[0][0]);
                            //                            firstElZero.add(i);
                            //                        }
                            addition = matrix[r, 0];
                        }

                        //                    //This is what adds the first row into the row with
                        //                    //a zero as the first constant.
                        //                    for (Integer row : firstElZero) {
                        //                        for (int g = 0; g < matrix[row].length; g++) {
                        //                            matrix[row][g] += matrix[0][g];
                        //                        }
                        //                    }

                        //Do the checking
                        Boolean[] check = new Boolean[m];//Boolean[][] check = new Boolean[matrix.length - 1][matrix[0].length];
                        for (int k = 0; k < r; k++)
                        {
                            int l = 0;
                            for (int d = 0; d < m; d++)
                            {
                                check[d] = false;
                            }
                            while (l < m)
                            {
                                float x = matrix[k, l];
                                float adder = 0;
                                for (int z = 0; z < r; z++)
                                {
                                    if (k != z)
                                    {
                                        adder += matrix[z , l];
                                    }
                                }
                                float s = matrix[r, l];
                                float f = x * constants.ElementAt(k) + adder;
                                if (x * constants.ElementAt(k) + adder == matrix[r, l])
                                {
                                    check[l] = true;
                                }
                                l++;
                            }

                            test = false;
                            if (Array.IndexOf(check, false) == (-1))
                            {//if (!Arrays.asList(check).contains(false)) {
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

                        //                    test = false;
                        //
                        //                    for (int w = 0; w < check.length; w++) {
                        //                        if (!Arrays.asList(check[w]).contains(false)) {
                        //                            test = true;
                        //                            break;
                        //                        }
                        //                    }
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

            //Console.WriteLine("Solution: ");
            //for (int f = 0; f < m - 1; f++)
            //{
            //    Console.WriteLine(solution.ElementAt(f));
            //}

            //Console.WriteLine("Matrix: ");
            //for (int q = 0; q < n; q++)
            //{
            //    for (int w = 0; w < m; w++)
            //    {
            //        Console.WriteLine(matrix[q, w]);
            //    }
            //}

            //Console.WriteLine("Free Var Matrix:");

            //for (int w = 0; w < n; w++)
            //{
            //    for (int q = 0; q < m; q++)
            //    {
            //        Console.WriteLine(matrixFree[w, q]);
            //    }
            //}
            //Console.ReadLine();
        }
    }
}