using CTFAK.MMFParser.Shared.Events;
using FusionX.Builder.Conditions.Common;
using FusionX.Builder.Conditions.Keyboard;
using FusionX.Builder.Conditions.System;

namespace FusionX.Builder.Conditions;

public class ConditionConverter
{
    public static ConditionWriterBase Convert(Condition cnd, CodeWriter code, CodeWriter variables)
    {
        var writer = GetWriter(cnd.ObjectType, cnd.Num);
        writer.condition = cnd;
        writer.Write(cnd, code, variables);
        return writer;
    }

    public static ConditionWriterBase GetWriter(int objectType, int num)
    {
        switch (objectType)
        {
            case -3:
                switch (num)
                {
                    case -1:
                        return new CND_StartOfFrame();
                }
                break;
            case -6:
                switch (num)
                {
                    case -1:
                        return new CND_KeyPressed();
                    case -2:
                        return new CND_KeyDown();
                }
                break;
            case -1:
                switch (num)
                {
                    case -1:
                        return new CND_Always();
                    case -3:
                        return new CND_Compare();

                }

                break;
        }

        return CreateCommon(num);
    }

    public static ConditionWriterBase CreateCommon(int num)
    {
        switch (num)
        {
            case -17:
                return new CND_CompareX();
            case -16:
                return new CND_CompareY();
        }

        return new ConditionWriterBase();
    }
}