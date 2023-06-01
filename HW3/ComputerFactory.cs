using HW3.Games.BlackJack;
using HW3.Models;

namespace HW3
{
    internal class ComputerFactory
    {
        public static Computer GetIIForGame(Type gameType)
        {
            if (gameType == typeof(BlackJack))
            {
                return new BlackJackComputer();
            }
            throw new Exception($"Cannot create II for game {gameType.Name}");
        }
    }
}
