using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Linq; //not using at moment
using System.Reflection;

namespace TitanRoverProject
{
    public class Coordinates //if using multiple coordinate system for different activities they should have posession of own delegate rather than sharing rover's delegate
    {
        public int X, Y;

        //Later if the coordinates' maximums need to be changed, built in methods in the get and set could protect against
        //moving the maxX to less than the current coordinate or to alter current location if changing orientation or numbering
        //of the coordinate system
        public int maxX { get; set; }
        public int minX { get; set; }
        public int maxY { get; set; }
        public int minY { get; set; }

        public string lastOrientation;
        //Constructor uses default location and size coordinates

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

        public string getCoordData()
        {
            return string.Format(" X={0}, Y={1}, MinX={2}, MinY={3}, maxX={4}, maxY={5} orientation={6} ", X, Y, minX, minY, maxX, maxY, lastOrientation);
        }
        public string getCoordDataShort() //could print as well but really userInterface should do it
        {
            return string.Format($"{X}, {Y}, {lastOrientation}");
        }
    }
}
