using Action = CTFAK.MMFParser.Shared.Events.Action;

namespace FusionX.Builder.Actions;

public class ActionWriterBase
{
    public bool alreadyHasContext;
    public virtual bool UsesInstances => false;
    public virtual bool ReturnsInstances => false;
    public virtual void Write(Action act, CodeWriter code, CodeWriter variables)
    {
        code.AppendLineIndented($"// Unimplemented action: {Utils.Utils.GetActionName(act.ObjectType, act.Num)} {act.ObjectType} - {act.Num}");
    }
}