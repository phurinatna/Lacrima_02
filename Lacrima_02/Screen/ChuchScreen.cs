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
using static System.Net.Mime.MediaTypeNames;

#endregion

namespace Lacrima_02
{
    public class ChuchScreen : screen
    {

        Game1 game;
        TiledMap _tiledMap;
        TiledMapRenderer _tiledMapRenderer;

        private readonly List<IEntity> _entities = new List<IEntity>();
        public readonly CollisionComponent _collisionComponent;

        TiledMapObjectLayer _wallTiledObj, _door1TiledObj, _door2TiledObj, _bookTiledObj, _chairTiledObj;

        Texture2D textbox, marcus;
        bool textpopup, textm, m1, m2, m3, m4, m5;
        SpriteFont font;

        private int _marcus = 1;

        public ChuchScreen(Game1 game, EventHandler theScreenEvent) : base(theScreenEvent)
        {
            this.game = game;
            _collisionComponent = new CollisionComponent(new RectangleF(0, 0, 640, 896));

            //Load tilemap 
            _tiledMap = game.Content.Load<TiledMap>("Resources\\mapchuch");
            _tiledMapRenderer = new TiledMapRenderer(game.GraphicsDevice, _tiledMap);

            //Get object layers 
            foreach (TiledMapObjectLayer layer in _tiledMap.ObjectLayers)
            {
                if (layer.Name == "Wall_Object")
                {
                    _wallTiledObj = layer;
                }

                if (layer.Name == "Door_1")
                {
                    _door1TiledObj = layer;
                }

                if (layer.Name == "Door_2")
                {
                    _door2TiledObj = layer;
                }

                if (layer.Name == "Book_Object")
                {
                    _bookTiledObj = layer;
                }

                if (layer.Name == "Chair_Object")
                {
                    _chairTiledObj = layer;
                }
            }

            //Create entities from map 
            foreach (TiledMapObject obj in _wallTiledObj.Objects)
            {
                Point2 position = new Point2(obj.Position.X, obj.Position.Y);
                _entities.Add(new WallEntity(this.game, new RectangleF(position, obj.Size)));
            }
            foreach (TiledMapObject obj in _door1TiledObj.Objects)
            {
                Point2 position = new Point2(obj.Position.X, obj.Position.Y);
                _entities.Add(new Door1Entity(this.game, new RectangleF(position, obj.Size)));
            }
            foreach (TiledMapObject obj in _door2TiledObj.Objects)
            {
                Point2 position = new Point2(obj.Position.X, obj.Position.Y);
                _entities.Add(new Door2Entity(this.game, new RectangleF(position, obj.Size)));
            }
            foreach (TiledMapObject obj in _bookTiledObj.Objects)
            {
                Point2 position = new Point2(obj.Position.X, obj.Position.Y);
                _entities.Add(new BookEntity(this.game, new RectangleF(position, obj.Size)));
            }
            foreach (TiledMapObject obj in _chairTiledObj.Objects)
            {
                Point2 position = new Point2(obj.Position.X, obj.Position.Y);
                _entities.Add(new ChairEntity(this.game, new RectangleF(position, obj.Size)));
            }

            //Setup player 
            SpriteSheet playerSheet = game.Content.Load<SpriteSheet>("Resources\\Mary_Animation.sf", new JsonContentLoader());
             _entities.Add(new PlayerEntity(this.game, new RectangleF(new Point2((32*2)+8, 32*10), new Size2(48, 48)), new AnimatedSprite(playerSheet)));

            //Setup ai 
            SpriteSheet marcusSheet = game.Content.Load<SpriteSheet>("Resources\\Marcus_Animation.sf", new JsonContentLoader());
            _entities.Add(new MarcusEntity(this.game, new RectangleF(new Point2((32 * 10) + 8, (32 * 14)), new Size2(48, 48)), new AnimatedSprite(marcusSheet)));

            foreach (IEntity entity in _entities)
            {
                _collisionComponent.Insert(entity);
            }

            textbox = game.Content.Load<Texture2D>("Resources\\Ui\\textboxchuch");
            font = game.Content.Load<SpriteFont>("Resources\\ArialFont");
            marcus = game.Content.Load<Texture2D>("Resources\\Marcus_1");
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

            if (game.poperoom == true)
            {
                ScreenEvent.Invoke(game.mPoperoomScreen, new EventArgs());
                return;
            }

            if (((game.booktext == true) ) && Keyboard.GetState().IsKeyDown(Keys.F))
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

            if (((game.marcustext == true) && _marcus == 1) && Keyboard.GetState().IsKeyDown(Keys.F))
            {
                if (!fKeyPressed)
                {
                    if (!textm)
                    {
                        textm = true;
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

            if (_marcus == 2 || _marcus == 3 || _marcus == 4 || _marcus == 5 )
            {
                textm = true;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Enter) && !enterPressed)
            {
                if (_marcus == 1 && m1)
                {
                    _marcus = 2;
                    m1 = false;
                }
                if (_marcus == 2 && m2)
                {
                    _marcus = 3;
                    m1 = false;
                }
                if (_marcus == 3 && m3)
                {
                    _marcus = 4;
                    m3 = false;
                }
                if (_marcus == 4 && m4)
                {
                    _marcus = 5;
                    m4 = false;
                }
                if (_marcus == 5 && m5)
                {
                    _marcus = 6;
                    m5 = false;
                }

                textpopup = false;
                game.booktext = false;
                game.marcustext = false;
                textm = false;
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

            string book, marcustext_1, marcustext_2, marcustext_3, marcustext_4, marcustext_5;
            book = "Podium";
            marcustext_1 = "Mary \n\nMarcus, have you heard anything about Vanessa yet.";
            marcustext_2 = "Marcus \n\nNo, there's been no news of her at all.";
            marcustext_3 = "Mary \n\nWill she be safe?";
            marcustext_4 = "Marcus \n\nOf course, she will be safe.\nMayGod protected her.";
            marcustext_5 = "Mary \n\nI hope so too.";

            if (textpopup == true && game.booktext == true)
            {
                theBatch.Draw(textbox, new Vector2(0, game.graphics.PreferredBackBufferHeight - textbox.Height), new Rectangle(0,0,638,197), Color.White);
                theBatch.DrawString(font, book, new Vector2(50, 725), Color.White);
            }
            else if (textm == true && _marcus == 1 && game.marcustext == true)
            {
                theBatch.Draw(textbox, new Vector2(0, game.graphics.PreferredBackBufferHeight - textbox.Height), new Rectangle(0, 0, 638, 197), Color.White);
                theBatch.DrawString(font, marcustext_1, new Vector2(50, 725), Color.White);
                m1 = true;
            }
            else if (textm == true && _marcus == 2)
            {
                theBatch.Draw(textbox, new Vector2(0, game.graphics.PreferredBackBufferHeight - textbox.Height), new Rectangle(0, 0, 638, 197), Color.White);
                theBatch.DrawString(font, marcustext_2, new Vector2(50, 725), Color.White);
                theBatch.Draw(marcus, new Vector2(game.graphics.PreferredBackBufferWidth - marcus.Width -20, game.graphics.PreferredBackBufferHeight - marcus.Height -20), Color.White);
                m2 = true;
            }
            else if (textm == true && _marcus == 3)
            {
                theBatch.Draw(textbox, new Vector2(0, game.graphics.PreferredBackBufferHeight - textbox.Height), new Rectangle(0, 0, 638, 197), Color.White);
                theBatch.DrawString(font, marcustext_3, new Vector2(50, 725), Color.White);
                m3 = true;
            }
            else if (textm == true && _marcus == 4)
            {
                theBatch.Draw(textbox, new Vector2(0, game.graphics.PreferredBackBufferHeight - textbox.Height), new Rectangle(0, 0, 638, 197), Color.White);
                theBatch.DrawString(font, marcustext_4, new Vector2(50, 725), Color.White);
                theBatch.Draw(marcus, new Vector2(game.graphics.PreferredBackBufferWidth - marcus.Width -20, game.graphics.PreferredBackBufferHeight - marcus.Height - 20), Color.White);
                m4 = true;
            }
            else if (textm == true && _marcus == 5)
            {
                theBatch.Draw(textbox, new Vector2(0, game.graphics.PreferredBackBufferHeight - textbox.Height), new Rectangle(0, 0, 638, 197), Color.White);
                theBatch.DrawString(font, marcustext_5, new Vector2(50, 725), Color.White);
                m5 = true;
            }

            base.Draw(theBatch);
        }


    }
}
