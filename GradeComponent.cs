//This is the grading component for the page. The Homework Controller
//dumps into a text file that serves as the solution key that this file
//will utilize. The user's answer will be passed in the constructor for
//this component and is then utilized in the actual Grade() method.
//Author: Kieran Ojakangas Date: 9/12/2013
//Code reuse from: http://msdn.microsoft.com/en-us/library/vstudio/ezwyzy7b.aspx

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GradeComponent
{
    //The class itself
    class Grader
    {
        public int x;
        public int y;

        //The constructor for the grading program. The controller passes
        //the solution values from the user through this constructor.
        public Grader(int x, int y) {
            this.x = x;
            this.y = y;
        }

        //Here is the actual grading method
        public String Grade()
        {
            // Read each line of our solution text file into a string array. Each element 
            // of the array is one line of the file. 
            string[] lines = System.IO.File.ReadAllLines(@"C:\data\GradeEx1.txt");

            //Below are commented commands for console testing purposes.

            //Console.WriteLine("Please enter the first solution for x (ex. 2)");
            //string input = Console.ReadLine();
            //Console.WriteLine("Please enter the second solution for y (ex. 5)");
            //string input2 = Console.ReadLine();

            //The logic comparison section. If the user enters both solutions correctly the class
            //returns a commendation as a string, otherwise the class returns an incorrect string
            if (System.Convert.ToInt32(lines[0]) == x && System.Convert.ToInt32(lines[1]) == y)
            {
                return "Very good! You got a good grade.";
            }
            else
            {
                return "Not good. Try again.";
            }
            //Below are more commented testing commands that should not be necessary

            //Console.WriteLine("Press any key to exit.");
            //Console.ReadKey();
        }
    }
}
