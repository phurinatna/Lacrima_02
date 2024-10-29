#region Includes
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;

using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using Microsoft.Xna.Framework.Media;

using MonoGame.Extended.Tiled;
using MonoGame.Extended.Tiled.Renderers;
using MonoGame.Extended.Timers;
using MonoGame.Extended;
using MonoGame.Extended.Collisions;
using MonoGame.Extended.Sprites;
using MonoGame.Extended.Serialization;
using MonoGame.Extended.Content;

#endregion

namespace Lacrima_02
{
    public class KitchenScreen : screen
    {
        Game1 game;
        TiledMap _tiledMap;
        TiledMapRenderer _tiledMapRenderer;

        private readonly List<IEntity> _entities = new List<IEntity>();
        public readonly CollisionComponent _collisionComponent;

        TiledMapObjectLayer _wallTiledObj, _door10TiledObj, _tableTiledObj, _binTiledObj;

        Texture2D textbox;
        bool textpopup, textch, ch16_end, ch17_end, ch18_end, ch19_end, ch20_end, ch21_end;
        SpriteFont font;

        public KitchenScreen(Game1 game, EventHandler theScreenEvent) : base(theScreenEvent)
        {
            this.game = game;
            _collisionComponent = new CollisionComponent(new RectangleF(0, 0, 832, 640));

            //Load tilemap 
            _tiledMap = game.Content.Load<TiledMap>("Resources\\kitchen");
            _tiledMapRenderer = new TiledMapRenderer(game.GraphicsDevice, _tiledMap);

            //Get object layers
            foreach (TiledMapObjectLayer layer in _tiledMap.ObjectLayers)
            {
                if (layer.Name == "Wall_Object")
                {
                    _wallTiledObj = layer;
                }

                if (layer.Name == "Door_10")
                {
                    _door10TiledObj = layer;
                }

                if (layer.Name == "Table_Object")
                {
                    _tableTiledObj = layer;
                }

                if (layer.Name == "Bin_Object")
                {
                    _binTiledObj = layer;
                }
            }

            //Create entities from map 
            foreach (TiledMapObject obj in _wallTiledObj.Objects)
            {
                Point2 position = new Point2(obj.Position.X, obj.Position.Y);
                _entities.Add(new WallEntity(this.game, new RectangleF(position, obj.Size)));
            }
            foreach (TiledMapObject obj in _door10TiledObj.Objects)
            {
                Point2 position = new Point2(obj.Position.X, obj.Position.Y);
                _entities.Add(new Door10Entity(this.game, new RectangleF(position, obj.Size)));
            }
            foreach (TiledMapObject obj in _tableTiledObj.Objects)
            {
                Point2 position = new Point2(obj.Position.X, obj.Position.Y);
                _entities.Add(new TableEntity(this.game, new RectangleF(position, obj.Size)));
            }
            foreach (TiledMapObject obj in _binTiledObj.Objects)
            {
                Point2 position = new Point2(obj.Position.X, obj.Position.Y);
                _entities.Add(new BinEntity(this.game, new RectangleF(position, obj.Size)));
            }


            //Setup player 
            SpriteSheet playerSheet = game.Content.Load<SpriteSheet>("Resources\\Mary_Animation.sf", new JsonContentLoader());
            _entities.Add(new PlayerEntity(this.game, new RectangleF(new Point2(32 * 3, 32 * 15), new Size2(48, 48)), new AnimatedSprite(playerSheet)));

            //Setup ai 
            SpriteSheet miriamSheet = game.Content.Load<SpriteSheet>("Resources\\Miriam_Animation.sf", new JsonContentLoader());
            _entities.Add(new MiriamEntity(this.game, new RectangleF(new Point2((32 * 3) + 8, 32 * 6), new Size2(48, 48)), new AnimatedSprite(miriamSheet)));

            foreach (IEntity entity in _entities)
            {
                _collisionComponent.Insert(entity);
            }

            textbox = game.Content.Load<Texture2D>("Resources\\Ui\\textboxkitchen");
            font = game.Content.Load<SpriteFont>("Resources\\ArialFont");
        }

