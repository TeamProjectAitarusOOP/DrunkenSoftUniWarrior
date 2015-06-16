using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using DrunkenSoftUniWarrior.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using DrunkenSoftUniWarrior.Characters.AI;
using DrunkenSoftUniWarrior;
using DrunkenSoftUniWarrior.Characters;
using DrunkenSoftUniWarrior.Items.Weapons;
using DrunkenSoftUniWarrior.Items.Armors;
using DrunkenSoftUniWarrior.Items;

namespace DrunkenSoftUniWarrior.Characters
{
    public class Enemy : Character
    {
        ////////// FIELDS //////////
        private static Random rand = new Random();
        private static Vector2 randomPosition;
        private float startPositionX;
        private float startPositionY;
        private EnemyState enemyState = EnemyState.Chill;
        private float enemyOrientation;
        public const float EnemyChaseDistance = 75.0f;
        public const float EnemyTurnSpeed = 2.0f;
        public const float MaxEnemySpeed = 0.7f;

        public Enemy(ContentManager content, string assetMoveLeft, string assetMoveRight, string assetHitLeft, string assetHitRight, float frameSpeed, int numberOfFrames, bool looping, Vector2 position, int level)
            : base(content, assetMoveLeft, assetMoveRight, assetHitLeft, assetHitRight, frameSpeed, numberOfFrames, looping, level)
        {
            this.Position = position;
            this.startPositionX = position.X;
            this.startPositionY = position.Y;
        }

        ////////// PROPERTIS //////////
        public override Vector2 Position { get; set; }
                
        ///////////// METHODS /////////////
        public override void Awareness()
        {
            float distanceFromMainCharacter = Vector2.Distance(this.Position, DrunkenSoftUniWarrior.Hero.Position);

            //Changing states according to distance between enemy and hero
            if (distanceFromMainCharacter < EnemyChaseDistance && distanceFromMainCharacter > HitDistance)
            {
                this.enemyState = EnemyState.Chasing;
            }
            else if (distanceFromMainCharacter < HitDistance)
            {
                this.enemyState = EnemyState.Caught;
            }
            else
            {
                this.enemyState = EnemyState.Chill;
            }

            float currentEnemySpeed;
            if (this.enemyState == EnemyState.Chasing)
            {
                if (DrunkenSoftUniWarrior.Hero.Position.X > this.Position.X)
                {
                    this.ChangeAsset(this.Content, this.AssetMoveRight, 4);
                }
                else
                {
                    this.ChangeAsset(this.Content, this.AssetMoveLeft, 4);
                }
                currentEnemySpeed = MaxEnemySpeed;
                enemyOrientation = TurnToFace(this.Position, DrunkenSoftUniWarrior.Hero.Position, enemyOrientation, EnemyTurnSpeed);
            }
            else if (this.enemyState == EnemyState.Chill)
            {
                currentEnemySpeed = MaxEnemySpeed;
                enemyOrientation = TurnToFace(this.Position, new Vector2(this.startPositionX, this.startPositionY), enemyOrientation, EnemyTurnSpeed);
                if (DrunkenSoftUniWarrior.Hero.Position.X > this.startPositionX)
                {
                    this.ChangeAsset(this.Content, this.AssetMoveLeft, 4);
                }
                else
                {
                    this.ChangeAsset(this.Content, this.AssetMoveRight, 4);
                }
                if (((this.startPositionX - this.Position.X) * (this.startPositionX - this.Position.X) +
                    (this.startPositionY - this.Position.Y) * (this.startPositionY - this.Position.Y)) <=
                    0.2 * 0.2)
                {
                    this.ChangeAsset(this.Content, this.AssetMoveRight, 1);
                    currentEnemySpeed = 0;
                }
            }
            else
            {
                if (DrunkenSoftUniWarrior.Hero.Position.X > this.Position.X)
                {
                    this.ChangeAsset(this.Content, this.AssetHitRight, 2);
                }
                else
                {
                    this.ChangeAsset(this.Content, this.AssetHitLeft, 2);
                }
                currentEnemySpeed = 0;
                this.Attack();                
                
            }
            Vector2 heading = new Vector2((float)Math.Cos(enemyOrientation), (float)Math.Sin(enemyOrientation));
            this.Position += heading * currentEnemySpeed;
        }
        private static float TurnToFace(Vector2 position, Vector2 faceThis, float currentAngle, float turnSpeed)
        {
            float x = faceThis.X - position.X;
            float y = faceThis.Y - position.Y;
            float desiredAngle = (float)Math.Atan2(y, x);
            float difference = WrapAngle(desiredAngle - currentAngle);
            difference = MathHelper.Clamp(difference, -turnSpeed, turnSpeed);
            return WrapAngle(currentAngle + difference);
        }
        private static float WrapAngle(float radians)
        {
            while (radians < -MathHelper.Pi)
            {
                radians += MathHelper.TwoPi;
            }
            while (radians > MathHelper.Pi)
            {
                radians -= MathHelper.TwoPi;
            }
            return radians;
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            if (IsAlive)
            {
                base.Draw(spriteBatch);
            }
        }
        protected void Attack()
        {
            DrunkenSoftUniWarrior.Hero.Health -= this.Damage;
            if (DrunkenSoftUniWarrior.Hero.Health <= 0)
            {
                DrunkenSoftUniWarrior.Hero.IsAlive = false;
            }
        }       
        internal static Vector2 GetRandomPosition()
        {
            float randomX = rand.Next(0, DrunkenSoftUniWarrior.WindowWidth - DrunkenSoftUniWarrior.MenuHeight);
            float randomY = rand.Next(DrunkenSoftUniWarrior.MenuHeight, DrunkenSoftUniWarrior.WindowHeight - DrunkenSoftUniWarrior.MenuHeight);
            randomPosition = new Vector2(randomX, randomY);
            foreach (var unit in DrunkenSoftUniWarrior.Units)
            {
                float distance = Vector2.Distance(unit.Position, randomPosition);
                if (distance <= EnemyChaseDistance)
                {
                    GetRandomPosition();
                }
            }
            return randomPosition;
        }  
        internal static void Drop(Vector2 position)
        {
            int randomX = rand.Next(0, 50) + (int)position.X;
            int randomY = rand.Next(0, 50) + (int)position.Y;
            int randomItem = rand.Next(0, 3);
            int randomLevel;
            switch (DrunkenSoftUniWarrior.Hero.Level)
            {
                case 1:
                    randomLevel = rand.Next(1, 4);
                    break;
                case 2:
                    randomLevel = rand.Next(1, 5);
                    break;
                case 3:
                    randomLevel = rand.Next(1, 6);
                    break;
                default:
                    randomLevel = rand.Next(DrunkenSoftUniWarrior.Hero.Level - 2, DrunkenSoftUniWarrior.Hero.Level + 3);
                    break;
            }

            switch (randomItem)
            {
                case 0:
                    DrunkenSoftUniWarrior.Items.Add(new Sword(new System.Drawing.Point(randomX, randomY), randomLevel));
                    break;
                case 1:
                    DrunkenSoftUniWarrior.Items.Add(new Pants(new System.Drawing.Point(randomX, randomY), randomLevel));
                    break;
                case 2:
                    DrunkenSoftUniWarrior.Items.Add(new Potion(new System.Drawing.Point(randomX, randomY), randomLevel));
                    break;
                default:
                    break;
            }
        }
    }
}
