namespace HW3.Services
{
    internal static class ConsoleService
    {
        static ConsoleService()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.InputEncoding = System.Text.Encoding.UTF8;
        }
        public static void ShowMessage(string message)
        {
            Console.Clear();
            Console.WriteLine(message + "\n");
            Console.WriteLine("Натисніть будь-яку клавішу для продовження...");
            Console.ReadKey();
        }

        public static Answer AskWithVariants(string question, params Answer[] answears)
        {
            Answer? result = null;
            while (result is null)
            {
                Console.Clear();
                Console.WriteLine(question + "\n");
                Console.WriteLine("Ваш вибір [Відповідь (способи передати відповідь)]:");
                Console.WriteLine(string.Join('\n', answears.Select(ans => ans.ToString())));
                var ans = Console.ReadLine();
                if (string.IsNullOrEmpty(ans)) continue;
                result = answears.FirstOrDefault(it => it.Litterals.Contains(ans));
            }
            return result;
        }

        public static string Ask(string question)
        {
            string? result = null;
            while (string.IsNullOrEmpty(result))
            {
                Console.Clear();
                Console.WriteLine(question + "\n\n");
                Console.WriteLine("Ваша відповідь:");
                result = Console.ReadLine();
            }
            return result;
        }

        internal class Answer
        {
            public string Label { get; private set; }
            public List<string> Litterals { get; private set; }

            public Answer(string label, params string[] litterals)
            {
                Label = label;
                Litterals = litterals.ToList();
            }

            public override string ToString() => $"{Label} ({string.Join('|', Litterals)})";
        }

    }
}
