using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using MonoGame.Extended.Collisions;
using MonoGame.Extended.Sprites;
using System.Xml.Linq;

namespace Lacrima_02
{
    internal class MarcusEntity : IEntity
    {
        private readonly Game1 game;
        public IShapeF Bounds { get; }

        private AnimatedSprite _marcusSprite;
        string animation;

        public MarcusEntity(Game1 game, IShapeF circleF, AnimatedSprite marcusSprite)
        {
            this.game = game;
            Bounds = circleF;


            animation = "idle_up";
            marcusSprite.Play(animation);
            _marcusSprite = marcusSprite;
        }

        public virtual void Update(GameTime gameTime)
        {

            if(game.marcustext == true)
            {
                animation = "idle_down";
            }
            else
            {
                animation = "idle_up";
            }

            _marcusSprite.Play(animation);
            _marcusSprite.Update((float)gameTime.ElapsedGameTime.TotalSeconds);
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            //spriteBatch.DrawRectangle((RectangleF)Bounds, Color.Red, 3f);
            spriteBatch.Draw(_marcusSprite, ((RectangleF)Bounds).Center);
        }
        public void OnCollision(CollisionEventArgs collisionInfo)
        {
        }
    }
}
