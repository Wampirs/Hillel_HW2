namespace HW4.Task2
{
    internal partial class Bank
    {
        private class Client : Entity, IReadOnlyClient
        {
            public string Name { get; } = "Undefined";

            public string Surname { get; } = "Undefined";

            public List<Account> Accounts { get; } = new List<Account>();

            IReadOnlyList<IReadOnlyAccount> IReadOnlyClient.Accounts => Accounts;

            public Client(string name, string surname)
            {
                Name = name;
                Surname = surname;
            }
        }

        internal interface IReadOnlyClient : IEntity
        {
            string Name { get; }
            string Surname { get; }
            IReadOnlyList<IReadOnlyAccount> Accounts { get; }
        }
    }
    
}
