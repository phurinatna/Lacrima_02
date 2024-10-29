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
    public class PoperoomScreen : screen
    {
        Game1 game;
        TiledMap _tiledMap;
        TiledMapRenderer _tiledMapRenderer;

        private readonly List<IEntity> _entities = new List<IEntity>();
        public readonly CollisionComponent _collisionComponent;

        TiledMapObjectLayer _wallTiledObj, _door4TiledObj, _tableTiledObj;

        public PoperoomScreen(Game1 game, EventHandler theScreenEvent) : base(theScreenEvent)
        {
            this.game = game;
            _collisionComponent = new CollisionComponent(new RectangleF(0, 0, 640, 704));

            //Load tilemap 
            _tiledMap = game.Content.Load<TiledMap>("Resources\\poperoom");
            _tiledMapRenderer = new TiledMapRenderer(game.GraphicsDevice, _tiledMap);

            //Get object layers
            foreach (TiledMapObjectLayer layer in _tiledMap.ObjectLayers)
            {
                if (layer.Name == "Wall_Object")
                {
                    _wallTiledObj = layer;
                }

                if (layer.Name == "Door_4")
                {
                    _door4TiledObj = layer;
                }

                if (layer.Name == "Table_Object")
                {
                    _tableTiledObj = layer;
                }
            }

            //Create entities from map 
            foreach (TiledMapObject obj in _wallTiledObj.Objects)
            {
                Point2 position = new Point2(obj.Position.X, obj.Position.Y);
                _entities.Add(new WallEntity(this.game, new RectangleF(position, obj.Size)));
            }
            foreach (TiledMapObject obj in _door4TiledObj.Objects)
            {
                Point2 position = new Point2(obj.Position.X, obj.Position.Y);
                _entities.Add(new Door4Entity(this.game, new RectangleF(position, obj.Size)));
            }
            foreach (TiledMapObject obj in _tableTiledObj.Objects)
            {
                Point2 position = new Point2(obj.Position.X, obj.Position.Y);
                _entities.Add(new TableEntity(this.game, new RectangleF(position, obj.Size)));
            }

            //Setup player 
            SpriteSheet playerSheet = game.Content.Load<SpriteSheet>("Resources\\Mary_Animation.sf", new JsonContentLoader());
            _entities.Add(new PlayerEntity(this.game, new RectangleF(new Point2(32 * 3, 32 * 17), new Size2(48, 48)), new AnimatedSprite(playerSheet)));

            foreach (IEntity entity in _entities)
            {
                _collisionComponent.Insert(entity);
            }
        }
        public override void Update(GameTime theTime)
        {
            if (game.chuch == true)
            {
                ScreenEvent.Invoke(game.mChuchScreen, new EventArgs());
                return;
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
            base.Draw(theBatch);
        }
    }
}
