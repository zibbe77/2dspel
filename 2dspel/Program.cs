using System;
using Raylib_cs;

Raylib.InitWindow(Raylib.GetScreenWidth(), Raylib.GetScreenHeight(), "Hello World");
Raylib.SetTargetFPS(60);

float[] witchpos = new float[2];
Texture2D witchTexture = Raylib.LoadTexture(@"witch3.png");
Rectangle witchRect = new Rectangle(1000, 1000, witchTexture.width, witchTexture.height);

string pointsS = "0";
int points = 0;

Rectangle r2 = new Rectangle(Raylib.GetScreenWidth() / 2, Raylib.GetScreenHeight() / 2, 100, 100);
Rectangle r3 = new Rectangle(0, 0, Raylib.GetScreenWidth(), Raylib.GetScreenHeight());
Rectangle r4 = new Rectangle(Raylib.GetScreenWidth() / 2, Raylib.GetScreenHeight() / 2, 200, 200);
Font f1 = Raylib.LoadFont(@"Metrophobic.ttf");

while (!Raylib.WindowShouldClose())
{
    // rörelse
    witchRect = Toolbox.Poswitch(witchRect);

    //Collison 
    pointsS = Toolbox.Hitboxes(witchRect, r2, r3, r4, points, pointsS);

    //konventerar från float till int (texturer behöver ints)
    int x = (int)witchRect.x;
    int y = (int)witchRect.y;
    
    //ritar saker
    Raylib.BeginDrawing();

    Raylib.ClearBackground(Color.WHITE);
    Raylib.DrawRectangleRec(witchRect, Color.SKYBLUE);
    Raylib.DrawRectangleRec(r2, Color.RED);


    Raylib.DrawText(pointsS, 100, 50, 20, Color.ORANGE);

    Raylib.DrawTexture(witchTexture, x, y, Color.WHITE);

    Raylib.EndDrawing();


}