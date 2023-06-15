using static System.Runtime.InteropServices.JavaScript.JSType;

namespace HW4.Task3
{
    internal class Clist
    {
        private uint _cap = 7;
        private object[] _arr;

        public uint Count { get; private set; } = 0;

        public Clist()
        {
            _arr = new object[_cap];
        }

        private void ChangeCap(uint toCap)
        {
            _cap = toCap;
            var _tArr = new object[_cap];
            for (int i = 0; i < _cap; i++)
            {
                if (_arr[i] != null) _tArr[i] = _arr[i];
            }
        }

        private uint NextCapStep(uint curCap)
        {
            if (!IsBitScaleble(curCap))
            {

            }
        }

        private bool IsBitScaleble(uint num)
        {
            bool foundFirstOne = false;

            for (int i = 31; i >= 0; i--)
            {
                uint mask = (uint)(1 << i);
                if ((num & mask) != 0)
                {
                    foundFirstOne = true;
                }
                else if (foundFirstOne)
                {
                    return false;
                }
            }

            return true;
        }
        private uint ToBitScaleble(uint num)
        {
            uint transformedNumber = num;
            bool foundFirstOne = false;

            for (int i = 31; i >= 0; i--)
            {
                uint mask = (uint)(1 << i);
                if (((transformedNumber & mask) != 0) && !foundFirstOne)
                {
                    foundFirstOne = true;
                }
                else if ((transformedNumber & mask) == 0 && foundFirstOne)
                {
                    transformedNumber |= mask;
                }
            }

            return transformedNumber;
        }

        public void Add(object item)
        {
            if (item == null) throw new ArgumentNullException("item");
            if (Count >= _cap)
        }
    }
}
