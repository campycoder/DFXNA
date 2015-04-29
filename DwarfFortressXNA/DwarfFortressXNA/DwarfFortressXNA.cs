using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.IO;

namespace DwarfFortressXNA
{

    public enum GameState
    {
        INTRO,
        MENU,
        WORLDGEN,
        PLAYING
    }
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    /// 
    public class DwarfFortressMono : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Texture2D font;
        public static Random random;

        public bool paused = false;
        public bool pdebounce = false;

        int selection = 0;
        bool arrowDeb = false;

        public static FontManager fontManager;
        public static LanguageManager languageManager;
        public static MaterialManager materialManager;
        public static SoundManager soundManager;
        public static ConfigManager configManager;


        public GameState gameState = GameState.MENU;

        public static int ROWS = 25;
        public static int COLS = 80;

        public static int MAP_HEIGHT = ROWS * 2;
        public static int MAP_WIDTH = COLS * 2;

        public int cursorX = 1;
        public int cursorY = 1;

        public int cursorBlinkTimer = 20;
        public bool cursorOn = true;

        public Material[,] materialMap = new Material[MAP_WIDTH,MAP_HEIGHT];

        int frames = 0;
        float elapsed = 0;
        int fps = 0;
        string randomName;
        public DwarfFortressMono()
            : base()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            fontManager = new FontManager();
            languageManager = new LanguageManager();
            materialManager = new MaterialManager();
            soundManager = new SoundManager();
            configManager = new ConfigManager();
            configManager.LoadConfigFiles();
            COLS = configManager.GetConfigValueAsInt("WINDOWEDX");
            ROWS = configManager.GetConfigValueAsInt("WINDOWEDY");
            soundManager.OnLoad(this.Content);
            this.Window.AllowUserResizing = true;
            this.Window.ClientSizeChanged += new EventHandler<EventArgs>(HandleResize);
            random = new Random();
            graphics.PreferredBackBufferHeight = (ROWS * 12);
            graphics.PreferredBackBufferWidth = (COLS * 8);
            this.TargetElapsedTime = new TimeSpan(0, 0, 0, 0, 10);
            this.Window.Title = "Dwarf Fortress";
            RawFile rawFile = new RawFile("./Raw/Objects/language_words.txt");
            RawFile rawFileH = new RawFile("./Raw/Objects/language_HUMAN.txt");
            RawFile rawFileD = new RawFile("./Raw/Objects/language_DWARF.txt");
            RawFile rawFileG = new RawFile("./Raw/Objects/language_GOBLIN.txt");
            RawFile rawFileS = new RawFile("./Raw/Objects/language_SYM.txt");
            RawFile rawFileMT = new RawFile("./Raw/Objects/material_template_default.txt");
            RawFile rawFileIG = new RawFile("./Raw/Objects/inorganic_stone_gem.txt");
            RawFile rawFileIL = new RawFile("./Raw/Objects/inorganic_stone_layer.txt");
            RawFile rawFileIM = new RawFile("./Raw/Objects/inorganic_stone_mineral.txt");
            RawFile rawFileIS = new RawFile("./Raw/Objects/inorganic_stone_soil.txt");
            RawFile rawFileIT = new RawFile("./Raw/Objects/inorganic_metal.txt");
            List<Material> materials = new List<Material>(materialManager.inorganicMaterialList.Values);
            for (int x = 0; x < MAP_WIDTH; x++)
            {
                for(int y = 0; y < MAP_HEIGHT;y++)
                {
                    Material material = materials[random.Next(materials.Count)];
                    materialMap[x, y] = material;
                }
            }
        }

        public void HandleResize(object sender, EventArgs e)
        {
            COLS = (int)Math.Floor((double)this.Window.ClientBounds.Width / 8);
            ROWS = (int)Math.Floor((double)this.Window.ClientBounds.Height / 12);
            if(configManager.GetConfigValueAsBool("BLACK_SPACE"))
            {
                graphics.PreferredBackBufferHeight = ROWS * 12;
                graphics.PreferredBackBufferWidth = COLS * 8;
                graphics.ApplyChanges();
                if (cursorX > COLS - 2) cursorX = COLS - 2;
                if (cursorX < 1) cursorX = 1;
                if (cursorY > ROWS - 2) cursorY = ROWS - 2;
                if (cursorY < 1) cursorY = 1;
            }  
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            //var form = (System.Windows.Forms.Form)System.Windows.Forms.Control.FromHandle(this.Window.Handle);
            //form.Location = new System.Drawing.Point(0, 0);
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            //font = this.Content.Load<Texture2D>("./Data/curses_640x300");
            font = Texture2D.FromStream(GraphicsDevice, new FileStream("./Data/curses_640x300.png", FileMode.Open));
            soundManager.PlaySong("TITLE_SONG");
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
            soundManager.StopCurrentSong();
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
           
            elapsed += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            if(elapsed >= 1000.0f)
            {
                fps = frames;
                frames = 0;
                elapsed = 0.0f;
            }

            

            // TODO: Add your update logic here
            if(gameState == GameState.PLAYING)
            {
                if (Keyboard.GetState().IsKeyDown(Keys.Space) && !pdebounce)
                {
                    paused = !paused;
                    pdebounce = true;
                }

                if (Keyboard.GetState().IsKeyUp(Keys.Space) && Keyboard.GetState().IsKeyUp(Keys.G) && pdebounce)
                {
                    pdebounce = false;
                }
                if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape)) GameStateChange(GameState.MENU);

                if(Keyboard.GetState().IsKeyDown(Keys.G) && !pdebounce)
                {
                    List<Material> materials = new List<Material>(materialManager.inorganicMaterialList.Values);
                    for (int x = 0; x < MAP_WIDTH; x++)
                    {
                        for (int y = 0; y < MAP_HEIGHT; y++)
                        {
                            Material material = materials[random.Next(materials.Count)];
                            materialMap[x, y] = material;
                        }
                    }
                    pdebounce = true;
                }

                if (cursorOn) cursorBlinkTimer--;
                else cursorBlinkTimer++;
                if (cursorBlinkTimer <= 0 || cursorBlinkTimer >= 20) cursorOn = !cursorOn;

                if(Keyboard.GetState().IsKeyDown(Keys.Down) && cursorY < ROWS - 2 && !arrowDeb)
                {
                    cursorY++;
                }
                if (Keyboard.GetState().IsKeyDown(Keys.Up) && cursorY > 1 && !arrowDeb)
                {
                    cursorY--;
                }
                if (Keyboard.GetState().IsKeyDown(Keys.Right) && cursorX < COLS - 2 && !arrowDeb)
                {
                    cursorX++;
                }
                if (Keyboard.GetState().IsKeyDown(Keys.Left) && cursorX > 1 && !arrowDeb)
                {
                    cursorX--;
                }
            }
            if(gameState == GameState.MENU)
            {
                if (Keyboard.GetState().IsKeyDown(Keys.Enter) && selection == 0) GameStateChange(GameState.PLAYING);
                if (Keyboard.GetState().IsKeyDown(Keys.Enter) && selection == 1) Exit();
                if (Keyboard.GetState().IsKeyDown(Keys.Up) && !arrowDeb && selection > 0)
                {
                    selection--;
                    arrowDeb = true;
                }
                if (Keyboard.GetState().IsKeyDown(Keys.Down) && !arrowDeb && selection < 1)
                {
                    selection++;
                    arrowDeb = true;
                }
                if (Keyboard.GetState().IsKeyUp(Keys.Up) && Keyboard.GetState().IsKeyUp(Keys.Down) && arrowDeb) arrowDeb = false;
            }
            base.Update(gameTime);
        }

        public void GameStateChange(GameState stateToChange)
        {
            if (stateToChange == this.gameState) return;
            soundManager.StopCurrentSong();
            if (stateToChange == GameState.MENU) soundManager.PlaySong("TITLE_SONG");
            if (stateToChange == GameState.PLAYING) soundManager.PlaySong("GAME_SONG");
            this.gameState = stateToChange;
            selection = 0;
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            frames++;
            // TODO: Add your drawing code here
            spriteBatch.Begin();

            if(gameState == GameState.PLAYING)
            {
                for (int i = 0; i < COLS; i++)
                {
                    for (int j = 0; j < ROWS; j++)
                    {
                        if (j == 0 || j == ROWS - 1 || i == 0 || i == COLS - 1) fontManager.DrawCharacter('█', spriteBatch, font, new Vector2(i, j), fontManager.dfColor.GetPairFromTriad(0, 0, 1));
                    }
                }
                for (int x = 1; x < COLS - 1; x++)
                {
                    for (int y = 1; y < ROWS - 1; y++)
                    {
                        if (x >= MAP_WIDTH || y >= MAP_HEIGHT) fontManager.DrawCharacter(random.Next(2) == 1 ? '≈' : '~', spriteBatch, font, new Vector2(x, y), fontManager.dfColor.GetPairFromTriad(7, 0, 1));
                        else fontManager.DrawCharacter(materialMap[x, y].itemSymbol, spriteBatch, font, new Vector2(x, y), materialMap[x, y].GetItemDisplayColor());
                    }
                }
                if (cursorOn) fontManager.DrawCharacter('X', spriteBatch, font, new Vector2(cursorX, cursorY), fontManager.dfColor.GetPairFromTriad(6, 0, 1));
                fontManager.DrawString("  Dwarf Fortress  ", spriteBatch, font, new Vector2(COLS/2 - 9, 0), fontManager.dfColor.GetPairFromTriad(0, 7, 0));
                if (paused) fontManager.DrawString("*PAUSED*", spriteBatch, font, new Vector2(1, 0), fontManager.dfColor.GetPairFromTriad(3, 2, 1));
                if(configManager.GetConfigValueAsBool("FPS")) fontManager.DrawString("FPS: " + fps, spriteBatch, font, new Vector2(COLS -  (COLS/4), 0), fontManager.dfColor.GetPairFromTriad(2, 2, 1));
            }
            else if(gameState == GameState.MENU)
            {
                fontManager.DrawString("Slaves to Armok:  God of Blood", spriteBatch, font, new Vector2(COLS/2 - 15, 2), fontManager.dfColor.GetPairFromTriad(4, 0, 1));
                fontManager.DrawString("Chapter II: Dwarf Fortress", spriteBatch, font, new Vector2(COLS/2 - 12, 3), fontManager.dfColor.GetPairFromTriad(7, 0, 1));
                fontManager.DrawString("(XNA EDITION)", spriteBatch, font, new Vector2(COLS/2 - 6, 5), fontManager.dfColor.GetPairFromTriad(6, 6, 1));
                fontManager.DrawString("Look at Something", spriteBatch, font, new Vector2(COLS/2 - 8, 10), fontManager.dfColor.GetPairFromTriad(7, 0, 1 - selection));
                fontManager.DrawString("Exit", spriteBatch, font, new Vector2(COLS/2 - 1, 12), fontManager.dfColor.GetPairFromTriad(7, 0, selection));
            }
            
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
