namespace Chronometer.Models.Contracts
{
    public interface IChronometer
    {
        string GetTime { get; }

        List<string> Laps { get; }

        void Start();

        void Stop();

        void Reset();

        string Lap();
    }
}
