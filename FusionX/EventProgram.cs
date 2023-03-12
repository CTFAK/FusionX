using Microsoft.Xna.Framework.Input;

namespace FusionX;

public class EventProgram
{

    #region ObjectContext

    public Dictionary<int, List<FObject>> ObjectContext = new();

    public List<FObject> GetAll(int oi)
    {
        var list = new List<FObject>();
        var instances = FGame.GetCurrentGame().currentFrame.ObjectInstances;
        foreach (var obj in instances)
        {
            if (obj.oi == oi)
                list.Add(obj);
        }

        return list;
    }
    public void SelectAll(int oi)
    {
        if (ObjectContext.ContainsKey(oi))
            ObjectContext[oi] = GetAll(oi);
        else ObjectContext.Add(oi, GetAll(oi));
    }
    public void ClearAll(int oi)
    {
        if (ObjectContext.ContainsKey(oi))
            ObjectContext[oi].Clear();
        else ObjectContext.Add(oi, new List<FObject>());
    }
    public List<FObject> GetContextForOI(int oi)
    {
        if (ObjectContext.ContainsKey(oi))
            return ObjectContext[oi];
        return new List<FObject>();
    }

    public FObject GetSingleInstance(int oi)
    {
        return GetAll(oi).Last();
    }

    public void ResetObjectContext()
    {
        ObjectContext.Clear();
    }

    public void AddAllToContext(int oi)
    {
        ObjectContext.Clear();
    }

    public void AddToContext(int oi, FObject obj)
    {
        if (!ObjectContext.ContainsKey(oi))
            ObjectContext.Add(oi, new List<FObject>());
        ObjectContext[oi].Add(obj);
    }
    public void RemoveFromContext(FObject obj)
    {
        if (ObjectContext.ContainsKey(obj.oi))
            ObjectContext[obj.oi].Remove(obj);
    }
    #endregion
    #region EventProperties

    public bool startOfFrame;
    public int timerValue;
    public int FrameWidth => FGame.GetCurrentGame().currentFrame.Width;
    public int FrameHeight => FGame.GetCurrentGame().currentFrame.Height;

    #endregion

    #region EventRoutines

    public bool IsKeyDown(Keys key)
    {
        return currentState.IsKeyDown(key);
    }
    public bool IsKeyPressed(Keys key)
    {
        return currentState.IsKeyDown(key) && !oldState.IsKeyDown(key);
    }
    public int Random(int max)
    {
        return FGame.GetCurrentGame().globalRandom.Next(max);
    }

    public int RandomRange(int min, int max)
    {
        return FGame.GetCurrentGame().globalRandom.Next(min, max);
    }


    #endregion
    #region InternalProperties

    public int frameIndex;

    #endregion

    public KeyboardState oldState;
    public KeyboardState currentState;
    public void MainLoop(int ms)
    {
        currentState = Keyboard.GetState();
        timerValue += ms;
        if (IsKeyPressed(Keys.Insert))
            Console.WriteLine(FGame.GetCurrentGame().currentFrame.ObjectInstances.Count);
        if (IsKeyPressed(Keys.Home))
            Console.WriteLine(1000f / ms);
        UserCode();
        startOfFrame = false;
        oldState = currentState;
    }

    public virtual void UserCode()
    {

    }
}