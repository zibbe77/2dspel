using System;
using Raylib_cs;
using System.Numerics;
using System.Collections.Generic;

public class Traking
{
    public int points = 0;
    public string pointsS = "";
    public bool[] picktUpR4 = new bool[5];
    public bool[] witchCheckX = { false, false };
    public bool[] witchCheckY = { false, false };
    public Rectangle witchRect = new Rectangle();
    public int[,] buletPos = new int[100, 100];
    public bool[,] buletDirection = new bool[100, 4];
    public int buletNum = 0;
}
public class Toolbox
{
    public static Rectangle Poswitch(Rectangle witchRect, Rectangle border, Traking T1)
    {
        // Vector2 v = new Vector2(30, 70);
        for (int i = 0; i < 2; i++)
        {
            T1.witchCheckX[i] = false;
            T1.witchCheckY[i] = false;
        }
        if (Raylib.IsKeyDown(KeyboardKey.KEY_A))
        {
            witchRect.x--;
            T1.witchCheckX[0] = true;
        }
        if (Raylib.IsKeyDown(KeyboardKey.KEY_D))
        {
            witchRect.x++;
            T1.witchCheckX[1] = true;
        }
        if (Raylib.IsKeyDown(KeyboardKey.KEY_S))
        {
            witchRect.y++;
            T1.witchCheckY[1] = true;
        }
        if (Raylib.IsKeyDown(KeyboardKey.KEY_W))
        {
            witchRect.y--;
            T1.witchCheckY[0] = true;
        }

        //förhindrar att gå utan för kartan
        if (witchRect.x < border.x)
        {
            witchRect.x++;
            T1.witchCheckX[1] = false;
        }
        if (witchRect.x > border.width - witchRect.width)
        {
            witchRect.x--;
            T1.witchCheckX[0] = false;
        }
        if (witchRect.y < border.y)
        {
            witchRect.y++;
            T1.witchCheckY[1] = false;
        }
        if (witchRect.y > border.height - witchRect.height)
        {
            witchRect.y--;
            T1.witchCheckY[0] = false;
        }
        return witchRect;
    }
    public static Traking BuletMove(Traking T1)
    {
        //bulet
        bool buletNumBool = false;

        if (Raylib.IsKeyDown(KeyboardKey.KEY_LEFT))
        {
            T1.buletDirection[T1.buletNum, 0] = true;
        }
        if (Raylib.IsKeyDown(KeyboardKey.KEY_RIGHT))
        {
            T1.buletDirection[T1.buletNum, 1] = true;
        }
        if (Raylib.IsKeyDown(KeyboardKey.KEY_DOWN))
        {
            T1.buletDirection[T1.buletNum, 2] = true;
        }
        if (Raylib.IsKeyDown(KeyboardKey.KEY_UP))
        {
            T1.buletDirection[T1.buletNum, 3] = true;
        }
        for (int i = 0; i > 4; i++)
        {
            if (T1.buletDirection[T1.buletNum, i] == false)
            {
                buletNumBool = true;
            }
        }
        if (buletNumBool == true)
        {
            T1.buletNum++;
        }
        return T1;
    }
    public static Traking Hitboxes(Rectangle r1, Rectangle r2, Rectangle r3, Rectangle[] points, Traking T1, Rectangle[] obstical)
    {
        bool areOverlapping = Raylib.CheckCollisionRecs(r1, r2); // true
        if (areOverlapping == true)
        {
            Raylib.DrawRectangleRec(r3, Color.RED);
        }

        for (int i = 0; i < 5; i++)
        {
            bool areOverlapping2 = Raylib.CheckCollisionRecs(r1, points[i]);
            if (T1.picktUpR4[i] == false && areOverlapping2 == false)
            {
                Raylib.DrawRectangleRec(points[i], Color.GREEN);
            }
            else if (T1.picktUpR4[i] == false)
            {
                T1.points++;
                T1.pointsS = T1.points.ToString();
                T1.picktUpR4[i] = true;
            }
        }
        for (int i = 0; i < 5; i++)
        {
            Raylib.DrawRectangleRec(obstical[i], Color.BLACK);
            bool areOverlapping3 = Raylib.CheckCollisionRecs(r1, obstical[i]);
            if (areOverlapping3 == true)
            {
                if (T1.witchCheckX[0] == true) { T1.witchRect.x++; }
                if (T1.witchCheckX[1] == true) { T1.witchRect.x--; }
                if (T1.witchCheckY[0] == true) { T1.witchRect.y++; }
                if (T1.witchCheckY[1] == true) { T1.witchRect.y--; }
                System.Console.WriteLine("möts");
            }
        }
        for (int i = 0; i < 100; i++)
        {
            Raylib.DrawRectangleRec(T1.buletPos, Color.RED);
        }



        return T1;
    }
    public int bulet(Traking T1)
    {
        for (int i = 0; i < 100; i++)
        {
            if (T1.buletDirection[i, 0] == true) {T1.buletPos[100, 100]++; }
            if (T1.buletDirection[i, 1] == true) { }
            if (T1.buletDirection[i, 2] == true) { }
            if (T1.buletDirection[i, 3] == true) { }
        }

        return 0;
    }
}