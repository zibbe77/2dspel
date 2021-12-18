using System;
using Raylib_cs;
using System.Numerics;

public class Traking
{
    public int points = 0;
    public string pointsS = "";
    public bool picktUpR4 = false;
}
public class Toolbox
{
    public static Rectangle Poswitch(Rectangle witchRect, Rectangle border)
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
        if(witchRect.x < border.x){
            witchRect.x++;
        }
        if(witchRect.x > border.width - witchRect.width){
            witchRect.x--;
        }
         if(witchRect.y < border.y){
            witchRect.y++;
        }
        if(witchRect.y > border.height - witchRect.height){
            witchRect.y--; 
        }
        return witchRect;
    }
    public static Traking Hitboxes(Rectangle r1, Rectangle r2, Rectangle r3, Rectangle points, Traking T1)
    {
        bool areOverlapping = Raylib.CheckCollisionRecs(r1, r2); // true
        if (areOverlapping == true)
        {
            System.Console.WriteLine("areOverlapping");
            Raylib.DrawRectangleRec(r3, Color.RED);
        }

        for (int i = 0; i < 5; i++)
        {
            bool areOverlapping2 = Raylib.CheckCollisionRecs(r1, points);
            if (T1.picktUpR4 == false && areOverlapping2 == false)
            {
                Raylib.DrawRectangleRec(points, Color.GREEN);
            }
            else if (T1.picktUpR4 == false)
            {
                T1.points++;
                T1.pointsS = T1.points.ToString();
                T1.picktUpR4 = true;
            }
        }

    return T1;
    }
}