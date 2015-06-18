using System;
using System.Drawing;

namespace DrunkenSoftUniWarrior.Items
{
    internal abstract class Weapon : Item
    {
        protected Weapon(Point position, int level)
            : base (position, level)
        {
            
        }

        public double Damage { get; set; }

        public override void itemButton_Click(object sender, EventArgs e)
        {
            this.Location = new Point(500, 10);
            this.Size = new Size(30, 30);
            this.Image = resizeImage(this.Picture, new Size(30, 30));
            this.ItemStats.Location = new Point(this.Location.X + 30, this.Location.Y + 30);
            if (DrunkenSoftUniWarrior.Hero.Inventory[0] == null)
            {
                DrunkenSoftUniWarrior.Hero.Inventory[0] = this;
            }
            else
            {
                DrunkenSoftUniWarrior.Hero.Inventory[0].Dispose();
                DrunkenSoftUniWarrior.Hero.Inventory[0] = this;
            }
            DrunkenSoftUniWarrior.Hero.Damage += this.Damage;
            DrunkenSoftUniWarrior.Items.Remove(this);
        }
    }
}
