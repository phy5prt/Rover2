using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Linq; //not using at moment
using System.Reflection;

namespace TitanRoverProject
{

    public class Program
    {

        public static void Main(String[] command)
        {

            //This block just catches unit tests

            //testing shouldnt be buil

            //bool unitTesting = (command.Length > 0);
            //if (unitTesting)
            //{
            //    Rover myRoverUnitTest = new Rover(unitTesting);
            //    String commandStr = (command.Length > 0) ? command[0] : "";
            //    myRoverUnitTest.tryExecuteCommandGetResult(myRoverUnitTest.askForValidInputUntilReceivedThenReturnIt(commandStr));
            //    return; //Just returning void so unit test doesnt trigger myRover code after myRoverUnitTest
            //}

            //End unit test block

            Rover myRover = new Rover();
            myRover.interfaceWithUser();
           
        }
        
    }
}

