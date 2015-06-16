using System.Drawing;

namespace DrunkenSoftUniWarrior.Items.DrinkingPotions
{
    internal class Vodka : Item
    {
        private const string Path = "";

        public Vodka(Point position, int level)
            : base(position, level)
        {
            this.Picture = new Bitmap(Path);
            this.HealthEffect = 60 * this.Level;
            this.ItemStats.Image = this.Picture;
            this.ItemButton.Image = resizeImage(this.Picture, new Size(ItemButtonSize, ItemButtonSize));
            this.ItemStats.Text = string.Format("{0}\n\nRestore Health: {1}\n\n Level: {2}", this.GetType().Name, this.HealthEffect.ToString(), this.Level);
        }
        public double HealthEffect { get; set; }
    }
}