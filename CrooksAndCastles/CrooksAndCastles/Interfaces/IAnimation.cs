﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace DrunkenSoftUniWarrior.Interfaces
{
    public interface IAnimation
    {
        void playCharacterAnimation(GameTime gameTime);
        void ChangeAsset(ContentManager content, string asset, int numberOfFrames);
    }
}