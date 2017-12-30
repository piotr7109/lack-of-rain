using UnityEngine;

[CreateAssetMenu(fileName = "New Bring Item Quest", menuName = "Inventory/Quests/Bring Item")]
public class BringItemQuest : Quest {

    public Item item;

    public override bool IsFinnished() {
        return Inventory.instance.FindItem(item.name) != null;
    }

    public override void GetReward() {
        base.GetReward();

        Inventory.instance.FindItem(item.name).RemoveFromInventory();

        LevelsManager.instance.AddExperience(reward.experience);
    }

}
