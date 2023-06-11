using HW4.Task2;

namespace HW4
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var bank = new Bank();

            var vas = bank.CreateClient("Vasya", "Pupkin");
            var pet = bank.CreateClient("Petya", "Pyatochkin");

            var VAcc = bank.CreateAccount();
            bank.BindAccountToClient(vas, VAcc);

            var PAcc = bank.CreateAccount();
            bank.BindAccountToClient(pet, PAcc);

            bank.PushMoney(vas, vas.Accounts.First(), 10, 50);
            bank.TransferMoney(vas, vas.Accounts.First(), pet, pet.Accounts.First(), 5,0);

            bank.PullMoney(vas, vas.Accounts.First(), 10, 50);
        }
    }
}