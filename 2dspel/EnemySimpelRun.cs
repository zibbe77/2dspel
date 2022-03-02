using System;
using System.Numerics;
using Raylib_cs;

public class EnemySimpelRun
{
    public Vector2 position = new Vector2();
    public Vector2 direction = new Vector2();
    public int hp = 3;
    public bool isAlive = true;
    public float speed = 5;
    public void Update()
    {
        
        /*
        foreach (Bullet b in Traking.bullets)
        {
            Rectangle refrens = new Rectangle((int)b.position.X, (int)b.position.Y, 20, 20);
            for(int i = 0 ;i > Traking.enemySimpelRuns.Count; ){
                bool areOverlapping = Raylib.CheckCollisionRecs(r1, refrens);
            }
        }
        */


    }
    public void Draw()
    {
        Raylib.DrawRectangle((int)position.X, (int)position.Y, 80, 80, Color.RED);
    }
}

public class EnemySimpelRunLogi
{
    public static void EnemySimpelRunSpawn()
    {
        (int x, int y) position = Mapbox.boxes[5];
        EnemySimpelRunCreat(position);
    }
    public static void EnemySimpelRunCreat((int x, int y) position){
        EnemySimpelRun E = new EnemySimpelRun();
        E.position.X = position.x;  
        E.position.Y = position.y;
        Traking.enemySimpelRuns.Add(E);
    }
    public static void EnemySimpelRunDraw (){
        foreach (EnemySimpelRun E in Traking.enemySimpelRuns)
        {
            E.Draw();
        }
    }
}
