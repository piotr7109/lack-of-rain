using UnityEngine;
using UnityEngine.EventSystems;

public class WeaponController : MonoBehaviour {

    public LayerMask whatToHit;
    public Transform bulletTrailPrefab;
    public Transform muzzleFlashPrefab;
    public Transform hitPrefab;

    private float timeToFire = 0;
    private float timeToSpawnEffect = 0;
    private Transform firePoint;
    private Camera cam;
    private Weapon weapon;

    void Start() {
        cam = Camera.main;
        firePoint = transform.FindChild("FirePoint");

        EquipmentManager.instance.onWeaponChanged += OnWeaponChanged;
    }

    void Update() {
        if (weapon != null) {
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
        }
    }

    void TryToShoot() {
        if (CheckIfCanShoot()) {
            Shoot();
        }
    }

    bool CheckIfCanShoot() {
        return (
            weapon.bullets > 0 &&
            !EventSystem.current.IsPointerOverGameObject()
            );
    }

    void OnWeaponChanged(Weapon weapon) {
        this.weapon = weapon;
    }

    void Shoot() {
        Vector2 mousePosition = (Vector2)cam.ScreenToWorldPoint(Input.mousePosition);
        Vector2 firePointPosition = (Vector2)firePoint.position;
        RaycastHit2D hit = Physics2D.Raycast(firePointPosition, mousePosition - firePointPosition, 100, whatToHit);

        if (hit.collider != null) {
            //damage enemy
        }

        if (Time.time >= timeToSpawnEffect) {
            Vector3 hitPos;
            Vector3 hitNormal;

            if (hit.collider == null) {
                hitPos = (mousePosition - firePointPosition) * 30;
                hitNormal = new Vector3(9999, 9999, 9999);
            } else {
                hitPos = hit.point;
                hitNormal = hit.normal;
            }

            Effect(hitPos, hitNormal);
            timeToSpawnEffect = Time.time + 1 / weapon.effectSpawnRate;
            weapon.bullets--;
        }
    }

    void Effect(Vector3 hitPos, Vector3 hitNormal) {
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
    }
}
