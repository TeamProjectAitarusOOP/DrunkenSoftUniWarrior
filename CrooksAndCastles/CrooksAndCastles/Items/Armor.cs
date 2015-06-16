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
            
        }

        public double Defence { get; set; }
    }
}
