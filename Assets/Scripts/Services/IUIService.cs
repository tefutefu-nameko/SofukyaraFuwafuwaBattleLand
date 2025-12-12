using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public interface IUIService
{
    void ShowPause();
    void HidePause();
    void ShowLevelUp();
    void HideLevelUp();
    void ShowResults();
    void ShowGameOver();
    void HideAll();

    void SetStopwatchText(string text);
    void SetTimeSurvivedText(string text);

    void AssignChosenCharacterUI(CharacterData chosenCharacterData);
    void AssignLevelReachedUI(int levelReachedData);
    void AssignChosenWeaponsAndPassiveItemsUI(List<PlayerInventory.Slot> chosenWeaponsData, List<PlayerInventory.Slot> chosenPassiveItemsData);
}

