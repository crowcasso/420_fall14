using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GameOne
{
    class UserControlledSprite : Sprite
    {
        // physics
        protected Vector2 velocity;
        Vector2 friction;
        Vector2 speed;
        protected readonly Vector2 gravity = new Vector2(0, 9.8f * 64);

        // used to tell if the sprite is in free fall or not
        protected bool onGround = false;
        Vector2 oldPosition = new Vector2(-1, -1);

        /*
         * Constructor
         */
        public UserControlledSprite(SpriteSheet spriteSheet, Vector2 position, 
            CollisionOffset collisionOffset, Vector2 speed, Vector2 friction)
            : base(spriteSheet, position, collisionOffset)
        {
            this.speed = speed;
            this.friction = friction;
        }

        /*
         * Sets the direction based on keyboard/gamepad and flips the image if necessary.
         */
        public override Vector2 direction
        {
            get
            {
                Vector2 inputDirection = Vector2.Zero;

                /* keyboard input */
                if (Keyboard.GetState().IsKeyDown(Keys.A))
                    inputDirection.X -= 1;
                if (Keyboard.GetState().IsKeyDown(Keys.D))
                    inputDirection.X += 1;
                if (Keyboard.GetState().IsKeyDown(Keys.W))
                    inputDirection.Y -= 1;
                if (Keyboard.GetState().IsKeyDown(Keys.S))
                    inputDirection.Y += 1;

                /* gamepad input */
                GamePadState gamepadState = GamePad.GetState(PlayerIndex.One);
                if (gamepadState.ThumbSticks.Left.X != 0)
                    inputDirection.X += gamepadState.ThumbSticks.Left.X;
                if (gamepadState.ThumbSticks.Left.Y < 0)
                    inputDirection.Y -= gamepadState.ThumbSticks.Left.Y;

                /* do we need to flip the image? */
                if (inputDirection.X < 0)
                    effects = SpriteEffects.FlipHorizontally;
                else if (inputDirection.X > 0)
                    effects = SpriteEffects.None;

                return inputDirection;
            }
        }

        /*
         * Add some physics to help move the sprite around. 
         */
        public override void Update(GameTime gameTime, Rectangle clientBounds)
        {
            // physics
            velocity += direction * speed;
            velocity += gravity * (float)gameTime.ElapsedGameTime.TotalSeconds;
            velocity *= friction;
            position += velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;

            /* If sprite is off the screen, move it back within the game window */
            if (position.X < -collisionOffset.east * spriteSheet.scale)
                position.X = -collisionOffset.east * spriteSheet.scale;
            if (position.Y < -collisionOffset.north * spriteSheet.scale)
                position.Y = -collisionOffset.north * spriteSheet.scale;
            if (position.X > clientBounds.Width - spriteSheet.scale *(spriteSheet.currentSegment.frameSize.X - collisionOffset.west))
                position.X = clientBounds.Width - spriteSheet.scale * (spriteSheet.currentSegment.frameSize.X - collisionOffset.west);
            if (position.Y > clientBounds.Height - spriteSheet.scale * (spriteSheet.currentSegment.frameSize.Y - collisionOffset.south))
            {
                velocity.Y = 0;
                onGround = true;
                position.Y = clientBounds.Height - spriteSheet.scale * (spriteSheet.currentSegment.frameSize.Y - collisionOffset.south);
            }
            

            base.Update(gameTime, clientBounds);
        }
    }
}
