using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace GameOne
{
    class SpriteSheetSegment
    {
        public Point frameSize;
        public Point startFrame;
        public Point endFrame;
        public int millisecondsPerFrame;

        public SpriteSheetSegment(Point frameSize, Point startFrame, Point endFrame, 
            int millisecondsPerFrame)
        {
            this.frameSize = frameSize;
            this.startFrame = startFrame;
            this.endFrame = endFrame;
            this.millisecondsPerFrame = millisecondsPerFrame;
        }
    }
}
