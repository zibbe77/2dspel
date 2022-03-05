using System;
using System.Numerics;
using Raylib_cs;
using System.Collections.Generic;


public class EnemySimpelRun
{
    public Vector2 position = new Vector2();
    public Vector2 direction = new Vector2();
    public int hp = 3;
    public bool isAlive = true;
    public float speed = 5;
    public void Update()
    {
        //kollar om finder blir träfaded av skott
        foreach (Bullet b in Traking.bullets)
        {
            Rectangle refrens = new Rectangle((int)b.position.X, (int)b.position.Y, 20, 20);
            foreach (EnemySimpelRun E in Traking.enemySimpelRuns)
            {
                Rectangle refrensE = new Rectangle((int)E.position.X, (int)E.position.Y, 80, 80);
                bool areOverlapping = Raylib.CheckCollisionRecs(refrensE, refrens);
                if (areOverlapping == true)
                {
                    hp--;
                    b.isAlive = false;
                }
                if (hp == 0)
                {
                    isAlive = false;
                }
            }
        }
        //kollar om spelaren blir dödad
        foreach (EnemySimpelRun E in Traking.enemySimpelRuns)
        {
            Rectangle refrensE = new Rectangle((int)E.position.X, (int)E.position.Y, 80, 80);
            bool areOverlapping = Raylib.CheckCollisionRecs(refrensE, Player.witchRect);
            if (areOverlapping == true)
            {
                Player.isAlive = false;
            }
        }
    }
    public void Draw()
    {
        switch (hp)
        {
            case 3:
                Raylib.DrawRectangle((int)position.X, (int)position.Y, 80, 80, Color.RED);
                break;
            case 2:
                Raylib.DrawRectangle((int)position.X, (int)position.Y, 80, 80, Color.PURPLE);
                break;
            case 1:
                Raylib.DrawRectangle((int)position.X, (int)position.Y, 80, 80, Color.ORANGE);
                break;
        }

    }
    public static void EnemySimpelRunPathfinder()
    {
        foreach (EnemySimpelRun E in Traking.enemySimpelRuns)
        {
            
            (float x, int y) position = (0, 0);
            position.x = (E.position.X - (Mapbox.lostSpaceG[0] / 2)) / 100;
            System.Console.WriteLine(Mapbox.lostSpaceG[0]/ 2);
            System.Console.WriteLine($"positon räknad {position.x}");
            System.Console.WriteLine($"bas positon {E.position.X}");
            position.y = (int)E.position.Y / 100 - (Mapbox.lostSpaceG[1] / 2);

        }
    }
}

public class EnemySimpelRunLogi
{
    public static void EnemySimpelRunSpawn()
    {
        (int x, int y) position = Mapbox.boxes[10];
      

        EnemySimpelRunCreat(position);
    }
    public static void EnemySimpelRunCreat((int x, int y) position)
    {
        EnemySimpelRun E = new EnemySimpelRun();
        E.position.X = position.x * 100 + (Mapbox.lostSpaceG[0] / 2);
        E.position.Y = position.y * 100 + (Mapbox.lostSpaceG[1] / 2);

        Traking.enemySimpelRuns.Add(E);
    }
    public static void EnemySimpelRunDraw()
    {
        foreach (EnemySimpelRun E in Traking.enemySimpelRuns)
        {
            E.Draw();
        }
    }
    public static void EnemySimpelRunUpdate()
    {
        foreach (EnemySimpelRun E in Traking.enemySimpelRuns)
        {
            E.Update();
        }
        Traking.enemySimpelRuns.RemoveAll(E => E.isAlive == false);
    }
}