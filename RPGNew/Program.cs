using System;

namespace RPGNew
{
    class Program
    {
        static void Main(string[] args)
        {
            Character character = new Character();

            var factory = new EnemyFactory();
            var enemy = factory.CreateEnemy("WyVeRn");
            character.Attack(enemy);
        }
    }

    class Character
    {
        public string Name { get; set; }
        public int Level { get; set; }
        public int Health { get; set; }

        public Character() 
        {
            Name = "Łowca";
            Level = 5;
            Health = 100;
        }

        public void Attack(Enemy enemy)
        {
            Console.WriteLine($"{Name} na poziomie {Level} atakuje potwora {enemy.Name}! Potwór ma {enemy.Health} punktów życia.");
        }

    }

    class Enemy
    {
        public string Name { get; set; }
        public int Health { get; set; }

        public Enemy() {}
    }

    class EnemyFactory
    {
        public Enemy CreateEnemy(string type)
        {
            switch (type.ToLower())
            {
                case "zombie":
                    return new Enemy { Name = "Zombie", Health = 80 };
                case "skeleton":
                    return new Enemy { Name = "Szkielet", Health = 50 };
                case "orc":
                    return new Enemy { Name = "Ork", Health = 120 };
                case "dragon":
                    return new Enemy { Name = "Dragon", Health = 1200 };
                case "wyvern":
                    return new Enemy { Name = "Wyvern", Health = 700 };
                default:
                    Console.WriteLine("Nieznany typ potwora. Tworzę domyślnego Zombie.");
                    return new Enemy { Name = "Zombie", Health = 80 };
            }
        }
    }
}