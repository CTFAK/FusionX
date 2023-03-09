using System.Text;

namespace FusionX.Builder;

public class CodeWriter
{
    public const string indent = "    ";
    public StringBuilder builder = new StringBuilder();
    public int currentIndent;
    
    public void AppendIndent()
    {
        for (int i = 0; i < currentIndent; i++)
        {
            Append(indent);
        }
    }
    public void Append(string str)
    {
        builder.Append(str);
    }
    
    public void AppendIndented(string str)
    {
        AppendIndent();
        Append(str);
    }
    public void AppendLine(string str)
    {
        Append(str);
        Append(Environment.NewLine);
    }
    public void AppendLineIndented(string str)
    {
        AppendIndent();
        AppendLine(str);
    }
    public void StartBrace()
    {
        AppendLineIndented("{");
        currentIndent++;
    }

    public void EndBrace()
    {
        currentIndent--;
        AppendLineIndented("}");
    }

    public void AppendWriterIndented(CodeWriter writer)
    {
        var data = writer.ToString().Split(Environment.NewLine);
        for (int i = 0; i < data.Length; i++)
        {
            AppendLineIndented(data[i]);
        }
    }

    public override string ToString()
    {
        return builder.ToString();
    }
    public void Replace(string oldStr, string newStr)
    {
        builder.Replace(oldStr, newStr);
    }
}