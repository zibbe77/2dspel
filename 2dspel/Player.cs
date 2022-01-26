using System;
using System.Numerics;
using Raylib_cs;

public class Player
{
    public Vector2 position = new Vector2();

    public void Update()
    {
        // Köra spelarens förflyttningskod etc
        if (Bullet.reloadtimer == 0)
        {
            Vector2 bulletDirection = new Vector2();
            if (Raylib.IsKeyDown(KeyboardKey.KEY_LEFT))
            {
                bulletDirection.X = -1;
                Bullet.reloadtimerbool = true;
            }
            if (Raylib.IsKeyDown(KeyboardKey.KEY_RIGHT))
            {
                bulletDirection.X = 1;
                Bullet.reloadtimerbool = true;
            }
            if (Raylib.IsKeyDown(KeyboardKey.KEY_DOWN))
            {
                bulletDirection.Y = 1;
                Bullet.reloadtimerbool = true;

            }
            if (Raylib.IsKeyDown(KeyboardKey.KEY_UP))
            {
                bulletDirection.Y = -1;
                Bullet.reloadtimerbool = true;
            }

            if (bulletDirection.Length() > 0)
            {
                Toolbox.FireBullet(position, Vector2.Normalize(bulletDirection));
            }
        }
        if (Bullet.reloadtimerbool == true)
        {
            Bullet.reloadtimer++;
            if (Bullet.reloadtimer == 60)
            {
                Bullet.reloadtimerbool = false;
                Bullet.reloadtimer = 0;
            }
        }
    }

    public void Draw()
    {
        // Rita ut spelaren
    }
}