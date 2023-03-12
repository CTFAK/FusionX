using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FusionX;

public class FObject
{
    public int oi;
    public FObjectInfo ObjectInfo;
    public int x;
    public int y;
    public Texture2D sprite;
    public double[] alterableValues = new double[32];

    public void Init()
    {
    }
    public void Draw(SpriteBatch batch)
    {
        batch.Draw(sprite, new Vector2(x, y), Color.White);
    }

    public void Update(GameTime time)
    {
    }

    #region CommonRoutines

    public void SetYPos(float pos)
    {
        y = (int)pos;
    }
    public void SetXPos(float pos)
    {
        x = (int)pos;
    }

    public void SetAlterableValue(int index, double value)
    {
        alterableValues[index] = value;
    }

    public double GetAlterableValue(int index)
    {
        return alterableValues[index];
    }
    public void AddToAlterableValue(int index, double value)
    {
        alterableValues[index] = alterableValues[index] + value;
    }

    public static FObject Create(int oi, int posX, int posY)
    {
        var obj = new FObject();
        obj.x = posX;
        obj.y = posY;
        obj.oi = oi;
        obj.ObjectInfo = FGame.GetCurrentGame().ObjectInfos[oi];
        obj.sprite = FGame.GetCurrentGame().Images.Items[obj.ObjectInfo.mainSprite].GetTexture();

        FGame.GetCurrentGame().currentFrame.ObjectInstances.Add(obj);
        return obj;
    }
    #endregion

}