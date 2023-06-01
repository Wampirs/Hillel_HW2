namespace HW3.Models
{
    internal class Card
    {
        public Suit Suit { get; set; }
        public CardValue Value { get; set; }

        public Card(Suit suit, CardValue value)
        {
            Suit = suit;
            Value = value;
        }

        public int GetCost()
        {
            switch (Value)
            {
                case CardValue.Two: return 2;
                case CardValue.Three: return 3;
                case CardValue.Four: return 4;
                case CardValue.Five: return 5;
                case CardValue.Six: return 6;
                case CardValue.Seven: return 7;
                case CardValue.Eight: return 8;
                case CardValue.Nine: return 9;
                case CardValue.Ten: return 10;
                case CardValue.Jack: return 2;
                case CardValue.Queen: return 3;
                case CardValue.King: return 4;
                case CardValue.Ace: return 11;
                default: throw new InvalidOperationException($"Cannot get cost of card with value {Value}");
            }
        }

        public override string ToString()
        {
            return $"{Value} of {Suit}";
        }
    }

    internal enum Suit
    {
        Club,
        Diamond,
        Heart,
        Spades
    }

    internal enum CardValue
    {
        Two = -3,
        Three = -2,
        Four = -1,
        Five,
        Six = 1,
        Seven = 2,
        Eight = 3,
        Nine = 4,
        Ten = 5,
        Jack = 6,
        Queen = 7,
        King = 8,
        Ace = 9
    }
}
