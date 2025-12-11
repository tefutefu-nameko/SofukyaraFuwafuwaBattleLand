using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// PlayerStats のUI表示を担当するプレゼンター。
/// </summary>
public class PlayerStatsUI : UIViewBase
{
    [Header("Current Stat Displays")]
    [SerializeField] TMP_Text currentHealthDisplay;
    [SerializeField] TMP_Text currentRecoveryDisplay;
    [SerializeField] TMP_Text currentMoveSpeedDisplay;
    [SerializeField] TMP_Text currentMightDisplay;
    [SerializeField] TMP_Text currentProjectileSpeedDisplay;
    [SerializeField] TMP_Text currentMagnetDisplay;

    [Header("Bars / Level")]
    [SerializeField] Image healthBar;
    [SerializeField] Image expBar;
    [SerializeField] TMP_Text levelText;

    PlayerStats playerStats;

    void OnEnable()
    {
        if (!playerStats) playerStats = FindObjectOfType<PlayerStats>();

        if (playerStats)
        {
            playerStats.OnHealthChanged += UpdateHealth;
            playerStats.OnRecoveryChanged += UpdateRecovery;
            playerStats.OnMoveSpeedChanged += UpdateMoveSpeed;
            playerStats.OnMightChanged += UpdateMight;
            playerStats.OnProjectileSpeedChanged += UpdateProjectileSpeed;
            playerStats.OnMagnetChanged += UpdateMagnet;
            playerStats.OnExperienceChanged += UpdateExpBar;
            playerStats.OnLevelChanged += UpdateLevel;
        }
    }

    void OnDisable()
    {
        if (playerStats)
        {
            playerStats.OnHealthChanged -= UpdateHealth;
            playerStats.OnRecoveryChanged -= UpdateRecovery;
            playerStats.OnMoveSpeedChanged -= UpdateMoveSpeed;
            playerStats.OnMightChanged -= UpdateMight;
            playerStats.OnProjectileSpeedChanged -= UpdateProjectileSpeed;
            playerStats.OnMagnetChanged -= UpdateMagnet;
            playerStats.OnExperienceChanged -= UpdateExpBar;
            playerStats.OnLevelChanged -= UpdateLevel;
        }
    }

    public void UpdateHealth(float current, float max)
    {
        if (currentHealthDisplay) currentHealthDisplay.text = $"HP: {current}";
        if (healthBar && max > 0) healthBar.fillAmount = current / max;
    }

    public void UpdateRecovery(float value)
    {
        if (currentRecoveryDisplay) currentRecoveryDisplay.text = "回復: " + value;
    }

    public void UpdateMoveSpeed(float value)
    {
        if (currentMoveSpeedDisplay) currentMoveSpeedDisplay.text = "移動速度: " + value;
    }

    public void UpdateMight(float value)
    {
        if (currentMightDisplay) currentMightDisplay.text = "攻撃倍率: " + value;
    }

    public void UpdateProjectileSpeed(float value)
    {
        if (currentProjectileSpeedDisplay) currentProjectileSpeedDisplay.text = "発射速度: " + value;
    }

    public void UpdateMagnet(float value)
    {
        if (currentMagnetDisplay) currentMagnetDisplay.text = "アイテム吸引: " + value;
    }

    public void UpdateExpBar(int exp, int expCap)
    {
        if (expBar && expCap > 0) expBar.fillAmount = (float)exp / expCap;
    }

    public void UpdateLevel(int level)
    {
        if (levelText) levelText.text = "LV " + level;
    }
}

