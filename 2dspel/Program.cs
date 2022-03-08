//setings till fönsteret 
using System;
using Raylib_cs;
using System.Numerics;
using System.Media;

Raylib.InitWindow(Raylib.GetScreenWidth(), Raylib.GetScreenHeight(), "Hello World");
Raylib.SetTargetFPS(60);

//play sound
SoundPlayer player = new SoundPlayer(@"Harvest Dawn.wav");
player.Play();

//skapar en spelar
Player p1 = new Player();

//witch varjablar
Texture2D witchTexture = Raylib.LoadTexture(@"witch3.png");
witchTexture.height = 80;
witchTexture.width = 80;

//skapar massa varjablar
Traking T1 = new Traking();

//bestämer att en av t1 varjablar är lika med en annan recktangel
Rectangle witchRect1 = new Rectangle(0, -50, witchTexture.width, witchTexture.height);
Player.witchRect = witchRect1;

Rectangle[] points = new Rectangle[5];

//räknar på hur stor kartar ska vara 
Rectangle border = new Rectangle(0, 0, Raylib.GetScreenWidth(), Raylib.GetScreenHeight());
int[] mapSize = Mapbox.Mapsize(border);
int[,] grid = new int[mapSize[0], mapSize[1]];

//skaper poängen 
for (int i = 0; i < 5; i++)
{
    Rectangle pointsRefrens = new Rectangle(Raylib.GetScreenWidth() / 3, Raylib.GetScreenHeight() / 2 + i * 40, 20, 20);
    points[i] = pointsRefrens;
    T1.picktUpR4[i] = false;
}

//väljer skriv still
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
                Raylib.DrawText("LOADING", 100, Raylib.GetScreenHeight() / 2 - 300, 400, Color.BLACK);
                Raylib.EndDrawing();
                //skappare mappen

                grid = Mapbox.MapCreat(mapSize);
                mapOkej = Mapbox.MapControl(grid, mapSize);
            }
            gameState++;

            // debug för vilka positoner som är toma (printar dom)
            for (int i = 0; i < Mapbox.boxes.Count; i++)
            {
                System.Console.WriteLine(Mapbox.boxes[i]);
            }

            // mapp grafik
            lostSpace = Mapbox.SideBox(mapSize, border);
            borderC = Mapbox.MapBorderCreat(lostSpace);
            Mapbox.obstical = Mapbox.MapPlace(lostSpace, grid, mapSize);

            EnemySimpelRunLogi.EnemySimpelRunSpawn();
            EnemySimpelRun.EnemySimpelRunPathfinder();
            break;
        case 1:
            //spawn
            EnemySimpelRunLogi.EnemySimpelRunSpawn();
            // rörelse
            Player.witchRect = Toolbox.Poswitch(Player.witchRect, border, T1, lostSpace);

            //sjuta 
            Toolbox.UpdateBullets();
            EnemySimpelRunLogi.EnemySimpelRunUpdate();

            //Collison 
            T1 = Toolbox.BlockHitboxPlayer(Player.witchRect, T1, Mapbox.obstical);
            T1 = Toolbox.PointHitbox(Player.witchRect, points, T1);

            //konventerar från float till int (texturer behöver ints)
            int x = (int)Player.witchRect.x;
            int y = (int)Player.witchRect.y;
            p1.position.X = Player.witchRect.x + 35;
            p1.position.Y = Player.witchRect.y + 35;
            p1.Update();

            //ritar saker
            Raylib.BeginDrawing();

            //tar bort allt som är ritat 
            Raylib.ClearBackground(Color.WHITE);

            //ritar kanten 
            Mapbox.MapBorderDraw(borderC);

            //ritar finden 
            EnemySimpelRunLogi.EnemySimpelRunDraw();

            //skriver text för hur många poäng du har
            Raylib.DrawText(T1.pointsS, 100, 50, 20, Color.ORANGE);

            //ritar spelarens textur
            Raylib.DrawTexture(witchTexture, x, y, Color.WHITE);

            //ritar skoten
            Toolbox.DrawBullets();

            Raylib.EndDrawing();

            //är spealeren i liv
            gameState = Player.PlayerAlive(gameState);
            break;
        case 2:
            //ritar utt du dog
            Raylib.BeginDrawing();
            Raylib.ClearBackground(Color.WHITE);
            Raylib.DrawText("You Died", 100, Raylib.GetScreenHeight() / 2 - 300, 400, Color.BLACK);
            Raylib.EndDrawing();
            break;
    }
}