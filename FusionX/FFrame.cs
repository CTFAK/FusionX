using CTFAK.MMFParser.CCN.Chunks.Frame;
using FusionX.UserCode;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FusionX;

public class FFrame
{
    public int Width;
    public int Height;
    public EventProgram Events = new UserCodeEventProgram();
    public List<FObject> ObjectInstances = new();
    public void FromGameData(Frame frm)
    {
        Width = frm.Width;
        Height = frm.Height;
        foreach (var frmObj in frm.Objects)
        {
            var newInst = new FObject();
            newInst.oi = frmObj.ObjectInfo;
            newInst.ObjectInfo = FGame.GetCurrentGame().ObjectInfos[newInst.oi];
            newInst.x = frmObj.X;
            newInst.y = frmObj.Y;
            newInst.sprite = FGame.GetCurrentGame().Images.Items[newInst.ObjectInfo.mainSprite].GetTexture();
            ObjectInstances.Add(newInst);
        }
    }
    public void Update(GameTime time)
    {
        Events.MainLoop(time.ElapsedGameTime.Milliseconds);
        foreach (var obj in ObjectInstances)
        {
            obj.Update(time);
        }
    }

    public void Render(SpriteBatch batch)
    {
        foreach (var obj in ObjectInstances)
        {
            obj.Draw(batch);
        }
    }
}