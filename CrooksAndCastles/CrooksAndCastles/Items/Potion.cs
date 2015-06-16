using System.Drawing;

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
            this.ItemStats.Text = string.Format("{0}\n\nRestore Health: {1}", this.GetType().Name, this.HealthEffect.ToString());
        }

        public double HealthEffect { get; set; }
    }
}
