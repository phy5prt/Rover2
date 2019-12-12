
using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Linq; //not using at moment
using System.Reflection;

namespace TitanRoverProject
{
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

        //Uncomment this comment block section to see how functionality is added

        //public static Coordinates X(Coordinates coords, int Scale)
        //{
        //    coords.X = coords.X + 1 * Scale;
        //    coords.Y = coords.Y - 1 * Scale;
        //    return coords;
        //}

        //End of example method comment block
    }
}
