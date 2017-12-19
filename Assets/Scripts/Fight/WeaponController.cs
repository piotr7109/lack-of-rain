using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using UnityStandardAssets._2D;

public class WeaponController : MonoBehaviour {

    private float timeToFire = 0;
    private float timeToSpawnEffect = 0;
    public Weapon weapon;
    private Transform firePoint;
    public PlatformerCharacter2D playerGFX;

    private bool isReloading = false;

    void Start() {
        firePoint = transform.FindChild("FirePoint");

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
            Shoot();
        }
    }

    bool CheckIfCanShoot() {
        return
            weapon.bullets > 0 &&
            !EventSystem.current.IsPointerOverGameObject();
    }

    void Shoot() {
        if (Time.time >= timeToSpawnEffect) {
            bool turnedRight = playerGFX.m_FacingRight;
            GameObject bullet = Instantiate(weapon.bulletPrefab, firePoint.position, firePoint.rotation) as GameObject;
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            Vector2 direction = turnedRight ? Vector2.right : Vector2.left;

            rb.AddForce(firePoint.rotation * direction * weapon.bulletSpeed * 10);

            bullet.GetComponent<BulletController>().damage = weapon.damage;

            timeToSpawnEffect = Time.time + 1 / weapon.effectSpawnRate;
            weapon.Shoot();

            if (!turnedRight) {
                bullet.transform.localScale = bullet.transform.localScale *= -1;
            }
        }
    }
}
