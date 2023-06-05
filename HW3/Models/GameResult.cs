using HW3.Games;
using System.Text;

namespace HW3.Models
{
    internal class GameResult
    {
        public string GameName { get;}

        public bool IsDraw { get;}

        public string WinnerName { get;}

        public List<PlayerResult> PlayersResults { get;}

        public GameResult(IGame game, IEnumerable<PlayerResult> playersResults)
        {
            GameName = game.Name;
            var res = game.WinnerSelector(playersResults);
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
