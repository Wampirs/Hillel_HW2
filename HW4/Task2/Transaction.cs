using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW4.Task2
{
    internal class Transaction
    {
        public IReadOnlyList<Operation> Operations { get; } = new List<Operation>();


    }
}
