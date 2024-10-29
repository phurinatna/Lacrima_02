using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using MonoGame.Extended.Collisions;
using System.Xml.Linq;

namespace Lacrima_02
{
    internal class Door4Entity : IEntity
    {
        private readonly Game1 game;
        public IShapeF Bounds { get; }

        public Door4Entity(Game1 game, RectangleF rectangleF)
        {
            this.game = game;
            Bounds = rectangleF;
        }

        public virtual void Update(GameTime gameTime)
        {
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            //spriteBatch.DrawRectangle((RectangleF)Bounds, Color.Red, 3);
        }
        public void OnCollision(CollisionEventArgs collisionInfo)
        {

        }
    }
}
