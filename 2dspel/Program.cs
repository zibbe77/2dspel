//setings till fönsteret 
using System;
using Raylib_cs;
using System.Numerics;
using System.Media;

Raylib.InitWindow(Raylib.GetScreenWidth(), Raylib.GetScreenHeight(), "Hello World");
Raylib.SetTargetFPS(60);

//play sound
// SoundPlayer player = new SoundPlayer(@"Harvest Dawn.wav");
// player.Play();

//skapar en spelar
Player p1 = new Player();

//witch varjablar
float[] witchpos = new float[2];
Texture2D witchTexture = Raylib.LoadTexture(@"witch3.png");
witchTexture.height = 80;
witchTexture.width = 80;

//skapar massa varjablar
Traking T1 = new Traking();

//bestämer att en av t1 varjablar är lika med en annan recktangel
Rectangle witchRect1 = new Rectangle(0, -50, witchTexture.width, witchTexture.height);
T1.witchRect = witchRect1;

Rectangle[] points = new Rectangle[5];
Rectangle[] obstical = new Rectangle[4];

//räknar på hur stor kartar ska vara 
Rectangle border = new Rectangle(0, 0, Raylib.GetScreenWidth(), Raylib.GetScreenHeight());
double floor = Math.Floor(border.width / 100.0) * 100.0;
double floor1 = Math.Floor(border.height / 100.0) * 100.0;
int[] mapSize = new int[2];

//konverterar double till int
mapSize[0] = Convert.ToInt32(floor);
mapSize[1] = Convert.ToInt32(floor1);
int[,] grid = new int[mapSize[0], mapSize[1]];

//skappare mappen
bool mapOkej = false;
while(mapOkej == false){
grid = Mapbox.MapCreat(mapSize);
mapOkej = Mapbox.MapControl(grid, mapSize);
}

// mapp grafik
int[] lostSpace = new int[2];

lostSpace = Mapbox.SideBox(mapSize, border);
Rectangle[] borderC = Mapbox.MapBorderCreat(lostSpace);
obstical = Mapbox.MapPlace(lostSpace, grid, mapSize);

for (int i = 0; i < 5; i++)
{
    Rectangle pointsRefrens = new Rectangle(Raylib.GetScreenWidth() / 3, Raylib.GetScreenHeight() / 2 + i * 40, 20, 20);
    points[i] = pointsRefrens;
    T1.picktUpR4[i] = false;
}

Font f1 = Raylib.LoadFont(@"Metrophobic.ttf");

while (!Raylib.WindowShouldClose())
{
    // rörelse
    T1.witchRect = Toolbox.Poswitch(T1.witchRect, border, T1, lostSpace);

    //sjuta 
    Toolbox.UpdateBullets();

    //Collison 
    T1 = Toolbox.Hitboxes(T1.witchRect, points, T1, obstical);

    //konventerar från float till int (texturer behöver ints)
    int x = (int)T1.witchRect.x;
    int y = (int)T1.witchRect.y;
    p1.position.X = T1.witchRect.x + 35;
    p1.position.Y = T1.witchRect.y + 35;
    p1.Update();

    //ritar saker
    Raylib.BeginDrawing();

    Mapbox.MapBorderDraw(borderC);

    Raylib.ClearBackground(Color.WHITE);

    Raylib.DrawText(T1.pointsS, 100, 50, 20, Color.ORANGE);

    Raylib.DrawTexture(witchTexture, x, y, Color.WHITE);
    Toolbox.DrawBullets();

    Raylib.EndDrawing();
}