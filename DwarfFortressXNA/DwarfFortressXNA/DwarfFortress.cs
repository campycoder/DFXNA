using DwarfFortressXNA.Managers;
using DwarfFortressXNA.Objects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
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
        FONTTEST,
        PLAYING
    }
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    /// 
    public class DwarfFortress : Game
    {
        readonly GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Texture2D font;
        public static Random Random;

        public static bool Paused = false;
        public static bool BoxLocked = false;
        public bool Pdebounce = false;

        int selection;
        bool arrowDeb;

        public static FontManager FontManager;
        public static LanguageManager LanguageManager;
        public static MaterialManager MaterialManager;
        public static SoundManager SoundManager;
        public static ConfigManager ConfigManager;
        public static TissueManager TissueManager;
        public static BodyManager BodyManager;
        public static AnnouncementManager AnnouncementManager;
        public static CreatureManager CreatureManager;


        public GameState GameState = GameState.MENU;

        public static int Rows = 25;
        public static int Cols = 80;

        public static int MapHeight = Rows * 2;
        public static int MapWidth = Cols * 2;

        public int CursorX = 1;
        public int CursorY = 1;

        public int CursorBlinkTimer = 20;
        public static int MoveConst = 10;
        public int CursorMoveTimer = MoveConst;

        public static int FrameLimit = 100;
        public bool CursorOn = true;

        public Tile[,] MaterialMap = new Tile[MapWidth,MapHeight];

        int frames;
        private TimeSpan elapsed;
        int fps;
        int refreshRate;
        int finalRef;
        public DwarfFortress()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            FontManager = new FontManager();
            LanguageManager = new LanguageManager();
            MaterialManager = new MaterialManager();
            TissueManager = new TissueManager();
            SoundManager = new SoundManager();
            ConfigManager = new ConfigManager();
            BodyManager = new BodyManager();
            CreatureManager = new CreatureManager();
            AnnouncementManager = new AnnouncementManager();
            ConfigManager.LoadConfigFiles();
            Cols = ConfigManager.GetConfigValueAsInt("WINDOWEDX");
            Rows = ConfigManager.GetConfigValueAsInt("WINDOWEDY");
            SoundManager.OnLoad(Content);
            Window.AllowUserResizing = true;
            Window.ClientSizeChanged += HandleResize;
            Random = new Random();
            graphics.PreferredBackBufferHeight = (Rows * FontManager.CharSizeY);
            graphics.PreferredBackBufferWidth = (Cols * FontManager.CharSizeX);
            TargetElapsedTime = new TimeSpan(0, 0, 0, 0, 1000/FrameLimit);
            Window.Title = "Dwarf Fortress";
// ReSharper disable UnusedVariable
            var rawFile = new RawFile("./Raw/Objects/language_words.txt");
            var rawFileH = new RawFile("./Raw/Objects/language_HUMAN.txt");
            var rawFileD = new RawFile("./Raw/Objects/language_DWARF.txt");
            var rawFileG = new RawFile("./Raw/Objects/language_GOBLIN.txt");
            var rawFileS = new RawFile("./Raw/Objects/language_SYM.txt");
            var rawFileMt = new RawFile("./Raw/Objects/material_template_default.txt");
            var rawFileIg = new RawFile("./Raw/Objects/inorganic_stone_gem.txt");
            var rawFileIl = new RawFile("./Raw/Objects/inorganic_stone_layer.txt");
            var rawFileIm = new RawFile("./Raw/Objects/inorganic_stone_mineral.txt");
            var rawFileIs = new RawFile("./Raw/Objects/inorganic_stone_soil.txt");
            var rawFileIt = new RawFile("./Raw/Objects/inorganic_metal.txt");
            var rawFileTt = new RawFile("./Raw/Objects/tissue_template_default.txt");
            var rawFileBd = new RawFile("./Raw/Objects/body_default.txt");
            var rawFileBc = new RawFile("./Raw/Objects/body_rcp.txt");
            var rawFileBp = new RawFile("./Raw/Objects/b_detail_plan_default.txt");
            var rawFileSpider = new RawFile("./Raw/Objects/creature_ggcs.txt");
// ReSharper restore UnusedVariable
            var materials = new List<Material>(MaterialManager.InorganicMaterialList.Values);
            for (var x = 0; x < MapWidth; x++)
            {
                for(var y = 0; y < MapHeight;y++)
                {
                    var material = Random.Next(20) == 0 ? null : materials[Random.Next(materials.Count)];
                    MaterialMap[x, y] = new Tile(material);
                }
            }
        }

        public void HandleResize(object sender, EventArgs e)
        {
            Cols = (int)Math.Floor((double)Window.ClientBounds.Width / FontManager.CharSizeX);
            Rows = (int)Math.Floor((double)Window.ClientBounds.Height / FontManager.CharSizeY);
            if (ConfigManager.GetConfigValueAsBool("BLACK_SPACE")) return;
            graphics.PreferredBackBufferHeight = Rows * FontManager.CharSizeY;
            graphics.PreferredBackBufferWidth = Cols * FontManager.CharSizeX;
            graphics.ApplyChanges();
            if (CursorX > Cols - 2) CursorX = Cols - 2;
            if (CursorX < 1) CursorX = 1;
            if (CursorY > Rows - 2) CursorY = Rows - 2;
            if (CursorY < 1) CursorY = 1;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            Console.WriteLine("Starting up!");
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

            //font = this.Content.Load<Texture2D>("./Data/curses_640x300");
            font = Texture2D.FromStream(GraphicsDevice, new FileStream("./Data/taffer.png", FileMode.Open));
            FontManager.SetCharSize(font.Width/16,font.Height/16);
            graphics.PreferredBackBufferHeight = (Rows * FontManager.CharSizeY);
            graphics.PreferredBackBufferWidth = (Cols * FontManager.CharSizeX);
            graphics.ApplyChanges();
            SoundManager.PlaySong("TITLE_SONG");
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            SoundManager.StopCurrentSong();
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            frames++;
            elapsed += gameTime.ElapsedGameTime;
            if (elapsed > TimeSpan.FromSeconds(1))
            {
                elapsed -= TimeSpan.FromSeconds(1);
                fps = frames;
                finalRef = refreshRate;
                frames = 0;
                refreshRate = 0;
            }

            if (GameState != GameState.MENU)
            {
                if (!BoxLocked)
                {
                    if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
                        Keyboard.GetState().IsKeyDown(Keys.Escape)) GameStateChange(GameState.MENU);
                }
            }

            if(GameState == GameState.PLAYING)
            {
                if(!BoxLocked)
                {
                    AnnouncementManager.Update();
                    if (Keyboard.GetState().IsKeyDown(Keys.Space) && !Pdebounce)
                    {
                        Paused = !Paused;
                        Pdebounce = true;
                    }

                    if (Keyboard.GetState().IsKeyUp(Keys.Space) && Keyboard.GetState().IsKeyUp(Keys.G) && Pdebounce)
                    {
                        Pdebounce = false;
                    }
                    if (Keyboard.GetState().IsKeyDown(Keys.G) && !Pdebounce)
                    {
                        var materials = new List<Material>(MaterialManager.InorganicMaterialList.Values);
                        for (var x = 0; x < MapWidth; x++)
                        {
                            for (var y = 0; y < MapHeight; y++)
                            {
                                var material = materials[Random.Next(materials.Count)];
                                MaterialMap[x, y] = new Tile(material);
                            }
                        }
                        var announcementType = (AnnouncementType) Random.Next(89,94);
                        AnnouncementManager.AnnouncementEvent(announcementType, new List<string> { "Urist McFuckwit" });
                        Pdebounce = true;
                    }

                    if (CursorOn) CursorBlinkTimer--;
                    else CursorBlinkTimer++;
                    if (CursorBlinkTimer <= 0 || CursorBlinkTimer >= 20) CursorOn = !CursorOn;
                    if (CursorMoveTimer > 0) CursorMoveTimer--;
                    if (CursorMoveTimer == 0) arrowDeb = false;

                    if (Keyboard.GetState().IsKeyDown(Keys.Down) && CursorY < Rows - 2 && !arrowDeb)
                    {
                        CursorY++;
                        arrowDeb = true;
                        CursorMoveTimer = MoveConst;
                        AnnouncementManager.AnnouncementEvent(AnnouncementType.ENDGAME_EVENT_1, new List<string>());
                    }
                    if (Keyboard.GetState().IsKeyDown(Keys.Up) && CursorY > 1 && !arrowDeb)
                    {
                        CursorY--;
                        arrowDeb = true;
                        CursorMoveTimer = MoveConst;
                    }
                    if (Keyboard.GetState().IsKeyDown(Keys.Right) && CursorX < Cols - 2 && !arrowDeb)
                    {
                        CursorX++;
                        arrowDeb = true;
                        CursorMoveTimer = MoveConst;
                    }
                    if (Keyboard.GetState().IsKeyDown(Keys.Left) && CursorX > 1 && !arrowDeb)
                    {
                        CursorX--;
                        arrowDeb = true;
                        CursorMoveTimer = MoveConst;
                    }
                }
                else
                {
                    if (Keyboard.GetState().IsKeyDown(Keys.Enter)) BoxLocked = false;
                }
            }
            if(GameState == GameState.MENU)
            {
                if (Keyboard.GetState().IsKeyDown(Keys.Enter) && selection == 0) GameStateChange(GameState.PLAYING);
                if(Keyboard.GetState().IsKeyDown(Keys.Enter) && selection == 1) GameStateChange(GameState.FONTTEST);
                if (Keyboard.GetState().IsKeyDown(Keys.Enter) && selection == 2) Exit();
                if (Keyboard.GetState().IsKeyDown(Keys.Up) && !arrowDeb && selection > 0)
                {
                    selection--;
                    arrowDeb = true;
                }
                if (Keyboard.GetState().IsKeyDown(Keys.Down) && !arrowDeb && selection < 2)
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
            if (stateToChange == GameState) return;
            SoundManager.StopCurrentSong();
            if (stateToChange == GameState.MENU) SoundManager.PlaySong("TITLE_SONG");
            else SoundManager.PlaySong("GAME_SONG");
            GameState = stateToChange;
            selection = 0;
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            spriteBatch.Begin();
            refreshRate++;
            if(GameState == GameState.PLAYING)
            {
                for (var i = 0; i < Cols; i++)
                {
                    for (var j = 0; j < Rows; j++)
                    {
                        if (j == 0 || j == Rows - 1 || i == 0 || i == Cols - 1) FontManager.DrawCharacter('█', spriteBatch, font, new Vector2(i, j), FontManager.ColorManager.GetPairFromTriad(0, 0, 1));
                    }
                }
                for (var x = 1; x < Cols - 1; x++)
                {
                    for (var y = 1; y < Rows - 1; y++)
                    {
                        if (x < MapWidth && y < MapHeight)
                        {
                            MaterialMap[x - 1, y - 1].RenderTile(spriteBatch, font, new Vector2(x, y));
                        }
                    }
                }
                if (CursorOn) FontManager.DrawCharacter('X', spriteBatch, font, new Vector2(CursorX, CursorY), FontManager.ColorManager.GetPairFromTriad(6, 0, 1));
                FontManager.DrawString(MaterialMap[CursorX-1,CursorY-1].GetNameBasedOnState(false, true), spriteBatch, font, new Vector2(Cols/5,0), FontManager.ColorManager.GetPairFromTriad(0,7,0));
                FontManager.DrawString("  Dwarf Fortress  ", spriteBatch, font, new Vector2(Cols/2 - 9, 0), FontManager.ColorManager.GetPairFromTriad(0, 7, 0));
                if (Paused) FontManager.DrawString("*PAUSED*", spriteBatch, font, new Vector2(1, 0), FontManager.ColorManager.GetPairFromTriad(3, 2, 1));
                if(ConfigManager.GetConfigValueAsBool("FPS")) FontManager.DrawString("FPS: " + fps + " (" +finalRef+")", spriteBatch, font, new Vector2(Cols -  (Cols/4), 0), FontManager.ColorManager.GetPairFromTriad(2, 2, 1));
                AnnouncementManager.Render(spriteBatch, font); 
            }
            else if (GameState == GameState.FONTTEST)
            {
                for (int s = 0; s < 2; s++)
                {
                    for (int i = 0; i < 8; i++)
                    {
                        FontManager.FontTest(spriteBatch, font, new Vector2(0 + i*16, 0 + s*16), FontManager.ColorManager.GetPairFromTriad(i, 0, s));
                    }
                }
                
                
            }
            else if(GameState == GameState.MENU)
            {
                FontManager.DrawString("Slaves to Armok:  God of Blood", spriteBatch, font, new Vector2(Cols/2 - 15, 2), FontManager.ColorManager.GetPairFromTriad(4, 0, 1));
                FontManager.DrawString("Chapter II: Dwarf Fortress", spriteBatch, font, new Vector2(Cols/2 - 12, 3), FontManager.ColorManager.GetPairFromTriad(7, 0, 1));
                FontManager.DrawString("(XNA EDITION)", spriteBatch, font, new Vector2(Cols/2 - 6, 5), FontManager.ColorManager.GetPairFromTriad(6, 6, 1));
                FontManager.DrawString("Look at Something", spriteBatch, font, new Vector2(Cols/2 - 8, 10), FontManager.ColorManager.GetPairFromTriad(7, 0, selection == 0 ? 1 : 0));
                FontManager.DrawString("Look at Something Else", spriteBatch, font, new Vector2(Cols/2 - 10, 12), FontManager.ColorManager.GetPairFromTriad(7,0,selection == 1 ? 1: 0));
                FontManager.DrawString("Exit", spriteBatch, font, new Vector2(Cols/2 - 1, 14), FontManager.ColorManager.GetPairFromTriad(7, 0, selection == 2 ? 1 : 0));
            }
            
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
