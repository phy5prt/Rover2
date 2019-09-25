using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq; //not using at moment
using System.Reflection;

namespace Rover2Project
{

        public class Program
        {
         
            public static void Main(String[] Command)//
            {
            Rover myRover = new Rover();
            //String CommandStr = (Command.Length > 0) ? Command[0] : "";
                
            //    Boolean unitTesting = true;
            //    if (unitTesting) { myRover.ExecutedCommand(myRover.checkValidInputCorrectAndReturn(CommandStr)); }
            //    else
            //    {

               

                    myRover.interfaceWithUser();


              //  }
            }




        }
    public class Rover
    {

        public class Coordinates {
            public int X, Y, maxX, minX, maxY, minY;
            public string lastOrientation;
            public Coordinates(int X = 1, int Y = 1, int maxX = 10, int minX = 1, int maxY = 10, int minY = 1, string lastOrientation = "S") {
                this.X = X;
                this.Y = Y;
                this.maxX = maxX;
                this.maxY = maxY;
                this.lastOrientation = lastOrientation;
            }
            

        }


        public Coordinates lastCoordinates = new Coordinates();
      
        //could be a public dictionary of orientationCommands so pull both set info from it ... but if use get set 
        Dictionary<string, MethodInfo> orientationCommands = new Dictionary<string, MethodInfo>();
        Dictionary<string, int> moveActions = new Dictionary<string, int>() {
                {"M",1 }
              //,{"B", -1 }
            };

        // private String[] roverActions = new String[] { "M" };

           
        public static class Directions
        {
            public static Coordinates N(Coordinates coords, int Scale)
            {
            
                coords.Y = coords.Y - 1 * Scale;
            return coords;
            }
            public static Coordinates S(Coordinates coords, int Scale)
            {
                coords.Y = coords.Y + 1 * Scale;
            return coords;

        }

            public static Coordinates W( Coordinates coords, int Scale)
            {
                coords.X = coords.X - 1 * Scale;
            return coords;
        }

            public static Coordinates E(Coordinates coords, int Scale)
            {
                coords.X = coords.X + 1 * Scale;
            return coords;
        }
         
        }
        public Dictionary<string, MethodInfo> MakeOrientationCommandDictionary( Dictionary<string, MethodInfo> orientationCommands)
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


        public delegate Coordinates Move(Coordinates coords, int numberExecutions); //no return type so maybe use action instead
            Move move = null;
        //Scale because like vectors and want to be able to take negative numbers
        public Rover()
        {
            //MakeOrientationCommandDictionary(ref orientationCommands);
          orientationCommands =  MakeOrientationCommandDictionary(orientationCommands);
            applyDirection(lastCoordinates.lastOrientation);

        }

        public String checkValidInputCorrectAndReturn(String input)
            {

                input = input.ToUpper();

                for (int i = 0; i < input.Length; i++)
                {
                    if (!(orientationCommands.ContainsKey(input[i].ToString()) || moveActions.ContainsKey(input[i].ToString())))
                    {
                       
                        Console.WriteLine($"The command {input} has invalid instructions in it. please input new string ");
                        Console.WriteLine($"Valid movements {String.Concat(moveActions)}. Valid directions  {string.Join("",orientationCommands.Keys.ToArray())}");
                        
                        input = checkValidInputCorrectAndReturn(Console.ReadLine()); 

                    }
                }


                return input;
            }
      


            //returns "x, y, orientation
            public void PrintCurrentLocationOrientation()
            {

                // Console.WriteLine("Rover location and orientation :"); //busts my unit tests currently
                Console.WriteLine($"{lastCoordinates.X}, {lastCoordinates.Y}, {lastCoordinates.lastOrientation}");
            }

            public void applyDirection(String ordinate)
            { //surely there is a way to cast the string as a method
            move = (Move)Delegate.CreateDelegate(typeof(Move), orientationCommands[ordinate]);
            
        
            }
            public String interfaceWithUser()
            { //should interface with use have all the user interaction functionality
              //or should i just put the functions used for it in here

                String userResponse = "";
                while (userResponse != "Q")//just incase using breaks to do it so could be set as true
                {
                    Console.WriteLine("Rover location and orientation :");
                    PrintCurrentLocationOrientation();
                    Console.WriteLine($"Please Enter Command. Valid movements { String.Concat(moveActions)}. Valid directions  {string.Join("", orientationCommands.Keys.ToArray())}. Or press Q to quit.");
                    userResponse = Console.ReadLine(); if (userResponse == "Q") { break; }
                    //needs to be wrapped in out of bounds checker too
                    if (!ExecutedCommand(checkValidInputCorrectAndReturn(userResponse)))
                    {
                        bool executedCommand = false;
                        while (!executedCommand)
                        {
                            Console.WriteLine($"Please reenter Command. Last Command exceeded allowed area. Valid movements { String.Concat(moveActions)}. Valid directions  {string.Join("", orientationCommands.Keys.ToArray())}. ");
                            userResponse = Console.ReadLine(); //if (userResponse == "Q") { break; } would need double break this is where if user has problems could be trapped could be it in function
                            executedCommand = ExecutedCommand(checkValidInputCorrectAndReturn(userResponse));
                        }
                    };
                    Console.WriteLine("Rover location and orientation :");
                    PrintCurrentLocationOrientation(); // currently will happen twice as built into execute command for tests
                    Console.WriteLine($"Press Q to quit press any other key to continue. ");
                    userResponse = Console.ReadLine(); if (userResponse == "Q") { break; }
                }
                return userResponse;
            }
            public bool ExecutedCommand(String Command) //check command would be same code except different variable

            {
                //this is bad because it would really be moving the rover
                //need start coords
                //rover object with coords so could be more rovers and receives
                //ideally given object being altered so could set testRover to realRover
                String startOrientation = lastCoordinates.lastOrientation;
               Coordinates startCoordinates = lastCoordinates; //lates use test Coordinates
            

                for (int i = 0; i < Command.Length; i++)
                {
                    if ( orientationCommands.ContainsKey(Command[i].ToString()))
                    {
                        //instead of setting lastOrientation to command which would mean NW would equal west we want to be able to 
                        //take it as if it was an array element, there is no need to point w then north 

                        lastCoordinates.lastOrientation = Command[i].ToString();

                        // Move move =  Command[i]; //if cant get this to work will have to use switch case less flexible
                        applyDirection(Command[i].ToString());

                    }
                    else if (moveActions.ContainsKey(Command[i].ToString()))
                { 
                    lastCoordinates = move(lastCoordinates, moveActions[Command[i].ToString()]);
                    }
                    else
                    { //error}
                    }
                    if (lastCoordinates.Y < lastCoordinates.minY || lastCoordinates.Y > lastCoordinates.maxY || lastCoordinates.X > lastCoordinates.maxX || lastCoordinates.X < lastCoordinates.minX)
                    {
                    lastCoordinates.Y = startCoordinates.Y;
                    lastCoordinates.X = startCoordinates.X;
                        lastCoordinates.lastOrientation = startOrientation;
                        applyDirection(lastCoordinates.lastOrientation);
                        return false;
                    }
                }
                PrintCurrentLocationOrientation();
                return true;
            }

        }
    }

