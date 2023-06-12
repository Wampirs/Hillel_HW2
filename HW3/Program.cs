using HW3.Models;
using HW3.Services;

namespace HW3
{
    internal class Program
    {
        static void Main(string[] args)
        {
            GameService _gs = new GameService();
            _gs.Start();
        }
    }
}