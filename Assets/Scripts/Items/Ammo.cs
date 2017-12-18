using UnityEngine;

[CreateAssetMenu(fileName = "New Bullets", menuName = "Inventory/Equipment/Bullets")]
public class Ammo : Item {

    public WeaponType type;
    public int amount;

    public override void Use() { }

    public override string GetAmount() {
        return amount + "";
    }
}
