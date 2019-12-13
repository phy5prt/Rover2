using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Linq; //not using at moment
using System.Reflection;

namespace TitanRoverProject
{
    public class Rover //If in the future there were multiple rovers, they'd need a name string
    {
        
        

       

        //The delegate holds the orientation, then movement is applied to the deligated orientation
        //There is one delegate for the rover, not one per coordinates, so if multiple coordinates are used, delegate will need to be reset
        //When changing to a new Coordinates, the delegate needs to be reset to the Coordinates last orientation
       
        private delegate Coordinates Move(Coordinates coords, int numberExecutions);
        Move move = null;

       //The Main class takes zero arguments unless unit testing. If this changes, the unit testing code will need to mock the console instead



        public Coordinates lastCoordinates;

        /// <summary>
        /// Rover Constructor
        /// </summary>


        public Rover(/*bool unitTesting = false*/)
        {
            lastCoordinates = new Coordinates(); //To create a rover with a custom coordinate system, use rover constructor to provide variable for the coordinates constructor
            applyDirectionToDelegate(lastCoordinates.lastOrientation);
            //this.unitTesting = unitTesting;
        }


        /// <summary>
        /// Rover Methods Section in order of calling
        /// </summary>
        




        //Runs test to see if command will go out of bounds
        //If out of bounds, requests new command
        //If command doesn't take out of bounds then applies it to rover
        public ResultType tryExecuteCommandGetResult(String command) //Make private if replacing Unit tests with console mocking ones
        {
            Coordinates testRoute = new Coordinates(lastCoordinates.X, lastCoordinates.Y, lastCoordinates.maxX, lastCoordinates.minX, lastCoordinates.maxY, lastCoordinates.minY, lastCoordinates.lastOrientation);
            Coordinates[] testThenRover = new Coordinates[2] { testRoute, lastCoordinates };

            //The reason we dont just put the rover coordinates in and reset them if they go out of bounds is that the actual rover would have to retrace steps to do so.
            //It also accommodates the rover having more functionality in the future. 
            //It may collect data as it goes, or carry out other actions that doesn't effect movement or orientation like take a soil sample or photograph.
            //These could be set only to be exectuted by the rover once its route is checked.

            for (int j = 0; j < testThenRover.Length; j++)
            {
                applyDirectionToDelegate(testThenRover[j].lastOrientation);

                //Because the test rover and real rover share a delegate it needs to be reset after the test rover has run
                //This suggests maybe the coordinate system should store its own delegate. 
                //This would allow different maps different limits, e.g, it had all-terrain mode with sensors protected, it may have a larger map in this mode

                for (int i = 0; i < command.Length; i++)
                {
                    if (MoveOrientationCommandsDics.orientationCommands.ContainsKey(command[i].ToString()))
                    {
                        testThenRover[j].lastOrientation = command[i].ToString();
                        applyDirectionToDelegate(command[i].ToString());
                    }
                    else if (MoveOrientationCommandsDics.moveActions.ContainsKey(command[i].ToString()))
                    {
                        testThenRover[j] = move(testThenRover[j], MoveOrientationCommandsDics.moveActions[command[i].ToString()]);
                    }
                    else
                    {
                       
                        ResultType failErrorResult = new ResultType(false);
                        return failErrorResult;
                    }

                    //If the command has resulted in the test going out of bounds, this information is returned and the rover itself isn't moved
                    if (j == 0)
                    {
                        if (testThenRover[j].Y < testThenRover[j].minY || testThenRover[j].Y > testThenRover[j].maxY || testThenRover[j].X > testThenRover[j].maxX || testThenRover[j].X < testThenRover[j].minX)
                        {
                            String failResultStr = string.Format(" {0}  X = {1}   Y = {2} ", command.Insert(i, "*").Insert(i + 2, "*"), testThenRover[j].X, testThenRover[j].Y);
                            ResultType failResult = new ResultType(false, failResultStr);
                            return failResult;
                        }
                    }
                }
            }
            //if (unitTesting) { Console.WriteLine(lastCoordinates.getCoordDataShort()); }

            ResultType succeedResult = new ResultType(true);
            return succeedResult;
        }

        private void applyDirectionToDelegate(String ordinate)
        {
            move = (Move)Delegate.CreateDelegate(typeof(Move), MoveOrientationCommandsDics.orientationCommands[ordinate]);
        }







    }
}
