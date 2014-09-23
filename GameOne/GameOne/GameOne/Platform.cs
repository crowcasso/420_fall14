using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace GameOne
{
    class Platform : Sprite
    {
        public Platform(SpriteSheet spriteSheet, Vector2 position, 
            CollisionOffset collisionOffset)
            : base(spriteSheet, position, collisionOffset)
        {
            Point frameSize = new Point(111, 67);
            spriteSheet.addSegment(frameSize, new Point(0, 0), new Point(0, 0), 10);
            
            spriteSheet.setCurrentSegment(0);
            base.isImpassable = true;
        }

        public override Vector2 direction
        {
            get
            {
                return Vector2.Zero;
            }
        }

    }
}
