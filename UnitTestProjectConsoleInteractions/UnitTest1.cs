using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;



namespace UnitTestProjectConsoleInteractions
{
    [TestClass]
    public class ConsoleUnitTest1StartLocationOrientation
    {
        private const string Expected = "1, 1, S";
        [TestMethod]
        public void TestMethod1()
        {
            using (var sw = new StringWriter())
         
            {
                Console.SetOut(sw);

                String[] myStringAr = new string[] { };
                Rover2Project.Program.Main(myStringAr);
                using (StringReader sr = new StringReader("Q")){
                    Console.SetIn(sr);

                }
                var result = sw.ToString().Trim();
                Assert.AreEqual(Expected, result);

            }
        }
    }

    [TestClass]
    public class UnitTest2ChangeStartLocationOrientation
    {
        private const string Expected = "1, 1, N";
        [TestMethod]
        public void TestMethod2()
        {
            using (var sw = new StringWriter())
            {
                Console.SetOut(sw);

                String myString = "N";
                String[] myStringAr = new string[] { myString };
                Rover2Project.Program.Main(myStringAr);
                var result = sw.ToString().Trim();
                Assert.AreEqual(Expected, result);
            }
        }
    }
    [TestClass]
    public class UnitTest3ChangeStartLocationOrientationThreeTimes
    {
        private const string Expected = "1, 1, W";
        [TestMethod]
        public void TestMethod3()
        {
            using (var sw = new StringWriter())
            {
                Console.SetOut(sw);

                String myString = "NSW";
                String[] myStringAr = new string[] { myString };
                Rover2Project.Program.Main(myStringAr);
                var result = sw.ToString().Trim();
                Assert.AreEqual(Expected, result);
            }
        }
    }
    [TestClass]
    public class UnitTest4MoveForwardsOnce
    {
        private const string Expected = "1, 2, S";
        [TestMethod]
        public void TestMethod4()
        {
            using (var sw = new StringWriter())
            {
                Console.SetOut(sw);
                String myString = "M";
                String[] myStringAr = new string[] { myString };
                Rover2Project.Program.Main(myStringAr);
                var result = sw.ToString().Trim();
                Assert.AreEqual(Expected, result);
            }
        }
    }
    [TestClass]
    public class UnitTest5LongCommand
    {
        private const string Expected = "3, 3, N";
        [TestMethod]
        public void TestMethod5()
        {
            using (var sw = new StringWriter())
            {
                Console.SetOut(sw);
                // String[] stringAr = new string[] { "M", "E", "M", "M", "M", "S","M", "M", "W","M","N","M" };
                String myString = "MEMMMSMMWMNM";
                String[] myStringAr = new string[] { myString };
                Rover2Project.Program.Main(myStringAr);
                var result = sw.ToString().Trim();
                Assert.AreEqual(Expected, result);
            }
        }
    }
    [TestClass]
    public class UnitTest6AcceptsAnyCase
    {
        private const string Expected = "3, 3, N";
        [TestMethod]
        public void TestMethod6()
        {
            using (var sw = new StringWriter())
            {
                Console.SetOut(sw);
                // String[] stringAr = new string[] { "M", "e", "M", "M", "M", "s", "M", "M", "W", "m", "n", "M" };
                String myString = "MeMMMsMMWmnM";
                String[] myStringAr = new string[] { myString };
                Rover2Project.Program.Main(myStringAr);
                var result = sw.ToString().Trim();
                Assert.AreEqual(Expected, result);
            }
        }
    }
    //[TestClass]   //doesnt trigger console so had to test using main and run
    //public class Unit7RerequestDataIfInvalidCommandUSed
    //{
    //    private const string Expected = "0, 0, N"; //Need input box test //type WMNM
    //    [TestMethod]
    //    public void TestMethod7()
    //    {
    //        using (var sw = new StringWriter())
    //        {
    //            Console.SetOut(sw);
    //            String[] stringAr = new string[] { "M", "E", "M", "M", "B", "S", "M", "M", "W", "M", "N", "M" };
    //            Rover2Project.Program.Main(stringAr);
    //            var result = sw.ToString().Trim();
    //            Assert.AreEqual(Expected, result);
    //        }
    //    }
    //}

    public class Unit8OnlyUseLastDirectionDontCombine
    {
        private const string Expected = "1, 2, E"; //Need input box test //type WMNM
        [TestMethod]
        public void TestMethod8()
        {
            using (var sw = new StringWriter())
            {
                Console.SetOut(sw);
                //  String[] stringAr = new string[] { "S","E","M" }; //if we use multiple delegate including start orientation this would be SSE
                String myString = "SEM";
                String[] myStringAr = new string[] { myString };
                Rover2Project.Program.Main(myStringAr);
                var result = sw.ToString().Trim();
                Assert.AreEqual(Expected, result);
            }
        }
    }


    [TestClass]
    public class UnitTest9CanReceiveMultipleInstructions //test fails because accessing through main so remaking object, rather than program requesting more instructions and test giving them
    {
        private const string Expected = "3, 3, N";
        [TestMethod]
        public void TestMethod9()
        {
            using (var sw = new StringWriter())
            {


                String myString1 = "MEMMM";
                String myString2 = "SMM";
                String myString3 = "WMNM";
                String[] myStringAr1 = new string[] { myString1 };
                Rover2Project.Program.Main(myStringAr1);

                String[] myStringAr2 = new string[] { myString2 };
                Rover2Project.Program.Main(myStringAr2);

                Console.SetOut(sw);

                String[] myStringAr3 = new string[] { myString3 };
                Rover2Project.Program.Main(myStringAr3);

                var result = sw.ToString().Trim();
                Assert.AreEqual(Expected, result);
            }
        }
    }
}
