using CTFAK.MMFParser.Shared.Events;
using FusionX.Builder.Actions;
using FusionX.Builder.Conditions;

namespace FusionX.Builder;

public class EventGroupConverter
{
    public static void Convert(EventGroup evGrp, CodeWriter code, CodeWriter variables)
    {

        bool alreadyHasActionContent = false;
        // Conditions
        List<ConditionWriterBase> cndWriters = new();
        for (int i = 0; i < evGrp.Conditions.Count; i++)
        {
            var cnd = evGrp.Conditions[i];
            var writer = new CodeWriter();
            var cndWrt = ConditionConverter.Convert(cnd, writer, variables);
            cndWriters.Add(cndWrt);
            if (cndWrt.IsSimple)
            {
                if (cndWrt.UsesInstances)
                    throw new NotImplementedException("Writer can't be simple and use instances at the same time");
                code.AppendLineIndented($"if (!({writer.ToString().Replace("result = ", "")})) return;");
            }
            else
            {
                if (cndWrt.UsesInstances)
                {
                    alreadyHasActionContent = true;
                    var rnd = Utils.Utils.GetUniqueNumber();
                    code.AppendLineIndented($"var context{rnd} = GetContextForOI({cnd.ObjectInfo});");
                    code.AppendLineIndented($"for (int c = 0; c < context{rnd}.Count; c++)");
                    code.StartBrace();
                    code.AppendLineIndented($"var currentObject = context{rnd}[c];");
                    var num = Utils.Utils.GetUniqueNumber();
                    code.AppendLineIndented($"bool result{num} = false;");
                    code.AppendLineIndented(writer.ToString().Replace("result", $"result{num}"));
                    code.AppendLineIndented($"if (!(result{num})) RemoveFromContext(currentObject);");
                    code.EndBrace();
                }
                else
                {
                    var num = Utils.Utils.GetUniqueNumber();
                    code.AppendLineIndented($"bool result{num} = false;");
                    writer.Replace("result", $"result{num}");
                    code.AppendWriterIndented(writer);
                    code.AppendLineIndented($"if (!result{num}) return;");
                }
            }
        }

        // Actions
        for (int i = 0; i < evGrp.Actions.Count; i++)
        {
            var act = evGrp.Actions[i];
            var writer = new CodeWriter();
            var actWrt = ActionConverter.Convert(act, writer, variables);
            if (actWrt.ReturnsInstances) alreadyHasActionContent = true;
            if (cndWriters.Any(a => { return a.condition.ObjectInfo != act.ObjectInfo; }))
                alreadyHasActionContent = false;
            if (actWrt.UsesInstances)
            {
                if (!alreadyHasActionContent)
                {
                    code.AppendLineIndented($"SelectAll({act.ObjectInfo});");
                }
                var rnd = Utils.Utils.GetUniqueNumber();
                code.AppendLineIndented($"var context{rnd} = GetContextForOI({act.ObjectInfo});");
                code.AppendLineIndented($"for (int a = 0; a < context{rnd}.Count; a++)");
                code.StartBrace();
                code.AppendLineIndented($"var currentObject = context{rnd}[a];");

                code.AppendWriterIndented(writer);
                code.EndBrace();
            }
            else
            {
                code.AppendWriterIndented(writer);
            }
        }

        code.AppendLineIndented("ResetObjectContext();");


    }
}