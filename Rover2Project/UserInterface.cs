using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Linq; //not using at moment
using System.Reflection;

namespace TitanRoverProject
{
    //should be only class to interface with user
    class UserInterface
    {
        //what if more than one and want to change
        //singleton as only one console
        //static?
        private Rover roverInterfacingWith; //later may want to change rovers or have an array
        public UserInterface(Rover roverInterfacingWith) {
            this.roverInterfacingWith = roverInterfacingWith;
        }

        public String interfaceWithUser()
        {
            Console.WriteLine("Rover Activated. Move rover with a sequence of movement commands and direction commands. E.g, 'EMMSM'");
            String userResponse = "";
            while (userResponse != "Q" || userResponse != "q")//just incase using breaks to do it so could be set as true
            {

                Console.WriteLine("Your current location and orientation is: " + roverInterfacingWith.lastCoordinates.getCoordDataShort());
                Console.WriteLine($"Please Enter Command:\r\nValid movement commands: { string.Join("", MoveOrientationCommandsDics.moveActions.Keys.ToArray())}.\r\nValid direction commands:  {string.Join("", MoveOrientationCommandsDics.orientationCommands.Keys.ToArray())}.\r\nPress return for current status. Or press Q/q to quit.");
                userResponse = Console.ReadLine().ToString(); if (userResponse == "Q" || userResponse == "q") { break; }
                ResultType result;

                while (!(result = roverInterfacingWith.tryExecuteCommandGetResult(askForValidInputUntilReceivedThenReturnIt(userResponse))).succeeded) //executed command checks command is in bounds
                {
                    //to get the fail information we have to run the function twice, so either get a resultVariable and a have a do while or turn executedCommand to just return bool without error feedback
                    
                    userResponse = Console.ReadLine().ToString(); //if (userResponse == "Q") { break; } //would need double break this is where if user has problems could be trapped could be it in function. To avoid nested while loop could use method instead enabling 'break' to work.
                };
            }
            return userResponse;
        }
        //changeName to process command
        private String askForValidInputUntilReceivedThenReturnIt(String input) //Make private if replace unit tests with console mocking unit test
        {
            input = input.ToUpper();

            for (int i = 0; i < input.Length; i++)
            {
                if (!(MoveOrientationCommandsDics.orientationCommands.ContainsKey(input[i].ToString()) || MoveOrientationCommandsDics.moveActions.ContainsKey(input[i].ToString())))
                {
                    Console.WriteLine($"The command {input} contains the invalid instruction: {input[i]}.\r\nRover location and orientation has not been changed.\r\nPlease input new command sequence ");
                    Console.WriteLine($"Valid movement command(s) are: {string.Join("", MoveOrientationCommandsDics.moveActions.Keys.ToArray())}.\r\nValid direction commands are:  {string.Join("", MoveOrientationCommandsDics.orientationCommands.Keys.ToArray())}");

                    input = askForValidInputUntilReceivedThenReturnIt(Console.ReadLine().ToString());
                }
            }
            return input;
        }
    }
}
