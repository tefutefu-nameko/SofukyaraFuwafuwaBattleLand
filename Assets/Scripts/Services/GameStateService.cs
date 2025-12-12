using UnityEngine;

/// <summary>
/// ゲーム状態・ポーズ・レベルアップ・ゲームオーバーを管理するサービス。
/// 可能な限りUIや時間管理は委譲し、責務を状態管理に限定する。
/// </summary>
public class GameStateService : ManagerBase, IGameStateService
{
    [SerializeField] UIService uiService;
    [SerializeField] TimeService timeService;
    [SerializeField] PlayerStats PlayerStats;
    [SerializeField] RankingManager rankingManager;

    public GameState CurrentState { get; private set; } = GameState.Gameplay;
    public bool IsGameOver { get; private set; }
    public bool IsChoosingUpgrade { get; private set; }
    GameState previousState;

    void Awake()
    {
        // Ensure UI hidden on start.
        uiService?.HideAll();
    }

    void Update()
    {
        switch (CurrentState)
        {
            case GameState.Gameplay:
                HandlePauseInput();
                timeService?.Tick(Time.deltaTime);
                break;
            case GameState.Paused:
                HandlePauseInput();
                break;
            case GameState.GameOver:
                if (!IsGameOver)
                {
                    IsGameOver = true;
                    Time.timeScale = 0f;
                    if(PlayerStats.CurrentHealth <= 0)
                    {
                        uiService?.ShowGameOver();
                    }
                    else
                    {
                        string charName = PlayerStats.Data.Name;
                        Debug.Log(charName);
                        rankingManager?.AddScore(charName, timeService.ElapsedSeconds);
                        uiService?.ShowResults();
                    }
                        
                }
                break;
            case GameState.LevelUp:
                if (!IsChoosingUpgrade)
                {
                    IsChoosingUpgrade = true;
                    Time.timeScale = 0f;
                    uiService?.ShowLevelUp();
                }
                break;
        }
    }

    void HandlePauseInput()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (CurrentState == GameState.Paused) ResumeGame();
            else PauseGame();
        }
    }

    public void ChangeState(GameState newState)
    {
        CurrentState = newState;
    }

    public void PauseGame()
    {
        if (CurrentState == GameState.Paused) return;
        previousState = CurrentState;
        ChangeState(GameState.Paused);
        Time.timeScale = 0f;
        uiService?.ShowPause();
    }

    public void ResumeGame()
    {
        if (CurrentState != GameState.Paused) return;
        ChangeState(previousState);
        Time.timeScale = 1f;
        uiService?.HidePause();
    }

    public void StartLevelUp()
    {
        ChangeState(GameState.LevelUp);
    }

    public void EndLevelUp()
    {
        IsChoosingUpgrade = false;
        Time.timeScale = 1f;
        uiService?.HideLevelUp();
        ChangeState(GameState.Gameplay);
    }

    public void GameOver()
    {
        uiService?.SetTimeSurvivedText(timeService != null ? timeService.GetFormattedTime() : "");
        ChangeState(GameState.GameOver);
    }
}

