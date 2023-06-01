namespace HW3.Models
{
    internal class PlayerResult
    {
        public string PlayerName { get; set; }

        public int Score { get; set; }

        public object GameAttributes { get; set; }

        public PlayerResult(Player player)
        {
            PlayerName = player.Name;
            Score = player.Hand.Cards.Sum(card => card.GetCost());
            GameAttributes = player.Hand;
        }
    }
}
