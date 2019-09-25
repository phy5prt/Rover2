using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq; //not using at moment
using System.Reflection;

namespace Rover2Project
{

    public class Program
    {

        public static void Main(String[] Command)
        {

             //this block just catches unit tests
                bool unitTesting = (Command.Length > 0);
                if (unitTesting)
                {
                Rover myRoverUnitTest = new Rover(unitTesting);
                String CommandStr = (Command.Length > 0) ? Command[0] : "";
                    myRoverUnitTest.ExecutedCommand(myRoverUnitTest.askForValidInputUntilReceivedThenReturnIt(CommandStr));
                    return; //just returning void so unit test doesnt trigger real test
                }
            //end unit test block

            Rover myRover = new Rover();
            myRover.interfaceWithUser();

        }
        public class Rover //if in future have multiple give a name string
        {
            public struct ResultType {
                public bool succeeded;
                public string failInformation;
                public ResultType(bool succeeded, string failInformation = "") {
                    this.succeeded = succeeded;
                    if (failInformation == "") { this.failInformation = this.succeeded ?   "succeeded" : "failed"; }
                    this.failInformation = failInformation;
                }
        
            }
            public class Coordinates //if using multiple coordinate system for different activities they should have posession of own delegate
            {
                public int X, Y;

                //later if the coordinates maximums need to be changed built in methods in the get and set could protect against
                //moving the maxX to less than the current coordinate or to alter current location if changing orientation or numbering
                //of the coordinate system
                public int maxX { get; set; }
                public int minX { get; set; }
                public int maxY { get; set; }
                public int minY { get; set; }

                public string lastOrientation;
                //constructor uses default location and size coordinates
                
                public Coordinates(int X = 1, int Y = 1, int maxX = 10, int minX = 1, int maxY = 10, int minY = 1, string lastOrientation = "S")
                {
                    this.X = X;
                    this.Y = Y;
                    this.minX = minX;
                    this.minY = minY;
                    this.maxX = maxX;
                    this.maxY = maxY;
                    this.lastOrientation = lastOrientation;
                }

                public string getCoordData() {
                    return string.Format(" X={0}, Y={1}, MinX={2}, MinY={3}, maxX={4}, maxY={5} orientation={6} ", X, Y, minX, minY, maxX, maxY, lastOrientation); 
                }
                public string getCoordDataShort() //could print as well but really userInterface should do it
                {
                    return string.Format($"{X}, {Y}, {lastOrientation}");
                }
            }

            //Orientation methods are in a dictionary so the commands can be keys, and keys can be automatically updated when new methods added (by rover constructor) 
            Dictionary<string, MethodInfo> orientationCommands = new Dictionary<string, MethodInfo>();

            //Movements put into delegate method to execute movement command based on orientation rover is in
            //This is where to add movement functionality
            Dictionary<string, int> moveActions = new Dictionary<string, int>() {
                {"M",1 }
              //,{"B", -1 }
            };

            //If rover in future requires actions unrelated to movement they should be put in an action dictionary

//This class is what needs to be changed to have extra direction methods they are automatically added to dictionary through rover constructor
//And the dictionary is used in producing the checks and information for acceptable keyboard input
            public static class Directions
            {
                public static Coordinates N(Coordinates coords, int Scale)
                {
                    coords.Y = coords.Y - 1 * Scale;
                    return coords;
                }
                public static Coordinates E(Coordinates coords, int Scale)
                {
                    coords.X = coords.X + 1 * Scale;
                    return coords;
                }
                public static Coordinates S(Coordinates coords, int Scale)
                {
                    coords.Y = coords.Y + 1 * Scale;
                    return coords;
                }

                public static Coordinates W(Coordinates coords, int Scale)
                {
                    coords.X = coords.X - 1 * Scale;
                    return coords;
                }

                //Uncomment this section to see how functionality is added

                //public static Coordinates X(Coordinates coords, int Scale)
                //{
                //    coords.X = coords.X + 1 * Scale;
                //    coords.Y = coords.Y - 1 * Scale;
                //    return coords;
                //}
            }
            public Dictionary<string, MethodInfo> MakeOrientationCommandDictionary(Dictionary<string, MethodInfo> orientationCommands)
            {
                string key = "";
                MethodInfo methodInfo;
                foreach (MethodInfo directionMethod in typeof(Directions).GetMethods())
                {
                    if (directionMethod.Name.Length == 1)
                    {
                        key = directionMethod.Name;
                        methodInfo = typeof(Directions).GetMethod(key);
                        orientationCommands.Add(key, methodInfo);
                    }
                }
                return orientationCommands;
            }

            //The delegate holds the orientation, then movement is applied to the deligated orientation
            //There is one delegate for the rover, not one per coordinates, so if multiple coordinates used delegate will need to be reset
            //With switches between coordinate groups or code will need restructing so they hhave their own delegates
            public delegate Coordinates Move(Coordinates coords, int numberExecutions); 
            Move move = null;

            private bool unitTesting = false; //main takes zero arguments unless unit testing currently if this changes the unit testing code will need to use mocking of the console instead


            
            public Coordinates lastCoordinates;
            public Rover(bool unitTesting = false)   
            {
                lastCoordinates = new Coordinates(); //if want to create with custom coordinate system could use rover constructor into coordinate constructor
                orientationCommands = MakeOrientationCommandDictionary(orientationCommands);
                applyDirectionToDelegate(lastCoordinates.lastOrientation);
                this.unitTesting = unitTesting;
            }

