namespace HW4.Task2
{
    internal partial class Bank
    {
        private class Account : Entity, IReadOnlyAccount
        {
            public Money Balance { get; private set; }

            public bool IncreaseBalance(Money sum, bool validate)
            {
                var prebalance = Balance + sum;
                if (!validate)
                {
                    Balance = prebalance;
                }
                return true;
            }

            public bool DecreaseBalance(Money sum, bool validate, out string error)
            {
                error = string.Empty;
                try
                {
                    var prebalance = Balance - sum;
                    if (!validate)
                    {
                        Balance = prebalance;
                    }
                    return true;
                }
                catch (InvalidOperationException ex)
                {
                    error = $"Неможливо зменшити баланс рахунку\n {ex.Message}";
                    return false;
                }
            }
        }

        public interface IReadOnlyAccount : IEntity
        {
            public Money Balance { get; }
        }
    }

}
