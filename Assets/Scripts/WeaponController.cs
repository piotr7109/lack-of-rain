using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class WeaponController : MonoBehaviour {

    public LayerMask whatToHit;
    public Transform bulletTrailPrefab;
    public Transform muzzleFlashPrefab;
    public Transform hitPrefab;

    private float timeToFire = 0;
    private float timeToSpawnEffect = 0;
    private Transform firePoint;
    private Camera cam;
    public Weapon weapon;

    private bool isReloading = false;

    void Start() {
        cam = Camera.main;
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

    void OnWeaponChanged(Weapon weapon) {
        this.weapon = weapon;
    }
    
    void Shoot() {
        if (Time.time >= timeToSpawnEffect) {
            GameObject bullet = Instantiate(weapon.bulletPrefab, firePoint.position, firePoint.rotation) as GameObject;
            Vector3 mousePosition = cam.ScreenToWorldPoint(Input.mousePosition);
            Vector3 mouseDirection = mousePosition - firePoint.position;
            mouseDirection.z = 0;
            mouseDirection = mouseDirection.normalized;
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.AddForce(mouseDirection * weapon.bulletSpeed * 10);

            bullet.GetComponent<BulletController>().damage = weapon.damage;

            timeToSpawnEffect = Time.time + 1 / weapon.effectSpawnRate;
            weapon.bullets--;
        }
    }

   /* void Effect(Vector3 hitPos, Vector3 hitNormal) {
        Transform trail = Instantiate(bulletTrailPrefab, firePoint.position, firePoint.rotation) as Transform;
        LineRenderer lr = trail.GetComponent<LineRenderer>();

        if (lr != null) {
            lr.SetPosition(0, firePoint.position);
            lr.SetPosition(1, hitPos);
        }

        Destroy(trail.gameObject, .04f);

        if (hitNormal != new Vector3(9999, 9999, 9999)) {
            Transform hitParticle = Instantiate(hitPrefab, hitPos, Quaternion.FromToRotation(Vector3.right, hitNormal)) as Transform;
            Destroy(hitParticle.gameObject, 1f);
        }

        Transform clone = Instantiate(muzzleFlashPrefab, firePoint.position, firePoint.rotation) as Transform;
        clone.parent = firePoint;
        float size = Random.Range(.6f, .9f);
        clone.localScale = new Vector3(size, size, size);
        Destroy(clone.gameObject, .02f);
    }*/
}
