using System.Runtime.InteropServices;

namespace Game;

internal static class Program
{
    private static void Main()
    {
        using var game = new MainWindow();
        game.Run();
    }
}
