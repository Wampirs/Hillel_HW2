namespace HW4.Task1
{
    internal class GrandFather : Сharacter
    {
        public GrandFather(int power) : base(power)
        {
        }

        public override Сharacter CallNextFamilyMember()
        {
            return new GrandMother(Power + 1);
        }

        public Vegetable PlantVegetable()
        {
            return new Vegetable(new Random().Next(1, 5));
        }
    }
}
