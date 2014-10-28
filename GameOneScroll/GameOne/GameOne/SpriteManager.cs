using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;


namespace GameOne
{
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class SpriteManager : Microsoft.Xna.Framework.DrawableGameComponent
    {
        SpriteBatch spriteBatch;
        Sprite player;
        List<Sprite> spriteList = new List<Sprite>();
        ScrollingBackground myBackground;

        public SpriteManager(Game game)
            : base(game)
        {
            // TODO: Construct any child components here
        }

        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public override void Initialize()
        {
            // TODO: Add your initialization code here

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(Game.GraphicsDevice);

            player = new GlitchPlayer(Game.Content.Load<Texture2D>(@"Images/LadyZ"));
            spriteList.Add(new Chicken(Game.Content.Load<Texture2D>(@"Images/chicken_walk")));
            spriteList.Add(new Platform(new SpriteSheet(Game.Content.Load<Texture2D>(@"Images/grade_aa_earth_block"), 
                new Point(1, 0), 1.0f), new Vector2(100, 630), new CollisionOffset(25, 0, 20, 20)));

            // Background
            myBackground = new ScrollingBackground();
            Texture2D background = Game.Content.Load<Texture2D>(@"Images\background");
            myBackground.Load(GraphicsDevice, background);

            base.LoadContent();
        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            player.Update(gameTime, Game.Window.ClientBounds);

            if (player.position.X >= Game.Window.ClientBounds.Width / 2)
            {
                myBackground.Update(player.direction.X * 3);
            }

            // update each automated sprite
            foreach (Sprite sprite in spriteList)
                sprite.Update(gameTime, Game.Window.ClientBounds);

            // collision
            foreach (Sprite sprite in spriteList)
                if (sprite.collisionRect.Intersects(player.collisionRect))
                {
<<<<<<< HEAD
                    //((Game1)Game).PlayCue("level_up_kazoo");
=======
>>>>>>> parent of 6fa03c9... ... now with sound!
                    player.Collision(sprite);
                    sprite.Collision(player);
                }

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            myBackground.Draw(spriteBatch);
            player.Draw(gameTime, spriteBatch);
            foreach (Sprite sprite in spriteList)
                sprite.Draw(gameTime, spriteBatch);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
