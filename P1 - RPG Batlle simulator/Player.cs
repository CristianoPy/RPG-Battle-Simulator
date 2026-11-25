using System;

namespace P1___RPG_Batlle_simulator
{
    public class Player
    {
        public string Name { get; set; }
        public int HP { get; set; }
        public int Strength { get; set; }

        public int Potions { get; set; } = 3;

        public int MaxHP { get; set; } = 100;

        public Player(string name, int hp, int strength)
        {
            Name = name;
            HP = Math.Min(hp, MaxHP);
            Strength = strength;
        }

        public (int damage, bool isCritical) Attack(Enemy enemy)
        {
            int damage = Random.Shared.Next(1, Strength + 1);
            int criticalChance = Random.Shared.Next(1, 101);
            bool isCritical = false;

            if (criticalChance <= 10)
            {
                damage *= 3;
                isCritical = true;
            }
            enemy.HP = Math.Max(0, enemy.HP - damage);

            return (damage, isCritical);
        }

        public bool UsePotion()
        {
            if (HP == MaxHP)
            {
                Console.WriteLine($"{Name} is already at full health!");
                return false;
            }

            else if (HP < MaxHP && Potions > 0)
            {
                int healAmount = Math.Min(20, MaxHP - HP);
                HP += healAmount;
                Potions--;
                Console.WriteLine($"{Name} uses a potion and restores {healAmount} HP! Now {Name} has {HP} HP left.");
                return true;
            }

            else
            {
                Console.WriteLine($"{Name} has no potions left!");
                return false;
            }
        }
    }
}
