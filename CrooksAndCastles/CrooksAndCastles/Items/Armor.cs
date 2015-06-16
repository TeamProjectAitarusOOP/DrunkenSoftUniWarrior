using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using CustomMessageBox;

namespace DrunkenSoftUniWarrior.Items
{
    internal abstract class Armor : Item
    {
        protected Armor(Point position, int level)
            : base (position, level)
        {
            this.Defence = 2 * level;
            this.ItemStats.Text = string.Format("{0}\n\nDefence: {1}", this.GetType().Name, this.Defence.ToString());
        }

        public int Defence { get; private set; }
    }
}
