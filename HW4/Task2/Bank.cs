using System.Security.Principal;

namespace HW4.Task2
{
    internal partial class Bank
    {
        private List<Client> _clients = new List<Client>();
        public IReadOnlyList<IReadOnlyClient> Clients => _clients;

        private List<Transaction> _history = new List<Transaction>();
        public IReadOnlyList<Transaction> History => _history;

        public IReadOnlyClient CreateClient(string name, string surname)
        {
            Client client = new Client(name, surname);
            if (Clients.FirstOrDefault(c => c.Name == name && c.Surname == surname) != null)
            {
                throw new InvalidOperationException($"Client witn name {name} and surname {surname} already exists");
            }
            _clients.Add(client);
            return client;
        }

        public IReadOnlyAccount CreateAccount()
        {
            return new Account();
        }

        public void BindAccountToClient(IReadOnlyClient client, IReadOnlyAccount account)
        {
            if (!Clients.Contains(client)) throw new InvalidOperationException("Переданий клієнт не є клієнтом банку");
            if (Clients.Any(c => c.Accounts.Contains(account))) throw new InvalidOperationException($"This account already binded to client");
            _clients.Single(c => c.Id == client.Id).Accounts.Add((Account)account); //приведення типів тут виглядає не дуже хорошим рішенням, але не певен
        }

        public Transaction PullMoney(IReadOnlyClient client, IReadOnlyAccount account, uint grn, uint kop)
        {
            if (!Clients.Contains(client)) throw new InvalidOperationException("Переданий клієнт не є клієнтом банку");
            if (!client.Accounts.Any(acc => acc.Id == account.Id)) throw new InvalidOperationException($"Клієнту {client.Id} не належить рахунок {account.Id}");
            Money presum = new Money(grn, kop);
            Transaction result;

            Transaction preTrans = new Transaction(TransactionType.PullMoney, presum, fromClient: client, fromAccount: account);

            if (((Account)account).DecreaseBalance(presum, true, out string error))
            {
                result = new Transaction(preTrans, true);
                ((Account)account).DecreaseBalance(presum, false, out error);
            }
            else
            {
                result = new Transaction(preTrans, false, error);
            }

            _history.Add(result);
            return result;
        }

        public Transaction PushMoney(IReadOnlyClient client, IReadOnlyAccount account, uint grn, uint kop)
        {
            if (!Clients.Contains(client)) throw new InvalidOperationException("Переданий клієнт не є клієнтом банку");
            if (!client.Accounts.Any(acc => acc.Id == account.Id)) throw new InvalidOperationException($"Клієнту {client.Id} не належить рахунок {account.Id}");
            Money presum = new Money(grn, kop);
            Transaction result;

            Transaction preTrans = new Transaction(TransactionType.PushMoney, presum, toClient: client, toAccount: account);

            result = new Transaction(preTrans, true);
            ((Account)account).IncreaseBalance(presum, false);

            _history.Add(result);
            return result;
        }

        public Transaction TransferMoney(IReadOnlyClient fromClient, IReadOnlyAccount fromAccount, IReadOnlyClient toClient, IReadOnlyAccount toAccount,
            uint grn, uint kop)
        {
            if (!Clients.Contains(fromClient) || !Clients.Contains(toClient)) throw new InvalidOperationException("Переданий клієнт не є клієнтом банку");
            if (!fromClient.Accounts.Any(acc => acc.Id == fromAccount.Id)) throw new InvalidOperationException($"Клієнту {fromClient.Id} не належить рахунок {fromAccount.Id}");
            if (!toClient.Accounts.Any(acc => acc.Id == toAccount.Id)) throw new InvalidOperationException($"Клієнту {toClient.Id} не належить рахунок {toAccount.Id}");

            Money presum = new Money(grn, kop);
            Transaction result;

            Transaction preTrans = new Transaction(TransactionType.Transfer, presum,
                fromClient: fromClient, fromAccount: fromAccount,
                toClient: toClient, toAccount:toAccount);

            if (((Account)fromAccount).DecreaseBalance(presum, true, out string error))
            {
                result = new Transaction(preTrans, true);
                ((Account)fromAccount).DecreaseBalance(presum, false, out error);
                ((Account)toAccount).IncreaseBalance(presum, false);
            }
            else
            {
                result = new Transaction(preTrans, false, error);
            }

            _history.Add(result);
            return result;
        }
    }
}
