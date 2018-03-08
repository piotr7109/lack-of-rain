using UnityEngine;

[CreateAssetMenu(fileName = "New Armour", menuName = "Inventory/Equipment/Armour")]
public class Armour : Item {
    public int armourModifier;
    public int radiationResistance = 0;
    public int condition = 100;
    public int movementReduction = 0;

    public override void Use(Equipment equipment) {
        if (this.equiped) {
            equipment.UnequipArmour();
        } else {
            base.Use(equipment);
            this.equiped = true;
            equipment.EquipArmour(this);
        }
    }

    public override string GetAmount() {
        return condition + "";
    }
}
