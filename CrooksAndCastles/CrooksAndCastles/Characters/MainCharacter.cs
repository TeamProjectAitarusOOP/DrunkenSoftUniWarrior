using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DrunkenSoftUniWarrior.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using DrunkenSoftUniWarrior.Items;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using System.Windows.Forms;
using System.Drawing;

namespace DrunkenSoftUniWarrior.Characters
{
    public class MainCharacter : Character, IMovabble, ISkills
    {
        private readonly string AssetMoveUp;
        private readonly string AssetMoveDown;
        private const int baseDamage = 20;
        private SoundEffect swordHit;
        private bool isPlayed = true;
        private int expirienceRequiredForTheNextLevel;

        public MainCharacter(ContentManager content, string assetMoveLeft, string assetMoveRight, string assetHitLeft, string assetHitRight, float frameSpeed, int numberOfFrames, bool looping, int level, string assetMoveDown, string assetMoveUp)
            : base(content, assetMoveLeft, assetMoveRight, assetHitLeft, assetHitRight, frameSpeed, numberOfFrames, looping, level)
        {
            this.Position = new Vector2(DrunkenSoftUniWarrior.WindowWidth / 2, DrunkenSoftUniWarrior.MenuHeight);
            this.Damage = baseDamage * level;
            this.IsAlive = true;
            this.AssetMoveUp = assetMoveUp;
            this.AssetMoveDown = assetMoveDown;
            this.Inventory = new Item[2];
            this.swordHit = content.Load<SoundEffect>("Sw");
            this.Expirience = 0;
            this.expirienceRequiredForTheNextLevel = 100;
            this.DamageButton = new Button();
            this.ArmorButton = new Button();
            this.DamageButton.Location = new System.Drawing.Point(500, 20);
            this.ArmorButton.Location = new System.Drawing.Point(550, 20);
            this.DamageButton.Size = new System.Drawing.Size(25, 25);
            this.ArmorButton.Size = new System.Drawing.Size(25, 25);
            this.ArmorButton.Text = "+";
            this.DamageButton.Text = "+";
            this.DamageButton.Visible = false;
            this.ArmorButton.Visible = false;
            this.DamageButton.MouseClick += DamageButton_MouseClick;
            this.ArmorButton.MouseClick += DamageButton_MouseClick;
        }

        private void DamageButton_MouseClick(object sender, MouseEventArgs e)
        {
            this.DamageButton.Visible = false;
            this.ArmorButton.Visible = false;
            this.Damage += 5;
        }

        private void ArmorButton_MouseClick(object sender, MouseEventArgs e)
        {
            this.DamageButton.Visible = false;
            this.ArmorButton.Visible = false;
            this.Armor += 5;
        }
        
        ////////// PROPERTIS //////////
        public override Vector2 Position { get; set; }

        internal Item[] Inventory { get; set; }

        public Button DamageButton { get; set; }

        public Button ArmorButton { get; set; }
       
        ///////////// METHODS /////////////
        public void MoveUp()
        {
            if (IsAlive)
            {
                float x = this.Position.X;
                float y = this.Position.Y;
                y -= 2f;
                if (y < DrunkenSoftUniWarrior.MenuHeight)
                {
                    y = DrunkenSoftUniWarrior.MenuHeight;
                }
                this.Position = new Vector2(x, y);
            }
        }
        public void MoveDown()
        {
            if (IsAlive)
            {
                float x = this.Position.X;
                float y = this.Position.Y;
                y += 2f;
                if (y > DrunkenSoftUniWarrior.WindowHeight - DrunkenSoftUniWarrior.MenuHeight)
                {
                    y = DrunkenSoftUniWarrior.WindowHeight - DrunkenSoftUniWarrior.MenuHeight;
                }
                this.Position = new Vector2(x, y);
            }
        }
        public void MoveRight()
        {
            if (IsAlive)
            {
                float x = this.Position.X;
                float y = this.Position.Y;
                x += 2f;
                if (x > DrunkenSoftUniWarrior.WindowWidth - DrunkenSoftUniWarrior.MenuHeight)
                {
                    x = DrunkenSoftUniWarrior.WindowWidth - DrunkenSoftUniWarrior.MenuHeight;
                }
                this.Position = new Vector2(x, y);
            }
        }
        public void MoveLeft()
        {
            if (IsAlive)
            {
                float x = this.Position.X;
                float y = this.Position.Y;
                x -= 2f;
                if (x < 0)
                {
                    x = 0;
                }
                this.Position = new Vector2(x, y);  
            }
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            if (IsAlive)
            {
                base.Draw(spriteBatch);
            }
        }
        public override void Awareness()
        {
            for (int index = 1; index < DrunkenSoftUniWarrior.Units.Count; index++)
            {
                float distanceFromEnemy = Vector2.Distance(this.Position, DrunkenSoftUniWarrior.Units[index].Position);
                if (distanceFromEnemy < HitDistance + 20 && DrunkenSoftUniWarrior.keyBoard.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Space))
                {
                    if (this.Position.X > DrunkenSoftUniWarrior.Units[index].Position.X)
                    {
                        this.ChangeAsset(this.Content, this.AssetHitLeft, 3);
                    }
                    else
                    {
                        this.ChangeAsset(this.Content, this.AssetHitRight, 3);
                    }
                    if (isPlayed)
                    {
                        swordHit.Play();
                        isPlayed = false;
                    }
                    Attack(index);
                }
            }
        }
        protected void Attack(int index)
        {
            Enemy enemy = (Enemy)DrunkenSoftUniWarrior.Units[index];
            enemy.Health -= this.Damage + this.Damage * enemy.Armor / 10;
            if (enemy.Health <= 0)
            {
                this.GetExpirience(enemy.Expirience);
                isPlayed = true;
                this.ChangeAsset(this.Content, this.AssetMoveDown, 1);
                switch ((int)enemy.Position.X % 3)
                {
                    case 0:
                        Enemy.Drop(enemy.Position);
                        break;
                    case 1:
                        Enemy.Drop(enemy.Position);
                        Enemy.Drop(enemy.Position);
                        break;
                    case 2:
                        Enemy.Drop(enemy.Position);
                        Enemy.Drop(enemy.Position);
                        Enemy.Drop(enemy.Position);
                        break;
                    default:
                        break;
                }
                DrunkenSoftUniWarrior.Units.Remove(enemy);
            }
        }

        private void GetExpirience(int xp)
        {
            if (this.Expirience + xp >= this.expirienceRequiredForTheNextLevel)
            {
                this.LevelUp();
            }
            this.Expirience += xp;
        }

        private void LevelUp()
        {
            this.DamageButton.Visible = true;
            this.ArmorButton.Visible = true;
            this.Level++;
            this.Health = this.Level * 1000;
            this.Damage = baseDamage * this.Level;
            this.expirienceRequiredForTheNextLevel += this.expirienceRequiredForTheNextLevel * 2;
        }
    }
}
