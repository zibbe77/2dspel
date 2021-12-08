using System;
using Raylib_cs;
using System.Numerics;
public class Toolbox
{
    public static Rectangle Poswitch(Rectangle witchRect)
    {
        // Vector2 v = new Vector2(30, 70);

        if (Raylib.IsKeyDown(KeyboardKey.KEY_LEFT))
        {
            witchRect.x--;
        }
        if (Raylib.IsKeyDown(KeyboardKey.KEY_RIGHT))
        {
            witchRect.x++;
        }
        if (Raylib.IsKeyDown(KeyboardKey.KEY_DOWN))
        {
            witchRect.y++;
        }
        if (Raylib.IsKeyDown(KeyboardKey.KEY_UP))
        {
            witchRect.y--;
        }

        //förhindrar att gå utan för kartan

        
        return witchRect;
    }
    public static string Hitboxes(Rectangle r1, Rectangle r2, Rectangle r3, Rectangle r4, int points, string pointsS)
    {
        bool picktUp = false;
        bool areOverlapping = Raylib.CheckCollisionRecs(r1, r2); // true
        if (areOverlapping == true)
        {
            System.Console.WriteLine("areOverlapping");
            Raylib.DrawRectangleRec(r3, Color.RED);
        }
        bool areOverlapping2 = Raylib.CheckCollisionRecs(r1, r4);
        if (areOverlapping2 != true && picktUp == false)
        {
            Raylib.DrawRectangleRec(r4, Color.GREEN);
        }
        else if(picktUp == false){
            points++; 
            pointsS = points.ToString();
            picktUp = true; 
        }

        return pointsS;
    }
}