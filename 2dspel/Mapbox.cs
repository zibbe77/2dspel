using System;
using Raylib_cs;
using System.Numerics;
using System.Collections.Generic;


public class Mapbox
{
    public static int blocks = 0;

    public static int trysMap = 0;

    public static int[,] MapCreat(int[] mapSize)
    {
        Random generator = new Random();
        int[,] grid = new int[mapSize[0] / 100, mapSize[1] / 100];

        for (int i = 0; i < grid.GetLength(0); i++)
        {
            for (int ii = 0; ii < grid.GetLength(1); ii++)
            {
                int r = generator.Next(0, 3);
                switch (r)
                {
                    case 1:
                        grid[i, ii] = r;
                        break;
                    default:
                        grid[i, ii] = 0;
                        break;
                }
                if (grid[i, ii] == 1)
                {
                    blocks++;
                }
            }
        }
        grid[0, 0] = 0;
        return grid;
    }

    public static bool MapControl(int[,] grid, int[] mapSize)
    {   
        bool test = false;
        bool mapOkej = false;
        int blockCount = 0;
        List<(int x, int y)> Que = new List<(int, int)>();

        (int x, int y) gridP = (0, 0);
        Que.Add((0, 0));

        while (test == false)
        {
            gridP = Que[0];
            try
            {
                if (grid[gridP.x + 1, gridP.y] == 0)
                {
                    grid[gridP.x + 1, gridP.y] = 2;
                    Que.Add((gridP.x + 1, gridP.y));
                    blockCount++;
                }
            }
            catch 
            {
                Console.WriteLine("utanför");
            }
            try
            {
                if (grid[gridP.x, gridP.y + 1] == 0)
                {
                    grid[gridP.x, gridP.y + 1] = 2;
                    Que.Add((gridP.x, gridP.y + 1));
                    blockCount++;
                }
            }
            catch 
            {
                Console.WriteLine("Utanför");
            }
            try
            {
                if (grid[gridP.x - 1, gridP.y] == 0)
                {
                    grid[gridP.x - 1, gridP.y] = 2;
                    Que.Add((gridP.x - 1, gridP.y));
                    blockCount++;
                }
            }
            catch 
            {
                Console.WriteLine("utanför");
            }
            try
            {
                if (grid[gridP.x, gridP.y - 1] == 0)
                {
                    grid[gridP.x, gridP.y - 1] = 2;
                    Que.Add((gridP.x, gridP.y - 1));
                    blockCount++;
                }
            }
            catch 
            {
                Console.WriteLine("Utanför");
            }
        
           Que.RemoveAt(0);
            

            if(Que.Count == 0){
                System.Console.WriteLine($"{blockCount} antal block");
                int filler = mapSize[0] / 100;
                filler *= mapSize[1] / 100;
                
                float fillerTwo = (float)blockCount / (float)filler;
                System.Console.WriteLine($"{fillerTwo} antal prosent block");
                
                if (fillerTwo > 0.7){
                    mapOkej = true;
                    System.Console.WriteLine($"antal försk för att göra en map {trysMap}");
                } else {
                    trysMap++;
                }
                test = true;
            }
        }
        return mapOkej;
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
        int i = 0;

        for (int x = 0; x < grid.GetLength(0); x++)
        {
            for (int y = 0; y < grid.GetLength(1); y++)
            {
                if (grid[x, y] == 1)
                {
                    Rectangle obsticalRefrens = new Rectangle(x * 100 + lostSpace[0] / 2, y * 100 + lostSpace[1] / 2, 100, 100);
                    obstical[i] = obsticalRefrens;
                    i++;
                }
            }
        }
        return obstical;
    }

    public static Rectangle[] MapBorderCreat(int[] lostSpace)
    {

        Rectangle[] borderC = new Rectangle[4]
        {
        new Rectangle(0, 0, Raylib.GetScreenWidth(), lostSpace[1] / 2),
        new Rectangle(0, 0, lostSpace[0] / 2, Raylib.GetScreenHeight()),
        new Rectangle(Raylib.GetScreenWidth(), Raylib.GetScreenHeight(), -Raylib.GetScreenWidth(), -lostSpace[1] / 2),
        new Rectangle(Raylib.GetScreenWidth(), Raylib.GetScreenHeight(), -lostSpace[0]/ 2, -Raylib.GetScreenHeight())
        };
        return borderC;
    }
    public static void MapBorderDraw(Rectangle[] borderC)
    {

        for (int i = 0; i < 4; i++)
        {
            Raylib.DrawRectangleRec(borderC[i], Color.BLACK);
        }
    }
}