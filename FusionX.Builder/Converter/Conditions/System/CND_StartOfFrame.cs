using CTFAK.MMFParser.Shared.Events;

namespace FusionX.Builder.Conditions.System;

public class CND_StartOfFrame:ConditionWriterBase
{
    public override bool IsSimple => true;
    public override bool UsesInstances => false;
    public override void Write(Condition act, CodeWriter code, CodeWriter variables)
    {
       code.AppendIndented("startOfFrame");
    }
}