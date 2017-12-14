using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "Inventory/Equipment/Weapon")]
public class Weapon : Item {

    public int damage;
    public float fireRate;
    public int magazineSize;
    public float bulletSpeed;
    public float effectSpawnRate = 1;
    public WeaponType type;
    public float reloadTime = 1f;
    public GameObject bulletPrefab;

    [HideInInspector]
    public int bullets;

    void Awake() {
        bullets = magazineSize;
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

    public bool Reload() {
        int gainedBullets = Inventory.instance.FindBullets(type, magazineSize - bullets);

        bullets += gainedBullets;
        EquipmentManager.instance.SubscribeChange();

        return gainedBullets > 0;
    }
}

public enum WeaponType { Pistol, MachineGun };

