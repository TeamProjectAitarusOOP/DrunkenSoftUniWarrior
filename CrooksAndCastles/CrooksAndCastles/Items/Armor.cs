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

        //public override void itemButton_Click(object sender, EventArgs e)
        //{
        //    this.ItemButton.Location = new Point(25, 1000);
        //}
    }
}
