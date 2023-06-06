using HW3.Models;
using HW3.Services;

namespace HW3
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Deck d = new Deck(DeckType.x52);
            d.SortDeck();

            GameService _gs = new GameService();
            _gs.Start();
        }
    }
}