        private bool enterPressed = false;
        private bool fKeyPressed = false;
        public override void Update(GameTime theTime)
        {
            Console.WriteLine($"Current game.state: {game.state}");

            if (game.hall1 == true)
            {
                ScreenEvent.Invoke(game.mHall1Screen, new EventArgs());
                return;
            }

            if (game.state == 22)
            {
                ScreenEvent.Invoke(game.mEndScreen, new EventArgs());
                return;
            }

            if (((game.miriamtext == true) && (game.state == 19)) && Keyboard.GetState().IsKeyDown(Keys.F))
            {
                if (!fKeyPressed)
                {
                    if (!textch)
                    {
                        textch = true;
                    }
                    else
                    { }
                    fKeyPressed = true;
                }
            }
            else if (Keyboard.GetState().IsKeyUp(Keys.F))
            {
                fKeyPressed = false;
            }




            if (game.state == 16 || game.state == 17 || game.state == 20)
            {
                textch = true;
            }

            if (((game.state == 18 || game.state == 21 )&& game.bin == true) && Keyboard.GetState().IsKeyDown(Keys.F))
            {
                if (!fKeyPressed)
                {
                    if (!textch)
                    {
                        textch = true;
                    }
                    fKeyPressed = true;
                }
            }
            else if (Keyboard.GetState().IsKeyUp(Keys.F))
            {
                fKeyPressed = false;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Enter) && !enterPressed)
            {
                if (game.state == 16 && ch16_end)
                {
                    game.state = 17;
                    ch16_end = false;
                }
                else if (game.state == 17 && ch17_end)
                {
                    game.state = 18;
                    ch17_end = false;
                }
                else if (game.state == 18 && ch18_end)
                {
                    game.state = 19;
                    ch18_end = false;
                }
                else if (game.state == 19 && ch19_end)
                {
                    game.state = 20;
                    ch19_end = false;
                }
                else if (game.state == 20 && ch20_end)
                {
                    game.state = 21;
                    ch20_end = false;
                }
                else if (game.state == 21 && ch21_end)
                {
                    game.state = 22;
                    ch21_end = false;
                }

                textpopup = false;
                game.miriamtext = false;
                game.bin = false;
                textch = false;
                enterPressed = true;
            }
            else if (Keyboard.GetState().IsKeyUp(Keys.Enter))
            {
                enterPressed = false;
            }

            foreach (IEntity entity in _entities)
            {
                entity.Update(theTime);
            }
            _collisionComponent.Update(theTime);
            _tiledMapRenderer.Update(theTime);
            base.Update(theTime);
        }

        public override void Draw(SpriteBatch theBatch)
        {
            _tiledMapRenderer.Draw();
            foreach (IEntity entity in _entities)
            {
                entity.Draw(theBatch);
            }

            string ch1_16, ch1_17, ch1_18, ch1_19, ch1_20, ch1_21;
            ch1_16 = "Mary \n\nShe is not here.";//เธอไม่ได้อยู่ที่นี่
            ch1_17 = "Mary \n\nHowever, let's take a little more time to explore the room.";
            ch1_18 = "Mary \n\nIt's full of garbage.";
            ch1_19 = "Miriam \n\nGood morning, Mary.\nCould you please take out the garbage.";
            ch1_20 = "Mary \n\nCertainly, I'd be more than pleased to assist you!";
            ch1_21 = "She is nothing but a terrible creature. She embodies evil. \nShe's about to shatter our peace.  \nShe will bring about a terrifying danger. \nThose glowing yellow eyes are utterly horrifying. \nThey are the eyes of a demon.";

            if (game.state == 16 && textch == true)
            {
                theBatch.Draw(textbox, new Vector2(0, game.graphics.PreferredBackBufferHeight - textbox.Height), new Rectangle(0, 0, 807, 148), Color.White);
                theBatch.DrawString(font, ch1_16, new Vector2(20, 520), Color.White);
                ch16_end = true;
            }
            else if (game.state == 17 && textch == true)
            {
                theBatch.Draw(textbox, new Vector2(0, game.graphics.PreferredBackBufferHeight - textbox.Height), new Rectangle(0, 0, 807, 148), Color.White);
                theBatch.DrawString(font, ch1_17, new Vector2(20, 520), Color.White);
                ch17_end = true;
            }
            else if (game.state == 18 && textch == true)
            {
                theBatch.Draw(textbox, new Vector2(0, game.graphics.PreferredBackBufferHeight - textbox.Height), new Rectangle(0, 0, 807, 148), Color.White);
                theBatch.DrawString(font, ch1_18, new Vector2(20, 520), Color.White);
                ch18_end = true;
            }
            else if (game.state == 19 && textch == true)
            {
                theBatch.Draw(textbox, new Vector2(0, game.graphics.PreferredBackBufferHeight - textbox.Height), new Rectangle(0, 0, 807, 148), Color.White);
                theBatch.DrawString(font, ch1_19, new Vector2(20, 520), Color.White);
                ch19_end = true;
            }
            else if (game.state == 20 && textch == true)
            {
                theBatch.Draw(textbox, new Vector2(0, game.graphics.PreferredBackBufferHeight - textbox.Height), new Rectangle(0, 0, 807, 148), Color.White);
                theBatch.DrawString(font, ch1_20, new Vector2(20, 520), Color.White);
                ch20_end = true;
            }
            else if (game.state == 21 && textch == true)
            {
                theBatch.Draw(textbox, new Vector2(0, game.graphics.PreferredBackBufferHeight - textbox.Height), new Rectangle(0, 0, 807, 148), Color.White);
                theBatch.DrawString(font, ch1_21, new Vector2(20, 520), Color.White);
                ch21_end = true;
            }

            base.Draw(theBatch);
        }
    }
}
