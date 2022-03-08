using System;
using Raylib_cs;
using System.Numerics;
using System.Collections.Generic;


public class Mapbox
{
    public static int blocks = 0;
    public static int trysMap = 0;
    public static Rectangle[] obstical = new Rectangle[4];
    public static List<(int x, int y)> boxes = new List<(int, int)>();
    public static int[] lostSpaceG = new int[2];

    public static int[,] MapCreat(int[] mapSize)
    {
        // slumpar utt en vilka block som är otiljänliga 
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
    public static int[] Mapsize(Rectangle border)
    {
        //räknar hur stor kartan är 
        double floor = Math.Floor(border.width / 100.0) * 100.0;
        double floor1 = Math.Floor(border.height / 100.0) * 100.0;
        int[] mapSize = new int[2];

        //konverterar double till int
        mapSize[0] = Convert.ToInt32(floor);
        mapSize[1] = Convert.ToInt32(floor1);
        return mapSize;
    }
    public static bool MapControl(int[,] grid, int[] mapSize)
    {
        //testar om kartan är okej att spella på 
        bool test = false;
        bool mapOkej = false;
        int blockCount = 0;

        // lista av block som är kvar att kolla
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
                    Mapbox.boxes.Add((gridP.x + 1, gridP.y));
                    blockCount++;
                }
            }
            catch { }
            try
            {
                if (grid[gridP.x, gridP.y + 1] == 0)
                {
                    grid[gridP.x, gridP.y + 1] = 2;
                    Que.Add((gridP.x, gridP.y + 1));
                    Mapbox.boxes.Add((gridP.x, gridP.y + 1));
                    blockCount++;
                }
            }
            catch { }
            try
            {
                if (grid[gridP.x - 1, gridP.y] == 0)
                {
                    grid[gridP.x - 1, gridP.y] = 2;
                    Que.Add((gridP.x - 1, gridP.y));
                    Mapbox.boxes.Add((gridP.x - 1, gridP.y));
                    blockCount++;
                }
            }
            catch { }
            try
            {
                if (grid[gridP.x, gridP.y - 1] == 0)
                {
                    grid[gridP.x, gridP.y - 1] = 2;
                    Que.Add((gridP.x, gridP.y - 1));
                    Mapbox.boxes.Add((gridP.x, gridP.y - 1));
                    blockCount++;
                }
            }
            catch { }
            Que.RemoveAt(0);

            //när listan är tom så ska den kolla om den är bra nu är den satt på 70% ska vara tilljänlig 
            if (Que.Count == 0)
            {
                System.Console.WriteLine($"{blockCount} antal block");
                int filler = mapSize[0] / 100;
                filler *= mapSize[1] / 100;

                float fillerTwo = (float)blockCount / (float)filler;
                System.Console.WriteLine($"{fillerTwo} antal prosent block");

                if (fillerTwo > 0.7)
                {
                    mapOkej = true;
                    System.Console.WriteLine($"antal försk för att göra en map {trysMap}");
                }
                else
                {
                    trysMap++;
                    Mapbox.blocks = 0;
                    Mapbox.boxes.Clear();
                }
                test = true;
            }
        }
        return mapOkej;
    }

    public static int[] SideBox(int[] mapSize, Rectangle border)
    {
        //räknar på hur mycket som fins kvar på sidan 
        int[] lostSpace = new int[2];
        lostSpace[0] = (int)border.width - mapSize[0];
        lostSpace[1] = (int)border.height - mapSize[1];
        Mapbox.lostSpaceG = lostSpace;
        return lostSpace;
    }
    public static Rectangle[] MapPlace(int[] lostSpace, int[,] grid, int[] mapSize)
    {
        //kollar visa positoner ska blockas och sedan skapar en mall 
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
        //skapar den svart kanten
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
        //ritar den svart kanten
        for (int i = 0; i < 4; i++)
        {
            Raylib.DrawRectangleRec(borderC[i], Color.BLACK);
        }
    }
}