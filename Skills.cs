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
            
            // SplashKitSDK.Timer _timer;
            // _timer = new SplashKitSDK.Timer("Timer");
            // _timer.Start();
            // while (_timer.Ticks < 1000)
            // {
            //     Console.WriteLine($"{user.Name} used {Name} and dealt {damageDealt} damage to {opponent.Name}.");
            //     SplashKit.FillRectangle(Color.Black, 0, 0, SplashKit.ScreenWidth(), SplashKit.ScreenHeight());
            //     // SplashKit.FillRectangle(Color.Black, SplashKit.ScreenWidth() / 4, SplashKit.ScreenHeight() / 3 * 2, SplashKit.ScreenWidth() - SplashKit.ScreenWidth() / 4 - 2, SplashKit.ScreenHeight() - SplashKit.ScreenHeight() / 3 * 2 - 2);
            //     SplashKit.DrawText($"{Name} used {Name}!", Color.White, 440, 650);
            // }
            // _timer.Stop();
            // _timer.Reset();


            
            // SplashKit.Delay(2000);
        }

        public void Display()
        {
            Console.WriteLine($"{Name} (Level {LevelRequirement}): {Description}. Damage: {Damage}");
        }
    }

}