using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// 画面表示と結果UIなどを担当するサービス。
/// </summary>
public class UIService : ManagerBase, IUIService
{
    [Header("Screens")]
    [SerializeField] GameObject pauseScreen;
    [SerializeField] GameObject resultsScreen;
    [SerializeField] GameObject levelUpScreen;

    [Header("Results Screen Displays")]
    [SerializeField] Image chosenCharacterImage;
    [SerializeField] TMP_Text chosenCharacterName;
    [SerializeField] TMP_Text levelReachedDisplay;
    [SerializeField] TMP_Text timeSurvivedDisplay;
    [SerializeField] List<Image> chosenWeaponsUI = new List<Image>(6);
    [SerializeField] List<Image> chosenPassiveItemsUI = new List<Image>(6);

    [Header("Stopwatch")]
    [SerializeField] TMP_Text stopwatchDisplay;

    public void HideAll()
    {
        if (pauseScreen) pauseScreen.SetActive(false);
        if (resultsScreen) resultsScreen.SetActive(false);
        if (levelUpScreen) levelUpScreen.SetActive(false);
    }

    public void ShowPause()
    {
        if (pauseScreen) pauseScreen.SetActive(true);
    }

    public void HidePause()
    {
        if (pauseScreen) pauseScreen.SetActive(false);
    }

    public void ShowLevelUp()
    {
        if (levelUpScreen) levelUpScreen.SetActive(true);
    }

    public void HideLevelUp()
    {
        if (levelUpScreen) levelUpScreen.SetActive(false);
    }

    public void ShowResults()
    {
        if (resultsScreen) resultsScreen.SetActive(true);
    }

    public void SetStopwatchText(string text)
    {
        if (stopwatchDisplay) stopwatchDisplay.text = text;
    }

    public void SetTimeSurvivedText(string text)
    {
        if (timeSurvivedDisplay) timeSurvivedDisplay.text = text;
    }

    public void AssignChosenCharacterUI(CharacterData chosenCharacterData)
    {
        if (!chosenCharacterData) return;
        if (chosenCharacterImage) chosenCharacterImage.sprite = chosenCharacterData.Icon;
        if (chosenCharacterName) chosenCharacterName.text = chosenCharacterData.Name;
    }

    public void AssignLevelReachedUI(int levelReachedData)
    {
        if (levelReachedDisplay) levelReachedDisplay.text = levelReachedData.ToString();
    }

    public void AssignChosenWeaponsAndPassiveItemsUI(List<PlayerInventory.Slot> chosenWeaponsData, List<PlayerInventory.Slot> chosenPassiveItemsData)
    {
        if (chosenWeaponsData == null || chosenPassiveItemsData == null) return;
        if (chosenWeaponsData.Count != chosenWeaponsUI.Count || chosenPassiveItemsData.Count != chosenPassiveItemsUI.Count)
        {
            Debug.LogError("Chosen weapons and passive items data lists have different lengths");
            return;
        }

        for (int i = 0; i < chosenWeaponsUI.Count; i++)
        {
            if (chosenWeaponsData[i].image.sprite)
            {
                chosenWeaponsUI[i].enabled = true;
                chosenWeaponsUI[i].sprite = chosenWeaponsData[i].image.sprite;
            }
            else
            {
                chosenWeaponsUI[i].enabled = false;
            }
        }

        for (int i = 0; i < chosenPassiveItemsUI.Count; i++)
        {
            if (chosenPassiveItemsData[i].image.sprite)
            {
                chosenPassiveItemsUI[i].enabled = true;
                chosenPassiveItemsUI[i].sprite = chosenPassiveItemsData[i].image.sprite;
            }
            else
            {
                chosenPassiveItemsUI[i].enabled = false;
            }
        }
    }
}

