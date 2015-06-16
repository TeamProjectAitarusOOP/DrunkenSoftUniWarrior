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
        public const int WindowHeight = 576;
        public const int WindowWidth = 1024; 
        public const int MenuHeight = 50;

        ////////// MENU COMPONENTS //////////
        private HealthBar healthBar;
        private Labels health = new Labels(10,5,"HEALTH",60,15,10);
        private Labels stats = new Labels(250, 5, "STATS", 50, 15, 10);
        private Labels heroStats = new Labels(250,22,240,18,11);

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
        //internal static ToolTip ItemStats { get; set; }
        internal static List<Item> Items { get; set; }
        internal static List<Character> Units { get; set; }
        public static MainCharacter Hero { get; set; }
        internal static KeyboardState keyBoard { get; set; }

        ////////// PROPERTIS UNMANAGEABLE //////////
        private NPC sleepNPC;

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
            this.background = new Background(Content, "BackgroundIMG", new Rectangle(0, MenuHeight, this.graphics.PreferredBackBufferWidth, this.graphics.PreferredBackBufferHeight));
            this.gameOver = new GameOver(Content, "GameOver", new Rectangle(0, 0, this.graphics.PreferredBackBufferWidth, this.graphics.PreferredBackBufferHeight));
            while (Units.Count < 6)
            {
                Enemy enemy = SpawnEnemy();
                Units.Add(enemy);
            }
            sleepNPC = new NPC(Content, "SleepingNPC", 500f, 3, true, 730, 190);
            
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

            if (Units.Count < 6)
            {
                Enemy enemy = SpawnEnemy();
                Units.Add(enemy);
            }

            for (int index = 1; index < Units.Count; index++)
            {
                Units[index].playCharacterAnimation(gameTime);
            }
            for (int index = 0; index < Units.Count; index++)
            {
                Units[index].Awareness();
            }

            for (int index = 0; index < Items.Count; index++)
            {
                Control.FromHandle(Window.Handle).Controls.Add(Items[index].ItemButton);
                Control.FromHandle(Window.Handle).Controls.Add(Items[index].ItemStats);
            }

            sleepNPC.playCharacterAnimation(gameTime);

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
            if (keyBoard.IsKeyDown(Keys.Up))
            {
                Hero.MoveUp();
                Hero.ChangeAsset(Content, "HeroMoveUp", 4);
                Units[0].playCharacterAnimation(gameTime);
                this.startingCharacter = false;
            }
            else if (keyBoard.IsKeyDown(Keys.Right))
            {
                Hero.MoveRight();
                Hero.ChangeAsset(Content, "HeroMoveRight", 4);
                Units[0].playCharacterAnimation(gameTime);
                this.startingCharacter = false;
            }
            else if (keyBoard.IsKeyDown(Keys.Down))
            {
                Hero.MoveDown();
                Hero.ChangeAsset(Content, "HeroMoveDown", 4);
                Units[0].playCharacterAnimation(gameTime);
                this.startingCharacter = false;
            }
            else if (keyBoard.IsKeyDown(Keys.Left))
            {
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
    }
}
