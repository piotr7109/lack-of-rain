using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using UnityStandardAssets._2D;

public class WeaponController : MonoBehaviour {

    private float timeToFire = 0;
    public Weapon weapon;
    public Transform firePoint;
    public PlatformerCharacter2D playerGFX;

    private bool isReloading = false;
    private ShootManager shootManager;

    void Start() {
        shootManager = gameObject.AddComponent<ShootManager>();
        shootManager.setParameters(playerGFX, firePoint);

        EquipmentManager.instance.onWeaponChanged += OnWeaponChanged;
    }

    void Update() {
        if (isReloading || weapon == null) {
            return;
        }

        if (weapon.fireRate == 0) {
            if (Input.GetButtonDown("Fire1")) {
                TryToShoot();
            }
        } else {
            if (Input.GetButton("Fire1") && Time.time > timeToFire) {
                timeToFire = Time.time + 1 / weapon.fireRate;
                TryToShoot();
            }
        }

        if (Input.GetButtonDown("Reload")) {
            StartCoroutine(Reload());
        }
    }

    void OnWeaponChanged(Weapon weapon) {
        this.weapon = weapon;
    }

    IEnumerator Reload() {
        isReloading = true;

        if (weapon.Reload()) {
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

    bool CheckIfCanShoot() {
        return
            weapon.bullets > 0 &&
            !EventSystem.current.IsPointerOverGameObject();
    }
}
