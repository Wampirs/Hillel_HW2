using HW3.Games;
using HW3.Models;

namespace HW3.Services
{
    internal class GameService
    {
        private Player Player { get; set; }
        private Player Computer { get; set; }
        private IGame? Game { get; set; }
        private Score Score { get; set; } = new Score();


        public void Start()
        {
            Player = SetPlayer();
            while (ConsoleService.AskWithVariants(Score.Results.Count == 0 ? "Зіграємо?" : "Зіграємо ще?"
                , new ConsoleService.Answer("Так", "1", "Т", "Так")
                , new ConsoleService.Answer("Ні", "2", "Н", "Ні")).Label == "Так")
            {
                var gameType = SelectGame();

                Game = Activator.CreateInstance(gameType) as IGame;

                Computer = ComputerFactory.GetIIForGame(gameType);
                var gameresult = Game?.PlayGame(Player, Computer);

                Score.AddGameResult(gameresult);
                ConsoleService.ShowMessage("Результати гри\n" +
                    gameresult.ToString());
            }
            if (ConsoleService.AskWithVariants("Бажаєш переглянути результати ігрової сесії?"
                , new ConsoleService.Answer("Так", "1", "Т", "Так")
                , new ConsoleService.Answer("Ні", "2", "Н", "Ні")).Label == "Так")
            {
                ConsoleService.ShowMessage("Результати ігрової сесії\n" + Score.ToString());
            }
        }

        private Player SetPlayer()
        {
            var name = ConsoleService.Ask("Як тебе звуть, гравець?");
            return new Player(name);
        }

        private Type SelectGame()
        {
            var games = AppDomain.CurrentDomain
                .GetAssemblies()
                .SelectMany(ass => ass.GetTypes())
                .Where(type => typeof(IGame).IsAssignableFrom(type)).ToList();

            ConsoleService.Answer? ans = null;

            while (ans is null)
            {
                ans = ConsoleService.AskWithVariants("Обери гру", new ConsoleService.Answer("BlackJack", "1", "Очко", "БлекДжек", "БЖ"));
            }

            return games.First(game => game.Name == ans.Label);
        }
    }
}
