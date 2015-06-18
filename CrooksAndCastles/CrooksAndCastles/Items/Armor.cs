using System;
using System.Drawing;

namespace DrunkenSoftUniWarrior.Items
{
    internal abstract class Armor : Item
    {
        protected Armor(Point position, int level)
            : base (position, level)
        {
            
        }

        public double Defence { get; set; }

        public override void itemButton_Click(object sender, EventArgs e)
        {
            this.Location = new Point(580, 10);
            this.Size = new Size(30, 30);
            this.Image = resizeImage(this.Picture, new Size(30, 30));
            this.ItemStats.Location = new Point(this.Location.X + 30, this.Location.Y + 30);
            if (DrunkenSoftUniWarrior.Hero.Inventory[1] == null)
            {
                DrunkenSoftUniWarrior.Hero.Inventory[1] = this;
            }
            else
            {
                DrunkenSoftUniWarrior.Hero.Inventory[1].Dispose();
                DrunkenSoftUniWarrior.Hero.Inventory[1] = this;
            }
            DrunkenSoftUniWarrior.Hero.Armor += this.Defence;
            DrunkenSoftUniWarrior.Items.Remove(this);
        }
    }
}
