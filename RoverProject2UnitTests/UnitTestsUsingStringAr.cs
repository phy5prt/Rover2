using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using TitanRoverProject;

/// <summary>
/// These unit tests put in a string array to main. Main identifies it is being unit tested by the fact it has received and string array. It then uses the string array as input data.
/// The data does not go through userInterface but directly into the command and checking functions. It does this only once so only one command can be given. 
/// The output it receives is only the coordinate data.
/// If the unit test causes the program to ask the user for clarification the test will hang because it cannot interact with the console directly.
/// 
/// The unit tests do not mock the console so do not test multiple responses to the console, incorrect letters into the console or commands
/// that result in the userInterface to ask for new information. Such as if a command string would result in the rover going out of bounds.
/// 
/// If you wish to test the console a second unit test project should be added with mocking.
/// These test should be duplicated and the unit testing block in main and the unit testing bool in rover and the if statement where it is used.
/// </summary>
/// 
//try this https://gist.github.com/asierba/ad9978c8b548f3fcef40
//or remove console controle from main and go direct to the individual classes and methods test per methods

namespace TitanRoverProjectUnitTests
{
    [TestClass]
    public class UnitTestRoverMoveResultSucceed
    {
        private const bool Expected = true;
        [TestMethod, Timeout(10000)]
        public void tryExecuteCommandGetResult_OutOfBoundsResultReturnsFalse()
        {
            // TitanRoverProject.Program.Main(new string[] { });
            TitanRoverProject.Rover rover = new TitanRoverProject.Rover();
            TitanRoverProject.ResultType resultType = rover.tryExecuteCommandGetResult("M");
            bool result = resultType.succeeded;
            Assert.AreEqual(Expected, result);

        }

    }
    [TestClass]
    public class UnitTestUserInterfaceInputM
    {
        private const string Expected = ( "Your current location and orientation is: 1, 2, S" + "2 Please Enter Command:" + "Valid movement commands: M." + "Valid direction commands:  NESW."+"Press return for current status.Or press Q/q to quit.");
        [TestMethod, Timeout(10000)]
        public void tryExecuteCommandGetResult_OutOfBoundsResultReturnsFalse()
        {

            TitanRoverProject.Rover myRover = new Rover();
            UserInterface userInterface = new UserInterface(myRover);
            var result= "";
         
                userInterface.interfaceWithUser(); //but now what it will write line!

                Assert.AreEqual(Expected, result);
            
        
    

            

        }

    }

}



    //old console test instead the above test the methods
    //[TestClass]
    //public class UnitTest1StartLocationOrientation
    //{
    //    private const string Expected = "cake";
    //    [TestMethod, Timeout(2000)]
    //    public void TestMethod1()
    //    {
    //        //using (var sw = new StringWriter())
    //        //{
    //        //    Console.SetOut(sw);
    //        //    String myString = "";
    //        //    var inputString = new StringReader(myString); 

    //        //    //String[] myStringAr = new string[] { myString };
    //        //    Console.SetIn(inputString);
    //        //    //TitanRoverProject.Program.Main(myStringAr);
    //        //    var result = sw.ToString().Trim();
    //        //    Assert.AreEqual(Expected, result);
    //        //}

    //        var output = new StringWriter();
    //        Console.SetOut(output);

    //        var input = new StringReader("");
    //        Console.SetIn(input);

    //        TitanRoverProject.Program.Main(new string[] { });
    //        var result = output.ToString().Trim();
        
    //        Assert.AreEqual(Expected, result);
        
    //    }

    //}

    //[TestClass]
    //public class UnitTest2ChangeStartLocationOrientation
    //{
    //    private const string Expected = "1, 1, N";
    //    [TestMethod, Timeout(2000)]
    //    public void TestMethod2()
    //    {
    //        using (var sw = new StringWriter())
    //        {
    //            Console.SetOut(sw);

    //            String myString = "N";
    //            String[] myStringAr = new string[] { myString };
    //            TitanRoverProject.Program.Main(myStringAr);
    //            var result = sw.ToString().Trim();
    //            Assert.AreEqual(Expected, result);
    //        }
    //    }
    //}
    //[TestClass]
    //public class UnitTest3ChangeStartLocationOrientationThreeTimes
    //{
    //    private const string Expected = "1, 1, W";
    //    [TestMethod, Timeout(2000)]
    //    public void TestMethod3()
    //    {
    //        using (var sw = new StringWriter())
    //        {
    //            Console.SetOut(sw);

    //            String myString = "NSW";
    //            String[] myStringAr = new string[] { myString };
    //            TitanRoverProject.Program.Main(myStringAr);
    //            var result = sw.ToString().Trim();
    //            Assert.AreEqual(Expected, result);
    //        }
    //    }
    //}
    //[TestClass]
    //public class UnitTest4MoveForwardsOnce
    //{
    //    private const string Expected = "1, 2, S";
    //    [TestMethod, Timeout(2000)]
    //    public void TestMethod4()
    //    {
    //        using (var sw = new StringWriter())
    //        {
    //            Console.SetOut(sw);
    //            String myString = "M";
    //            String[] myStringAr = new string[] { myString };
    //            TitanRoverProject.Program.Main(myStringAr);
    //            var result = sw.ToString().Trim();
    //            Assert.AreEqual(Expected, result);
    //        }
    //    }
    //}
    //[TestClass]
    //public class UnitTest5LongCommand
    //{
    //    private const string Expected = "3, 3, N";
    //    [TestMethod, Timeout(2000)]
    //    public void TestMethod5()
    //    {
    //        using (var sw = new StringWriter())
    //        {
    //            Console.SetOut(sw);
    //            // String[] stringAr = new string[] { "M", "E", "M", "M", "M", "S","M", "M", "W","M","N","M" };
    //            String myString = "MEMMMSMMWMNM";
    //            String[] myStringAr = new string[] { myString };
    //            TitanRoverProject.Program.Main(myStringAr);
    //            var result = sw.ToString().Trim();
    //            Assert.AreEqual(Expected, result);
    //        }
    //    }
    //}
    //[TestClass]
    //public class UnitTest6AcceptsAnyCase
    //{
    //    private const string Expected = "3, 3, N";
    //    [TestMethod, Timeout(2000)]
    //    public void TestMethod6()
    //    {
    //        using (var sw = new StringWriter())
    //        {
    //            Console.SetOut(sw);
    //            // String[] stringAr = new string[] { "M", "e", "M", "M", "M", "s", "M", "M", "W", "m", "n", "M" };
    //            String myString = "MeMMMsMMWmnM";
    //            String[] myStringAr = new string[] { myString };
    //            TitanRoverProject.Program.Main(myStringAr);
    //            var result = sw.ToString().Trim();
    //            Assert.AreEqual(Expected, result);
    //        }
    //    }
    //}


