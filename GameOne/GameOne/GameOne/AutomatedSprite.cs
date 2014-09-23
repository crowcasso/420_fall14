using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameOne
{
    class AutomatedSprite : Sprite
    {
        protected Vector2 speed;
        protected Vector2 velocity;
        protected readonly Vector2 gravity = new Vector2(0, 9.8f * 64);
        Vector2 friction = new Vector2(0.8f, 1f);
        protected bool onGround = false;
        Vector2 oldPosition = new Vector2(-1, -1);

        public AutomatedSprite(SpriteSheet spriteSheet, Vector2 position, 
            CollisionOffset collionOffset, Vector2 speed)
            : base(spriteSheet, position, collionOffset)
        {
            this.speed = speed;
        }

        public override Vector2 direction
        {
            get
            {
                if (speed.X < 0)
                    effects = SpriteEffects.FlipHorizontally;
                else
                    effects = SpriteEffects.None;

                return speed;
            }
        }

        public override void Update(GameTime gameTime, Rectangle clientBounds)
        {
            velocity += direction;
            velocity += gravity * (float)gameTime.ElapsedGameTime.TotalSeconds;
            velocity *= friction;
            position += velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;

            // Is the sprite standing on something
            onGround = position.Y == oldPosition.Y;
            oldPosition = position;

            if (onGround == true)
                velocity.Y = 0;

            // If sprite is off the screen, move it back within the game window
            if (position.X < -collisionOffset.east)
            {
                speed *= -1;
                position.X = -collisionOffset.east + 10;
            }
            if (position.Y < -collisionOffset.north)
                position.Y = -collisionOffset.north;
            if (position.X > clientBounds.Width - (spriteSheet.currentSegment.frameSize.X - collisionOffset.west))
            {
                speed *= -1;
                position.X = clientBounds.Width - (spriteSheet.currentSegment.frameSize.X - collisionOffset.west) - 10;
            }
            if (position.Y > clientBounds.Height - (spriteSheet.currentSegment.frameSize.Y - collisionOffset.south))
            {
                position.Y = clientBounds.Height - (spriteSheet.currentSegment.frameSize.Y - collisionOffset.south);
            }

            base.Update(gameTime, clientBounds);
        }
    }
}
