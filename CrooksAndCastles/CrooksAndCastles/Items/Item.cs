using System;
using System.Windows.Forms;
using System.Drawing;

namespace DrunkenSoftUniWarrior.Items
{
    internal abstract class Item : PictureBox
    {
        protected const int ItemButtonSize = 15;
        private const int ItemStatsSize = 85;

        protected Item(Point position, int level) : base()
        {
            this.Level = level;
            this.Location = position;
            this.Size = new System.Drawing.Size(ItemButtonSize, ItemButtonSize);
            this.BackColor = Color.Transparent;
            this.TabStop = false;
            this.ItemStats = new Label();
            this.ItemStats.BringToFront();
            this.ItemStats.Location = new Point(this.Location.X + ItemButtonSize, this.Location.Y + ItemButtonSize);
            this.ItemStats.Size = new Size(ItemStatsSize, ItemStatsSize);
            this.ItemStats.Font = new Font("Arial", 9, FontStyle.Bold);
            this.ItemStats.Visible = false;
            this.MouseHover += new EventHandler(this.itemButton_Hover);
            this.MouseLeave += new EventHandler(this.itemButton_MouseLeave);
            this.MouseClick += new MouseEventHandler(this.itemButton_Click);
        }
        protected Image Picture { get; set; }

        public Label ItemStats { get; set; }

        public int Level { get; set; }

        public Image resizeImage(Image imgToResize, Size size)
        {
            return (Image)(new Bitmap(imgToResize, size));
        }

        public void itemButton_Hover(object sender, EventArgs e)
        {
            this.ItemStats.Visible = true;
        }

        public void itemButton_MouseLeave(object sender, EventArgs e)
        {
            this.ItemStats.Visible = false;
        }

        public abstract void itemButton_Click(object sender, EventArgs e);

    }
}
