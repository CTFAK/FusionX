using CTFAK.MMFParser.CCN.Chunks.Frame;
using FusionX.Builder.Utils;

namespace FusionX.Builder;

public class FrameConverter
{
    public static void Convert(Frame frame,CodeWriter code,CodeWriter variables)
    {
        // Generate event code. Can be potentially optimized by using identical code for identical events
        for (int i = 0; i < frame.Events.Items.Count; i++)
        {
            code.AppendLineIndented($"void eventGroup_{frame.Name.ClearName()}_{i}()");
            code.StartBrace();
            var evGrp = frame.Events.Items[i];
            EventGroupConverter.Convert(evGrp,code,variables);
            code.EndBrace();
        }
        // Frame loop. 
        code.AppendLineIndented($"void events_Frm_{frame.Name.ClearName()}()");
        code.StartBrace();
        for (int i = 0; i < frame.Events.Items.Count; i++)
        {
            code.AppendLineIndented($"eventGroup_{frame.Name.ClearName()}_{i}();");
        }
        code.EndBrace();
        
    }
}