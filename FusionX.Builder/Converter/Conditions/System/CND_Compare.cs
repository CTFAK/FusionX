using CTFAK.MMFParser.Shared.Events;
using FusionX.Builder.Expressions;

namespace FusionX.Builder.Conditions.System;

public class CND_Compare : ConditionWriterBase
{
    public override bool IsSimple => false;
    public override bool UsesInstances => false;
    public override void Write(Condition act, CodeWriter code, CodeWriter variables)
    {
        var expr1 = act.Items[0].Loader as ExpressionParameter;
        var expr2 = act.Items[1].Loader as ExpressionParameter;
        code.AppendLineIndented($"result = {ExpressionConverter.ConvertExpression(expr1,-1)} {expr2.GetOperator()} {ExpressionConverter.ConvertExpression(expr2,-1)};");
    }
}