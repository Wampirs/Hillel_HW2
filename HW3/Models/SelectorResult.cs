using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW3.Models
{
    internal class SelectorResult
    {
        public bool IsDraw { get; }

        public string? Winner { get; }

        public SelectorResult(bool isDraw, string? winner = null)
        {
            Winner = winner;
            IsDraw = isDraw;
        }
    }
}
