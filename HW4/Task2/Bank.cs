using System.Xml.Linq;

namespace HW4.Task2
{
    internal class Bank
    {
        public List<Client> Clients { get; } = new List<Client>();

        public void CreateClient(string name, string surname)
        {
            Client client = new Client(name, surname);
            if (Clients.FirstOrDefault(c => c.Name == name && c.Surname == surname) != null)
            {
                throw new InvalidOperationException($"Client witn name {name} and surname {surname} already exists");
            }
            Clients.Add(client);
        }

        public Account CreateAccount()
        {
            return new Account();
        }

        public void BindAccountToClient(Account account, Client client) 
        {
            if(Clients.Any(c=>c.Accounts.Contains(account))) throw new InvalidOperationException($"This account already binded to client");
            client.Accounts.Add(account);
        }

        public void TakeMoney(Account account, uint grn, uint kop)
        {
            Money presum = new Money(grn, kop); 
            account.DecreaseBalance(presum,true)
            account.Balance
        }
    }
}
