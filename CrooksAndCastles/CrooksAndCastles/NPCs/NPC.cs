using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace DrunkenSoftUniWarrior.NPCs
{
    class NPC
    {
        private Rectangle sourceRectangle; //Base Bounderies
        protected float elapsed; //elapse time
        private int currentFrame; // current frame

        public NPC(ContentManager content, string assetMoveRight, float frameSpeed, int numberOfFrames, bool looping, float possitionX, float possitionY)
        {
            this.Content = content;
            this.FrameTime = frameSpeed; // frame speed
            this.NumberOfFrames = numberOfFrames; // numbers of frames in sprite animation
            this.Looping = looping; //loopin bool
            this.CharacterTexture = content.Load<Texture2D>(assetMoveRight); // load texture
            this.FrameWidth = (CharacterTexture.Width / this.NumberOfFrames); // calculate frame in asset
            this.FrameHeight = (CharacterTexture.Height); // frame hight base on NPC hight 
            this.Position = new Vector2(possitionX,possitionY);
        }

        public Vector2 Position { get; set; }
        public int FrameWidth { get; set; }
        public int FrameHeight { get; set; }
        public float FrameTime { get; set; }
        public int NumberOfFrames { get; set; }
        public bool Looping { get; set; }
        public Texture2D CharacterTexture { get; set; }
        public ContentManager Content { get; set; }

        public void playCharacterAnimation(GameTime gameTime)
        {
            this.elapsed += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            this.sourceRectangle = new Rectangle(currentFrame * this.FrameWidth, 0, this.FrameWidth, this.FrameHeight);
            if (elapsed >= this.FrameTime)
            {
                if (this.currentFrame >= this.NumberOfFrames - 1)
                {
                    if (Looping)
                    {
                        this.currentFrame = 0;
                    }
                }
                else
                {
                    this.currentFrame++;
                }
                this.elapsed = 0;
            }
        }
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(this.CharacterTexture, this.Position, this.sourceRectangle, Color.White);
        }
    }
}
