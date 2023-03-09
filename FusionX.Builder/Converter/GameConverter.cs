using CTFAK.MMFParser.CCN;
using FusionX.Builder.Utils;

namespace FusionX.Builder;

public static class GameConverter
{
    public static string Convert(GameData game)
    {
        var globalWriter = new Builder.CodeWriter();
        
        //Generate some basic stuff
        globalWriter.AppendLineIndented("using Microsoft.Xna.Framework.Input;");
        globalWriter.AppendLineIndented("namespace FusionX.UserCode;");
        globalWriter.AppendLineIndented("public class UserCodeEventProgram : EventProgram");
        globalWriter.StartBrace();
        
        // Generate frame code
        var frameWriter = new CodeWriter();
        var variableWriter = new CodeWriter();
        for (int i = 0; i < game.Frames.Count; i++)
        {
            var frm = game.Frames[i];
            frameWriter.AppendLineIndented($"// Frame \"{frm.Name}\" ");
            
            FrameConverter.Convert(frm,frameWriter,variableWriter);
            
        }
        globalWriter.AppendWriterIndented(variableWriter);
        globalWriter.AppendWriterIndented(frameWriter);
        
        // Generate stuff that calls our new frame code
        globalWriter.AppendLineIndented("public override void UserCode()");
        globalWriter.StartBrace();
        
        globalWriter.AppendLineIndented("switch (frameIndex)");
        globalWriter.StartBrace();
        for (int i = 0; i < game.Frames.Count; i++)
        {
            var frm = game.Frames[i];
            globalWriter.AppendLineIndented($"case {i}:");
            globalWriter.currentIndent++;
            globalWriter.AppendLineIndented($"events_Frm_{frm.Name.ClearName()}();");
            globalWriter.AppendLineIndented($"break;");
            
            globalWriter.currentIndent--;
        }

        globalWriter.EndBrace();
        
        globalWriter.EndBrace();
        
        
        globalWriter.EndBrace();
        return globalWriter.ToString();
    }
}