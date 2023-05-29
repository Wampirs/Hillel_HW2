using System.Text;

namespace HW2
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine(Task4.GetRandomString(1,"qwertyuiop"));

        }
    }

    public static class Task1
    {
        public static T[] CustomReverse<T>(this T[] arr)
        {
            int halfLength = arr.Length / 2;

            for (int i = 0; i < halfLength; i++)
            {
                T temp = arr[i];
                arr[i] = arr[^(i + 1)];
                arr[^(i + 1)] = temp;
            }
            return arr;
        }
    }

    public static class Task2
    {
        public static void SiracuzTest(int num)
        {
            if (num % 2 == 0)
                num /= 2;
            else
                num = (num * 3 + 1) / 2;

            Console.WriteLine(num);
            if (num == 1)
                Console.WriteLine("Гіпотезу підтверджено");
            else
                SiracuzTest(num);
        }
    }

    public class Task3
    {
        public class FilterFactory
        {
            private char _replacer = '*';
            private IEnumerable<string> _badWords = new string[0];

            public Filter GetFilter()
            {
                return new Filter(_badWords, _replacer);
            }

            public FilterFactory SetReplacer(char repl)
            {
                _replacer = repl;
                return this;
            }

            public FilterFactory SetBannedWords(IEnumerable<string> banned)
            {
                _badWords = banned;
                return this;
            }
        }

        public class Filter
        {
            private char _replacer;
            private IEnumerable<string> _badWords;

            public Filter(IEnumerable<string> badWords, char replacer)
            {
                _replacer = replacer;
                _badWords = badWords;
            }

            public string ReplaceBadwords(string str)
            {
                string[] words = str.Split(' ')
                    .Select(w => new string(w.Where(c => !char.IsPunctuation(c)).ToArray())).ToArray();

                foreach (string word in words)
                {
                    if (_badWords.Contains(word)) str = str.Replace(word, new string(_replacer, word.Length));
                }
                return str;
            }
        }
    }

    public static class Task4
    {
        public static string GetRandomString(int count, string? spectr = @"ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789!@#$%^&*()-=+_?><\|/")
        {
            if (count < 1) throw new InvalidOperationException();

            var sb = new StringBuilder();
            var rand = new Random();
            for (int i = 0; i < count; i++)
            {
                sb.Append(spectr[rand.Next(spectr.Length)]);
            }

            //return sb.ToString();
            return new string(Enumerable.Repeat(spectr, count).Select(spec => spec[new Random().Next(spec.Length)]).ToArray());
        }
    }
}