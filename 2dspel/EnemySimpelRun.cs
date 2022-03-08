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

            (int x, int y) position = (0, 0);
            position.x = Convert.ToInt32(E.position.X);
            position.y = Convert.ToInt32(E.position.Y);

            List<(int x, int y)> Que = new List<(int, int)>();
            Que.Add(position);

            //foreach (Tuple(x, y) in Mapbox.boxes)
            // {

            //}
        }
    }
}

public class EnemySimpelRunLogi
{
    public static void EnemySimpelRunSpawn()
    {
        if (Traking.enemySimpelRuns.Count == 0)
        {
            Random generator = new Random();
            int r = generator.Next(Mapbox.boxes.Count / 2, Mapbox.boxes.Count);
            (int x, int y) position = Mapbox.boxes[r];

            EnemySimpelRunCreat(position);
        }
    }
    public static void EnemySimpelRunCreat((int x, int y) position)
    {
        EnemySimpelRun E = new EnemySimpelRun();
        E.position.X = position.x * 100 + (Mapbox.lostSpaceG[0] / 2) + 10;
        E.position.Y = position.y * 100 + (Mapbox.lostSpaceG[1] / 2) + 10;

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