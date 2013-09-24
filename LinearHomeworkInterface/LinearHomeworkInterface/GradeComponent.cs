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
        //public instance variables//

        //our check answer counter for the grading method
        public int checkAnswer = 0;

        //This is the given range that will provide a correct answer
        public float acceptableRange = .001F;

        //default constructor for the controller
        public Grader()
        {
            //no need for code here
        }

        //Here is the grading method
        public String Grade(List<float> ActualSolution, float[] ListUserSolutions)
        {
            //The logic comparison section. If the user enters both solutions correctly the class
            //returns a commendation as a string, otherwise the class returns a string prompting the student
            //to try again.

            //if the number of user solutions is less or greater than the number of actual solutions
            if (ListUserSolutions.Length > ActualSolution.Count ||
                ListUserSolutions.Length < ActualSolution.Count)
            {
                //we inform the user he/she either has too many or too few solutions (for all relevant
                //purposes in our sprint we decided to let the student decide how many solutions he/she
                //may be short of or vice versa)
                return "We couldn't match up all the solutions with our key. Please verify that you" +
                    " have the correct number of solutions";
            }

            //otherwise
            else {
            //For each element in our local array, check:
            foreach (float i in ListUserSolutions)
            {
                //if our actual solution for the current solution is in the acceptable threshold of
                //the current user solution being checked
                
                if (Math.Abs(ActualSolution.ElementAt(this.checkAnswer)-i) < acceptableRange)
                {
                    //increment our public check answer counter
                    this.checkAnswer++;
                }

                //otherwise
                else
                {
                    //we have found a wrong answer, so the user must try again (currently we only
                    //aim to check if the user solution is perfect, we're not grading any work shown
                    //along the way for this sprint)
                    return "Not good. Try again.";
                }

            }

            //inform the user they have an acceptable answer,
            //since the loop found no solution exceeding the threshold against the
            //solutions of the generated matrix
            return "Very good! You got a good grade.";

            }
        
        }    
      }
            
 }
