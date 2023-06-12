namespace HW3.Models
{
    internal class Hand
    {
        private readonly List<Card> _cards = new List<Card>();
        public IReadOnlyList<Card> Cards => _cards;

        public void TakeCard(Card card)
        {
            _cards.Add(card);
        }

        public Card? DropCard(Predicate<Card> predicate)
        {
            Card card = _cards.Find(predicate);
            _cards.Remove(card);
            return card;
        }

        public void ClearHand()
        {
            _cards.Clear();
        }

        public override string ToString()
        {
            return string.Join(", ", _cards);
        }
    }
}
