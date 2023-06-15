namespace HW4.Task1
{
    internal class Dog : Сharacter
    {
        public Dog(int power) : base(power)
        {
        }

        public override Сharacter CallNextFamilyMember()
        {
            throw new NotImplementedException("I`m cant call anyone more");
        }
    }
}
