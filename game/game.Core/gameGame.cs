using System;
using System.Collections.Generic;
using System.Globalization;
using game.Core.Localization;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using static System.Net.Mime.MediaTypeNames;
using game.Core;

namespace game.Core
{
    /// <summary>
    /// The main class for the game, responsible for managing game components, settings, 
    /// and platform-specific configurations.
    /// </summary>
    public class gameGame : Game
    {
        // Resources for drawing.
        private GraphicsDeviceManager graphics;
        private Movement movement;
        SpriteFont font;
        World world;
        Texture2D grass;
       

        /// <summary>
        /// Indicates if the game is running on a mobile platform.
        /// </summary>
        public readonly static bool IsMobile = OperatingSystem.IsAndroid() || OperatingSystem.IsIOS();

        /// <summary>
        /// Indicates if the game is running on a desktop platform.
        /// </summary>
        public readonly static bool IsDesktop = OperatingSystem.IsMacOS() || OperatingSystem.IsLinux() || OperatingSystem.IsWindows();

        /// <summary>
        /// Initializes a new instance of the game. Configures platform-specific settings, 
        /// initializes services like settings and leaderboard managers, and sets up the 
        /// screen manager for screen transitions.
        /// </summary>
        public gameGame()
        {
            graphics = new GraphicsDeviceManager(this);
            var screen = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode;

            // Share GraphicsDeviceManager as a service.
            Services.AddService(typeof(GraphicsDeviceManager), graphics);

            Content.RootDirectory = "Content";

            // Configure screen orientations.
            graphics.SupportedOrientations = DisplayOrientation.LandscapeLeft | DisplayOrientation.LandscapeRight;


            if (IsDesktop) {
                IsMouseVisible = true;
            }
            else { IsMouseVisible = false; 
            }
            int Witdh = screen.Width;
            int height = screen.Height;

            graphics.PreferredBackBufferWidth = Witdh;
            graphics.PreferredBackBufferHeight = height;
            graphics.IsFullScreen = false;
        }

        public void toggleFullscreen() {

            var adapter = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode;


            graphics.IsFullScreen = !graphics.IsFullScreen;

            if (!graphics.IsFullScreen)
            {
                graphics.PreferredBackBufferWidth = (int)Math.Round(adapter.Width / 1.4);
                graphics.PreferredBackBufferHeight = (int)Math.Round(adapter.Height / 1.4);
            }
            else { 
                graphics.PreferredBackBufferWidth = adapter.Width;
                graphics.PreferredBackBufferHeight = adapter.Height;
            }

                graphics.ApplyChanges();
        }
       
        /// <summary>
        /// Initializes the game, including setting up localization and adding the 
        /// initial screens to the ScreenManager.
        /// </summary>
        protected override void Initialize()
        {
            base.Initialize();

            // Load supported languages and set the default language.
            List<CultureInfo> cultures = LocalizationManager.GetSupportedCultures();
            var languages = new List<CultureInfo>();
            for (int i = 0; i < cultures.Count; i++)
            {
                languages.Add(cultures[i]);
            }

            // TODO You should load this from a settings file or similar,
            // based on what the user or operating system selected.
            var selectedLanguage = LocalizationManager.DEFAULT_CULTURE_CODE;
            LocalizationManager.SetCulture(selectedLanguage);
        }

        /// <summary>
        /// Loads game content, such as textures and particle systems.
        /// </summary>
        protected override void LoadContent()
        {
            base.LoadContent();
            movement = new Movement(GraphicsDevice);
            font = Content.Load<SpriteFont>("DefaultFont");
            movement.LoadContent(font);

            grass = new Texture2D(GraphicsDevice, 1, 1);
            grass = Content.Load<Texture2D>("tiles/grass");
            world = new World(500, grass, GraphicsDevice);
        }

        /// <summary>
        /// Updates the game's logic, called once per frame.
        /// </summary>
        /// <param name="gameTime">
        /// Provides a snapshot of timing values used for game updates.
        /// </param>
        protected override void Update(GameTime gameTime)
        {
            // Exit the game if the Back button (GamePad) or Escape key (Keyboard) is pressed.
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed
                || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            if (Keyboard.GetState().IsKeyDown(Keys.F11)) {
                toggleFullscreen();
            }

            world.Update(gameTime);
            // TODO: Add your update logic here
            movement.Update(gameTime);

            base.Update(gameTime);
        }

        /// <summary>
        /// Draws the game's graphics, called once per frame.
        /// </summary>
        /// <param name="gameTime">
        /// Provides a snapshot of timing values used for rendering.
        /// </param>
        protected override void Draw(GameTime gameTime)
        {

            GraphicsDevice.Clear(Color.White);


            // TODO: Add your drawing code here
            world.Draw();
            movement.Draw(gameTime);


            base.Draw(gameTime);
        }
    }
}