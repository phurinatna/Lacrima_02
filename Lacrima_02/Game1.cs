using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Tiled.Renderers;
using MonoGame.Extended.Tiled;
using MonoGame.Extended.Screens;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;
using System;

namespace Lacrima_02
{
    public class Game1 : Game
    {
        public GraphicsDeviceManager graphics;
        public SpriteBatch spriteBatch;

        public ChuchScreen mChuchScreen;
        public TitleScreen mTitleScreen;
        public LevelScreen mLevelScreen;
        public Hall1Screen mHall1Screen;
        public Hall2Screen mHall2Screen;
        public PoperoomScreen mPoperoomScreen;
        public BedroomScreen mBedroomScreen;
        public KitchenScreen mKitchenScreen;
        public EndScreen mEndScreen;
        public screen mCurrentScreen;

        public int MapWidth = 1280;
        public int MapHeight = 720;

        public bool booktext = false;
        public bool roomtext = false;
        public bool bedtext = false;
        public bool bookshelf = false;
        public bool window = false;
        public bool tablevanessa = false;
        public bool bedvanessa = false;
        public bool jacobtext = false;
        public bool marcustext = false;
        public bool miriamtext = false;
        public bool bin = false;


        public bool level_2 = false;
        public bool level_3 = false;

        public bool hall1 = false;
        public bool hall2 = false;
        public bool poperoom = false;
        public bool bedroom = false;
        public bool kitchen = false;
        public bool chuch = false;

        public int state = 1;
        public bool state16 = false;

        Song song;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            graphics.PreferredBackBufferWidth = MapWidth;
            graphics.PreferredBackBufferHeight = MapHeight;
            graphics.ApplyChanges();
            GraphicsDevice.BlendState = BlendState.AlphaBlend;


            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            mChuchScreen = new ChuchScreen(this, new EventHandler(GameplayScreenEvent));
            mTitleScreen = new TitleScreen(this, new EventHandler(GameplayScreenEvent));
            mLevelScreen = new LevelScreen(this, new EventHandler(GameplayScreenEvent));
            mHall1Screen = new Hall1Screen(this, new EventHandler(GameplayScreenEvent));
            mHall2Screen = new Hall2Screen(this, new EventHandler(GameplayScreenEvent));
            mPoperoomScreen = new PoperoomScreen(this, new EventHandler(GameplayScreenEvent));
            mBedroomScreen = new BedroomScreen(this, new EventHandler(GameplayScreenEvent));
            mKitchenScreen = new KitchenScreen(this, new EventHandler(GameplayScreenEvent));
            mEndScreen = new EndScreen(this, new EventHandler(GameplayScreenEvent));
            mCurrentScreen = mTitleScreen;

            this.song = Content.Load<Song>("Resources\\bgm");
            MediaPlayer.Play(song);
            // Uncomment the following line will also loop the song
            MediaPlayer.IsRepeating = true;
            MediaPlayer.Volume = 0.1f;
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

           
            mCurrentScreen.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin();
            mCurrentScreen.Draw(spriteBatch);
            spriteBatch.End();

            base.Draw(gameTime);
        }

        public void GameplayScreenEvent(object obj, EventArgs e)
        {
            mCurrentScreen = (screen)obj;

            if (mCurrentScreen is TitleScreen)
            {
                MapHeight = 1280;
                MapHeight = 720;
            }
            else if (mCurrentScreen is ChuchScreen)
            {
                MapWidth = 640;
                MapHeight = 896;
            }
            else if (mCurrentScreen is LevelScreen)
            {
                MapWidth = 1280;
                MapHeight = 720;
            }
            else if (mCurrentScreen is Hall1Screen)
            {
                MapWidth = 320;
                MapHeight = 992;
            }
            else if (mCurrentScreen is PoperoomScreen)
            {
                MapWidth = 640;
                MapHeight = 704;
            }
            else if (mCurrentScreen is Hall2Screen)
            {
                MapWidth = 928;
                MapHeight = 352;
            }
            else if (mCurrentScreen is BedroomScreen)
            {
                MapWidth = 832;
                MapHeight = 640;
            }
            else if (mCurrentScreen is KitchenScreen)
            {
                MapWidth = 800;
                MapHeight = 640;
            }
            else if (mCurrentScreen is EndScreen)
            {
                MapWidth = 1280;
                MapHeight = 720;
            }

            graphics.PreferredBackBufferWidth = MapWidth;
            graphics.PreferredBackBufferHeight = MapHeight;
            graphics.ApplyChanges();

            
        }

        public int GetMapWidth()
        {
            return MapWidth;
        }

        public int GetMapHeight()
        {
            return MapHeight;
        }
    }
}
