using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using MonoGame.Extended.Collisions;
using MonoGame.Extended.Sprites;
using System.Xml.Linq;

namespace Lacrima_02
{
    internal class JacobEntity : IEntity
    {
        private readonly Game1 game;
        public IShapeF Bounds { get; }

        private AnimatedSprite _jacobSprite;
        string animation;

        public JacobEntity(Game1 game, IShapeF circleF, AnimatedSprite jacobSprite)
        {
            this.game = game;
            Bounds = circleF;


            animation = "idle_right";
            jacobSprite.Play(animation);
            _jacobSprite = jacobSprite;
        }

        public virtual void Update(GameTime gameTime)
        {
            animation = "idle_right";

            _jacobSprite.Play(animation);
            _jacobSprite.Update((float)gameTime.ElapsedGameTime.TotalSeconds);
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            //spriteBatch.DrawRectangle((RectangleF)Bounds, Color.Red, 3f);
            spriteBatch.Draw(_jacobSprite, ((RectangleF)Bounds).Center);
        }
        public void OnCollision(CollisionEventArgs collisionInfo)
        {
        }
    }
}
