namespace HW3.Models
{
    internal class Player
    {
        public string Name { get; private set; }

        public Hand Hand { get; private set; }

        public void ClearHand()
        {
            Hand = new Hand();
        }

        public Player(string name)
        {
            Name = name;
            ClearHand();
        }
    }
}
