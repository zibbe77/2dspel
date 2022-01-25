//setings till fönsteret 
using System;
using Raylib_cs;
using System.Numerics;
Raylib.InitWindow(Raylib.GetScreenWidth(), Raylib.GetScreenHeight(), "Hello World");
Raylib.SetTargetFPS(60);


//witch varjablar
float[] witchpos = new float[2];
Texture2D witchTexture = Raylib.LoadTexture(@"witch3.png");
witchTexture.height = 200;
witchTexture.width = 200;

Traking T1 = new Traking();
//bestämer att en av t1 varjablar är lika med en annan recktangel
Rectangle witchRect1 = new Rectangle(1000, 1000, witchTexture.width, witchTexture.height);
T1.witchRect = witchRect1;

Rectangle r2 = new Rectangle(Raylib.GetScreenWidth() / 2, Raylib.GetScreenHeight() / 2, 100, 100);
Rectangle r3 = new Rectangle(0, 0, Raylib.GetScreenWidth(), Raylib.GetScreenHeight());
Rectangle[] points = new Rectangle[5];
Rectangle[] obstical = new Rectangle[5];

//räknar på hur stor kartar ska vara 
Rectangle border = new Rectangle(0, 0, Raylib.GetScreenWidth(), Raylib.GetScreenHeight());
double floor = Math.Floor(border.width / 100.0) * 100.0;
double floor1 = Math.Floor(border.height / 100.0) * 100.0;
int[] mapSize = new int[2];
//glömt vad den gör 
mapSize[0] = Convert.ToInt32(floor);
mapSize[1] = Convert.ToInt32(floor1);

//ger värden på flera obijekt 
for(int i = 0; i < 5; i++){
    Rectangle obsticalRefrens = new Rectangle(Raylib.GetScreenWidth() / 4, Raylib.GetScreenHeight() / 2 + i * 100, 100, 100);
    obstical[i] = obsticalRefrens;
}

for (int i = 0; i < 5; i++){
    Rectangle pointsRefrens = new Rectangle(Raylib.GetScreenWidth() / 3, Raylib.GetScreenHeight() / 2 + i * 40, 20, 20);
    points[i] = pointsRefrens;
    T1.picktUpR4[i] = false;
}

Font f1 = Raylib.LoadFont(@"Metrophobic.ttf");

while (!Raylib.WindowShouldClose())
{
    // rörelse
    T1.witchRect = Toolbox.Poswitch(T1.witchRect, border, T1);

    //Collison 
    T1 = Toolbox.Hitboxes(T1.witchRect, r2, r3, points, T1, obstical);

    //konventerar från float till int (texturer behöver ints)
    int x = (int)T1.witchRect.x;
    int y = (int)T1.witchRect.y;
    
    //ritar saker
    Raylib.BeginDrawing();

    Raylib.ClearBackground(Color.WHITE);
    Raylib.DrawRectangleRec(T1.witchRect, Color.SKYBLUE);
    Raylib.DrawRectangleRec(r2, Color.RED);

    Raylib.DrawText(T1.pointsS, 100, 50, 20, Color.ORANGE);

    Raylib.DrawTexture(witchTexture, x, y, Color.WHITE);

    Raylib.EndDrawing();
}