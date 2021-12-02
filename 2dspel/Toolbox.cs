using System;
using Raylib_cs;
using System.Numerics;
public class Toolbox
{
    public static float[] Poswitch(float[] witchpos)
    {
        // Vector2 v = new Vector2(30, 70);


        if (Raylib.IsKeyDown(KeyboardKey.KEY_LEFT))
        {
            witchpos[0]--;
        }
        if (Raylib.IsKeyDown(KeyboardKey.KEY_RIGHT))
        {
            witchpos[0]++;
        }
        if (Raylib.IsKeyDown(KeyboardKey.KEY_DOWN))
        {
            witchpos[1]++;
        }
        if (Raylib.IsKeyDown(KeyboardKey.KEY_UP))
        {
            witchpos[1]--;
        }
        return witchpos;
    }
}
