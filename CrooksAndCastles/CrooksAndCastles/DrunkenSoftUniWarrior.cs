using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using DrunkenSoftUniWarrior.BackgroundObjects.HealthBar;
using DrunkenSoftUniWarrior.Characters;
using DrunkenSoftUniWarrior.BackgroundObjects.Objects;
using DrunkenSoftUniWarrior.NPCs;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Keys = Microsoft.Xna.Framework.Input.Keys;
using CustomMessageBox;
using DrunkenSoftUniWarrior.Items;
using DrunkenSoftUniWarrior.Items.Weapons;



namespace DrunkenSoftUniWarrior
{
    public class DrunkenSoftUniWarrior : Microsoft.Xna.Framework.Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        private Background background;
        private GameOver gameOver;
        private Random rand;
        private bool startingCharacter;
        public const int WindowHeight = 676;
        public const int WindowWidth = 1024; 
        public const int MenuHeight = 50;

        ////////// MENU COMPONENTS //////////
        private HealthBar healthBar;
        private Labels health = new Labels(10,5,"HEALTH",60,15,10);
        private Labels stats = new Labels(250, 5, "STATS", 50, 15, 10);
        private Labels heroStats = new Labels(250,22,240,18,11);

        ////////// COLLISION DETECTION //////////
        private string direction;
        private Rectangle ForestLeftOne = new Rectangle(0,0,350,150);
        private Rectangle ForestLeftTwo = new Rectangle(0, 130, 270, 70);
        private Rectangle ForestLeftTre = new Rectangle(0, 200, 240, 220);
        private Rectangle ForestLeftFour = new Rectangle(0, 400, 200, 50);
        private Rectangle ForestLeftDown = new Rectangle(0, 550, 150, 200);
        private Rectangle SmallHouse = new Rectangle(338, 340, 105, 130);
        private Rectangle BigHouse = new Rectangle(750, 62, 175, 140);
        private Rectangle Well = new Rectangle(713, 355, 41, 10);
        private Rectangle ForestRight = new Rectangle(900, 470, 400, 250);
        //Boolians
        private bool ForestLeftOneBool;
        private bool ForestLeftTwoBool;
        private bool ForestLeftTreBool;
        private bool ForestLeftFourBool;
        private bool ForestLeftDownBool;
        private bool SmallHouseBool;
        private bool BigHouseBool;
        private bool WellBool;
        private bool ForestRightBool;
        private string state = "normal";

        public DrunkenSoftUniWarrior()
        {
            startingCharacter = true;
            healthBar = new HealthBar();
            Content.RootDirectory = "Content";
            this.graphics = new GraphicsDeviceManager(this);
            this.graphics.PreferredBackBufferHeight = WindowHeight;
            this.graphics.PreferredBackBufferWidth = WindowWidth;
            Units = new List<Character>();
            Items = new List<Item>();
            this.rand = new Random();
        }

        ////////// PROPERTIS MANAGEABLE //////////
        internal static List<Item> Items { get; set; }
        internal static List<Character> Units { get; set; }
        public static MainCharacter Hero { get; set; }
        internal static KeyboardState keyBoard { get; set; }

        ////////// PROPERTIS UNMANAGEABLE //////////
        private NPC sleepNPC;
        private NPC drRadeva;

