using SplashKitSDK;

namespace Idimon
{
    public class Skills
    {
        public string Name { get; private set; }
        public int LevelRequirement { get; private set; }
        public int Damage { get; private set; }
        public string Description { get; private set; }

        public Skills(string name, int levelRequirement, int damage, string description)
        {
            Name = name;
            LevelRequirement = levelRequirement;
            Damage = damage;
            Description = description;
        }

        public void Use(Idimons user, Idimons opponent)
        {
            int damageDealt = Damage * user.Attack / opponent.Defense;
            if (damageDealt < 0) damageDealt = 0;
            opponent.TakeDamage(damageDealt);

            user.Draw(100, 100);
        }

        public void Display()
        {
            Console.WriteLine($"{Name} (Level {LevelRequirement}): {Description}. Damage: {Damage}");
        }
    }

}