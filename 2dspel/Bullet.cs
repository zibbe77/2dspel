using System;
using Raylib_cs;
using System.Numerics;
public class Bullet
{
    public Vector2 position = new Vector2();
    public Vector2 direction = new Vector2();
    public float speed = 5;
    public static float reloadtimer = 0;
    public static bool reloadtimerbool = false;
    public void Update()
    {
        position += direction * speed;
    }
    public void Draw()
    {
        Raylib.DrawRectangle((int)position.X, (int)position.Y, 20, 20, Color.RED);
    }
}