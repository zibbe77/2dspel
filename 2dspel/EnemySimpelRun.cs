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
        // kolar hp och uptaterar färgen 
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
        //inte färdig kod för pathfinding 
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
        //slumpar ett positon som är hälften av kartan bort från dig. Men bara pitoner som är tomma.
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
        //skapar det programet behöver veta när en finde skapas alså postion. 
        EnemySimpelRun E = new EnemySimpelRun();
        E.position.X = position.x * 100 + (Mapbox.lostSpaceG[0] / 2) + 10;
        E.position.Y = position.y * 100 + (Mapbox.lostSpaceG[1] / 2) + 10;

        Traking.enemySimpelRuns.Add(E);
    }
    public static void EnemySimpelRunDraw()
    {
        //ritar alla finden (Finns bara en nu)
        foreach (EnemySimpelRun E in Traking.enemySimpelRuns)
        {
            E.Draw();
        }
    }
    public static void EnemySimpelRunUpdate()
    {
        //går igenom och uptaterar alla finder
        foreach (EnemySimpelRun E in Traking.enemySimpelRuns)
        {
            E.Update();
        }
        //tar bort dom från en lista om dom är döda. 
        Traking.enemySimpelRuns.RemoveAll(E => E.isAlive == false);
    }
}