using HW3.Games;
using System.Text;

namespace HW3.Models
{
    internal class GameResult
    {


        public string GameName { get; private set; }

        public bool IsDraw { get; private set; }

        public string WinnerName { get; private set; }

        public List<PlayerResult> PlayersResults { get; private set; }

        public GameResult(Type game, IEnumerable<PlayerResult> playersResults, Func<IEnumerable<PlayerResult>, SelectorResult> selector)
        {
            GameName = game.Name;
            var res = selector(playersResults);
            IsDraw = res.IsDraw;
            if (res.IsDraw) WinnerName = "Нічия";
            else WinnerName = res.Winner;
            PlayersResults = playersResults.ToList();
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(new string('-', 30));
            sb.AppendLine($"Назва гри: {GameName}");
            sb.AppendLine(IsDraw ? "Результат гри: Нічия" : $"Результат гри: перемога {WinnerName}");
            foreach (PlayerResult p in PlayersResults)
            {
                sb.AppendLine($"Рука гравця {p.PlayerName}: {(Hand)p.GameAttributes} | Сума очок = {((Hand)p.GameAttributes).Cards.Sum(card => card.GetCost())}");
            }
            sb.AppendLine(new string('-', 30));
            return sb.ToString();
        }
    }
}
