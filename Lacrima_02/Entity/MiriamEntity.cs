using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using MonoGame.Extended.Collisions;
using MonoGame.Extended.Sprites;
using System.Xml.Linq;

namespace Lacrima_02
{
    internal class MiriamEntity : IEntity
    {
        private readonly Game1 game;
        public IShapeF Bounds { get; }

        private AnimatedSprite _miriamSprite;
        string animation;

        public MiriamEntity(Game1 game, IShapeF circleF, AnimatedSprite miriamSprite)
        {
            this.game = game;
            Bounds = circleF;


            animation = "idle_up";
            miriamSprite.Play(animation);
            _miriamSprite = miriamSprite;
        }

        public virtual void Update(GameTime gameTime)
        {
            if (game.miriamtext == true)
            {
                animation = "idle_down";
            }
            else
            {
                animation = "idle_up";
            }

            _miriamSprite.Play(animation);
            _miriamSprite.Update((float)gameTime.ElapsedGameTime.TotalSeconds);
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            //spriteBatch.DrawRectangle((RectangleF)Bounds, Color.Red, 3f);
            spriteBatch.Draw(_miriamSprite, ((RectangleF)Bounds).Center);
        }
        public void OnCollision(CollisionEventArgs collisionInfo)
        {
        }
    }
}
