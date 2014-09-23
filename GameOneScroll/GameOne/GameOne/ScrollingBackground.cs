using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameOne
{
    class ScrollingBackground
    {
        private Vector2 screenpos, origin, texturesize;
        private Texture2D mytexture;
        private int screenheight, screenwidth;

        public void Load(GraphicsDevice device, Texture2D backgroundTexture)
        {
            mytexture = backgroundTexture;
            screenheight = device.Viewport.Height;
            screenwidth = device.Viewport.Width;

            origin = new Vector2(0, mytexture.Height - screenheight);

            screenpos = new Vector2(0, 0);

            texturesize = new Vector2(mytexture.Width, 0);
        }

        public void Update(float delta)
        {
            screenpos.X -= delta;
            screenpos.X = screenpos.X % mytexture.Width;
        }

        public void Draw(SpriteBatch batch)
        {
            if (screenpos.X < screenwidth)
            {
                batch.Draw(mytexture, screenpos, null, Color.White, 0, origin, 1.0f, SpriteEffects.None, 0.0f);
            }

            batch.Draw(mytexture, screenpos - texturesize, null, Color.White, 0, origin, 1.0f, SpriteEffects.None, 0.0f);
        }
    }
}
