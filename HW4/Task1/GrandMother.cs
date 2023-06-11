namespace HW4.Task1
{
    internal class GrandMother : Сharacter
    {
        public GrandMother(int power) : base(power)
        {
        }

        public override Сharacter CallNextFamilyMember()
        {
            return new Dog(Power + 1);
        }
    }
}
