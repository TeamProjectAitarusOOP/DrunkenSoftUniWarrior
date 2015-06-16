using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
namespace DrunkenSoftUniWarrior.Items.Armors
{
    internal class Gloves : Armor
    {
        private const string path = "";

        public Gloves(Point position, int level)
            :base(position, level)
        {
            this.Picture = new Bitmap(path);
            this.Defence = 0.3 * this.Level;
            this.ItemStats.Image = this.Picture;
            this.ItemButton.Image = resizeImage(this.Picture, new Size(ItemButtonSize, ItemButtonSize));
            this.ItemStats.Text = string.Format("{0}\n\nDefence: {1}\n\n Level: {2}", this.GetType().Name, this.Defence.ToString(), this.Level);
        }
    }
}
