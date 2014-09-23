using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameOne
{
    class Chicken : AutomatedSprite
    {
        public Chicken(Texture2D texture)
            :base(new SpriteSheet(texture, new Point(24, 0), 1.0f), new Vector2(600, 50), 
            new CollisionOffset(50, 0, 50, 50), new Vector2(32f, 0))
        {
            Point frameSize = new Point(148, 110);
            spriteSheet.addSegment(frameSize, new Point(0, 0), new Point(23, 0), 10);
            
            spriteSheet.setCurrentSegment(0);
        }

        public override void Update(GameTime gameTime, Rectangle clientBounds)
        {
            base.Update(gameTime, clientBounds);
        }

        public override void Collision(Sprite otherSprite)
        {
            position.X += -speed.X;
            speed *= -1;
        }
    }
}
