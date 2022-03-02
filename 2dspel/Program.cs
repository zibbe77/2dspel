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

//räknar på hur stor kartar ska vara 
Rectangle border = new Rectangle(0, 0, Raylib.GetScreenWidth(), Raylib.GetScreenHeight());
double floor = Math.Floor(border.width / 100.0) * 100.0;
double floor1 = Math.Floor(border.height / 100.0) * 100.0;
int[] mapSize = new int[2];

//konverterar double till int
mapSize[0] = Convert.ToInt32(floor);
mapSize[1] = Convert.ToInt32(floor1);
int[,] grid = new int[mapSize[0], mapSize[1]];



for (int i = 0; i < 5; i++)
{
    Rectangle pointsRefrens = new Rectangle(Raylib.GetScreenWidth() / 3, Raylib.GetScreenHeight() / 2 + i * 40, 20, 20);
    points[i] = pointsRefrens;
    T1.picktUpR4[i] = false;
}

Font f1 = Raylib.LoadFont(@"Metrophobic.ttf");

//för switchen ska fungera
int gameState = 0;
// nåra varjablar som behvde ut ur switchen
Rectangle[] borderC = new Rectangle[4];
int[] lostSpace = new int[2];
while (!Raylib.WindowShouldClose())
{
    switch (gameState)
    {
        case 0:
            bool mapOkej = false;
            while (mapOkej == false)
            {
            Raylib.BeginDrawing();
            Raylib.ClearBackground(Color.WHITE);
            Raylib.DrawText("LOADING", 0, Raylib.GetScreenHeight() / 2 - 300, 400, Color.BLACK);
            Raylib.EndDrawing();
            //skappare mappen
           
                grid = Mapbox.MapCreat(mapSize);
                mapOkej = Mapbox.MapControl(grid, mapSize);
            }
            gameState++;

            // mapp grafik
            lostSpace = Mapbox.SideBox(mapSize, border);
            borderC = Mapbox.MapBorderCreat(lostSpace);
            Mapbox.obstical = Mapbox.MapPlace(lostSpace, grid, mapSize);
            
            EnemySimpelRunLogi.EnemySimpelRunSpawn();
            break;
        case 1:
            // rörelse
            T1.witchRect = Toolbox.Poswitch(T1.witchRect, border, T1, lostSpace);

            //sjuta 
            Toolbox.UpdateBullets();
            EnemySimpelRunLogi.EnemySimpelRunUpdate();

            //Collison 
            T1 = Toolbox.BlockHitboxPlayer(T1.witchRect, T1, Mapbox.obstical);
            T1 = Toolbox.PointHitbox(T1.witchRect, points, T1);

            //konventerar från float till int (texturer behöver ints)
            int x = (int)T1.witchRect.x;
            int y = (int)T1.witchRect.y;
            p1.position.X = T1.witchRect.x + 35;
            p1.position.Y = T1.witchRect.y + 35;
            p1.Update();

            //ritar saker
            Raylib.BeginDrawing();

            Mapbox.MapBorderDraw(borderC);
            EnemySimpelRunLogi.EnemySimpelRunDraw();

            Raylib.ClearBackground(Color.WHITE);

            Raylib.DrawText(T1.pointsS, 100, 50, 20, Color.ORANGE);

            Raylib.DrawTexture(witchTexture, x, y, Color.WHITE);
            Toolbox.DrawBullets();

            Raylib.EndDrawing();
            break;
    }
}