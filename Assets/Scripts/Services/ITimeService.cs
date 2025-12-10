public interface ITimeService
{
    float ElapsedSeconds { get; }
    string GetFormattedTime();
    void ResetTimer();
    void Tick(float deltaTime);
}

