using System.Diagnostics.CodeAnalysis;
using System.Text;
using CTFAK.MMFParser.Shared.Events;


namespace FusionX.Builder.Expressions;

public class ExpressionConverter
{
    public static string ConvertExpression(ExpressionParameter param, int selfHandle)
    {
        var stringBuilder = new StringBuilder();

        foreach (var expr in param.Items)
        {
            stringBuilder.Append(GetExpression(expr, selfHandle));
        }

        return stringBuilder.ToString();
    }

    public static string GetExpression(Expression expr, int selfHandle)
    {
        switch (expr.ObjectType)
        {
            case 0:
                switch (expr.Num)
                {
                    case 2:
                        return "+";
                    case 4:
                        return "-";
                    case 6:
                        return "*";
                    case 8:
                        return "/";
                }
                break;
            case -1:
                switch (expr.Num)
                {
                    case 0:
                        return (expr.Loader as LongExp).Value.ToString();
                    case 1:
                        return "Random(";
                    case 65:
                        return "RandomRange(";
                    case -1:
                        return "(";
                    case -3:
                        return ",";
                    case -2:
                        return ")";
                    case 10:
                        return $"MathHelper.SinDeg(";
                    case 23:
                        return (expr.Loader as DoubleExp).Value.ToString().Replace(",",".");
                }
                break;
            case -3:
                switch (expr.Num)
                {
                    case 6:
                        return "FrameWidth";
                    case 7:
                        return "FrameHeight";
                }
                break;
            case -4:
                switch (expr.Num)
                {
                    case 0:
                        return "timerValue";
                }
                break;
        }

        return CreateCommon(expr,selfHandle);

    }

    public static string CreateCommon(Expression expr, int selfHandle)
    {
        string objectRef = expr.ObjectInfo == selfHandle ? "currentObject" : $"GetSingleInstance({expr.ObjectInfo})";
        switch (expr.Num)
        {
            case 16:
                return $"{objectRef}.GetAlterableValue({(expr.Loader as ExtensionExp).Value})";
        }

        return GetNoImpl(expr);
    }

    public static string GetNoImpl(Expression expr)
    {
        return $"NoImpl({expr.ObjectType}:{expr.Num})";
    }
}