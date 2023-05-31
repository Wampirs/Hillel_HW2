using System.Text;

namespace HW2
{
    internal class Program
    {
        public static void Main(string[] args)
        {

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
                num = ((num * 3) + 1) / 2;

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
        public static string GetRandomString(int count, string spectr = @"ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789!@#$%^&*()-=+_?><\|/")
        {
            if (count < 1) throw new InvalidOperationException();

            var sb = new StringBuilder();
            var rand = new Random();
            for (int i = 0; i < count; i++)
            {
                sb.Append(spectr[rand.Next(spectr.Length)]);
            }

            return sb.ToString();
        }
    }

    public static class Task8
    {
        public static class Caesar
        {
            public static string Encrypt(string input, out int key)
            {
                var locKey = key = new Random().Next(26);
                return new string(input
                    .ToLower()
                    .Select(ch =>
                    {
                        ch = (char)(ch + locKey);
                        if (ch > 122) ch = (char)(ch - 26);
                        return ch;
                    })
                    .ToArray());
            }

            public static string Decrypt(string input, int key)
            {
                return new string(input
                    .ToLower()
                    .Select(ch =>
                    {
                        ch = (char)(ch - key);
                        if (ch < 97) ch = (char)(ch + 26);
                        return ch;
                    })
                    .ToArray());
            }
        }

        public static class StreamClipher
        {
            public static string Encrypt(string input, out string key)
            {
                StringBuilder res = new StringBuilder();
                var rand = new Random();

                int keyLength = rand.Next(input.Length / 2, input.Length + 1);
                var spectr = @"abcdefghijklmnopqrstuvwxyz0123456789!@#$%^&*()-=+_?><\|/";
                key = StringToBinary(new string(Enumerable.Repeat(spectr, keyLength).Select(spec => spec[new Random().Next(spec.Length)]).ToArray()));

                string binInp = StringToBinary(input);

                for (int i = 0; i < binInp.Length; i++)
                {
                    var locKey = i < key.Length ? key[i] : key[i - key.Length];
                    res.Append(binInp[i] ^ locKey);
                }
                return res.ToString();
            }

            public static string Decrypt(string input, string key)
            {
                StringBuilder res = new StringBuilder();

                for (int i = 0; i < input.Length; i++)
                {
                    var locKey = i < key.Length ? key[i] : key[i - key.Length];
                    res.Append(input[i] ^ locKey);
                }
                return BinaryToString(res.ToString());
            }

            static string StringToBinary(string data)
            {
                StringBuilder sb = new StringBuilder();

                foreach (char c in data)
                {
                    sb.Append(Convert.ToString(c, 2).PadLeft(8, '0'));
                }
                return sb.ToString();
            }

            static string BinaryToString(string data)
            {
                List<Byte> byteList = new List<Byte>();

                for (int i = 0; i < data.Length; i += 8)
                {
                    byteList.Add(Convert.ToByte(data.Substring(i, 8), 2));
                }
                return Encoding.ASCII.GetString(byteList.ToArray());
            }
        }
    }

    public static class Task7
    {
        public static string Compress(string inpDnk)
        {
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < inpDnk.Length;)
            {
                char curNuc = inpDnk[i];
                int solidLength = 1;
                while (i + solidLength < inpDnk.Length && inpDnk[i + solidLength] == curNuc) solidLength++;
                sb.Append(solidLength == 1 ? curNuc : $"{solidLength}{curNuc}");
                i += solidLength;
            }

            return sb.ToString();
        }

        public static string Decompress(string inpDnk)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < inpDnk.Length;)
            {
                if (char.IsNumber(inpDnk[i]))
                {
                    for (int j = 0; j < Convert.ToInt32(inpDnk[i].ToString()); j++)
                    {
                        sb.Append(inpDnk[i + 1]);
                    }
                    i += 2;
                    continue;
                }
                sb.Append(inpDnk[i]);
                i++;
            }
            return sb.ToString();
        }
    }
}