using CTFAK.MMFParser.Shared.Events;
using FusionX.Builder.Expressions;

namespace FusionX.Builder.Conditions.Common;

public class CND_CompareX : ConditionWriterBase
{
    public override bool IsSimple => false;
    public override bool UsesInstances => true;
    public override void Write(Condition act, CodeWriter code, CodeWriter variables)
    {
        var expr = act.Items[0].Loader as ExpressionParameter;
        code.AppendLineIndented($"result = currentObject.x {expr.GetOperator()} {ExpressionConverter.ConvertExpression(expr,act.ObjectInfo)};");
    }
}