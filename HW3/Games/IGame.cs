using HW3.Models;

namespace HW3.Games
{
    internal interface IGame
    {
        string Name { get; }

        GameResult PlayGame(params Player[] players);

        SelectorResult? WinnerSelector(IEnumerable<PlayerResult> playersReults);
    }
}
