using System.Globalization;
using System.Linq;
using DwarfFortressXNA.Managers;
using DwarfFortressXNA.Objects;
using DwarfFortressXNA.World;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.IO;

namespace DwarfFortressXNA
{

    public enum SidebarState
    {
        FULLBAR,
        HALFBAR,
        NONE,
        MAP,
        HALFBAR_WITH_MAP
    }
    public enum GameState
    {
        INTRO,
        MENU,
        WORLDGEN,
        FONTTEST,
        FONTSELECT,
        PLAYING,
        ERROR
    }

    public enum FortressState
    {
        GAME,
        BUILDING,
        ANNOUNCEMENTS
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
        public bool EscDebounce = false;
        public bool TabDebounce = false;
        public bool EnterDebounce = false;

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
        public static InteractionManager InteractionManager;


        public GameState GameState = GameState.MENU;
        public FortressState FortressState = FortressState.GAME;
        public SidebarState SidebarState = SidebarState.HALFBAR_WITH_MAP;

        public static int Rows = 25;
        public static int Cols = 80;

        public static int MapHeight = 192;
        public static int MapWidth = 192;
        public static int MapDepth = 100;
        public static int SurfaceDepth = 10;

        public List<string> FontList;

        public int SelectedFont = 0;

        public int CursorX = 1;
        public int CursorY = 1;

        public int SelectedZ = 0;
        public bool ZChangeDebounce = false;

        public int CursorBlinkTimer = 20;
        public static int MoveConst = 10;
        public int CursorMoveTimer = MoveConst;

        public static int FrameLimit = 100;
        public bool CursorOn = true;

        private List<string> errorList; 

        //public static Tile[,,] MaterialMap = new Tile[MapWidth,MapHeight,MapDepth];
        public static WorldObject World;

        int frames;
        private TimeSpan elapsed;
        int fps;
        int refreshRate;
        int finalRef;
        public DwarfFortress()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            Random = new Random();
            FontManager = new FontManager();
            LanguageManager = new LanguageManager();
            MaterialManager = new MaterialManager();
            TissueManager = new TissueManager();
            ConfigManager = new ConfigManager();
            BodyManager = new BodyManager();
            CreatureManager = new CreatureManager();
            AnnouncementManager = new AnnouncementManager();
            InteractionManager = new InteractionManager();
            FontList = new List<string>();
            FontList = Directory.GetFiles("./Data/", "*.png").ToList();
            for (var i = 0; i < 500; i++)
            {
                var announcementType = (AnnouncementType)Random.Next(89, 94);
                AnnouncementManager.AnnouncementEvent(announcementType, new List<string> { "Urist McGenericdwarf" });
            }
            AnnouncementManager.NumberBuffered = 0;
            ConfigManager.LoadConfigFiles();
            SoundManager = new SoundManager(ConfigManager.GetConfigValueAsBool("SOUND"));
            Cols = ConfigManager.GetConfigValueAsInt("WINDOWEDX");
            Rows = ConfigManager.GetConfigValueAsInt("WINDOWEDY");
            SoundManager.OnLoad(Content);
            Window.AllowUserResizing = true;
            Window.ClientSizeChanged += HandleResize;
            graphics.PreferredBackBufferHeight = (Rows * FontManager.CharSizeY);
            graphics.PreferredBackBufferWidth = (Cols * FontManager.CharSizeX);
            TargetElapsedTime = new TimeSpan(0, 0, 0, 0, 1000/FrameLimit);
            Window.Title = "Dwarf Fortress";
// ReSharper disable UnusedVariable
            try
            {
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
                var rawFileIn = new RawFile("./Raw/Objects/interaction_standard.txt");
                var rawFileTt = new RawFile("./Raw/Objects/tissue_template_default.txt");
                var rawFileBd = new RawFile("./Raw/Objects/body_default.txt");
                var rawFileBc = new RawFile("./Raw/Objects/body_rcp.txt");
                var rawFileBp = new RawFile("./Raw/Objects/b_detail_plan_default.txt");
                var rawFileSpider = new RawFile("./Raw/Objects/creature_ggcs.txt");
            }
            catch (TokenParseException e)
            {
                if(errorList == null) errorList = new List<string>();
                GameState = GameState.ERROR;
                errorList.Add(e.Message);
                SoundManager.SoundEnabled = false;
                SoundManager.StopCurrentSong();
            }
// ReSharper restore UnusedVariable
            World = new WorldObject(MapWidth, MapHeight, MapDepth, SurfaceDepth, new StandardWorldGen());
        }

