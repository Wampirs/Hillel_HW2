namespace HW3.Models
{
    internal abstract class Computer : Player
    {
        public Computer() : base("_computer")
        {
        }
    }

    internal class BlackJackComputer : Computer
    {
        public bool WantToTakeCard()
        {
            int sum = Hand.Cards.Sum(card => card.GetCost());
            if (Hand.Cards.All(card => card.Value == CardValue.Ace)
                || sum >= 21) return false;

            if (sum >= 17) return false;
            return true;

        }
    }
}
