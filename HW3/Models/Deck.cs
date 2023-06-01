namespace HW3.Models
{
    internal class Deck
    {
        private readonly List<Card> _cards = new List<Card>();
        public IReadOnlyList<Card> Cards => _cards.ToList();

        public Deck(DeckType type)
        {
            FillDeck(type);
            ShuffleDeck();
        }

        private void FillDeck(DeckType type)
        {
            _cards.Clear();
            foreach (var suit in Enum.GetValues<Suit>())
            {
                var values = type == DeckType.x36
                    ? Enum.GetValues<CardValue>().Where(val => val > 0)
                    : Enum.GetValues<CardValue>();
                foreach (var val in values)
                {
                    _cards.Add(new Card(suit, val));
                }
            }
        } //Task 1

        private void ShuffleDeck()
        {
            Random random = new Random();
            for (int n = _cards.Count - 1; n > 0; --n)
            {
                int k = random.Next(n + 1);
                var temp = _cards[n];
                _cards[n] = _cards[k];
                _cards[k] = temp;
            }
        } //Task 2

        public Card GetTopCard()
        {
            Card card = _cards.First();
            _cards.Remove(card);
            return card;
        }

        public IEnumerable<int> FindAllAceIndexes()
        {
            for(int i=0; i < _cards.Count; i++)
            {
                if (_cards[i].Value == CardValue.Ace)
                {
                    yield return i;
                }
            }
        } //Task 3

        public void SortDeck() //Task 5
        {
            _cards.Sort(new CardComparer());
        }

        public void TopSpades()
        {
            int lastSpadeInd = 0;
            for(int  i = 0; i < _cards.Count - 1; i++)
            {
                if (_cards[i].Suit == Suit.Spades)
                {
                    Card temp;
                    temp = _cards[lastSpadeInd];
                    _cards[lastSpadeInd] = _cards[i];
                    _cards[i] = temp;
                    lastSpadeInd++;
                }
            }
        }
    }

    internal class CardComparer : IComparer<Card>
    {
        public int Compare(Card? x, Card? y)
        {
            if(x.Suit < y.Suit) return -1;
            if (x.Suit > y.Suit) return 1;
            return 0;
        }
    }

    internal enum DeckType
    {
        x36,
        x52
    }
}