        public void HandleResize(object sender, EventArgs e)
        {
            Cols = (int)Math.Floor((double)Window.ClientBounds.Width / FontManager.CharSizeX);
            Rows = (int)Math.Floor((double)Window.ClientBounds.Height / FontManager.CharSizeY);
            AnnouncementManager.WindowResize();
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
            FileStream fontStream = new FileStream("./Data/curses_640x300.png", FileMode.Open);
            font = Texture2D.FromStream(GraphicsDevice, fontStream);
            fontStream.Close();
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
        /// 
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
                        Keyboard.GetState().IsKeyDown(Keys.Escape) && !EscDebounce && FortressState == FortressState.GAME) GameStateChange(GameState.MENU);
                }
            }

            if(GameState == GameState.PLAYING)
            {
                if(!BoxLocked)
                {
                    if (FortressState == FortressState.ANNOUNCEMENTS)
                    {
                        if (Keyboard.GetState().IsKeyDown(Keys.Down) && !arrowDeb)
                        {
                            if (AnnouncementManager.PageNumber < AnnouncementManager.NumberOfPages - 1)
                            {
                                AnnouncementManager.PageNumber++;
                                arrowDeb = true;
                            }
                        }
                        if (Keyboard.GetState().IsKeyDown(Keys.Up) && !arrowDeb)
                        {
                            if (AnnouncementManager.PageNumber > 0)
                            {
                                AnnouncementManager.PageNumber--;
                                arrowDeb = true;
                            }
                        }
                        if (Keyboard.GetState().IsKeyUp(Keys.Up) && Keyboard.GetState().IsKeyUp(Keys.Down) && arrowDeb) arrowDeb = false;
                    }
                    if (FortressState == FortressState.GAME || FortressState == FortressState.BUILDING)
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
                            var announcementType = (AnnouncementType) Random.Next(89, 94);
                            AnnouncementManager.AnnouncementEvent(announcementType, new List<string> {"Urist McGenericdwarf"});
                            Pdebounce = true;
                        }

                        if (CursorOn) CursorBlinkTimer--;
                        else CursorBlinkTimer++;
                        if (CursorBlinkTimer <= 0 || CursorBlinkTimer >= 20) CursorOn = !CursorOn;
                        if (CursorMoveTimer > 0) CursorMoveTimer--;
                        if (CursorMoveTimer == 0) arrowDeb = false;

                        if ((Keyboard.GetState().IsKeyDown(Keys.LeftShift) || Keyboard.GetState().IsKeyDown(Keys.RightShift)) &&
                            Keyboard.GetState().IsKeyDown(Keys.OemPeriod) && SelectedZ < MapDepth - 1 && !ZChangeDebounce)
                        {
                            SelectedZ++;
                            ZChangeDebounce = true;
                        }

                        if ((Keyboard.GetState().IsKeyDown(Keys.LeftShift) || Keyboard.GetState().IsKeyDown(Keys.RightShift)) &&
                            Keyboard.GetState().IsKeyDown(Keys.OemComma) && SelectedZ > 0 && !ZChangeDebounce)
                        {
                            SelectedZ--;
                            ZChangeDebounce = true;
                        }

                        if (Keyboard.GetState().IsKeyUp(Keys.OemPeriod) && Keyboard.GetState().IsKeyUp(Keys.OemComma) && ZChangeDebounce)
                        {
                            ZChangeDebounce = false;
                        }

                        if (Keyboard.GetState().IsKeyDown(Keys.Tab) && !TabDebounce)
                        {
                            switch (SidebarState)
                            {
                                case SidebarState.HALFBAR:
                                    SidebarState = SidebarState.NONE;
                                    break;
                                case SidebarState.NONE:
                                    SidebarState = SidebarState.MAP;
                                    break;
                                case SidebarState.MAP:
                                    SidebarState = SidebarState.HALFBAR_WITH_MAP;
                                    break;
                                case SidebarState.HALFBAR_WITH_MAP:
                                    SidebarState = SidebarState.FULLBAR;
                                    break;
                                case SidebarState.FULLBAR:
                                    SidebarState = SidebarState.HALFBAR;
                                    break;
                            }
                            TabDebounce = true;
                        }
                        if (Keyboard.GetState().IsKeyUp(Keys.Tab) && TabDebounce)
                        {
                            TabDebounce = false;
                        }
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
                        if (FortressState == FortressState.GAME && !EscDebounce)
                        {
                            if (Keyboard.GetState().IsKeyDown(Keys.A))
                            {
                                FortressState = FortressState.ANNOUNCEMENTS;
                                AnnouncementManager.ClearBuffered();
                                EscDebounce = true;
                            }
                            if (Keyboard.GetState().IsKeyDown(Keys.B))
                            {
                                FortressState = FortressState.BUILDING;
                                AnnouncementManager.ClearBuffered();
                                EscDebounce = true;
                            }
                        }
                    }
                    if(FortressState != FortressState.GAME)
                    {
                        if (Keyboard.GetState().IsKeyDown(Keys.Escape) && !EscDebounce)
                        {
                            FortressState = FortressState.GAME;
                            EscDebounce = true;
                        }
                    }
                    if (Keyboard.GetState().IsKeyUp(Keys.Escape) && EscDebounce)
                    {
                        EscDebounce = false;
                    }
                }
                else
                {
                    if (Keyboard.GetState().IsKeyDown(Keys.Enter)) BoxLocked = false;
                }
            }
            if (GameState == GameState.FONTSELECT)
            {
                if (Keyboard.GetState().IsKeyDown(Keys.Up) && !arrowDeb && SelectedFont > 0)
                {
                    SelectedFont--;
                    arrowDeb = true;
                }
                if (Keyboard.GetState().IsKeyDown(Keys.Down) && !arrowDeb && SelectedFont < FontList.Count-1)
                {
                    SelectedFont++;
                    arrowDeb = true;
                }
                if (Keyboard.GetState().IsKeyUp(Keys.Up) && Keyboard.GetState().IsKeyUp(Keys.Down) && arrowDeb) arrowDeb = false;
                if (Keyboard.GetState().IsKeyDown(Keys.Enter) && !EnterDebounce)
                {
                    EnterDebounce = true;
                    FileStream fontStream = new FileStream(FontList[SelectedFont], FileMode.Open);
                    font = Texture2D.FromStream(GraphicsDevice, fontStream);
                    fontStream.Close();
                    FontManager.SetCharSize(font.Width / 16, font.Height / 16);
                    graphics.PreferredBackBufferHeight = (Rows * FontManager.CharSizeY);
                    graphics.PreferredBackBufferWidth = (Cols * FontManager.CharSizeX);
                    graphics.ApplyChanges();
                    SelectedFont = 0;
                    GameStateChange(GameState.MENU);
                }
                if (Keyboard.GetState().IsKeyUp(Keys.Enter) && EnterDebounce) EnterDebounce = false;
            }
            if(GameState == GameState.MENU)
            {
                if (Keyboard.GetState().IsKeyDown(Keys.Enter) && selection == 0 && !EnterDebounce) GameStateChange(GameState.PLAYING);
                if(Keyboard.GetState().IsKeyDown(Keys.Enter) && selection == 1 && !EnterDebounce) GameStateChange(GameState.FONTTEST);
                if (Keyboard.GetState().IsKeyDown(Keys.Enter) && selection == 2 && !EnterDebounce)
                {
                    EnterDebounce = true;
                    GameStateChange(GameState.FONTSELECT);
                }
                if (Keyboard.GetState().IsKeyDown(Keys.Enter) && selection == 3) Exit();
                if (Keyboard.GetState().IsKeyDown(Keys.Up) && !arrowDeb && selection > 0)
                {
                    selection--;
                    arrowDeb = true;
                }
                if (Keyboard.GetState().IsKeyDown(Keys.Down) && !arrowDeb && selection < 3)
                {
                    selection++;
                    arrowDeb = true;
                }
                if (Keyboard.GetState().IsKeyUp(Keys.Up) && Keyboard.GetState().IsKeyUp(Keys.Down) && arrowDeb) arrowDeb = false;
                if (Keyboard.GetState().IsKeyUp(Keys.Enter) && EnterDebounce) EnterDebounce = false;
            }
            base.Update(gameTime);
        }

        public void GameStateChange(GameState stateToChange)
        {
            if (stateToChange == GameState) return;
            SoundManager.StopCurrentSong();
            SoundManager.PlaySong(stateToChange == GameState.MENU ? "TITLE_SONG" : "GAME_SONG");
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
            if (GameState == GameState.ERROR)
            {
                FontManager.DrawString("One or more errors were encountered when parsing RawFiles:",  spriteBatch, font, new Vector2(0,0), new ColorPair(ColorManager.Red, ColorManager.Black));
                for (var i = 0; i < (errorList.Count > (Rows - 2) ? (Rows - 2) : errorList.Count); i++)
                {
                    FontManager.DrawString(errorList[i], spriteBatch, font, new Vector2(0,i+1), new ColorPair(ColorManager.LightRed, ColorManager.Black));
                }
            }
            if(GameState == GameState.PLAYING)
            {
                for (var i = 0; i < Cols; i++)
                {
                    for (var j = 0; j < Rows; j++)
                    {
                        if (j == 0 || j == Rows - 1 || i == 0 || i == Cols - 1) FontManager.DrawCharacter('█', spriteBatch, font, new Vector2(i, j), FontManager.ColorManager.GetPairFromTriad(0, 0, 1));
                    }
                }
                FontManager.DrawString("  Dwarf Fortress  ", spriteBatch, font, new Vector2(Cols / 2 - 9, 0), FontManager.ColorManager.GetPairFromTriad(0, 7, 0));
                if (ConfigManager.GetConfigValueAsBool("FPS")) FontManager.DrawString("FPS: " + fps + " (" + finalRef + ")", spriteBatch, font, new Vector2(Cols - (Cols / 4), 0), FontManager.ColorManager.GetPairFromTriad(2, 2, 1));
                switch (FortressState)
                {
                    default:
                        for (var x = 1; x < Cols - 1; x++)
                        {
                            for (var y = 1; y < Rows - 1; y++)
                            {
                                if (x < MapWidth && y < MapHeight)
                                {
                                    World.MapTiles[x - 1, y - 1,SelectedZ].RenderTile(spriteBatch, font, new Vector2(x, y));
                                }
                            }
                        }
                        if (CursorOn) FontManager.DrawCharacter('X', spriteBatch, font, new Vector2(CursorX, CursorY), FontManager.ColorManager.GetPairFromTriad(6, 0, 1));
                        FontManager.DrawString(World.MapTiles[CursorX-1,CursorY-1,SelectedZ].GetNameBasedOnState(false, true), spriteBatch, font, new Vector2(Cols/5,0), FontManager.ColorManager.GetPairFromTriad(0,7,0));
                        AnnouncementManager.Render(spriteBatch, font);
                        if (Paused) FontManager.DrawString("*PAUSED*", spriteBatch, font, new Vector2(1, 0), FontManager.ColorManager.GetPairFromTriad(3, 2, 1));
                        var adjustedDepth = SurfaceDepth - SelectedZ;
                        var depthRender =
                            ((adjustedDepth > 0 ? "+" : "") + adjustedDepth.ToString(CultureInfo.InvariantCulture))
                                .ToCharArray();
                        for (var i = 0; i < depthRender.Length; i++)
                        {
                            FontManager.DrawCharacter(depthRender[i],spriteBatch,font, new Vector2(Cols-1,adjustedDepth == 0 ? i + 2 : i + 1), new ColorPair((adjustedDepth > 0 ? ColorManager.Green : (adjustedDepth == 0 ? ColorManager.Black : ColorManager.Red)), ColorManager.LightGrey));
                        }
                        switch (SidebarState)
                        {
                                //WIDTH OF MAPBAR IS ~24 Characters
                            case SidebarState.NONE:
                                break;
                            case SidebarState.HALFBAR:
                            {
                                for (var i = 32; i > 1; i--)
                                {
                                    for (var j = 1; j < Rows - 1; j++)
                                    {
                                        FontManager.DrawCharacter('█', spriteBatch, font, new Vector2(Cols - i, j),
                                            new ColorPair(i == 32 ? ColorManager.DarkGrey : ColorManager.Black,
                                                ColorManager.Black));
                                    }
                                }
                                DrawSidebarText();
                                break;
                            }
                            case SidebarState.FULLBAR:
                            {
                                for (var i = 56; i > 1; i--)
                                {
                                    for (var j = 1; j < Rows - 1; j++)
                                    {
                                        FontManager.DrawCharacter('█', spriteBatch, font, new Vector2(Cols - i, j),
                                            new ColorPair(i == 56 ? ColorManager.DarkGrey : ColorManager.Black,
                                                ColorManager.Black));
                                    }
                                }
                                DrawSidebarText();
                                break;
                            }
                            case SidebarState.MAP:
                            {
                                DrawMap();
                                break;
                            }
                            case SidebarState.HALFBAR_WITH_MAP:
                            {

                                for (var i = 56; i > 1; i--)
                                {
                                    for (var j = 1; j < Rows - 1; j++)
                                    {
                                        FontManager.DrawCharacter('█', spriteBatch, font, new Vector2(Cols - i, j),
                                            new ColorPair(i == 56 ? ColorManager.DarkGrey : ColorManager.Black,
                                                ColorManager.Black));
                                    }
                                }
                                DrawSidebarText();
                                DrawMap();
                                break;
                            }
                        }
                        break;
                    case FortressState.ANNOUNCEMENTS:
                        AnnouncementManager.RenderAnnouncementList(spriteBatch, font);
                        break;
                }
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
            else if (GameState == GameState.FONTSELECT)
            {
                for (int i = 0; i < FontList.Count; i++)
                {
                    FontManager.DrawString(FontList[i],spriteBatch, font, new Vector2(0,0+i), FontManager.ColorManager.GetPairFromTriad(7,0,SelectedFont == i ? 1 : 0));
                }
            }
            else if(GameState == GameState.MENU)
            {
                FontManager.DrawString("Slaves to Armok:  God of Blood", spriteBatch, font, new Vector2(Cols/2 - 15, 2), FontManager.ColorManager.GetPairFromTriad(4, 0, 1));
                FontManager.DrawString("Chapter II: Dwarf Fortress", spriteBatch, font, new Vector2(Cols/2 - 12, 3), FontManager.ColorManager.GetPairFromTriad(7, 0, 1));
                FontManager.DrawString("(XNA EDITION)", spriteBatch, font, new Vector2(Cols/2 - 6, 5), FontManager.ColorManager.GetPairFromTriad(6, 6, 1));
                FontManager.DrawString("Look at Something", spriteBatch, font, new Vector2(Cols/2 - 8, 10), FontManager.ColorManager.GetPairFromTriad(7, 0, selection == 0 ? 1 : 0));
                FontManager.DrawString("Look at Something Else", spriteBatch, font, new Vector2(Cols/2 - 10, 12), FontManager.ColorManager.GetPairFromTriad(7,0,selection == 1 ? 1: 0));
                FontManager.DrawString("Font Selection", spriteBatch, font, new Vector2(Cols/2 - 6, 14), FontManager.ColorManager.GetPairFromTriad(7,0,selection == 2 ? 1 : 0));
                FontManager.DrawString("Exit", spriteBatch, font, new Vector2(Cols/2 - 1, 16), FontManager.ColorManager.GetPairFromTriad(7, 0, selection == 3 ? 1 : 0));
            }
            FontManager.DrawString("XNA Beta Version " + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version, spriteBatch, font, new Vector2(0, Rows-1), FontManager.ColorManager.GetPairFromTriad(5, 0, 1));
            spriteBatch.End();
            base.Draw(gameTime);
        }

        public void DrawSidebarText()
        {
            //This will perform the switching for particular sets of FortressStates
            //Sidebar text varies :D
            var baseOffset = (SidebarState == SidebarState.HALFBAR)
                ? 32
                : 56;
            switch (FortressState)
            {
                case FortressState.GAME:
                    FontManager.DrawCharacter('a', spriteBatch, font, new Vector2(Cols - (baseOffset - 2), 2), new ColorPair(ColorManager.LightGreen, ColorManager.Black));
                    FontManager.DrawString(": View Announcements", spriteBatch, font, new Vector2(Cols - (baseOffset - 3), 2), new ColorPair(ColorManager.LightGrey, ColorManager.Black));
                    FontManager.DrawCharacter('b', spriteBatch, font, new Vector2(Cols - (baseOffset - 2), 3), new ColorPair(ColorManager.LightGreen, ColorManager.Black));
                    FontManager.DrawString(": Building", spriteBatch, font, new Vector2(Cols - (baseOffset - 3), 3), new ColorPair(ColorManager.LightGrey, ColorManager.Black));
                    break;
                case FortressState.BUILDING:
                    FontManager.DrawString("Motha-fuckin building!", spriteBatch, font, new Vector2(Cols - (baseOffset - 2), 2), new ColorPair(ColorManager.Red, ColorManager.Black));
                    break;
            }
        }

        public void DrawMap()
        {
            for (var i = 25; i > 1; i--)
            {
                for (var j = 1; j < Rows - 1; j++)
                {
                    FontManager.DrawCharacter(i == 25 ? '█' : 'Ω', spriteBatch, font, new Vector2(Cols - i, j),
                        (i == 25 ? new ColorPair(ColorManager.DarkGrey, ColorManager.Black) : new ColorPair(ColorManager.LightGrey, ColorManager.Black)));
                }
            }
        }
    }
}
