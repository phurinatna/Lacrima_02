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
    public class BedroomScreen : screen
    {
        Game1 game;
        TiledMap _tiledMap;
        TiledMapRenderer _tiledMapRenderer;

        private readonly List<IEntity> _entities = new List<IEntity>();
        public readonly CollisionComponent _collisionComponent;

        TiledMapObjectLayer _wallTiledObj, _door8TiledObj, _tableTiledObj, _tablevanessaTiledObj, _bedTiledObj, _bedvanessaTiledObj, _bookshelfTiledObj, _windowTiledObj;

        Texture2D textbox;
        bool textpopup, textch, ch1_end, ch2_end, ch3_end, ch4_end, ch5_end, ch6_end, ch7_end;
        SpriteFont font;

        public BedroomScreen(Game1 game, EventHandler theScreenEvent) : base(theScreenEvent)
        {
            this.game = game;
            _collisionComponent = new CollisionComponent(new RectangleF(0, 0, 832, 640));

            //Load tilemap 
            _tiledMap = game.Content.Load<TiledMap>("Resources\\bedroom");
            _tiledMapRenderer = new TiledMapRenderer(game.GraphicsDevice, _tiledMap);

            //Get object layers
            foreach (TiledMapObjectLayer layer in _tiledMap.ObjectLayers)
            {
                if (layer.Name == "Wall_Object")
                {
                    _wallTiledObj = layer;
                }

                if (layer.Name == "Door_8")
                {
                    _door8TiledObj = layer;
                }

                if (layer.Name == "Table_Object")
                {
                    _tableTiledObj = layer;
                }

                if (layer.Name == "Table_Vanessa")
                {
                    _tablevanessaTiledObj = layer;
                }

                if (layer.Name == "Bed_Object")
                {
                    _bedTiledObj = layer;
                }

                if (layer.Name == "Bed_Vanessa")
                {
                    _bedvanessaTiledObj = layer;
                }

                if (layer.Name == "Book_Shelf")
                {
                    _bookshelfTiledObj = layer;
                }

                if (layer.Name == "Window_Object")
                {
                    _windowTiledObj = layer;
                }
            }

            //Create entities from map 
            foreach (TiledMapObject obj in _wallTiledObj.Objects)
            {
                Point2 position = new Point2(obj.Position.X, obj.Position.Y);
                _entities.Add(new WallEntity(this.game, new RectangleF(position, obj.Size)));
            }
            foreach (TiledMapObject obj in _door8TiledObj.Objects)
            {
                Point2 position = new Point2(obj.Position.X, obj.Position.Y);
                _entities.Add(new Door8Entity(this.game, new RectangleF(position, obj.Size)));
            }
            foreach (TiledMapObject obj in _tableTiledObj.Objects)
            {
                Point2 position = new Point2(obj.Position.X, obj.Position.Y);
                _entities.Add(new TableEntity(this.game, new RectangleF(position, obj.Size)));
            }
            foreach (TiledMapObject obj in _tablevanessaTiledObj.Objects)
            {
                Point2 position = new Point2(obj.Position.X, obj.Position.Y);
                _entities.Add(new TableVanessaEntity(this.game, new RectangleF(position, obj.Size)));
            }
            foreach (TiledMapObject obj in _bedTiledObj.Objects)
            {
                Point2 position = new Point2(obj.Position.X, obj.Position.Y);
                _entities.Add(new BedEntity(this.game, new RectangleF(position, obj.Size)));
            }
            foreach (TiledMapObject obj in _bedvanessaTiledObj.Objects)
            {
                Point2 position = new Point2(obj.Position.X, obj.Position.Y);
                _entities.Add(new BedVanessaEntity(this.game, new RectangleF(position, obj.Size)));
            }
            foreach (TiledMapObject obj in _bookshelfTiledObj.Objects)
            {
                Point2 position = new Point2(obj.Position.X, obj.Position.Y);
                _entities.Add(new BookshelfEntity(this.game, new RectangleF(position, obj.Size)));
            }
            foreach (TiledMapObject obj in _windowTiledObj.Objects)
            {
                Point2 position = new Point2(obj.Position.X, obj.Position.Y);
                _entities.Add(new WindowEntity(this.game, new RectangleF(position, obj.Size)));
            }


            //Setup player 
            SpriteSheet playerSheet = game.Content.Load<SpriteSheet>("Resources\\Mary_Animation.sf", new JsonContentLoader());
            _entities.Add(new PlayerEntity(this.game, new RectangleF(new Point2((32 * 16)+8, 32 * 6), new Size2(48, 48)), new AnimatedSprite(playerSheet)));

            foreach (IEntity entity in _entities)
            {
                _collisionComponent.Insert(entity);
            }

            textbox = game.Content.Load<Texture2D>("Resources\\Ui\\textboxbedroom");
            font = game.Content.Load<SpriteFont>("Resources\\ArialFont");
        }

        private bool enterPressed = false;
        private bool fKeyPressed = false;
        public override void Update(GameTime theTime)
        {
            Console.WriteLine($"Current game.state: {game.state}");

            if (((game.bedtext == true) || (game.bookshelf == true) || (game.window == true) || (game.tablevanessa == true) || (game.bedvanessa == true))  && Keyboard.GetState().IsKeyDown(Keys.F))
            {
                if (!fKeyPressed)
                {
                    if (!textpopup)
                    {
                        textpopup = true;
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

            if (game.state == 1 || game.state == 2 || game.state == 3 || game.state == 4 || game.state == 5 || game.state == 6 || game.state == 7)
            {
                textch = true;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Enter) && !enterPressed)
            {
                if (game.state == 1 && ch1_end)
                {
                    game.state = 2; 
                    ch1_end = false; 
                }
                else if (game.state == 2 && ch2_end)
                {
                    game.state = 3; 
                    ch2_end = false; 
                }
                else if (game.state == 3 && ch3_end)
                {
                    game.state = 4; 
                    ch3_end = false; 
                }
                else if (game.state == 4 && ch4_end)
                {
                    game.state = 5; 
                    ch4_end = false; 
                }
                else if (game.state == 5 && ch5_end)
                {
                    game.state = 6; 
                    ch5_end = false; 
                }
                else if (game.state == 6 && ch6_end)
                {
                    game.state = 7; 
                    ch6_end = false; 
                }
                else if (game.state == 7 && ch7_end)
                {
                    game.state = 8; 
                    ch7_end = false; 
                }

                textch = false;
                enterPressed = true;
                textpopup = false;
                game.bedtext = false;
                game.bookshelf = false;
                game.window = false;
                game.tablevanessa = false;
                game.bedvanessa = false;

            }
            else if (Keyboard.GetState().IsKeyUp(Keys.Enter))
            {
                enterPressed = false;
            }


            foreach (IEntity entity in _entities)
            {
                entity.Update(theTime);
            }

            if (game.hall2 == true)
            {
                ScreenEvent.Invoke(game.mHall2Screen, new EventArgs());
                return;
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

            string bed, bedvanessa, bookshelf, window, tablevanessa, ch1_1, ch1_2, ch1_3, ch1_4, ch1_5, ch1_6, ch1_7;
            bed = "Mary \n\nI don't need to sleep right now.";
            bookshelf = "Mary \n\nIt's not the right time for reading.";
            window = "Mary \n\nThe weather is nice for going outside.";
            tablevanessa = "Mary \n\nHer desk looks as tidy as always.";
            bedvanessa = "Mary \n\nWhere are you, Vanessa?";

            ch1_1 = "Mary \n\nOur Father, who art in heaven, hallowed be thy name.\nThy kingdom come, thy will be done, on earth, as it is in heaven.";
            ch1_2 = "Mary \n\nGive us this day our daily bread and forgive us our trespasses as we forgive those who trespass against us.";
            ch1_3 = "Mary \n\nand lead us not into temptation, but deliver us from evil.";
            ch1_4 = "Mary \n\nAmen.";
            ch1_5 = "\n\n\n. . .";
            ch1_6 = "Mary \n\nWhere did she go?";
            ch1_7 = "Mary \n\nToday, she might be in the dining room.";

            if (game.bedtext == true && textpopup == true)
            {
                theBatch.Draw(textbox, new Vector2(0, game.graphics.PreferredBackBufferHeight - textbox.Height), new Rectangle(0, 0, 834, 154), Color.White);
                theBatch.DrawString(font, bed, new Vector2(25, 511), Color.White);
            }
            if (game.bookshelf == true && textpopup == true)
            {
                theBatch.Draw(textbox, new Vector2(0, game.graphics.PreferredBackBufferHeight - textbox.Height), new Rectangle(0, 0, 834, 154), Color.White);
                theBatch.DrawString(font, bookshelf, new Vector2(25, 511), Color.White);
            }
            if (game.window == true && textpopup == true)
            {
                theBatch.Draw(textbox, new Vector2(0, game.graphics.PreferredBackBufferHeight - textbox.Height), new Rectangle(0, 0, 834, 154), Color.White);
                theBatch.DrawString(font, window, new Vector2(25, 511), Color.White);
            }
            if (game.tablevanessa == true && textpopup == true)
            {
                theBatch.Draw(textbox, new Vector2(0, game.graphics.PreferredBackBufferHeight - textbox.Height), new Rectangle(0, 0, 834, 154), Color.White);
                theBatch.DrawString(font, tablevanessa, new Vector2(25, 511), Color.White);
            }
            if (game.bedvanessa == true && textpopup == true)
            {
                theBatch.Draw(textbox, new Vector2(0, game.graphics.PreferredBackBufferHeight - textbox.Height), new Rectangle(0, 0, 834, 154), Color.White);
                theBatch.DrawString(font, bedvanessa, new Vector2(25, 511), Color.White);
            }



            if (game.state == 1 && textch == true)
            {
                theBatch.Draw(textbox, new Vector2(0, game.graphics.PreferredBackBufferHeight - textbox.Height), new Rectangle(0, 0, 834, 154), Color.White);
                theBatch.DrawString(font, ch1_1, new Vector2(25, 511), Color.White);
                ch1_end = true;
            }
            else if(game.state == 2 && textch == true)
            {
                theBatch.Draw(textbox, new Vector2(0, game.graphics.PreferredBackBufferHeight - textbox.Height), new Rectangle(0, 0, 834, 154), Color.White);
                theBatch.DrawString(font, ch1_2, new Vector2(25, 511), Color.White);
                ch2_end = true;
            }
            else if (game.state == 3 && textch == true)
            {
                theBatch.Draw(textbox, new Vector2(0, game.graphics.PreferredBackBufferHeight - textbox.Height), new Rectangle(0, 0, 834, 154), Color.White);
                theBatch.DrawString(font, ch1_3, new Vector2(25, 511), Color.White);
                ch3_end = true;
            }
            else if (game.state == 4 && textch == true)
            {
                theBatch.Draw(textbox, new Vector2(0, game.graphics.PreferredBackBufferHeight - textbox.Height), new Rectangle(0, 0, 834, 154), Color.White);
                theBatch.DrawString(font, ch1_4, new Vector2(25, 511), Color.White);
                ch4_end = true;
            }
            else if (game.state == 5 && textch == true)
            {
                theBatch.Draw(textbox, new Vector2(0, game.graphics.PreferredBackBufferHeight - textbox.Height), new Rectangle(0, 0, 834, 154), Color.White);
                theBatch.DrawString(font, ch1_5, new Vector2(25, 511), Color.White);
                ch5_end = true;
            }
            else if (game.state == 6 && textch == true)
            {
                theBatch.Draw(textbox, new Vector2(0, game.graphics.PreferredBackBufferHeight - textbox.Height), new Rectangle(0, 0, 834, 154), Color.White);
                theBatch.DrawString(font, ch1_6, new Vector2(25, 511), Color.White);
                ch6_end = true;
            }
            else if (game.state == 7 && textch == true)
            {
                theBatch.Draw(textbox, new Vector2(0, game.graphics.PreferredBackBufferHeight - textbox.Height), new Rectangle(0, 0, 834, 154), Color.White);
                theBatch.DrawString(font, ch1_7, new Vector2(25, 511), Color.White);
                ch7_end = true;
            }


            base.Draw(theBatch);
        }
    }
}
