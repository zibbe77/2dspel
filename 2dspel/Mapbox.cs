using System;
using Raylib_cs;
using System.Numerics;

public class Mapbox
{

    public static int blocks = 0;
    public static int[,] Mapcreat(int[] mapSize)
    {
        Random generator = new Random();
        int[,] grid = new int[mapSize[0] / 100, mapSize[1] / 100];

        for (int i = 0; i < mapSize[0] / 100; i++)
        {
            for (int ii = 0; ii < mapSize[1] / 100; ii++)
            {
                int r = generator.Next(0, 2);
                grid[i, ii] = r;
                if (grid[i, ii] == 1)
                {
                    blocks++;
                }
            }
        }
        return grid;

    }

    public static int[] SideBox(int[] mapSize, Rectangle border)
    {
        int[] lostSpace = new int[2];
        lostSpace[0] = (int)border.width - mapSize[0];
        lostSpace[1] = (int)border.height - mapSize[1];

        return lostSpace;
    }
    public static Rectangle[] MapPlace(int[] lostSpace, int[,] grid, int[] mapSize)
    {
        Rectangle[] obstical = new Rectangle[blocks];

        for (int y = 0; y < grid.GetLength(0); y++)
        {
            for (int x = 0; x < grid.GetLength(1); x++)
            {
                if (grid[y, x] == 1)
                {
                    Rectangle obsticalRefrens = new Rectangle(x * 100, y * 100, 100, 100);
                    obstical[x] = obsticalRefrens;
                }
            }
        }
        return obstical;
    }
}