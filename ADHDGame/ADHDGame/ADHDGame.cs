using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace ADHDGame
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class ADHDGame : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Screen currentScreen;
        RobotActions r = RobotActions.Instance;
        public GameplayScreen GameScreen { get { return (GameplayScreen)currentScreen; } }
        public TitleScreen titleScreen { get { return (TitleScreen)currentScreen;}}

        public ADHDGame()
        {
            r.setADHDGame(this);
            Content.RootDirectory = "Content";
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 1024;
            graphics.PreferredBackBufferHeight = 768;
            IsFixedTimeStep = true;
            TargetElapsedTime = TimeSpan.FromSeconds(1.0 / 60.0);
            this.IsMouseVisible = true;
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            currentScreen = new TitleScreen(this, spriteBatch);
            currentScreen.Load();
        }

        protected override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            currentScreen.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            currentScreen.Draw(gameTime);
            base.Draw(gameTime);
        }

        public void LoadScreen(Screen s)
        {
            //out with the old, in with the new
            currentScreen.Unload();
            currentScreen = s;
            currentScreen.Load();
        }
    }
}
