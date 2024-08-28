using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SofukenBatteryPassiveItem: PassiveItem
{
    protected override void ApplyModifier()
    {
        player.CurrentMight *= 1 + passiveItemData.Multiplier / 100f;
    }
}