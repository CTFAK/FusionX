using CTFAK.Memory;
using CTFAK.MMFParser.CCN;
using CTFAK.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FusionX;

public class MainWindow : Game
{
    public GraphicsDeviceManager graphics;

    public SpriteBatch spriteBatch;
    public FGame game;
    public static MainWindow inst;
    public MainWindow()
    {
        inst = this;
        graphics = new GraphicsDeviceManager(this);
        graphics.PreferredBackBufferWidth = 1280;
        graphics.PreferredBackBufferHeight = 720;
        graphics.PreferMultiSampling = true;
        Content.RootDirectory = "Content";
        TargetElapsedTime = TimeSpan.FromSeconds(1 / 60f);
        Window.AllowUserResizing = false;
        IsMouseVisible = true;
    }
    protected override void LoadContent()
    {
        spriteBatch = new SpriteBatch(GraphicsDevice);
        Core.Init();
        Core.Parameters = "";
        Settings.gameType = Settings.GameType.CBM;
        Settings.Build = 284;
        var reader = new ByteReader(new FileStream("Application.ccn", FileMode.Open));
        var gameData = new GameData();
        gameData.Read(reader);
        game = new FGame();
        game.LoadFromGameData(gameData, this);

    }

    protected override void Draw(GameTime gameTime)
    {
        game.Render(spriteBatch);
    }

    protected override void Update(GameTime gameTime)
    {
        game.Update(gameTime);
    }
}