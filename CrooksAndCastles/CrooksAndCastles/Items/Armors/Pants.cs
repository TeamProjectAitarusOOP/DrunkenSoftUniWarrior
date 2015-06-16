using System.Drawing;

namespace DrunkenSoftUniWarrior.Items.Armors
{
    internal class Pants : Armor
    {
        private const string Path = "Pants.jpg";

        public Pants(Point position, int level)
            : base(position, level)
        {
            this.Picture = new Bitmap(Path);
            this.Defence = 0.5 * this.Level;
            this.ItemStats.Image = this.Picture;
            this.ItemButton.Image = resizeImage(this.Picture, new Size(ItemButtonSize, ItemButtonSize));
            this.ItemStats.Text = string.Format("{0}\n\nDefence: {1}\n\n Level: {2}", this.GetType().Name, this.Defence.ToString(), this.Level);
        }
    }
}
