using UnityEngine;

[CreateAssetMenu(fileName = "New Armour", menuName = "Inventory/Equipment/Armour")]
public class Armour : Item {
    public int armourModifier;
    public int radiationResistance = 0;
    public int condition = 100;
    public int movementReduction = 0;

    public override void Use() {
        if (this.equiped) {
            EquipmentManager.instance.UnequipArmour();
        } else {
            base.Use();
            this.equiped = true;
            EquipmentManager.instance.EquipArmour(this);
        }
    }
}
