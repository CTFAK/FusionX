using CTFAK;
using CTFAK.Memory;
using CTFAK.MMFParser.CCN;
using CTFAK.Utils;
using FusionX.Builder;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;
using Utils = FusionX.Builder.Utils.Utils;

public class Program
{
    [DllImport("kernel32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static extern bool AllocConsole();

    [DllImport("kernel32.dll")]
    public static extern bool AttachConsole(int dwProcessId);
    public const string basePath = @"D:\Repos\FusionX\FusionX\FusionX";
    public const string testBuildFolder = @"D:\FusionXTestBuild";
    public static void Main(string[] args)
    {

        AllocConsole();
        AttachConsole(Process.GetCurrentProcess().Id);
        Console.SetOut(new StreamWriter(Console.OpenStandardOutput()));
        Console.OutputEncoding = Encoding.UTF8;
        Console.InputEncoding = Encoding.UTF8;
        // Initialize CTFAK, Load the application and convert events

        var ccnPath = "Application.ccn";
        var outputPath = "D:\\FusionXTestBuild";

        if (args.Length > 1)
        {
            ccnPath = args[0];
        }

        Core.Init();
        Core.Parameters = "";
        Settings.Build = 284;
        Settings.gameType = Settings.GameType.CBM;
        var reader = new ByteReader(new FileStream(ccnPath, FileMode.Open));
        var gameData = new GameData();
        gameData.Read(reader);
        reader.Close();
        var code = GameConverter.Convert(gameData);

        //Copy base
        var newBaseDir = testBuildFolder + @"\base";
        Directory.CreateDirectory(newBaseDir);
        Utils.Copy(basePath, newBaseDir);

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
        Utils.Copy("dependencies", testBuildFolder);

        //Copy CCN
        File.Delete(Path.Combine(outputDirectory, "Application.ccn"));
        File.Copy(ccnPath, Path.Combine(outputDirectory, "Application.ccn"));
    }
}