using Action = CTFAK.MMFParser.Shared.Events.Action;

namespace FusionX.Builder.Actions.Create;

public class ACT_CreateObject : ActionWriterBase
{
    public override bool UsesInstances => false;
    public override bool ReturnsInstances => true;

    public override void Write(Action act, CodeWriter code, CodeWriter variables)
    {
        var unique = Utils.Utils.GetUniqueNumber();
        var param = act.Items[0].Loader as CTFAK.MMFParser.Shared.Events.Create;
        var pos = param.Position;
        code.AppendLineIndented($"var newObj{unique} = FObject.Create({param.ObjectInfo},{pos.X},{pos.Y});");
        code.AppendLineIndented($"ClearAll({param.ObjectInfo});");
        code.AppendLineIndented($"AddToContext({param.ObjectInfo},newObj{unique});");
    }
}