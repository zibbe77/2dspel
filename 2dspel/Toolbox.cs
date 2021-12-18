using System;
using Raylib_cs;
using System.Numerics;

public class Traking
{
    public int points = 0;
    public string pointsS = "";
    public bool[] picktUpR4 = new bool[5];
    public bool[] witchCheckX = { false, false };
    public bool[] witchCheckY = { false, false };
    public Rectangle witchRect = new Rectangle();

}
public class Toolbox
{
    public static Rectangle Poswitch(Rectangle witchRect, Rectangle border, Traking T1)
    {
        // Vector2 v = new Vector2(30, 70);
        for(int i = 0;i < 2;i++){
            T1.witchCheckX[i] = false;
            T1.witchCheckY[i] = false;
        }
        if (Raylib.IsKeyDown(KeyboardKey.KEY_LEFT))
        {
            witchRect.x--;
            T1.witchCheckX[0] = true;
        }
        if (Raylib.IsKeyDown(KeyboardKey.KEY_RIGHT))
        {
            witchRect.x++;
            T1.witchCheckX[1] = true;
        }
        if (Raylib.IsKeyDown(KeyboardKey.KEY_DOWN))
        {
            witchRect.y++;
            T1.witchCheckY[1] = true;
        }
        if (Raylib.IsKeyDown(KeyboardKey.KEY_UP))
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
    public static Traking Hitboxes(Rectangle r1, Rectangle r2, Rectangle r3, Rectangle[] points, Traking T1, Rectangle[] obstical)
    {
        bool areOverlapping = Raylib.CheckCollisionRecs(r1, r2); // true
        if (areOverlapping == true)
        {
            System.Console.WriteLine("areOverlapping");
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
                if(T1.witchCheckX[0] == true){T1.witchRect.x++;}
                if(T1.witchCheckX[1] == true){T1.witchRect.x--;}
                if(T1.witchCheckY[0] == true){T1.witchRect.y++;}
                if(T1.witchCheckY[1] == true){T1.witchRect.y--;}
                System.Console.WriteLine("möts");
            }
        }

        return T1;
    }
}