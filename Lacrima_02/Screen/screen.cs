#region Includes
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;

using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using Microsoft.Xna.Framework.Media;

using MonoGame.Extended.Tiled;
using MonoGame.Extended.Tiled.Renderers;
using MonoGame.Extended;
using MonoGame.Extended.Collisions;

#endregion

namespace Lacrima_02
{
    public class screen
    {
        protected EventHandler ScreenEvent;
        public screen(EventHandler theScreenEvent)
        {
            ScreenEvent = theScreenEvent;
        }

        public virtual void Update(GameTime theTime)
        {

        }

        public virtual void Draw(SpriteBatch theBatch)
        {

        }
    }
}
