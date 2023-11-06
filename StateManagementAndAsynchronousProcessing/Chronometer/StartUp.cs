namespace Chronometer
{
    using Models;
    using Chronometer.Models.Contracts;
    public class StartUp
    {
        public static void Main(string[] args)
        {
            IChronometer chronometer = new Chronometer();

            string command;

            while ((command = Console.ReadLine()!) != "exit")
            {
                if (command == "start")
                {
                    Task.Run(() =>
                    {
                        chronometer.Start();
                    });
                }
                else if (command == "stop")
                {
                    chronometer.Stop();
                }
                else if (command == "lap")
                {
                    Console.WriteLine(chronometer.Lap());
                }
                else if(command == "laps")
                {
                    if (chronometer.Laps.Count == 0)
                    {
                        Console.WriteLine("Laps: No laps");
                        continue;
                    }

                    Console.WriteLine("Laps:");

                    for (int i = 0; i < chronometer.Laps.Count; i++)
                    {
                        Console.WriteLine($"{i}. {chronometer.Laps[i]}");
                    }
                }
                else if (command == "reset")
                {
                    chronometer.Reset();
                }
                else if (command == "time")
                {
                    Console.WriteLine(chronometer.GetTime);
                }
            }

            chronometer.Stop();
        }
    }
}