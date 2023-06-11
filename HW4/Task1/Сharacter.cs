namespace HW4.Task1
{
    internal abstract class Сharacter
    {
        public int Power { get; }

        public Сharacter(int power)
        {
            Power = power;
        }

        public bool PullVegetable(Plant plant)
        {
            return plant.Weight > Power;
        }

        public abstract Сharacter CallNextFamilyMember();
    }
}
