using System.Text;

namespace HW3.Models
{
    internal class Score
    {
        private List<GameResult> _results = new List<GameResult>();
        public IReadOnlyList<GameResult> Results => _results;

        public void AddGameResult(GameResult? gameResult)
        {
            if (gameResult == null) return;
            _results.Add(gameResult);
        }

        public override string ToString()
        {
            if (_results.Count == 0) return "Не було зіграно жодної гри";
            StringBuilder sb = new StringBuilder();
            foreach (GameResult gameResult in _results)
            {
                sb.Append(gameResult.ToString());
            }
            return sb.ToString();
        }

    }
}
