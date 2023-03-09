using CTFAK.MMFParser.Shared.Events;
using FusionX.Builder.Expressions;
using Action = CTFAK.MMFParser.Shared.Events.Action;

namespace FusionX.Builder.Actions.Common;

public class ACT_AddAlterableValue:ActionWriterBase
{
    public override bool UsesInstances => true;
    public override void Write(Action act, CodeWriter code, CodeWriter variables)
    {
        var index = (act.Items[0].Loader as Short).Value;
        var expr = ExpressionConverter.ConvertExpression((act.Items[1].Loader as ExpressionParameter), act.ObjectInfo);
        code.AppendLineIndented($"currentObject.AddToAlterableValue({index},{expr});");
    }
}