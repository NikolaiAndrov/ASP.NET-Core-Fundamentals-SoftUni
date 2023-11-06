namespace SumEvensInBackground
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
            long sum = 0;

            Task task = Task.Run(() =>
            {
                for (long i = 1; i <= 1000000000; i++)
                {
                    if (i % 2 == 0)
                    {
                        sum += i;
                    }
                }
            });

            string command;

            while ((command = Console.ReadLine()!) != "exit")
            {
                if (command == "show")
                {
                    Console.WriteLine(sum);
                }
            }
        }
    }
}