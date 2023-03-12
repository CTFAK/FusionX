using CTFAK.MMFParser.CCN;
using CTFAK.MMFParser.CCN.Chunks.Objects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FusionX;

public class FGame
{
    public FFrame currentFrame;
    public static FGame instance;
    public Dictionary<int, FObjectInfo> ObjectInfos = new Dictionary<int, FObjectInfo>();
    public FImageBank Images;
    public Random globalRandom = new Random(31012007);

    public static FGame GetCurrentGame()
    {
        return instance;
    }
    public void LoadFromGameData(GameData game, MainWindow xnaGame)
    {
        instance = this;
        xnaGame.Window.Title = game.Name;
        xnaGame.graphics.PreferredBackBufferWidth = game.Header.WindowWidth;
        xnaGame.graphics.PreferredBackBufferHeight = game.Header.WindowHeight;
        xnaGame.graphics.ApplyChanges();
        Images = new FImageBank();
        foreach (var img in game.Images.Items)
        {
            var newImg = new FImage();
            newImg.Handle = img.Key;
            newImg.fileImage = img.Value;
            Images.Items.Add(img.Key, newImg);
        }

        foreach (var oi in game.FrameItems)
        {
            var newOI = new FObjectInfo();
            newOI.Handle = oi.Key;
            if (oi.Value.Properties is ObjectCommon common)
            {
                newOI.mainSprite = common.Animations.AnimationDict[0].DirectionDict[0].Frames[0];
                ObjectInfos.Add(newOI.Handle, newOI);
            }
        }

        currentFrame = new FFrame();
        currentFrame.FromGameData(game.Frames[0]);
    }

    public void Update(GameTime time)
    {
        currentFrame.Update(time);
    }

    public void Render(SpriteBatch batch)
    {
        MainWindow.inst.GraphicsDevice.Clear(Color.White);
        batch.Begin();
        currentFrame.Render(batch);
        batch.End();
    }
}