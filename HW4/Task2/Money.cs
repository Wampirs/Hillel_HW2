namespace HW4.Task2
{
    internal readonly struct Money
    {
        public uint Grn { get; } = 0;
        public uint Kop { get; } = 0;

        public Money(uint grn, uint kop)
        {
            Grn = grn;
            Kop = kop;
        }

        public static Money operator + (Money left, Money right)
        {
            uint grn = left.Grn + right.Grn;
            uint kop = left.Kop + right.Kop;
            while(kop >= 100)
            {
                grn += 1;
                kop -= 100;
            }
            return new Money(grn, kop);
        }

        public static Money operator - (Money left, Money right)
        {
            var res = (left.Grn * 100 + left.Kop) - (right.Grn * 100 + right.Kop);
            if (res<0) throw new InvalidOperationException("Result sum will be less than 0");
            return new Money(res / 100, res % 100);
        }

        public override string ToString()
        {
            return $"{Grn} гривень та {Kop} копійок";
        }
    }
}
