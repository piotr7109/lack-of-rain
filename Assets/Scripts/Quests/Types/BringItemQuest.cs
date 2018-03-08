using UnityEngine;

[CreateAssetMenu(fileName = "New Bring Item Quest", menuName = "Inventory/Quests/Bring Item")]
public class BringItemQuest : Quest {

    public Item item;

    public override bool IsFinnished() {
        Inventory inventory = PlayerManager.inventory;

        return inventory.FindItem(item.name) != null;
    }

    public override void GetReward() {
        base.GetReward();

        Inventory inventory = PlayerManager.inventory;

        inventory.Remove(inventory.FindItem(item.name));

        LevelsManager.instance.AddExperience(reward.experience);
    }

}
