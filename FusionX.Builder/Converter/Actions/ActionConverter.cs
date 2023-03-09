using FusionX.Builder.Actions.Common;
using FusionX.Builder.Actions.Create;
using Action = CTFAK.MMFParser.Shared.Events.Action;

namespace FusionX.Builder.Actions;

public class ActionConverter
{
    public static ActionWriterBase Convert(Action act, CodeWriter code, CodeWriter variables)
    {
        var writer = GetWriter(act.ObjectType, act.Num);
        writer.Write(act,code,variables);
        return writer;
    }

    public static ActionWriterBase GetWriter(int objectType, int num)
    {
        switch (objectType)
        {
            case -5:
                switch (num)
                {
                    case 0:
                        return new ACT_CreateObject();
                }
                break;
        }

        return CreateCommon(num);
    }

    public static ActionWriterBase CreateCommon(int num)
    {
        switch (num)
        {
            case 2:
                return new ACT_SetX();
            case 3:
                return new ACT_SetY();
            case 31:
                return new ACT_SetAlterableValue();
            case 32:
                return new ACT_AddAlterableValue();
        }

        return new ActionWriterBase();
    }
}