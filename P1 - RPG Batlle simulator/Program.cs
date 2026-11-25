using P1___RPG_Batlle_simulator;

Player player = new Player("Warrior", 100, 20);

Enemy enemy = new Enemy("Goblin", 80, 15);


BattleResult result = StartBattle(player, enemy);

switch (result)
{
    case BattleResult.Victory:
        Console.WriteLine("================================"); 
        Console.WriteLine("            VICTORY!"); 
        Console.WriteLine("================================");
        break;
    case BattleResult.Defeat:
        Console.WriteLine("================================");
        Console.WriteLine("            DEFEAT!");
        Console.WriteLine("================================");
        break;
    case BattleResult.Escaped:
        Console.WriteLine("================================");
        Console.WriteLine("            ESCAPED!");
        Console.WriteLine("================================");
        break;
}

Console.ReadLine();


static BattleResult StartBattle(Player player, Enemy enemy)
{
    bool battleRunning = true;
    bool escaped = false;

    while (player.HP > 0 && enemy.HP > 0 && battleRunning)
    {
        Console.WriteLine("What do you want to do?");
        Console.WriteLine("1. Attack!");
        Console.WriteLine("2. Use Potion!");
        Console.WriteLine("3. Run!");
        Console.WriteLine();

        int choice = int.TryParse(Console.ReadLine(), out int result) ? result : 0;

        switch (choice)
        {
            case 1:
                Console.WriteLine();

               (int damageDealt, bool isCritical) =  player.Attack(enemy);
                Console.WriteLine($"{player.Name} attacks {enemy.Name} for {damageDealt} damage! Now {enemy.Name} has {enemy.HP} HP left.");

                if (IsEnemyDefeated(enemy))
                {
                    break;
                }

                if(isCritical)
                {
                    Console.WriteLine("CRITICAL HIT!");
                    Console.WriteLine($"{player.Name} lands a critical hit!");
                }

                int damageTaken = enemy.Attack(player);
                Console.WriteLine($"{enemy.Name} attacks {player.Name} for {damageTaken} damage! Now {player.Name} has {player.HP} HP left.");

                if (IsPlayerDefeated(player))
                {
                    break;
                }
                Console.WriteLine();
                break;


            case 2:
            {
                    Console.WriteLine();

                    if (player.UsePotion())
                    {
                        int damageAfterPotion = enemy.Attack(player);

                        Console.WriteLine($"{enemy.Name} attacks {player.Name} for {damageAfterPotion} damage! Now {player.Name} has {player.HP} HP left.");

                        if (IsPlayerDefeated(player))
                        {
                            break;
                        }
                    }

                    Console.WriteLine();
                    break;
            }

            
                    
            


            case 3:
                Console.WriteLine();

                Console.WriteLine("You escaped from the battle!");
                battleRunning = false;
                escaped = true;
                Console.WriteLine();
                break;


            default:
                Console.WriteLine();
                Console.WriteLine("Invalid choice. Please select 1, 2, or 3.");
                Console.WriteLine();
                break;
        }

        ShowStatus(player, enemy);

    }

    if (IsEnemyDefeated(enemy))
    {
        return BattleResult.Victory;
    }

    else if (IsPlayerDefeated(player))
    {
        return BattleResult.Defeat;
    }

    else if (escaped)
    {
        return BattleResult.Escaped;
    }

    throw new InvalidOperationException("Battle ended without result.");
}



static bool IsPlayerDefeated(Player player)
{
    return player.HP <= 0;
}

static bool IsEnemyDefeated(Enemy enemy)
{
    return enemy.HP <= 0;
}

static void ShowStatus(Player player, Enemy enemy)
{
    Console.WriteLine("================================");
    Console.WriteLine("Player HP: " + player.HP + "/" + player.MaxHP);
    Console.WriteLine("Potions: " + player.Potions);
    Console.WriteLine("Enemy HP: " + enemy.HP);
    Console.WriteLine("================================");
    Console.WriteLine();
}

enum BattleResult
{
    Victory,
    Defeat,
    Escaped
}
