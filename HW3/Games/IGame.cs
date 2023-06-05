using HW3.Models;

namespace HW3.Games
{
    internal interface IGame
    {
        string Name { get; }

        GameResult PlayGame(params Player[] players);

        SelectorResult? WinnerSelector(IEnumerable<PlayerResult> playersReults);
    }

    internal class SelectorResult
    {
        public bool IsDraw { get; }

        public string? Winner { get; }

        public SelectorResult(bool isDraw, string? winner = null)
        {
            Winner = winner;
            IsDraw = isDraw;
        }
    }
}