            public String askForValidInputUntilReceivedThenReturnIt(String input)
            {
                input = input.ToUpper();

                for (int i = 0; i < input.Length; i++)
                {
                    if (!(orientationCommands.ContainsKey(input[i].ToString()) || moveActions.ContainsKey(input[i].ToString())))
                    {
                        Console.WriteLine($"The command {input} has invalid instruction in it {input[i]}.\r\nRover location and orientation has not been changed.\r\nPlease input new string ");
                        Console.WriteLine($"Valid movements {string.Join("", moveActions.Keys.ToArray())}.\r\nValid directions  {string.Join("", orientationCommands.Keys.ToArray())}");

                        input = askForValidInputUntilReceivedThenReturnIt(Console.ReadLine().ToString());
                    }
                }
                return input;
            }

            public void applyDirectionToDelegate(String ordinate)
            { 
                move = (Move)Delegate.CreateDelegate(typeof(Move), orientationCommands[ordinate]);
            }
            public String interfaceWithUser()
            { 
                Console.WriteLine("Rover Activated");
                String userResponse = "";
                while (userResponse != "Q" || userResponse != "q")//just incase using breaks to do it so could be set as true
                {

                    Console.WriteLine(lastCoordinates.getCoordDataShort());
                    Console.WriteLine($"Please Enter Command.\r\nValid movements { string.Join("", moveActions.Keys.ToArray())}.\r\nValid directions  {string.Join("", orientationCommands.Keys.ToArray())}.\r\nReturn for current status. Or press Q/q to quit.");
                    userResponse = Console.ReadLine().ToString(); if (userResponse == "Q" || userResponse == "Q") { break; }
                    ResultType result;
                    while (!(result = ExecutedCommand(askForValidInputUntilReceivedThenReturnIt(userResponse))).succeeded) //executed command checks command is in bounds
                    {
                        //to get the fail information we have to run the function twice, so either get a resultVariable and a have a do while or turn executedCommand to just return bool without error feedback
                            Console.WriteLine($"Please reenter Command.\r\nLast Command exceeded allowed area at {result.failInformation}.\r\nThe rover has not been activated.\r\nValid movements { string.Join("", moveActions.Keys.ToArray())}.\r\nValid directions  {string.Join("", orientationCommands.Keys.ToArray())}.\r\nReturn for current status. ");
                            userResponse = Console.ReadLine().ToString(); //if (userResponse == "Q") { break; } would need double break this is where if user has problems could be trapped could be it in function
                    };
                }
                return userResponse;
            }
            //Runs test to see if command will go out of bounds
            //If out of bound requests new command
            //If command doesnt take out of bounds then applies it to rover
            
            public ResultType ExecutedCommand(String Command) 
            {
                Coordinates testRoute = new Coordinates (lastCoordinates.X, lastCoordinates.Y, lastCoordinates.maxX, lastCoordinates.minX, lastCoordinates.maxY, lastCoordinates.minY, lastCoordinates.lastOrientation); //lates use test Coordinates
                Coordinates[] testThenRover = new Coordinates[2] { testRoute, lastCoordinates };
                //the reason we dont just put the rover coordinates in and reset them if they go out of bounds is that the actual rover would have to retrace steps to do so.
                //It also allows for that the rover may have more functionality it future. 
                //It may collect data as it goes or other actions that dont effect movement or orientation like take a soil sample or photograph.
                //These could be set only to be exectuted by the rover once its route is checked.

                for (int j = 0; j < testThenRover.Length; j++)
                {
                        
                        applyDirectionToDelegate(testThenRover[j].lastOrientation); 
                    //because test rover and real rover share a delegate it needs to be reset after test rover has run
                    //this suggests maybe the coordinate system should store its own delegate. 
                    //this would allow different maps different limits, say it had all terain mode with sensors protected it may have a larger map in this mode

                    for (int i = 0; i < Command.Length; i++)
                    {
                        if (orientationCommands.ContainsKey(Command[i].ToString()))
                        {
                            testThenRover[j].lastOrientation = Command[i].ToString();
                            applyDirectionToDelegate(Command[i].ToString());
                        }
                        else if (moveActions.ContainsKey(Command[i].ToString()))
                        {
                            testThenRover[j] = move(testThenRover[j], moveActions[Command[i].ToString()]);
                        }
                        else
                        {
                            Console.WriteLine("Uncaught error");
                            ResultType failErrorResult = new ResultType(false);
                            return failErrorResult; 
                        }

                        //If the command has resulted in the test going out of bounds this information is returned and the rover itself isn't moved
                        if (j ==0) {
                            if (testThenRover[j].Y < testThenRover[j].minY || testThenRover[j].Y > testThenRover[j].maxY || testThenRover[j].X > testThenRover[j].maxX || testThenRover[j].X < testThenRover[j].minX)
                            {
                                String failResultStr = string.Format(" {0}  X = {1}   Y = {2} ", Command.Insert(i, "*").Insert(i + 2, "*"), testThenRover[j].X, testThenRover[j].Y);
                                ResultType failResult = new ResultType(false, failResultStr);
                                return failResult;
                            }
                        }
                    }
                }
                if (unitTesting) { Console.WriteLine(lastCoordinates.getCoordDataShort()); }

                ResultType succeedResult = new ResultType(true);
                return succeedResult;
            }
        }
    }
}

