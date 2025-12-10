public interface IGameStateService
{
    GameState CurrentState { get; }
    bool IsGameOver { get; }
    bool IsChoosingUpgrade { get; }
    void ChangeState(GameState newState);
    void PauseGame();
    void ResumeGame();
    void StartLevelUp();
    void EndLevelUp();
    void GameOver();
}

public enum GameState
{
    Gameplay,
    Paused,
    GameOver,
    LevelUp
}

