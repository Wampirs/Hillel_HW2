namespace HW3.Models
{
    internal readonly struct Card
    {
        public readonly Suit Suit { get;}
        public readonly CardValue Value { get;}

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
}
