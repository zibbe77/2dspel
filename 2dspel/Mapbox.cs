using System;
using Raylib_cs;
using System.Numerics;

public class Mapbox
{
    public static void Mapcreat(int[] mapsize)
    {
        Random generator = new Random();

        int[,] grid = new int[mapsize[0], mapsize[1]];

        for (int i = 0; i > mapsize[0]; i++)
        {
            for (int ii = 0; i > mapsize[1]; ii++)
            {
                int r = generator.Next(0, 1);
                r = grid[i, ii];
            }
        }
    }
}