using EventPreprocessor;

namespace FusionX.Builder.Utils;

public static class Utils
{
    public static string GetActionName(int objectType, int num)
    {
        if (ActionNames.systemDict.ContainsKey(objectType))
        {
            if (ActionNames.systemDict[objectType].ContainsKey(num))
                return $"{ActionNames.systemDict[objectType][num]}";
        }
        else
        {
            if (ActionNames.extensionDict.ContainsKey(num))
                return $"{ActionNames.extensionDict[num]}";
        }

        return "Unknown";
    }
    public static string GetConditionName(int objectType, int num)
    {
        if (ConditionNames.ConditionSystemDict.ContainsKey(objectType))
        {
            if (ConditionNames.ConditionSystemDict[objectType].ContainsKey(num))
                return ConditionNames.ConditionSystemDict[objectType][num];
        }

        return "Unknown";
    }

    public static int uniqueCache;
    public static int GetUniqueNumber()
    {
        return uniqueCache++;
    }

    public static string ClearName(this string data)
    {
        return CTFAK.Utils.Utils.ClearName(data).Replace(" ", "_");
    }
    public static void Copy(string sourceDirectory, string targetDirectory)
    {
        DirectoryInfo diSource = new(sourceDirectory);
        DirectoryInfo diTarget = new(targetDirectory);

        CopyAll(diSource, diTarget);
    }

    public static void CopyAll(DirectoryInfo source, DirectoryInfo target)
    {
        Directory.CreateDirectory(target.FullName);

        // Copy each file into the new directory.
        foreach (FileInfo fi in source.GetFiles())
        {
            Console.WriteLine(@"Copying {0}\{1}", target.FullName, fi.Name);
            fi.CopyTo(Path.Combine(target.FullName, fi.Name), true);
        }

        // Copy each subdirectory using recursion.
        foreach (DirectoryInfo diSourceSubDir in source.GetDirectories())
        {
            DirectoryInfo nextTargetSubDir =
                target.CreateSubdirectory(diSourceSubDir.Name);
            CopyAll(diSourceSubDir, nextTargetSubDir);
        }
    }
    public static void DeleteDirectory(string target_dir)
    {
        string[] files = Directory.GetFiles(target_dir);
        string[] dirs = Directory.GetDirectories(target_dir);

        foreach (string file in files)
        {
            File.SetAttributes(file, FileAttributes.Normal);
            File.Delete(file);
        }

        foreach (string dir in dirs)
        {
            DeleteDirectory(dir);
        }

        Directory.Delete(target_dir, false);
    }
}