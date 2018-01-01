using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerShooting : WeaponShooting {

    void Start() {
        EquipmentManager.instance.onWeaponChanged += OnWeaponChanged;
    }

    void OnWeaponChanged(Weapon weapon) {
        this.weapon = weapon;
        characterAnimator.UpdateWeapon(weapon);
    }

    protected override bool WantShoot() {
        return Input.GetButtonDown("Fire1");
    }

    protected override bool WantShootMultiple() {
        return Input.GetButton("Fire1");
    }

    protected override bool WantReload() {
        return Input.GetButtonDown("Reload");
    }

    protected override bool WeaponReload() {
        return weapon.Reload();
    }

    protected override bool CheckIfCanShoot() {
        return
            weapon.bullets > 0 &&
            !EventSystem.current.IsPointerOverGameObject();
    }

    protected override string GetTargetTag() {
        return "Enemy";
    }
}
