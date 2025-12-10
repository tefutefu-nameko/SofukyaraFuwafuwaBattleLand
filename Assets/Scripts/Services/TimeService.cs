using UnityEngine;

/// <summary>
/// ストップウォッチ時間を管理し、UIServiceに表示を委譲する。
/// </summary>
public class TimeService : ManagerBase, ITimeService
{
    [SerializeField] private float timeLimit; // 未使用だが互換のため保持
    private float stopwatchTime;
    [SerializeField] private UIService uiService;

    public float ElapsedSeconds => stopwatchTime;

    public void ResetTimer()
    {
        stopwatchTime = 0f;
        uiService?.SetStopwatchText(GetFormattedTime());
    }

    public void Tick(float deltaTime)
    {
        stopwatchTime += deltaTime;
        uiService?.SetStopwatchText(GetFormattedTime());
    }

    public string GetFormattedTime()
    {
        int minutes = Mathf.FloorToInt(stopwatchTime / 60);
        int seconds = Mathf.FloorToInt(stopwatchTime % 60);
        return string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}

