using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW4.Task2
{
    internal partial class Account
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

        public bool DecreaseBalance(Money sum, bool validate) 
        {
            try
            {
                var prebalance = Balance - sum;
                if (!validate)
                {
                    Balance = prebalance;
                }
                return true;
            }
            catch (InvalidOperationException)
            {
                return false;
            }
        }
    }
}
