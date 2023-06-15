using HW4.Task2;

namespace HW4
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(TransformNumber(6));
            Console.WriteLine(TransformNumber(2));
            Console.WriteLine(TransformNumber(uint.MaxValue));

        }

        static uint TransformNumber(uint number)
        {
            uint transformedNumber = number;
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

    }
}