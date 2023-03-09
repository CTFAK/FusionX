using CTFAK.MMFParser.Shared.Events;
using FusionX.Application;

namespace FusionX.Builder.Conditions.Keyboard;

public class CND_KeyDown:ConditionWriterBase
{
    public override bool IsSimple => true;
    public override bool UsesInstances => false;
    public override void Write(Condition act, CodeWriter code, CodeWriter variables)
    {
        var key = (act.Items[0].Loader as KeyParameter);
        
        code.AppendIndented($"IsKeyDown({CKeyConvert.getXnaKey(key.Key)})");
    }
}