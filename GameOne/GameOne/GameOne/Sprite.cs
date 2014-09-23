using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameOne
{
    abstract class Sprite
    {
        protected SpriteSheet spriteSheet;
        protected Point currentFrame;
        protected Boolean pauseAnimation = false;
        protected SpriteEffects effects = SpriteEffects.None;

        public CollisionOffset collisionOffset;
        
        int timeSinceLastFrame = 0;
        public Vector2 position;

        public bool isImpassable = false;

        public Sprite(SpriteSheet spriteSheet, Vector2 position, CollisionOffset collisionOffset)
        {
            this.spriteSheet = spriteSheet;
            this.position = position;
            this.collisionOffset = collisionOffset;
        }

        public virtual void Update(GameTime gameTime, Rectangle clientBounds)
        {
            // animate based on time
            timeSinceLastFrame += gameTime.ElapsedGameTime.Milliseconds;
            if (timeSinceLastFrame >= spriteSheet.currentSegment.millisecondsPerFrame)
            {
                timeSinceLastFrame = 0;
                if (!pauseAnimation)
                {
                    ++currentFrame.X;   // next frame
                    // do we need to wrap-around?
                    if (currentFrame.X >= spriteSheet.sheetSize.X || 
                        currentFrame.X > spriteSheet.currentSegment.endFrame.X)
                    {
                        currentFrame.X = spriteSheet.currentSegment.startFrame.X;
                        
                        ++currentFrame.Y;  // next frame
                        // do we need to go back to the start?
                        if (currentFrame.Y >= spriteSheet.sheetSize.Y || 
                            currentFrame.Y > spriteSheet.currentSegment.endFrame.Y)
                        {
                            currentFrame.Y = spriteSheet.currentSegment.startFrame.Y;
                        }
                    }
                }
            }
        }

        /*
         * Draw the sprite!
         */
        public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(spriteSheet.texture,
                position,
                new Rectangle(
                    currentFrame.X * spriteSheet.currentSegment.frameSize.X,
                    currentFrame.Y * spriteSheet.currentSegment.frameSize.Y,
                    spriteSheet.currentSegment.frameSize.X, 
                    spriteSheet.currentSegment.frameSize.Y
                ),
                Color.White,
                0,
                Vector2.Zero,
                spriteSheet.scale,
                effects,
                0);
        }

        /*
         * Used to notify sprites if they are involved in a collision.
         */
        public virtual void Collision(Sprite otherSprite)
        {
        }

        /*
         * Used to determine the direction
         */
        public abstract Vector2 direction
        {
            get;
        }


        /*
         * Using the collision offset and the frame size, define the collision rectangle.
         */
        public Rectangle collisionRect
        {
            get
            {
                return new Rectangle(
                    (int)(position.X + (collisionOffset.east * spriteSheet.scale)),
                    (int)(position.Y + (collisionOffset.north * spriteSheet.scale)),
                    (int)(spriteSheet.scale * (spriteSheet.currentSegment.frameSize.X - (collisionOffset.east + collisionOffset.west))),
                    (int)(spriteSheet.scale * (spriteSheet.currentSegment.frameSize.Y - (collisionOffset.north + collisionOffset.south))));
            }
        }
    }
}
