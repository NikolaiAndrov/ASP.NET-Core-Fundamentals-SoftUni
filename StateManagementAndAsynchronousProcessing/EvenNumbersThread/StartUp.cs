namespace EvenNumbersThread
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
            int startNumber = int.Parse(Console.ReadLine()!);
            int endNumber = int.Parse(Console.ReadLine()!);

            Thread evenNumbers = new Thread(() => PrintEveneNumbers(startNumber, endNumber));

            evenNumbers.Start();
            evenNumbers.Join();

            Console.WriteLine("Thread finished work");
        }

        static void PrintEveneNumbers(int startNumber, int endNumber)
        {
            for (int i = startNumber; i <= endNumber; i++)
            {
                if (i % 2 == 0)
                {
                    Console.WriteLine(i);
                }
            }
        }
    }
}