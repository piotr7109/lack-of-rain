using UnityEngine;
using System.Collections;
using UnityStandardAssets._2D;

public abstract class WeaponShooting : MonoBehaviour {

    private float timeToFire = 0;
    public Weapon weapon;
    public Transform firePoint;
    public PlatformerCharacter2D characterGFX;
    public CharacterAnimator characterAnimator;

    private bool isReloading = false;
    private ShootManager shootManager;

    void Awake() {
        shootManager = gameObject.AddComponent<ShootManager>();
        shootManager.setParameters(characterGFX, firePoint);
    }

    void Update() {
        bool wantShoot = WantShoot();

        if (weapon == null) {
            if (wantShoot) {
                characterAnimator.MeleeAttack();
            }
            return;
        }

        if (isReloading) {
            return;
        }

        if (weapon.fireRate == 0) {
            if (wantShoot) {
                TryToShoot();
            }
        } else {
            if (WantShootMultiple() && Time.time > timeToFire) {
                timeToFire = Time.time + 1 / weapon.fireRate;
                TryToShoot();
            }
        }

        if (WantReload()) {
            StartCoroutine(Reload());
        }
    }

    private IEnumerator Reload() {
        isReloading = true;

        if (WeaponReload()) {
            characterAnimator.ReloadWeapon(weapon.reloadTime);
            yield return new WaitForSeconds(weapon.reloadTime);
        } else {
            yield return null;
        }

        isReloading = false;
    }

    void TryToShoot() {
        if (CheckIfCanShoot()) {
            shootManager.Shoot(weapon);
        }
    }

    protected abstract bool WantShoot();
    protected abstract bool WantShootMultiple();
    protected abstract bool WantReload();
    protected abstract bool WeaponReload();
    protected abstract bool CheckIfCanShoot();
}
