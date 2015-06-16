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
    }
}
