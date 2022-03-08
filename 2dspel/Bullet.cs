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
    public bool isAlive = true;

    public void Update()
    {
        // räknar utt hur skot ska åka 
        position += direction * speed;

        //kolar om den är utanför skärmen 
        if (position.X > Raylib.GetScreenWidth())
        {
            isAlive = false;
        }
        if (position.X < 0)
        {
            isAlive = false;
        }
        if (position.Y > Raylib.GetScreenHeight())
        {
            isAlive = false;
        }
        if (position.Y < 0)
        {
            isAlive = false;
        }
        //kolar om den krokar med blocks på kartan 
        for (int i = 0; i < Mapbox.blocks; i++)
        {
            Rectangle refrens = new Rectangle((int)position.X, (int)position.Y, 20, 20);
            bool areOverlapping = Raylib.CheckCollisionRecs(refrens, Mapbox.obstical[i]);
            if (areOverlapping == true)
            {
                isAlive = false;
            }
        }
    }
    public void Draw()
    {
        Raylib.DrawRectangle((int)position.X, (int)position.Y, 20, 20, Color.GRAY);
    }
}