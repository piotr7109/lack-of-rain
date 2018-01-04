using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "Inventory/Equipment/Weapon")]
public class Weapon : Item {

    public int damage;
    public float fireRate;
    public int magazineSize;
    public int bullets;
    public float bulletSpeed;
    public float effectSpawnRate = 1;
    public WeaponType type;
    public float reloadTime = 1f;
    [Tooltip("Bullets spread angle in degress")]
    public int aimSpread = 0;
    public GameObject bulletPrefab;

    public override string GetAmount() {
        return bullets + "";
    }

    public override void Use() {
        if (this.equiped) {
            EquipmentManager.instance.UnequipWeapon();
        } else {
            base.Use();
            this.equiped = true;
            EquipmentManager.instance.EquipWeapon(this);
        }
    }

    public void Shoot() {
        bullets--;
        EquipmentManager.instance.SubscribeChange();
    }

    public bool Reload() {
        int gainedBullets = Inventory.instance.FindBullets(type, magazineSize - bullets);

        bullets += gainedBullets;
        EquipmentManager.instance.SubscribeChange();

        return gainedBullets > 0;
    }
}

public enum WeaponType { Pistol = 1, MachineGun = 2 };

