using System;

namespace RPGNew
{
    class Program
    {
        static void Main(string[] args)
        {
            Character character = new Character();
            character.AttackStrategy = new MagicAttack();

            var factory = new EnemyFactory();
            var enemy = factory.CreateEnemy("orc");

            while (character.Health > 0 && enemy.Health > 0)
            {
                character.Attack(enemy);
                if (enemy.Health > 0) enemy.Attack(character);
                Console.WriteLine("-----");
            }

            Console.WriteLine("Walka zakończona.");
        }
    }

    class Character
    {
        public string Name { get; set; }
        public int Level { get; set; }
        public int Health { get; set; }
        public int Damage { get; set; }
        public IAttackStrategy AttackStrategy { get; set; }

        public Character()
        {
            Name = "Łowca";
            Level = 1;
            Health = 100;
            Damage = 20;
        }

        public void Attack(Enemy enemy)
        {
            AttackStrategy.Attack(this, enemy);
        }

        public void Die()
        {
            Console.WriteLine($"{Name} traci wszystkie punkty życia. Ginie!");
        }

    }

    class Enemy
    {
        public string Name { get; set; }
        public int Health { get; set; }
        public int Damage { get; set; }
        public IMonsterAttackStrategy MonsterAttackStrategy { get; set; }

        public Enemy() { }

        public void Attack(Character character)
        {
            MonsterAttackStrategy.Attack(this, character);
        }

        public void Die()
        {
            Console.WriteLine($"{Name} został pokonany!");
        }
    }

    class EnemyFactory
    {
        public Enemy CreateEnemy(string type)
        {
            switch (type.ToLower())
            {
                case "zombie":
                    return new Enemy 
                    {
                        Name = "Zombie", 
                        Health = 80, 
                        Damage = 10,
                        MonsterAttackStrategy = new MeleeAttack()
                    };
                case "skeleton":
                    return new Enemy 
                    { 
                        Name = "Szkielet", 
                        Health = 50,
                        Damage = 8,
                        MonsterAttackStrategy = new MeleeAttack()
                    };
                case "orc":
                    return new Enemy 
                    { 
                        Name = "Ork", 
                        Health = 120, 
                        Damage = 15,
                        MonsterAttackStrategy = new MeleeAttack()
                    };
                case "dragon":
                    return new Enemy 
                    { 
                        Name = "Dragon", 
                        Health = 1200, 
                        Damage = 300,
                        MonsterAttackStrategy = new MeleeAttack()
                    };
                case "wyvern":
                    return new Enemy 
                    { 
                        Name = "Wyvern", 
                        Health = 700, 
                        Damage = 150,
                        MonsterAttackStrategy = new MeleeAttack()
                    };
                default:
                    Console.WriteLine("Nieznany typ potwora. Tworzę domyślnego Zombie.");
                    return new Enemy
                    {
                        Name = "Zombie",
                        Health = 80,
                        Damage = 10,
                        MonsterAttackStrategy = new MeleeAttack()
                    };
            }
        }
    }

    interface IAttackStrategy
    {
        void Attack(Character attacker, Enemy target);
    }

    class SwordAttack : IAttackStrategy
    {
        public void Attack(Character attacker, Enemy target)
        {
            int damage = 20;
            Console.WriteLine($"{attacker.Name} atakuje mieczem! Zadaje {damage} obrażeń.");
            target.Health -= damage;

            if (target.Health <= 0)
            {
                target.Die();
                return;
            }
            else
            {
                Console.WriteLine($"{target.Name} ma teraz {target.Health} punktów życia.");
            }
        }
    }

    class MagicAttack : IAttackStrategy
    {
        public void Attack(Character attacker, Enemy target)
        {
            int damage = 30;
            Console.WriteLine($"{attacker.Name} rzuca zaklęcie! Zadaje {damage} obrażeń.");
            target.Health -= damage;

            if (target.Health <= 0)
            {
                target.Die();
                return;
            }
            else
            {
                Console.WriteLine($"{target.Name} ma teraz {target.Health} punktów życia.");
            }
        }
    }

    class BowAttack : IAttackStrategy
    {
        public void Attack(Character attacker, Enemy target)
        {
            int damage = 15;
            Console.WriteLine($"{attacker.Name} strzela z łuku! Zadaje {damage} obrażeń.");
            target.Health -= damage;

            if (target.Health <= 0)
            {
                target.Die();
                return;
            }
            else
            {
                Console.WriteLine($"{target.Name} ma teraz {target.Health} punktów życia.");
            }
        }
    }

    interface IMonsterAttackStrategy
    {
        void Attack(Enemy attacker, Character target);
    }

    class MeleeAttack : IMonsterAttackStrategy
    {
        public void Attack(Enemy attacker, Character target)
        {
            int damage = attacker.Damage;
            Console.WriteLine($"{attacker.Name} atakuje wręcz i zadaje {damage} obrażeń!");
            target.Health -= damage;

            if (target.Health <= 0)
            {
                target.Die();
                return;
            }
            else
            {
                Console.WriteLine($"{target.Name} ma teraz {target.Health} punktów życia.");
            }
        }
    }
}