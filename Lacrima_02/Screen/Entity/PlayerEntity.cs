using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using MonoGame.Extended;
using MonoGame.Extended.Collisions;

namespace susu
{
    internal class PlayerEntity : IEntity
    {
        private readonly Game1 game;

        public int Velocity = 4;
        Vector2 move;
        public IShapeF Bounds { get; }

        private KeyboardState _currentKey;
        private KeyboardState _oldKey;

        public PlayerEntity(Game1 game, IShapeF circleF)
        {
            this.game = game;
            Bounds = circleF;
        }

        public virtual void Update(GameTime gameTime)
        {
            _currentKey = Keyboard.GetState();

            if (_currentKey.IsKeyDown(Keys.D) && Bounds.Position.X < 640 - ((RectangleF)Bounds).Width)
            {
                move = new Vector2(Velocity, 0) * gameTime.GetElapsedSeconds() * 50;
                Bounds.Position += move;
            }
            else if (_currentKey.IsKeyDown(Keys.A) && Bounds.Position.X > 0)
            {
                move = new Vector2(-Velocity, 0) * gameTime.GetElapsedSeconds() * 50;
                Bounds.Position += move;
            }

            if (_currentKey.IsKeyDown(Keys.W) && Bounds.Position.Y > 0)
            {
                move = new Vector2(0, -Velocity) * gameTime.GetElapsedSeconds() * 50;
                Bounds.Position += move;
            }
            else if (_currentKey.IsKeyDown(Keys.S) && Bounds.Position.Y < 800 - ((RectangleF)Bounds).Width)
            {
                move = new Vector2(0, +Velocity) * gameTime.GetElapsedSeconds() * 50;
                Bounds.Position += move;
            }
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawRectangle((RectangleF)Bounds, Color.Red, 3f);
        }
        public void OnCollision(CollisionEventArgs collisionInfo)
        {
            if (collisionInfo.Other.ToString().Contains("WallEntity"))
            {
                Bounds.Position -= collisionInfo.PenetrationVector;
            }
        }
    }
}
