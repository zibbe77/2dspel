using System;
using Raylib_cs;

Raylib.InitWindow(Raylib.GetScreenWidth(), Raylib.GetScreenHeight(), "Hello World");
Raylib.SetTargetFPS(60);

float[] witchpos = new float[2];

while (!Raylib.WindowShouldClose())
{
    //skapa
    Texture2D witchTexture = Raylib.LoadTexture(@"witch3.png");
    Rectangle r1 = new Rectangle(witchpos[0], witchpos[1], witchTexture.width, witchTexture.height);
    Rectangle r2 = new Rectangle(1000,1000,100,100);
    //ritar saker
    Raylib.BeginDrawing();

    Raylib.ClearBackground(Color.WHITE);
    Raylib.DrawRectangleRec(r1, Color.SKYBLUE);
    Raylib.DrawRectangleRec(r2, Color.RED);

    //konventerar från float till int (texturer behöver ints)
    int x = (int)witchpos[0];
    int y = (int)witchpos[1];

    Raylib.DrawTexture(witchTexture, x, y, Color.WHITE);

    Raylib.EndDrawing();

    // rörelse
    Toolbox.Poswitch(witchpos);
    
    //Collison 
    Toolbox.Hitboxes(r1,r2);

}