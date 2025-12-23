using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasureChest : MonoBehaviour
{
    

    private void OnTriggerEnter2D(Collider2D col)
    {
        PlayerInventory p = col.GetComponent<PlayerInventory>();
        if (p)
        {
            bool randomBool = Random.Range(0, 2) == 0;

            OpenTreasureChest(p, randomBool);

            Destroy(gameObject);
        }
    }

    public void OpenTreasureChest(PlayerInventory inventory, bool isHigherTier)
    {
        bool evolutionFound = false;

        // Loop through every weapon to check whether it can evolve.
        foreach (PlayerInventory.Slot s in inventory.weaponSlots)
        {
            if (s.IsEmpty()) continue;

            Weapon w = s.item as Weapon;
            if (w.data.evolutionData == null) continue; // Ignore weapon if it cannot evolve.

            // Loop through every possible evolution of the weapon.
            foreach (ItemData.Evolution e in w.data.evolutionData)
            {
                // Only attempt to evolve weapons via treasure chest evolution.
                if (e.condition == ItemData.Evolution.Condition.treasureChest)
                {
                    bool attempt = w.AttemptEvolution(e, 0);
                    if (attempt)
                    {
                        evolutionFound = true;
                        break;
                    }
                }
            }
            if (evolutionFound) break;
        }

        // Trigger the Level Up / Reward UI
        // We act as if a level up occurred, which will check the evolutionWeapons list we just populated.
        GameStateService gss = FindObjectOfType<GameStateService>();
        
        if (gss) gss.StartLevelUp();
        inventory.RemoveAndApplyUpgrades();
    }
}
