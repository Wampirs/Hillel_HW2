namespace HW4.Task2
{
    internal class Client
    {
        public string Name { get; } = "Undefined";

        public string Surname { get; } = "Undefined";

        public List<Account> Accounts { get; } = new List<Account>();

        public Client(string name, string surname)
        {
            Name = name;
            Surname = surname;
        }
    }
}
