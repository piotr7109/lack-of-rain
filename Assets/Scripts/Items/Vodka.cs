﻿using UnityEngine;

[CreateAssetMenu(fileName = "Vodka", menuName = "Inventory/Vodka")]
public class Vodka : UsableItem {

    public override void Use() {
        base.Use();
        TimeManager.instance.DrunkEffect();
    }    
}