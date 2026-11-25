using System;

namespace P1___RPG_Batlle_simulator
{
    public class Enemy
    {
        public string Name { get; set; }
        public int HP { get; set; }
        public int Strength { get; set; }

        public Enemy(string name, int hp, int strength)
        {
            Name = name;
            HP = hp;
            Strength = strength;
        }

        public int Attack(Player player)
        {
            int damage = Random.Shared.Next(1, Strength + 1);
            player.HP = Math.Max(0, player.HP - damage);
            return damage;
        }
    }
}
        