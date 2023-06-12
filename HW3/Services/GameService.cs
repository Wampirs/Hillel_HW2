using HW3.Games;
using HW3.Models;

namespace HW3.Services
{
    internal class GameService
    {
        private Player? _player { get; set; }
        private Player? _computer { get; set; }
        private IGame? Game { get; set; }
        private readonly Score _score = new Score();


        public void Start()
        {
            _player = SetPlayer();
            while (ConsoleService.AskWithVariants(_score.Results.Count == 0 ? "Зіграємо?" : "Зіграємо ще?"
                , new ConsoleService.Answer("Так", "1", "Т", "Так")
                , new ConsoleService.Answer("Ні", "2", "Н", "Ні")).Label == "Так")
            {
                var gameType = SelectGame();

                Game = Activator.CreateInstance(gameType) as IGame;

                _computer = ComputerFactory.GetIIForGame(gameType);
                var gameresult = Game?.PlayGame(_player, _computer);

                _score.AddGameResult(gameresult);
                ConsoleService.ShowMessage("Результати гри\n" +
                    gameresult.ToString());
            }
            if (ConsoleService.AskWithVariants("Бажаєш переглянути результати ігрової сесії?"
                , new ConsoleService.Answer("Так", "1", "Т", "Так")
                , new ConsoleService.Answer("Ні", "2", "Н", "Ні")).Label == "Так")
            {
                ConsoleService.ShowMessage("Результати ігрової сесії\n" + _score.ToString());
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
                .Where(type => typeof(IGame).IsAssignableFrom(type)&&type.IsClass&&!type.IsAbstract).ToList();

            ConsoleService.Answer? ans = null;

            while (ans is null)
            {
                int counter = 0;
                ans = ConsoleService.AskWithVariants("Обери гру", games.Select(g=> new ConsoleService.Answer(g.Name, (++counter).ToString())).ToArray());
            }

            return games.First(game => game.Name == ans.Label);
        }
    }
}
