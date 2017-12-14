using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "Inventory/Equipment/Weapon")]
public class Weapon : Item {

    public int damage;
    public float fireRate;
    public int magazineSize;
    public float bulletSpeed;

    public override void Use() {
        if (this.equiped) {
            EquipmentManager.instance.UnequipWeapon();
        } else {
            base.Use();
            this.equiped = true;
            EquipmentManager.instance.EquipWeapon(this);
        }
    }
}