        /////////// Internal GAME XNA METHODS ///////////
        protected override void Initialize()
        {
            base.Initialize();
            this.IsMouseVisible = true;
        }
        protected override void LoadContent()
        {
            this.spriteBatch = new SpriteBatch(GraphicsDevice);
            Hero = new MainCharacter(Content, "HeroMoveLeft", "HeroMoveRight", "HeroHitLeft", "HeroHitRight", 150f, 4, true, 1, "HeroMoveDown", "HeroMoveUp");
            Units.Add(Hero);
            this.background = new Background(Content, "Background2", new Rectangle(0, MenuHeight, this.graphics.PreferredBackBufferWidth, this.graphics.PreferredBackBufferHeight));
            this.gameOver = new GameOver(Content, "GameOver", new Rectangle(0, 0, this.graphics.PreferredBackBufferWidth, this.graphics.PreferredBackBufferHeight));
            while (Units.Count < 6)
            {
                Enemy enemy = SpawnEnemy();
                Units.Add(enemy);
            }
            //NPC Load
            sleepNPC = new NPC(Content, "SleepingNPC", 500f, 3, true, 730, 200);
            drRadeva = new NPC(Content, "drRadeva", 1500f, 2, true, 435, 450);

            //Control Handle XNA
            Control.FromHandle(Window.Handle).Controls.Add(healthBar.HealBar);
            Control.FromHandle(Window.Handle).Controls.Add(health.Label);
            Control.FromHandle(Window.Handle).Controls.Add(stats.Label);
            Control.FromHandle(Window.Handle).Controls.Add(heroStats.Label);
        }
        protected override void UnloadContent()
        {

        }
        protected override void Update(GameTime gameTime)
        {
            keyBoard = Keyboard.GetState();
            //Player Movement
            MovePlayer(gameTime);
            if (this.startingCharacter == true)
            {
                Hero.ChangeAsset(Content, "HeroMoveDown", 1);
                Hero.playCharacterAnimation(gameTime);
            }

            //Collision Detection Load
            ForestLeftOneBool =
                ForestLeftOne.Intersects(new Rectangle((int)Hero.Position.X, (int)Hero.Position.Y, 30, 30));
            ForestLeftTwoBool =
                ForestLeftTwo.Intersects(new Rectangle((int)Hero.Position.X, (int)Hero.Position.Y, 30, 30));
            ForestLeftTreBool =
                ForestLeftTre.Intersects(new Rectangle((int)Hero.Position.X, (int)Hero.Position.Y, 30, 30));
            ForestLeftFourBool =
                ForestLeftFour.Intersects(new Rectangle((int)Hero.Position.X, (int)Hero.Position.Y, 30, 30));
            ForestLeftDownBool =
                ForestLeftDown.Intersects(new Rectangle((int)Hero.Position.X, (int)Hero.Position.Y, 30, 30));
            SmallHouseBool =
                SmallHouse.Intersects(new Rectangle((int)Hero.Position.X, (int)Hero.Position.Y, 30, 30));
            BigHouseBool =
                BigHouse.Intersects(new Rectangle((int)Hero.Position.X, (int)Hero.Position.Y, 30, 30));
            WellBool =
                Well.Intersects(new Rectangle((int)Hero.Position.X, (int)Hero.Position.Y, 30, 30));
            ForestRightBool =
                ForestRight.Intersects(new Rectangle((int)Hero.Position.X, (int)Hero.Position.Y, 30, 30));
            
            //Collision detection
            if (ForestLeftTwoBool || ForestLeftOneBool || ForestLeftTreBool || ForestLeftFourBool
                || ForestLeftDownBool || SmallHouseBool || BigHouseBool || WellBool || ForestRightBool)
            {
                state = "border";
                HitBorder();
            }
            else
            {
                state = "normal";
            }


            if (Units.Count < 6)
            {
                Enemy enemy = SpawnEnemy();
                Units.Add(enemy);
            }

            if (Items.Count > 6)
            {
                Items[0].ItemButton.Dispose();
                Items.RemoveAt(0);
            }

            for (int index = 1; index < Units.Count; index++)
            {
                Units[index].playCharacterAnimation(gameTime);
            }

            for (int index = 0; index < Units.Count; index++)
            {
                Units[index].Awareness();
            }

            foreach (var item in Items)
            {
                Control.FromHandle(Window.Handle).Controls.Add(item.ItemButton);
                Control.FromHandle(Window.Handle).Controls.Add(item.ItemStats);
            }

            sleepNPC.playCharacterAnimation(gameTime);
            drRadeva.playCharacterAnimation(gameTime);

            //initialization and update Menu components
            healthBar.HealBar.Maximum = Hero.Level * 1000;
            healthBar.ChangeSize(Math.Max(Hero.Health, 0));
            heroStats.SetText(String.Format("Damage:{0}    Armor:{1}    Level:{2}", Hero.Damage, 20, Hero.Level));

            base.Update(gameTime);
        }
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.GreenYellow);
            this.spriteBatch.Begin();

            if (Hero.IsAlive == false)
            {
                healthBar.HealBar.Visible = false;
                heroStats.Label.Visible = false;
                stats.Label.Visible = false;
                health.Label.Visible = false;

                gameOver.Draw(spriteBatch);             
            }
            else
            {
                this.background.Draw(spriteBatch);
                sleepNPC.Draw(spriteBatch);
                drRadeva.Draw(spriteBatch);
                for (int index = 0; index < Units.Count; index++)
                {
                    Units[index].Draw(this.spriteBatch);
                }
                Hero.Draw(spriteBatch);
            }
            
            this.spriteBatch.End();
            base.Draw(gameTime);
        }

        /////////// External GAME XNA METHODS ///////////
        private void MovePlayer(GameTime gameTime)
        {
            if (keyBoard.IsKeyDown(Keys.W))
            {
                direction = "Up";
                Hero.MoveUp();
                Hero.ChangeAsset(Content, "HeroMoveUp", 4);
                Units[0].playCharacterAnimation(gameTime);
                this.startingCharacter = false;
            }
            else if (keyBoard.IsKeyDown(Keys.D))
            {
                direction = "Right";
                Hero.MoveRight();
                Hero.ChangeAsset(Content, "HeroMoveRight", 4);
                Units[0].playCharacterAnimation(gameTime);
                this.startingCharacter = false;
            }
            else if (keyBoard.IsKeyDown(Keys.S))
            {
                direction = "Down";
                Hero.MoveDown();
                Hero.ChangeAsset(Content, "HeroMoveDown", 4);
                Units[0].playCharacterAnimation(gameTime);
                this.startingCharacter = false;
            }
            else if (keyBoard.IsKeyDown(Keys.A))
            {
                direction = "Left";
                Hero.MoveLeft();
                Hero.ChangeAsset(Content, "HeroMoveLeft", 4);
                Units[0].playCharacterAnimation(gameTime);
                this.startingCharacter = false;
            }
            else if (keyBoard.IsKeyDown(Keys.Space))
            {
                Units[0].playCharacterAnimation(gameTime);
            }
        }
        private Enemy SpawnEnemy()
        {
            switch (this.GetRandomNumber())
            {
                case 0:
                    return new Enemy(Content, "EnemyOneLeft", "EnemyOneRight", "EnemyOneHitLeft", "EnemyOneHitRight", 150f, 4, true, Enemy.GetRandomPosition(), Hero.Level);
                case 1:
                    return new Enemy(Content, "EnemyTwoLeft", "EnemyTwoRight", "EnemyTwoHitLeft", "EnemyTwoHitRight", 150f, 4, true, Enemy.GetRandomPosition(), Hero.Level);
            }
            return new Enemy(Content, "EnemyTwoLeft", "EnemyTwoRight", "EnemyTwoHitLeft", "EnemyTwoHitRight", 150f, 4, true, Enemy.GetRandomPosition(), Hero.Level);
        }
        private int GetRandomNumber()
        {
            int result = this.rand.Next(0, 2);
            return result;
        }
        private void HitBorder()
        {
            if (direction == "Left")
            {
                Hero.MoveRight();
            }
            else if (direction == "Up")
            {
                Hero.MoveDown();
            }
            else if (direction == "Right")
            {
                Hero.MoveLeft();
            }
            else if (direction == "Down")
            {
                Hero.MoveUp();
            }
        }
    }
}
