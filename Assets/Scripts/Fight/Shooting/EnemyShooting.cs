public class EnemyShooting : WeaponShooting {

    protected override bool WantShoot() {
        return weapon.type == WeaponType.Pistol;
    }

    protected override bool WantShootMultiple() {
        return weapon.type == WeaponType.MachineGun;
    }

    protected override bool WantReload() {
        return weapon.bullets <= 0;
    }

    protected override bool WeaponReload() {
        weapon.bullets = weapon.magazineSize;
        return true;
    }

    protected override bool CheckIfCanShoot() {
        return weapon.bullets > 0;
    }
}
