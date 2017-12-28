using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BringItemQuest : Quest {

    public Item item;
    private Inventory inventory;

    protected override void Start() {
        base.Start();

        inventory = Inventory.instance;
    }

    public override bool IsFinnished() {
        return inventory.FindItem(item.name) != null;
    }

    public override void GetReward() {
        base.GetReward();

        inventory.FindItem(item.name).RemoveFromInventory();

        levelsManager.AddExperience(reward.experience);
    }

}
