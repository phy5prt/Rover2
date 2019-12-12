using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Linq; //not using at moment
using System.Reflection;

namespace TitanRoverProject
{
    //made this a class because if many rovers they would all need it, 
    // though all being the same maybe it should be part of rover

      


   class MoveOrientationCommandsDics
    {
        //Orientation methods are in a dictionary so the commands can be keys, and keys can be automatically updated when new methods added (by rover constructor) 
        public static Dictionary<string, MethodInfo> orientationCommands = new Dictionary<string, MethodInfo>();

        static MoveOrientationCommandsDics() {
            orientationCommands = MakeOrientationCommandDictionary(orientationCommands);
        }
       

        //MoveActions provide the value for the delegate when it is executed. The delegate holds the last orientation method it has been given.
        //This is where to add movement functionality
        public static Dictionary<string, int> moveActions = new Dictionary<string, int>() {
                {"M",1 }
              //,{"B", -1 } //Remove comment lines to see how an additional moveAction is added to the program.
            };

        //If rover in future requires actions unrelated to movement they should be put in an action dictionary

        //This class is where to add additional direction methods. They are automatically added to their dictionary through rover constructor
        //And the dictionary is used in producing the checks and information for acceptable keyboard input
        //Method name should be a single character that hasn't been used already as it will become the keyboard command and dictionary key

        private static Dictionary<string, MethodInfo> MakeOrientationCommandDictionary(Dictionary<string, MethodInfo> orientationCommands)
        {
            string key = "";
            MethodInfo methodInfo;
            foreach (MethodInfo directionMethod in typeof(Directions).GetMethods())
            {
                if (directionMethod.Name.Length == 1) // length == 1 is necessary to exclude in-built default class methods
                {
                    key = directionMethod.Name;
                    methodInfo = typeof(Directions).GetMethod(key);
                    orientationCommands.Add(key, methodInfo);
                }
            }
            return orientationCommands;
        }
    }
}
