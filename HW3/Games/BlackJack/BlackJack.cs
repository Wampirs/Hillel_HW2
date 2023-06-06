using HW3.Models;
using HW3.Services;

namespace HW3.Games.BlackJack
{
    internal class BlackJack : IGame
    {
        public string Name => nameof(BlackJack);

        private Deck _deck = new Deck(DeckType.x36);

        public GameResult PlayGame(params Player[] players)
        {
            if (players.Length != 2) throw new InvalidOperationException("This game can play only 2 players");
            Player man = players.First(pl => pl is not Computer);
            BlackJackComputer computer = (BlackJackComputer)players.First(pl => pl is BlackJackComputer);


            bool _manTurnFirst = ConsoleService.AskWithVariants("Бажаєш ходити першим"
                , new ConsoleService.Answer("Так", "1", "Т", "Так")
                , new ConsoleService.Answer("Ні", "2", "Н", "Ні")).Label == "Так";

            foreach (var player in players)
            {
                player.Hand.ClearHand();
                for (int i = 0; i < 2; i++)
                {
                    player.Hand.TakeCard(_deck.GetTopCard());
                }
            }

            if (_manTurnFirst)
            {
                PlayMan(man);
                PlayComputer(computer);
            }
            else
            {
                PlayComputer(computer);
                PlayMan(man);
            }

            var playerResults = players.Select(pl => new PlayerResult(pl));

            return new GameResult(this, playerResults);
        }

        private void PlayMan(Player man)
        {
            while (man.Hand.Cards.Sum(card => card.GetCost()) <= 21
                && ConsoleService.AskWithVariants($"В тебе в руці {man.Hand}\n" +
                $"Сумарна вартість = {man.Hand.Cards.Sum(card => card.GetCost())}\n" +
                $"Бажаєш взяти ще одну карту?"
                , new ConsoleService.Answer("Так", "1", "Т", "Так")
                , new ConsoleService.Answer("Ні", "2", "Н", "Ні")).Label == "Так")
            {
                man.Hand.TakeCard(_deck.GetTopCard());
            }
            ConsoleService.ShowMessage($"В тебе в руці {man.Hand}\n" +
                $"Сумарна вартість = {man.Hand.Cards.Sum(card => card.GetCost())}");
        }

        private void PlayComputer(BlackJackComputer comp)
        {
            while (comp.WantToTakeCard())
            {
                comp.Hand.TakeCard(_deck.GetTopCard());
            }
        }

        public SelectorResult WinnerSelector(IEnumerable<PlayerResult> playersReults)
        {
            // Варіанти коли зараховуємо нічию
            if (playersReults.All(res => res.Score == 21
            || ((Hand)res.GameAttributes).Cards.All(card => card.Value == CardValue.Ace))
                || playersReults.GroupBy(by => by.Score).Any(g => g.Count() > 1))
            {
                return new SelectorResult(true);
            }

            //Виграє той, в кого 21 або 2 тузи
            if (playersReults.Any(res => res.Score == 21 || ((Hand)res.GameAttributes).Cards.All(card => card.Value == CardValue.Ace)))
            {
                return new SelectorResult(false, playersReults.First(res => res.Score == 21 || ((Hand)res.GameAttributes).Cards.All(card => card.Value == CardValue.Ace)).PlayerName);
            }

            //Виграє інший якщо тільки один переступив 21
            if (playersReults.Where(res => res.Score > 21).Count() == 1)
            {
                return new SelectorResult(false, playersReults.First(res => res.Score <= 21).PlayerName);
            }

            //Виграє той, хто на меншу кількість очок переступив 21
            if (playersReults.All(res => res.Score > 21))
            {
                return new SelectorResult(false, playersReults.MinBy(res => res.Score - 21).PlayerName);
            }

            //виграє той, хто ближче підійшов до 21
            return new SelectorResult(false, playersReults.MinBy(res => 21 - res.Score).PlayerName);
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
