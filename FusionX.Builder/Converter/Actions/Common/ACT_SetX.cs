using System.Data.SqlTypes;
using CTFAK.MMFParser.Shared.Events;
using FusionX.Builder.Expressions;
using Action = CTFAK.MMFParser.Shared.Events.Action;


namespace FusionX.Builder.Actions.Common;

public class ACT_SetX:ActionWriterBase
{
    
    public override bool UsesInstances => true;
    public override void Write(Action act, CodeWriter code, CodeWriter variables)
    {
        var param = act.Items[0].Loader as ExpressionParameter;
        
        code.AppendLineIndented($"currentObject.SetXPos((int)({ExpressionConverter.ConvertExpression(param,act.ObjectInfo)}));");
    }
}