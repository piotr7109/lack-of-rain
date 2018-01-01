using UnityEngine;

public class EnemyShooting : WeaponShooting {

    [HideInInspector]
    public bool wantToShoot;

    protected override bool WantShoot() {
        return wantToShoot && (weapon == null || weapon.type == WeaponType.Pistol);
    }

    protected override bool WantShootMultiple() {
        return wantToShoot && weapon.type == WeaponType.MachineGun;
    }

    protected override bool WantReload() {
        return weapon != null && weapon.bullets <= 0;
    }

    protected override bool WeaponReload() {
        weapon.bullets = weapon.magazineSize;
        return true;
    }

    protected override bool CheckIfCanShoot() {
        return weapon.bullets > 0;
    }
}
