namespace FusionX;

public class Program
{
    public static void Main(string[] args)
    {
        using (var game = new MainWindow())
        {
            game.Run();
        }
    }
}