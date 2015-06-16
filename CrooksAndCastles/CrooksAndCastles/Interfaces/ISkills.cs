using DrunkenSoftUniWarrior.Characters;

namespace DrunkenSoftUniWarrior.Interfaces
{
    public interface ISkills
    {
        int Health { get; set; }
        int Damage { get; set; }
        bool IsAlive { get; set; }
    }
}