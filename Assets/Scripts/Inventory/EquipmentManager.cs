using UnityEngine;

public class EquipmentManager : MonoBehaviour {

    #region Singleton

    public static EquipmentManager instance;

    void Awake() {
        if (instance == null) {
            instance = this;
        } else {
            Debug.LogWarning("More than one instance of EqipmentManager found!");
        }
    }

    #endregion

    public Armour armour;
    public Weapon weapon;

    public delegate void OnArmourChanged(Armour armour);
    public delegate void OnWeaponChanged(Weapon weapon);
    public OnArmourChanged onArmourChanged;
    public OnWeaponChanged onWeaponChanged;

    public void EquipArmour(Armour armour) {
        UnequipArmour();
        this.armour = armour;
        SubscribeChange();
        SubscribeArmourChange();
    }

    public void UnequipArmour() {
        if (armour != null) {
            armour.equiped = false;
            armour = null;
            SubscribeChange();
            SubscribeArmourChange();
        }
    }

    public void EquipWeapon(Weapon weapon) {
        UnequipWeapon();
        this.weapon = weapon;
        SubscribeChange();
        SubscribeWeaponChange();
    }

    public void UnequipWeapon() {
        if (weapon != null) {
            weapon.equiped = false;
            weapon = null;
            SubscribeChange();
            SubscribeWeaponChange();
        }
    }

    public void SubscribeChange() {
        Inventory.instance.SubscribeChange();
    }

    public void SubscribeArmourChange() {
        if (onArmourChanged != null) {
            onArmourChanged.Invoke(armour);
        }
    }

    public void SubscribeWeaponChange() {
        if (onWeaponChanged != null) {
            onWeaponChanged.Invoke(weapon);
        }
    }
}
