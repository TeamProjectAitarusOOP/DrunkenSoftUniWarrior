using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using CustomMessageBox;

namespace DrunkenSoftUniWarrior.Items.Weapons
{
    
    internal class Sword : Weapon
    {
        private const string Path = "Sword.jpg";

        public Sword(Point position, int level)
            : base(position, level)
        {
            this.Picture = new Bitmap(Path);
            this.Damage = 0.5 * this.Level;
            this.ItemStats.Image = this.Picture;
            this.ItemButton.Image = resizeImage(this.Picture, new Size(ItemButtonSize, ItemButtonSize));
        }
    }
}
