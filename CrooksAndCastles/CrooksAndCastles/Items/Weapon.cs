using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using CustomMessageBox;
using System.Windows.Forms.VisualStyles;
using DrunkenSoftUniWarrior;

namespace DrunkenSoftUniWarrior.Items
{
    internal abstract class Weapon : Item
    {
        protected Weapon(Point position, int level)
            : base (position, level)
        {
            this.Damage = 0.5 * this.Level;
            this.ItemStats.Text = string.Format("{0}\n\nDamage: {1}", this.GetType().Name, this.Damage.ToString());
        }

        public double Damage { get; set; }
    }
}
