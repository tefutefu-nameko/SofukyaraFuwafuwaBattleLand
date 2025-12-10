using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class GameManager : ManagerBase
{
    public static GameManager instance;

    [Header("Services")]
    [SerializeField] GameStateService gameStateService;
    [SerializeField] TimeService timeService;
    [SerializeField] UIService uiService;
    [SerializeField] DamageTextService damageTextService;

    void Awake()
    {
        if (instance == null) instance = this;
        else
        {
            Debug.LogWarning("EXTRA " + this + " DELETED");
            Destroy(gameObject);
            return;
        }

        // Fallback search if not wired
        if (!gameStateService) gameStateService = FindObjectOfType<GameStateService>();
        if (!timeService) timeService = FindObjectOfType<TimeService>();
        if (!uiService) uiService = FindObjectOfType<UIService>();
        if (!damageTextService) damageTextService = FindObjectOfType<DamageTextService>();
    }

    public static void GenerateFloatingText(string text, Transform target, float duration = 1f, float speed = 1f)
    {
        if (instance == null || instance.damageTextService == null) return;
        instance.damageTextService.GenerateFloatingText(text, target, duration, speed);
    }

    // Facade methods to keep compatibility
    public bool choosingUpgrade => gameStateService != null && gameStateService.IsChoosingUpgrade;
    public void StartLevelUp() => gameStateService?.StartLevelUp();
    public void EndLevelUp() => gameStateService?.EndLevelUp();
    public void GameOver()
    {
        gameStateService?.GameOver();
    }

    public void PauseGame() => gameStateService?.PauseGame();
    public void ResumeGame() => gameStateService?.ResumeGame();
}
