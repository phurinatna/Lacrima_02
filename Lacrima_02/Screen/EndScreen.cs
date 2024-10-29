#region Includes
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;

using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using Microsoft.Xna.Framework.Media;

using MonoGame.Extended.Tiled;
using MonoGame.Extended.Tiled.Renderers;
using MonoGame.Extended;
using MonoGame.Extended.Collisions;

#endregion

namespace Lacrima_02
{
    public class EndScreen : screen
    {
        Texture2D endBG;
        Game1 game;

        public EndScreen(Game1 game, EventHandler theScreenEvent) : base(theScreenEvent)
        {
            this.game = game;
            endBG = game.Content.Load<Texture2D>("Resources\\end");
            
        }

        public override void Update(GameTime theTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Back) == true)
            {
                ScreenEvent.Invoke(game.mTitleScreen, new EventArgs());
                return;
            }

            base.Update(theTime);
        }

        public override void Draw(SpriteBatch theBatch)
        {
            theBatch.Draw(endBG, Vector2.Zero, Color.White);

            base.Draw(theBatch);

        }


    }
}
