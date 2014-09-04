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
        public UserControlledSprite(SpriteSheet spriteSheet, Vector2 position, int collisionOffset, Vector2 speed)
            : base(spriteSheet, position, collisionOffset, speed)
        {
        }

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
                if (gamepadState.ThumbSticks.Left.Y != 0)
                    inputDirection.Y -= gamepadState.ThumbSticks.Left.Y;

                /* do we need to flip the image? */
                if (inputDirection.X < 0)
                    effects = SpriteEffects.FlipHorizontally;
                else if (inputDirection.X > 0)
                    effects = SpriteEffects.None;

                return inputDirection * speed;
            }
        }

        public override void Update(GameTime gameTime, Rectangle clientBounds)
        {
            position += direction;

            // If sprite is off the screen, move it back within the game window
            if (position.X < 0)
                position.X = 0;
            if (position.Y < 0)
                position.Y = 0;
            if (position.X > clientBounds.Width - spriteSheet.currentSegment.frameSize.X)
                position.X = clientBounds.Width - spriteSheet.currentSegment.frameSize.X;
            if (position.Y > clientBounds.Height - spriteSheet.currentSegment.frameSize.Y)
                position.Y = clientBounds.Height - spriteSheet.currentSegment.frameSize.Y;

            base.Update(gameTime, clientBounds);
        }
    }
}
