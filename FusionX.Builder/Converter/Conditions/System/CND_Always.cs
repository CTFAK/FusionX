using CTFAK.MMFParser.Shared.Events;

namespace FusionX.Builder.Conditions.System;

public class CND_Always:ConditionWriterBase
{
    public override bool IsSimple => true;
    public override bool UsesInstances => false;
    public override void Write(Condition act, CodeWriter code, CodeWriter variables)
    {
        code.Append("true");
    }
}