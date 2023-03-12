using CTFAK.MMFParser.Shared.Events;

namespace FusionX.Builder.Conditions;

public class ConditionWriterBase
{
    public Condition condition;
    public virtual bool UsesInstances => false;
    public virtual bool IsSimple => false;

    public virtual void Write(Condition act, CodeWriter code, CodeWriter variables)
    {
        code.AppendLineIndented($"result = false; /* Unimplemented Condition: {Utils.Utils.GetActionName(act.ObjectType, act.Num)} {act.ObjectType} - {act.Num}*/");
    }
}