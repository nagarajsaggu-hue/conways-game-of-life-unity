using UnityEngine;

public class PatternManager : MonoBehaviour
{
    private static int SCREEN_WIDTH = 40;
    private static int SCREEN_HEIGHT = 40;

    public static void Pattern1(Cell[,] grid, int startX, int startY)
    {
        BatchSetAlive(grid, startX, startY,
            (19, 19), (20, 20), (19, 20),(20,19));
    }

    public static void Pattern2(Cell[,] grid, int startX, int startY)
    {
        BatchSetAlive(grid, startX, startY,
            (19, 19), (20, 20), (20, 21), (20, 22),
            (19, 22), (18, 22), (17, 22), (16, 21), (16, 19));
    }

    public static void Pattern3(Cell[,] grid, int startX, int startY)
    {
        BatchSetAlive(grid, startX, startY,
            (19, 17), (19, 16), (18, 16), (18, 17),
            (19, 21), (20, 21), (21, 21), (19, 22), (20, 23));
    }

    private static void BatchSetAlive(Cell[,] grid, int startX, int startY, params (int, int)[] positions)
    {
        foreach (var (x, y) in positions)
        {
            grid[startX + x, startY + y].SetState(true);
        }
    }
}
