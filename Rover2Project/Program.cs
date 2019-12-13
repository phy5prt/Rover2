using System;



namespace TitanRoverProject
{

    public class Program
    {

        public static void Main(string[] args)
        {

            //This block just catches unit tests

            //testing shouldnt be buil

            //bool unittesting = (command.Length > 0);
            //if (unittesting)
            //{
            //    Rover myroverunittest = new Rover(unittesting);
            //    string commandstr = (command.Length > 0) ? command[0] : "";
            //    myroverunittest.tryExecuteCommandGetResult(myroverunittest.askForValidInputUntilReceivedThenReturnIt(commandstr));
            //    return; //just returning void so unit test doesnt trigger myrover code after myroverunittest
            //}

            //End unit test block


            //later versions creating rovers and type of rover should be part of interface
            //constructor for start location and map etc.

            Rover myRover = new Rover();
            UserInterface userInterface = new UserInterface(myRover);
            userInterface.interfaceWithUser();

        }

    }
}

