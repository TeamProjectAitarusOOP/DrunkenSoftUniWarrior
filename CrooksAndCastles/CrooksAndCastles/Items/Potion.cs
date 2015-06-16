using System;
using System.Drawing;
using System.Windows.Forms;

namespace DrunkenSoftUniWarrior.Items
{
    internal class Potion : Item
    {
        private const string Path = "Potion.jpg";

        public Potion(Point position, int level)
            : base (position, level)
        {
            this.Picture = new Bitmap(Path);
            this.HealthEffect = 50 * this.Level;
            this.ItemStats.Image = this.Picture;
            this.ItemButton.Image = resizeImage(this.Picture, new Size(ItemButtonSize, ItemButtonSize));
            this.ItemStats.Text = string.Format("{0}\n\nRestore Health: {1}\n\n Level: {2}", this.GetType().Name, this.HealthEffect.ToString(), this.Level);
        }

        public double HealthEffect { get; set; }

        //public override void itemButton_Click(object sender, EventArgs e)
        //{
        //    if (DrunkenSoftUniWarrior.Hero.Health > DrunkenSoftUniWarrior.Hero.Level * 1000)
        //    {
        //        DrunkenSoftUniWarrior.Hero.Health = DrunkenSoftUniWarrior.Hero.Level * 1000;
        //    }
        //    Button potion = (Button)sender;
        //    potion.Dispose();
        //}
    }
}
