using CTFAK;
using CTFAK.FileReaders;
using CTFAK.Memory;
using CTFAK.MMFParser.CCN;
using CTFAK.Utils;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;

namespace FusionX.Builder;

public class Program
{
    [DllImport("kernel32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static extern bool AllocConsole();

    [DllImport("kernel32.dll")]
    public static extern bool AttachConsole(int dwProcessId);

    public static void Main(string[] args)
    {
        if (AllocConsole())
        {
            AttachConsole(Process.GetCurrentProcess().Id);
            Console.SetOut(new StreamWriter(Console.OpenStandardOutput()));
        }
        Console.OutputEncoding = Encoding.UTF8;
        Console.InputEncoding = Encoding.UTF8;

        if (args.Length < 2) 
        {
            throw new ArgumentException("The program expects at least 2 arguments: [Input Path] [Output Path] (Build ID = 0)");
        }

        var inputPath = args[0];
        var outputPath = args[1];
        var buildId = args.Length >= 3 ? int.Parse(args[3]) : 0;

        Core.Init();
        Core.Parameters = "";
        
        var reader = new AutoFileReader();
        reader.LoadGame(inputPath);
        var code = GameConverter.Convert(reader.GetGameData());

        //Copy base
        var newBaseDir = testBuildFolder + @"\base";
        Directory.CreateDirectory(newBaseDir);
        Utils.Utils.Copy(basePath, newBaseDir);

        //Write code
        File.WriteAllText(Path.Combine(newBaseDir, "UserCode", "UserCodeEventProgram.cs"), code);

        //Build with dotnet
        var processStartInfo = new ProcessStartInfo();
        processStartInfo.WorkingDirectory = newBaseDir;
        processStartInfo.FileName = "dotnet";
        processStartInfo.Arguments = "publish -r win-x64 -c Release";
        processStartInfo.UseShellExecute = false;
        processStartInfo.RedirectStandardOutput = true;
        var proc = Process.Start(processStartInfo);
        proc.OutputDataReceived += (sender, args) => Console.WriteLine(args.Data);
        proc.BeginOutputReadLine();
        proc.WaitForExit();

        //Copy the build results
        var outputDirectory = testBuildFolder;
        File.Delete(Path.Combine(outputDirectory, "game.exe"));
        File.Copy(Path.Combine(newBaseDir, "bin", "Release", "net7.0", "win-x64", "native", "FusionX.exe"), Path.Combine(outputDirectory, "game.exe"));

        //Delete the build folder
        //Utils.DeleteDirectory(newBaseDir);

        //Copy dependencies
        Utils.Utils.Copy("dependencies", testBuildFolder);

        //Copy CCN
        File.Copy(inputPath, Path.Combine(outputDirectory, Path.GetFileName(inputPath)), true);
    }
}