using SplashKitSDK;

namespace Idimon
{
    public class Candy : Items
    {
        private double _successRate;
        public bool Result { get; set; }

        public Candy(string name, string description, string imagePath, double successRate) : base(name, description, imagePath, 1, false, true, "Items")
        {
            _successRate = successRate;
        }

        public void Use(Idimons idimon, Inventory inventory)
        {
            Random random = new Random();
            double roll = random.NextDouble();
            if (roll <= _successRate)
            {
                inventory.AddIdimon(idimon);
                Result = true;
            }
            else
            {
                Result = false;
            }
        }
    }

    public class Potion : Items
    {
        private int _recoverAmount;
        public Potion(string name, string description, string imagePath, int recoverAmount) : base(name, description, imagePath, 1, false, true, "Items")
        {
            _recoverAmount = recoverAmount;
        }

        public void Use(Idimons idimon)
        {
            idimon.CurrentHP += _recoverAmount;
            if (idimon.CurrentHP > idimon.MaxHP)
            {
                idimon.CurrentHP = idimon.MaxHP;
            }
        }
    }
}