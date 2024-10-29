using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using MonoGame.Extended;
using MonoGame.Extended.Collisions;
using MonoGame.Extended.Sprites;

namespace Lacrima_02
{
    internal class PlayerEntity : IEntity
    {
        private readonly Game1 game;

        public int Velocity = 4;
        Vector2 move;
        public IShapeF Bounds { get; }

        private KeyboardState _currentKey;
        private KeyboardState _oldKey;
        bool isMoved = false;

        private AnimatedSprite _playerSprite;
        string animation;

        public PlayerEntity(Game1 game, IShapeF circleF, AnimatedSprite playerSprite)
        {
            this.game = game;
            Bounds = circleF;
            

            animation = "idle_down";
            playerSprite.Play(animation);
            _playerSprite = playerSprite;
        }

        public virtual void Update(GameTime gameTime)
        {
            _currentKey = Keyboard.GetState();
            

            if (_currentKey.IsKeyDown(Keys.D) && Bounds.Position.X < game.MapWidth - ((RectangleF)Bounds).Width)
            {
                move = new Vector2(Velocity, 0) * gameTime.GetElapsedSeconds() * 50;
                Bounds.Position += move;
                isMoved = true;
            }
            else if(_currentKey.IsKeyUp(Keys.D) && _oldKey.IsKeyDown(Keys.D))
            {
                isMoved = false;
            }

            if (_currentKey.IsKeyDown(Keys.A) && Bounds.Position.X > 0)
            {
                move = new Vector2(-Velocity, 0) * gameTime.GetElapsedSeconds() * 50;
                Bounds.Position += move;
                isMoved = true;
            }
            else if (_currentKey.IsKeyUp(Keys.A) && _oldKey.IsKeyDown(Keys.A))
            {
                isMoved = false;
            }

            if (_currentKey.IsKeyDown(Keys.W) && Bounds.Position.Y > 0)
            {
                move = new Vector2(0, -Velocity) * gameTime.GetElapsedSeconds() * 50;
                Bounds.Position += move;
                isMoved = true;
            }
            else if (_currentKey.IsKeyUp(Keys.W) && _oldKey.IsKeyDown(Keys.W))
            {
                isMoved = false;
            }

            if (_currentKey.IsKeyDown(Keys.S) && Bounds.Position.Y < game.MapHeight - ((RectangleF)Bounds).Height)
            {
                move = new Vector2(0, +Velocity) * gameTime.GetElapsedSeconds() * 50;
                Bounds.Position += move;
                isMoved = true;
            }
            else if (_currentKey.IsKeyUp(Keys.S) && _oldKey.IsKeyDown(Keys.S))
            {
                isMoved = false;
            }

            if (isMoved == true && (_currentKey.IsKeyDown(Keys.D)))
            {
                animation = "walk_right";
            }
            else if (isMoved == true && (_currentKey.IsKeyDown(Keys.A)))
            {
                animation = "walk_left";
            }
            else if (isMoved == true && (_currentKey.IsKeyDown(Keys.W)))
            {
                animation = "walk_up";
            }
            else if (isMoved == true && (_currentKey.IsKeyDown(Keys.S)))
            {
                animation = "walk_down";
            }

            if(isMoved == false && animation == "walk_right")
            {
                animation = "idle_right";
            }
            else if(isMoved == false && animation == "walk_left")
            {
                animation = "idle_left";
            }
            else if (isMoved == false && animation == "walk_up")
            {
                animation = "idle_up";
            }
            else if (isMoved == false && animation == "walk_down")
            {
                animation = "idle_down";
            }

            _playerSprite.Play(animation);
            _playerSprite.Update((float)gameTime.ElapsedGameTime.TotalSeconds);
            _oldKey = _currentKey;
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            //spriteBatch.DrawRectangle((RectangleF)Bounds, Color.Red, 3f);
            spriteBatch.Draw(_playerSprite, ((RectangleF)Bounds).Center);
        }
        public void OnCollision(CollisionEventArgs collisionInfo)
        {
            if (collisionInfo.Other.ToString().Contains("WallEntity"))
            {
                Bounds.Position -= collisionInfo.PenetrationVector;
            }

            if (collisionInfo.Other.ToString().Contains("Door1Entity"))
            {
                game.hall1 = true;
                game.chuch = false;
                game.poperoom = false;
                game.hall2 = false;
                game.bedroom = false;
                game.kitchen = false;
                Bounds.Position -= collisionInfo.PenetrationVector;
            }

            if (collisionInfo.Other.ToString().Contains("Door2Entity"))
            {
                game.hall1 = false;
                game.chuch = false;
                game.poperoom = true;
                game.hall2 = false;
                game.bedroom = false;
                game.kitchen = false;
                Bounds.Position -= collisionInfo.PenetrationVector;
            }

            if (collisionInfo.Other.ToString().Contains("Door3Entity"))
            {
                game.hall1 = false;
                game.chuch = true;
                game.poperoom = false;
                game.hall2 = false;
                game.bedroom = false;
                game.kitchen = false;
                Bounds.Position -= collisionInfo.PenetrationVector;
            }

            if (collisionInfo.Other.ToString().Contains("Door4Entity"))
            {
                game.hall1 = false;
                game.chuch = true;
                game.poperoom = false;
                game.hall2 = false;
                game.bedroom = false;
                game.kitchen = false;
                Bounds.Position -= collisionInfo.PenetrationVector;
            }

            if (collisionInfo.Other.ToString().Contains("Door5Entity"))
            {
                game.hall1 = false;
                game.chuch = false;
                game.poperoom = false;
                game.hall2 = true;
                game.bedroom = false;
                game.kitchen = false;
                Bounds.Position -= collisionInfo.PenetrationVector;
            }

            if (collisionInfo.Other.ToString().Contains("Door6Entity"))
            {
                game.hall1 = true;
                game.chuch = false;
                game.poperoom = false;
                game.hall2 = false;
                game.bedroom = false;
                game.kitchen = false;
                Bounds.Position -= collisionInfo.PenetrationVector;
            }

            if (collisionInfo.Other.ToString().Contains("Door7Entity"))
            {
                game.hall1 = false;
                game.chuch = false;
                game.poperoom = false;
                game.hall2 = false;
                game.bedroom = true;
                game.kitchen = false;
                Bounds.Position -= collisionInfo.PenetrationVector;
            }

            if (collisionInfo.Other.ToString().Contains("Door8Entity"))
            {
                game.hall1 = false;
                game.chuch = false;
                game.poperoom = false;
                game.hall2 = true;
                game.bedroom = false;
                game.kitchen = false;
                Bounds.Position -= collisionInfo.PenetrationVector;
            }

            if (collisionInfo.Other.ToString().Contains("Door9Entity"))
            {
                game.hall1 = false;
                game.chuch = false;
                game.poperoom = false;
                game.hall2 = false;
                game.bedroom = false;
                game.kitchen = true;
                Bounds.Position -= collisionInfo.PenetrationVector;
            }

            if (collisionInfo.Other.ToString().Contains("Door10Entity"))
            {
                game.hall1 = true;
                game.chuch = false;
                game.poperoom = false;
                game.hall2 = false;
                game.bedroom = false;
                game.kitchen = false;
                Bounds.Position -= collisionInfo.PenetrationVector;
            }

            if (collisionInfo.Other.ToString().Contains("Door11Entity"))
            {
                game.roomtext = true;
                Bounds.Position -= collisionInfo.PenetrationVector;
            }

            if (collisionInfo.Other.ToString().Contains("BookEntity"))
            {
                game.booktext = true;
                Bounds.Position -= collisionInfo.PenetrationVector;
            }

            if (collisionInfo.Other.ToString().Contains("ChairEntity"))
            {
                
                Bounds.Position -= collisionInfo.PenetrationVector;
            }

            if (collisionInfo.Other.ToString().Contains("TableEntity"))
            {

                Bounds.Position -= collisionInfo.PenetrationVector;
            }

            if (collisionInfo.Other.ToString().Contains("TableVanessaEntity"))
            {
                game.tablevanessa = true;
                Bounds.Position -= collisionInfo.PenetrationVector;
            }

            if (collisionInfo.Other.ToString().Contains("BedEntity"))
            {
                game.bedtext = true;
                Bounds.Position -= collisionInfo.PenetrationVector;
            }

            if (collisionInfo.Other.ToString().Contains("BedVanessaEntity"))
            {
                game.bedvanessa = true;
                Bounds.Position -= collisionInfo.PenetrationVector;
            }

            if (collisionInfo.Other.ToString().Contains("BookshelfEntity"))
            {
                game.bookshelf = true;
                Bounds.Position -= collisionInfo.PenetrationVector;
            }

            if (collisionInfo.Other.ToString().Contains("WindowEntity"))
            {
                game.window = true;
                Bounds.Position -= collisionInfo.PenetrationVector;
            }

            if (collisionInfo.Other.ToString().Contains("JacobEntity"))
            {
                game.jacobtext = true;
                Bounds.Position -= collisionInfo.PenetrationVector;
            }

            if (collisionInfo.Other.ToString().Contains("MarcusEntity"))
            {
                game.marcustext = true;
                Bounds.Position -= collisionInfo.PenetrationVector;
            }

            if (collisionInfo.Other.ToString().Contains("MiriamEntity"))
            {
                game.miriamtext = true;
                Bounds.Position -= collisionInfo.PenetrationVector;
            }

            if (collisionInfo.Other.ToString().Contains("BinEntity"))
            {
                game.bin = true;
                Bounds.Position -= collisionInfo.PenetrationVector;
            }


        }
    }
}
