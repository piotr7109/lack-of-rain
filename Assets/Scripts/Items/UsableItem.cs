using UnityEngine;

[CreateAssetMenu(fileName = "New Usable Item", menuName = "Inventory/Equipment/Usable Item")]
public class UsableItem : Item {

    public int healthRestoration = 0;
    public int radiationReduction = 0;

    public override void Use(CharacterStats stats) {
        base.Use(stats);
        stats.Heal(this);
    }

    public override void Use(Inventory inventory) {
        base.Use(inventory);
        inventory.Remove(this);
    }
}
