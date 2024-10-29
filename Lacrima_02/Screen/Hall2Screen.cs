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
    public class Hall2Screen : screen
    {
        Game1 game;
        TiledMap _tiledMap;
        TiledMapRenderer _tiledMapRenderer;

        private readonly List<IEntity> _entities = new List<IEntity>();
        public readonly CollisionComponent _collisionComponent;

        TiledMapObjectLayer _wallTiledObj, _door6TiledObj, _door7TiledObj, _door11TiledObj;

        Texture2D textbox, jacob;
        bool textpopup,textch, ch8_end, ch9_end, ch10_end, ch11_end, ch12_end, ch13_end, ch14_end, ch15_end;
        SpriteFont font;

        public Hall2Screen(Game1 game, EventHandler theScreenEvent) : base(theScreenEvent)
        {
            this.game = game;
            _collisionComponent = new CollisionComponent(new RectangleF(0, 0, 928, 352));

            //Load tilemap 
            _tiledMap = game.Content.Load<TiledMap>("Resources\\hall2");
            _tiledMapRenderer = new TiledMapRenderer(game.GraphicsDevice, _tiledMap);

            //Get object layers
            foreach (TiledMapObjectLayer layer in _tiledMap.ObjectLayers)
            {
                if (layer.Name == "Wall_Object")
                {
                    _wallTiledObj = layer;
                }

                if (layer.Name == "Door_6")
                {
                    _door6TiledObj = layer;
                }

                if (layer.Name == "Door_7")
                {
                    _door7TiledObj = layer;
                }

                if (layer.Name == "Door_11")
                {
                    _door11TiledObj = layer;
                }
            }

            //Create entities from map 
            foreach (TiledMapObject obj in _wallTiledObj.Objects)
            {
                Point2 position = new Point2(obj.Position.X, obj.Position.Y);
                _entities.Add(new WallEntity(this.game, new RectangleF(position, obj.Size)));
            }
            foreach (TiledMapObject obj in _door6TiledObj.Objects)
            {
                Point2 position = new Point2(obj.Position.X, obj.Position.Y);
                _entities.Add(new Door6Entity(this.game, new RectangleF(position, obj.Size)));
            }
            foreach (TiledMapObject obj in _door7TiledObj.Objects)
            {
                Point2 position = new Point2(obj.Position.X, obj.Position.Y);
                _entities.Add(new Door7Entity(this.game, new RectangleF(position, obj.Size)));
            }
            foreach (TiledMapObject obj in _door11TiledObj.Objects)
            {
                Point2 position = new Point2(obj.Position.X, obj.Position.Y);
                _entities.Add(new Door11Entity(this.game, new RectangleF(position, obj.Size)));
            }

            //Setup player 
            SpriteSheet playerSheet = game.Content.Load<SpriteSheet>("Resources\\Mary_Animation.sf", new JsonContentLoader());
            _entities.Add(new PlayerEntity(this.game, new RectangleF(new Point2((32 * 20)+8, 32 * 5), new Size2(48, 48)), new AnimatedSprite(playerSheet)));

            
            //Setup ai 
            SpriteSheet jacobSheet = game.Content.Load<SpriteSheet>("Resources\\Jacob_Animation.sf", new JsonContentLoader());
            _entities.Add(new JacobEntity(this.game, new RectangleF(new Point2((32 * 9) + 8, 32 * 5), new Size2(48, 48)), new AnimatedSprite(jacobSheet)));
            

            foreach (IEntity entity in _entities)
            {
                _collisionComponent.Insert(entity);
            }

            textbox = game.Content.Load<Texture2D>("Resources\\Ui\\textboxhall2");
            font = game.Content.Load<SpriteFont>("Resources\\ArialFont");
            jacob = game.Content.Load<Texture2D>("Resources\\Jacob_1");
        }

        private bool enterPressed = false;
        private bool fKeyPressed = false;
        public override void Update(GameTime theTime)
        {
            Console.WriteLine($"Current game.state: {game.state}");

            if (game.hall1 == true)
            {
                ScreenEvent.Invoke(game.mHall1Screen, new EventArgs());
                if (game.state16 == true)
                {
                    game.state = 16;
                    if (game.state == 16)
                    {
                        foreach (var entity in _entities.Where(e => e is JacobEntity).ToList())
                        {
                            _collisionComponent.Remove(entity);
                        }
                        _entities.RemoveAll(entity => entity is JacobEntity);
                    }
                }
                return;
            }

            if (game.bedroom == true)
            {
                ScreenEvent.Invoke(game.mBedroomScreen, new EventArgs());
                return;
            }


            if ((game.roomtext == true) && Keyboard.GetState().IsKeyDown(Keys.F))
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

            if (game.state == 8 || game.state == 10 || game.state == 11 || game.state == 12 || game.state == 13 || game.state == 14 || game.state == 15)
            {
                textch = true;
            }

            if ((game.state == 9 && game.jacobtext == true) && Keyboard.GetState().IsKeyDown(Keys.F))
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
            

            if (Keyboard.GetState().IsKeyDown(Keys.Enter) && !enterPressed)
            {
                if (game.state == 8 && ch8_end)
                {
                    game.state = 9;
                    ch8_end = false;
                }
                else if (game.state == 9 && ch9_end)
                {
                    game.state = 10;
                    ch9_end = false;
                    game.jacobtext = false;
                }
                else if (game.state == 10 && ch10_end)
                {
                    game.state = 11;
                    ch10_end = false;
                }
                else if (game.state == 11 && ch11_end)
                {
                    game.state = 12;
                    ch11_end = false;
                }
                else if (game.state == 12 && ch12_end)
                {
                    game.state = 13;
                    ch12_end = false;
                }
                else if (game.state == 13 && ch13_end)
                {
                    game.state = 14;
                    ch13_end = false;
                }
                else if (game.state == 14 && ch14_end)
                {
                    game.state = 15;
                    ch14_end = false;
                }
                else if (game.state == 15 && ch15_end)
                {
                    game.state = 9;
                    ch15_end = false;
                    game.jacobtext = false;
                    game.state16 = true;
                }

                textpopup = false;
                game.roomtext = false;
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

            string str, ch1_8, ch1_9, ch1_10, ch1_11, ch1_12, ch1_13, ch1_14, ch1_15;
            str = "Lock.";
            ch1_8 = "Jacob \n\nMary.";
            ch1_9 = "Jacob \n\nThank heavens, you finally come out of your room.";
            ch1_10 = "Jacob \n\nI . . . I was worried sick.";
            ch1_11 = "Jacob \n\nI was worried sick that you would cry your heart out in there. \nIt's good that you are still able to overcome that feeling.";
            ch1_12 = "Jacob \n\nI'm terribly sorry that I can't be there for you, my child. \nI'm really sorry.";
            ch1_13 = "Jacob \n\nYou must be on your way for breakfast, I don't want to disturb you anymore.";
            ch1_14 = "Jacob \n\nEnjoy your meal. God bless you.";
            ch1_15 = "Mary \n\nGod bless you, Father Jacob.";

            if (game.roomtext == true && textpopup == true)
            {
                theBatch.Draw(textbox, new Vector2(0, game.graphics.PreferredBackBufferHeight - textbox.Height), new Rectangle(0, 0, 935, 102), Color.White);
                theBatch.DrawString(font, str, new Vector2(20, 270), Color.White);
            }

            if (game.state == 8 && textch == true)
            {
                theBatch.Draw(textbox, new Vector2(0, game.graphics.PreferredBackBufferHeight - textbox.Height), new Rectangle(0, 0, 935, 102), Color.White);
                theBatch.DrawString(font, ch1_8, new Vector2(20, 270), Color.White);
                theBatch.Draw(jacob, new Vector2(game.graphics.PreferredBackBufferWidth - jacob.Width-10, game.graphics.PreferredBackBufferHeight - jacob.Height - 10), Color.White);
                ch8_end = true;
            }
            else if (game.state == 9 && textch == true)
            {
                theBatch.Draw(textbox, new Vector2(0, game.graphics.PreferredBackBufferHeight - textbox.Height), new Rectangle(0, 0, 935, 102), Color.White);
                theBatch.DrawString(font, ch1_9, new Vector2(20, 270), Color.White);
                theBatch.Draw(jacob, new Vector2(game.graphics.PreferredBackBufferWidth - jacob.Width - 10, game.graphics.PreferredBackBufferHeight - jacob.Height - 10), Color.White);
                ch9_end = true;
            }
            else if (game.state == 10 && textch == true)
            {
                theBatch.Draw(textbox, new Vector2(0, game.graphics.PreferredBackBufferHeight - textbox.Height), new Rectangle(0, 0, 935, 102), Color.White);
                theBatch.DrawString(font, ch1_10, new Vector2(20, 270), Color.White);
                theBatch.Draw(jacob, new Vector2(game.graphics.PreferredBackBufferWidth - jacob.Width - 10, game.graphics.PreferredBackBufferHeight - jacob.Height - 10), Color.White);
                ch10_end = true;
            }
            else if (game.state == 11 && textch == true)
            {
                theBatch.Draw(textbox, new Vector2(0, game.graphics.PreferredBackBufferHeight - textbox.Height), new Rectangle(0, 0, 935, 102), Color.White);
                theBatch.DrawString(font, ch1_11, new Vector2(20, 270), Color.White);
                theBatch.Draw(jacob, new Vector2(game.graphics.PreferredBackBufferWidth - jacob.Width - 10, game.graphics.PreferredBackBufferHeight - jacob.Height - 10), Color.White);
                ch11_end = true;
            }
            else if (game.state == 12 && textch == true)
            {
                theBatch.Draw(textbox, new Vector2(0, game.graphics.PreferredBackBufferHeight - textbox.Height), new Rectangle(0, 0, 935, 102), Color.White);
                theBatch.DrawString(font, ch1_12, new Vector2(20, 270), Color.White);
                theBatch.Draw(jacob, new Vector2(game.graphics.PreferredBackBufferWidth - jacob.Width - 10, game.graphics.PreferredBackBufferHeight - jacob.Height - 10), Color.White);
                ch12_end = true;
            }
            else if (game.state == 13 && textch == true)
            {
                theBatch.Draw(textbox, new Vector2(0, game.graphics.PreferredBackBufferHeight - textbox.Height), new Rectangle(0, 0, 935, 102), Color.White);
                theBatch.DrawString(font, ch1_13, new Vector2(20, 270), Color.White);
                theBatch.Draw(jacob, new Vector2(game.graphics.PreferredBackBufferWidth - jacob.Width - 10, game.graphics.PreferredBackBufferHeight - jacob.Height - 10), Color.White);
                ch13_end = true;
            }
            else if (game.state == 14 && textch == true)
            {
                theBatch.Draw(textbox, new Vector2(0, game.graphics.PreferredBackBufferHeight - textbox.Height), new Rectangle(0, 0, 935, 102), Color.White);
                theBatch.DrawString(font, ch1_14, new Vector2(20, 270), Color.White);
                theBatch.Draw(jacob, new Vector2(game.graphics.PreferredBackBufferWidth - jacob.Width - 10, game.graphics.PreferredBackBufferHeight - jacob.Height - 10), Color.White);
                ch14_end = true;
            }
            else if (game.state == 15 && textch == true)
            {
                theBatch.Draw(textbox, new Vector2(0, game.graphics.PreferredBackBufferHeight - textbox.Height), new Rectangle(0, 0, 935, 102), Color.White);
                theBatch.DrawString(font, ch1_15, new Vector2(20, 270), Color.White);
                ch15_end = true;
            }

            base.Draw(theBatch);
        }
    }
}
