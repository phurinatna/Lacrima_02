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
    public class LevelScreen : screen
    {
        Texture2D levelBG, levelUI_1, levelUI_2;
        Game1 game;
        public int currentLevel = 0;
        bool keyActiveUp = false;
        bool keyActiveDown = false;

        const int MapWidth = 1280;
        const int MapHeight = 720;


        public LevelScreen(Game1 game, EventHandler theScreenEvent) : base(theScreenEvent)
        {

            //Load the background texture for the screen
            levelBG = game.Content.Load<Texture2D>("Resources\\Background\\Background");
            levelUI_1 = game.Content.Load<Texture2D>("Resources\\Ui\\Level_UI_1");
            levelUI_2 = game.Content.Load<Texture2D>("Resources\\Ui\\Level_UI_2");
            this.game = game;
        }
        public override void Update(GameTime theTime)
        {
            KeyboardState keyboard = Keyboard.GetState();
            if (keyboard.IsKeyDown(Keys.Back) == true)
            {
                ScreenEvent.Invoke(game.mTitleScreen, new EventArgs());
                currentLevel = 0;
                return;
            }

            if (keyboard.IsKeyDown(Keys.Up))
            {
                if (keyActiveUp == true)
                {
                    if (currentLevel > 1)
                    {
                        currentLevel = currentLevel - 1;
                        keyActiveUp = false;
                    }
                }
            }
            if (keyboard.IsKeyDown(Keys.Down))
            {
                if (keyActiveDown == true)
                {
                    if (currentLevel < 3)
                    {
                        currentLevel = currentLevel + 1;
                        keyActiveDown = false;
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

            

            if (currentLevel == 1 && keyboard.IsKeyDown(Keys.Enter) == true)
            {
                ScreenEvent.Invoke(game.mBedroomScreen, new EventArgs());
                currentLevel = 0;
                return;
            }
            if (currentLevel == 2 && keyboard.IsKeyDown(Keys.Enter) == true && game.level_2 == true)
            {
                ScreenEvent.Invoke(game.mChuchScreen, new EventArgs());
                currentLevel = 0;
                return;
            }

            base.Update(theTime);
        }
        public override void Draw(SpriteBatch theBatch)
        {
            theBatch.Draw(levelBG, Vector2.Zero, Color.White);

            if (currentLevel == 1 )
            {
                theBatch.Draw(levelUI_1, new Vector2(100, 75), new Rectangle(70, 75, 585, 150), Color.White);
            }
            else
            {
                theBatch.Draw(levelUI_2, new Vector2(100, 75), new Rectangle(40, 60, 585, 150), Color.White);
            }

            if (game.level_2 == false)
            {
                theBatch.Draw(levelUI_2, new Vector2(350, 275), new Rectangle(355, 270, 585, 150), Color.White);
            }
            else if (currentLevel == 2 && game.level_2 == true)
            {
                theBatch.Draw(levelUI_1, new Vector2(350, 275), new Rectangle(375, 300, 585, 150), Color.White);
            }
            else
            {
                theBatch.Draw(levelUI_2, new Vector2(350, 275), new Rectangle(355, 270, 585, 150), Color.White);
            }

            if (game.level_3 == false)
            {
                theBatch.Draw(levelUI_2, new Vector2(600, 475), new Rectangle(675, 515, 580, 150), Color.White);
            }
            else if (currentLevel == 3 && game.level_3 == true)
            {
                theBatch.Draw(levelUI_1, new Vector2(600, 475), new Rectangle(660, 535, 580, 150), Color.White);
            }
            else
            {
                theBatch.Draw(levelUI_2, new Vector2(580, 475), new Rectangle(675, 515, 580, 150), Color.White);
            }

            base.Draw(theBatch);

        }
    }
}

