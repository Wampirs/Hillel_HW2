using static HW4.Task2.Bank;

namespace HW4.Task2
{
    internal class Transaction 
    {
        public DateTime DateTime { get; }
        public bool IsSucces { get; }
        public TransactionType Type { get; }

        public string ErrorReason { get; } = string.Empty;

        public Money Sum { get; }

        public IReadOnlyAccount? FromAccount { get; }
        public IReadOnlyClient? FromClient { get; }

        public IReadOnlyAccount? ToAccount { get; }
        public IReadOnlyClient? ToClient { get; }

        public Transaction(Transaction transaction, bool isSucces, string? error = null)
        {
            if (!isSucces && error == null) throw new ArgumentNullException(nameof(error), "Транзакція була не успішна, але не було передано причину помилки");
            DateTime = DateTime.Now;
            IsSucces = isSucces;
            ErrorReason = error ?? string.Empty;
            Type = transaction.Type;
            FromAccount = transaction.FromAccount;
            FromClient = transaction.FromClient;
            ToAccount = transaction.ToAccount;
            ToClient = transaction.ToClient;
            Sum = transaction.Sum;
        }

        public Transaction(TransactionType type, Money sum,
            IReadOnlyClient? fromClient = null, IReadOnlyAccount? fromAccount = null,
            IReadOnlyClient? toClient = null, IReadOnlyAccount? toAccount = null)
        {
            switch (type)
            {
                case TransactionType.PullMoney:
                    if (fromClient == null || fromAccount == null) throw new ArgumentNullException(message: $"При створенні транзакції типу {type} не передано обов'язкові поля {nameof(fromAccount)} або {nameof(fromClient)}", null);
                    break;
                case TransactionType.PushMoney:
                    if (toClient == null || toAccount == null) throw new ArgumentNullException(message: $"При створенні транзакції типу {type} не передано обов'язкові поля {nameof(toAccount)} або {nameof(toClient)}", null);
                    break;
                case TransactionType.Transfer:
                    if (toClient == null || toAccount == null || toClient == null || toAccount == null) throw new ArgumentNullException(message: $"При створенні транзакції типу {type} не передано повний набір полів", null);
                    break;
                default:
                    throw new ArgumentException($"Передано непередбачений тип транзакції {type}");
            }
            Type = type;
            Sum = sum;
            FromAccount = fromAccount;
            ToAccount = toAccount;
            FromClient = fromClient;
            ToClient = toClient;
        }
    }

    internal enum TransactionType : byte
    {
        Transfer,
        PullMoney,
        PushMoney
    }
}
