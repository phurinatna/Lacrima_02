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
    public class TitleScreen : screen
    {
        Texture2D titleBG, titleUI;
        Game1 game;
        
        int currentMenu = 1;
        bool keyActiveUp = false;
        bool keyActiveDown = false;
        
        public TitleScreen(Game1 game, EventHandler theScreenEvent) : base(theScreenEvent)
        {

            titleBG = game.Content.Load<Texture2D>("Resources\\Background\\Background");
            titleUI = game.Content.Load<Texture2D>("Resources\\Ui\\Title_Ui");
            this.game = game;
        }

        public override void Update(GameTime theTime)
        {
            KeyboardState keyboard = Keyboard.GetState();
            if (keyboard.IsKeyDown(Keys.Up))
            {
                if (keyActiveUp == true)
                {
                    if (currentMenu > 1)
                    {
                        currentMenu = currentMenu - 1;
                        keyActiveUp = false;
                    }
                }
            }
            if (keyboard.IsKeyDown(Keys.Down))
            {
                if (keyActiveDown == true)
                {
                    if (currentMenu < 3)
                    {
                        currentMenu = currentMenu + 1; keyActiveDown = false;
                    }
                }
            }
            if (keyboard.IsKeyUp(Keys.Up))
            {
                keyActiveUp = true;
            }
            if (keyboard.IsKeyUp(Keys.Down))
            {
                keyActiveDown = true;
            }

            if (currentMenu == 1 && keyboard.IsKeyDown(Keys.Enter) == true)
            {
                ScreenEvent.Invoke(game.mLevelScreen, new EventArgs());
                return;
            }
            if (currentMenu == 3 && keyboard.IsKeyDown(Keys.Enter) == true)
            {
                game.Exit();
            }

            base.Update(theTime);
        }

        public override void Draw(SpriteBatch theBatch)
        {
            theBatch.Draw(titleBG, Vector2.Zero, Color.White);

            theBatch.Draw(titleUI, new Vector2(350, 0), new Rectangle(0, 0, 550, 250), Color.White);

            if (currentMenu == 1)
            {
                theBatch.Draw(titleUI, new Vector2(520, 280), new Rectangle(50, 515, 245, 60), Color.White);
            }
            else
            {
                theBatch.Draw(titleUI, new Vector2(520, 280), new Rectangle(60, 310, 245, 60), Color.White);
            }

            if (currentMenu == 2)
            {
                theBatch.Draw(titleUI, new Vector2(520, 360), new Rectangle(395, 510, 245, 70), Color.White);
            }
            else
            {
                theBatch.Draw(titleUI, new Vector2(520, 360), new Rectangle(410, 300, 245, 70), Color.White);
            }

            if (currentMenu == 3)
            {
                theBatch.Draw(titleUI, new Vector2(520, 450), new Rectangle(745, 510, 245, 65), Color.White);
            }
            else
            {
                theBatch.Draw(titleUI, new Vector2(520, 450), new Rectangle(765, 295, 245, 65), Color.White);
            }

            base.Draw(theBatch);

        }

        
    }
}
