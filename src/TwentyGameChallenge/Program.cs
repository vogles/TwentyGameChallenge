using TwentyGameChallenge.DungeonSlime;

namespace TwentyGameChallenge;

public static class Program
{
    public static void Main(string[] args)
    {
        using var game = new DungeonSlimeGame();
        game.Run();
    }
}
