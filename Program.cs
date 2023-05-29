using System.Text;

namespace HW2
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            new Task6.GameController().StartGame();
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
                var locKey = key = new Random().Next(1,26);
                return new string(input
                    .Select(ch =>
                    {
                        if (!char.IsLetter(ch)) return ch;
                        char res = (char)(ch + locKey);
                        if ((char.IsUpper(ch) && res > 90) || (char.IsLower(ch) && res > 122)) res = (char)(res - 26);
                        return res;
                    })
                    .ToArray());
            }

            public static string Decrypt(string input, int key)
            {
                return new string(input
                    .Select(ch =>
                    {
                        if (!char.IsLetter(ch)) return ch;
                        char res = (char)(ch - key);
                        if ((char.IsUpper(ch) && res < 65) || (char.IsLower(ch) && res < 97)) res = (char)(res + 26);
                        return res;
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

    public class Task6
    {
        public class GameController
        {
            private Board? _gameBoard;
            public void StartGame()
            {
                Console.OutputEncoding = Encoding.UTF8;
                ConsoleController.ShowMessage("Початок гри");
                CreateBoard();

            }

            private Board CreateBoard()
            {
                ConsoleController.ShowMessage("Для початку гри заповніть перший рядок клітин виставляючи \"L\" для живих клітин та \"D\" для мертвих\n" +
                    "Довжина першого рядка визначить ширину поля");
                BoardBuilder bb = new BoardBuilder();
                while (true)
                {
                    var inp = ConsoleController.AskQuestion(bb.PreHeight < 2 
                        ? $"{bb.ToString()}\nВведіть рядок ігрового поля" 
                        : $"{bb.ToString()}\nВведіть рядок ігрового поля або \"end\" для переходу на фазу гри");
                    if (inp == "end")
                    {
                        if (bb.PreHeight < 2)
                        {
                            ConsoleController.ShowMessage($"Мінімальна висота поля повинна бути 2 або більше\nПоточна висота {bb.PreHeight}");
                            continue;
                        }
                        break;
                    }
                    try
                    {
                        bb.AddLine(inp);
                    }
                    catch (ArgumentException ex)
                    {
                        ConsoleController.ShowMessage(ex.Message);
                    }
                }
                return bb.GetBoard();
            }


        }

        public class BoardBuilder
        {
            private List<bool[]> _preBoard = new List<bool[]>();

            public int PreWidth { get; private set; }
            public int PreHeight => _preBoard.Count();

            public void AddLine(string line)
            {
                if (!line.All(ch => ch == 'L' || ch == 'D'))
                    throw new ArgumentException("В строці знайдено символи окрім \"L\" та \"D\"");
                if (PreWidth == 0)
                {
                    if (line.Length < 2) throw new ArgumentException("Ширина поля менше 2 клітин неможлива");
                    PreWidth = line.Length;
                }
                if (PreWidth != line.Length)
                    throw new ArgumentException($"Введений рядок не відповідає раніше заданій ширині поля.\nШирина поля {PreWidth}");
                _preBoard.Add(ToBoolArr(line));
            }

            public Board GetBoard()
            {
                Board res = new Board(PreWidth, PreHeight);
                res.SetValues(_preBoard);
                return res;
            }

            private bool[] ToBoolArr(string inp) => inp.ToUpper().Select(ch => ch == 'L').ToArray();

            public override string ToString()
            {
                return string.Join("\n", string.Join(" ", _preBoard.Select(line=>line.Select(val=>val?"L":"D"))));///TODO
            }
        }

        public class Board
        {
            private bool[,] _board;
            public int Width { get; private set; }
            public int Height { get; private set; }

            public Board(int width, int height)
            {
                _board = new bool[width, height];
            }

            public void SetValues(List<bool[]> vals)
            {
                if (vals.Count != Height || vals.Any(val => val.Length != Width))
                    throw new ArgumentException($"Недопустиме заповнення дошки значеннями");
                for (int i = 0; i < Height; i++)
                {
                    for (int j = 0; j < Width; j++)
                    {
                        _board[i, j] = vals[i][j];
                    }
                }
            }
        }

        public static class ConsoleController
        {
            public static void ShowMessage(string msg)
            {
                Console.Clear();
                Console.WriteLine(msg);
                Console.WriteLine("\nНатисніть будь-яку клавішу для продовження");
                Console.ReadKey();
            }

            public static string AskQuestion(string question)
            {
                Console.Clear();
                Console.WriteLine(question);
                var ans = Console.ReadLine();
                while (string.IsNullOrEmpty(ans)) ans = Console.ReadLine();
                return ans;
            }
        }
    }
}