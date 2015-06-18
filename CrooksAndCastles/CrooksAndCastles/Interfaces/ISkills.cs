using DrunkenSoftUniWarrior.Characters;

namespace DrunkenSoftUniWarrior.Interfaces
{
    public interface ISkills
    {
        double Health { get; set; }
        double Damage { get; set; }
        bool IsAlive { get; set; }
    }
}