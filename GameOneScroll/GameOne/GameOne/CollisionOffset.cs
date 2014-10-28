using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameOne
{
    class CollisionOffset
    {
        public readonly int north, south, east, west;

        public CollisionOffset(int north, int south, int east, int west)
        {
            this.north = north;
            this.south = south;
            this.east = east;
            this.west = west;
        }
    }
}
