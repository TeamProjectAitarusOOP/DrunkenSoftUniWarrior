using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DrunkenSoftUniWarrior.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace DrunkenSoftUniWarrior.BackgroundObjects
{
    abstract class BackgroundObject:IDraw
    {
        public BackgroundObject(ContentManager content, string asset, Rectangle baseRectangle)
        {
            this.Object = content.Load<Texture2D>(asset);
            this.BaseRectangle = baseRectangle;
        }

        public Texture2D Object { get; set; }
        public Rectangle BaseRectangle { get; set; }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(this.Object, this.BaseRectangle, Color.White);
        }
    }
}
