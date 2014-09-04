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

        int collisionOffset;

        int timeSinceLastFrame = 0;

        protected Vector2 speed;
        protected Vector2 position;

        public Sprite(SpriteSheet spriteSheet, Vector2 position, int collisionOffset, Vector2 speed)
        {
            this.spriteSheet = spriteSheet;
            this.position = position;
            this.collisionOffset = collisionOffset;
            this.speed = speed;
        }

        public virtual void Update(GameTime gameTime, Rectangle clientBounds)
        {
            timeSinceLastFrame += gameTime.ElapsedGameTime.Milliseconds;
            if (timeSinceLastFrame >= spriteSheet.currentSegment.millisecondsPerFrame)
            {
                timeSinceLastFrame = 0;
                if (!pauseAnimation)
                {
                    ++currentFrame.X;
                    // move the frame forward
                    if (currentFrame.X >= spriteSheet.sheetSize.X || currentFrame.X > spriteSheet.currentSegment.endFrame.X)
                    {
                        currentFrame.X = spriteSheet.currentSegment.startFrame.X;

                        ++currentFrame.Y;
                        if (currentFrame.Y >= spriteSheet.sheetSize.Y || currentFrame.Y > spriteSheet.currentSegment.endFrame.Y)
                        {
                            currentFrame.Y = spriteSheet.currentSegment.startFrame.Y;
                        }
                    }
                }
            }
        }

        public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(spriteSheet.texture,
                position,
                new Rectangle(currentFrame.X * spriteSheet.currentSegment.frameSize.X,
                    currentFrame.Y * spriteSheet.currentSegment.frameSize.Y,
                    spriteSheet.currentSegment.frameSize.X, spriteSheet.currentSegment.frameSize.Y),
                Color.White,
                0,
                Vector2.Zero,
                1f,
                effects,
                0);
        }

        public abstract Vector2 direction
        {
            get;
        }

        public Rectangle collisionRect
        {
            get
            {
                return new Rectangle(
                    (int)position.X + collisionOffset,
                    (int)position.Y + collisionOffset,
                    spriteSheet.currentSegment.frameSize.X - (collisionOffset * 2),
                    spriteSheet.currentSegment.frameSize.Y - (collisionOffset * 2));
            }
        }
    }
}
