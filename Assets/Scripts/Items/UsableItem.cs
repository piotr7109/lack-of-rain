using UnityEngine;

[CreateAssetMenu(fileName = "New Usable Item", menuName = "Inventory/Equipment/Usable Item")]
public class UsableItem : Item {

    public int healthRestoration = 0;
    public int radiationReduction = 0;

    public override void Use() {
        base.Use();
        PlayerStats.instance.Heal(this);
        RemoveFromInventory();
    }
}
