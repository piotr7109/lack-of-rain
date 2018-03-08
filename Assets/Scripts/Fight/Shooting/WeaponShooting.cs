using UnityEngine;
using System.Collections;
using UnityStandardAssets._2D;

public class WeaponShooting : MonoBehaviour {

    private float timeToFire = 0;
    public Transform firePoint;
    public float meleeAttackSpeed = 1f;
    public PlatformerCharacter2D characterGFX;
    public CharacterAnimator characterAnimator;
    private CharacterStats selfStats;
    public Animator anim;

    private Inventory inventory;
    private Equipment equipment;
    private bool isReloading = false;
    private ShootManager shootManager;
    private MeleePoint meleePoint;
    private Weapon weapon;

    void Awake() {
        shootManager = gameObject.AddComponent<ShootManager>();
        selfStats = GetComponentInParent<CharacterStats>();
        inventory = GetComponentInParent<Inventory>();
        equipment = GetComponentInParent<Equipment>();
        meleePoint = GetComponentInChildren<MeleePoint>();
        shootManager.setParameters(characterGFX, firePoint, equipment);

        equipment.onWeaponChanged += UpdateWeapon;
        UpdateWeapon(equipment.weapon);
    }

    //simplify reference
    void UpdateWeapon(Weapon weapon) {
        this.weapon = weapon;
    }

    public void ShootSingle() {
        if (weapon == null) {
            StartCoroutine(AttackMelee());
            return;
        }

        if (isReloading || weapon.fireRate > 0) {
            return;
        }

        if (Time.time > timeToFire) {
            timeToFire = Time.time + 1 / weapon.effectSpawnRate;
            TryToShoot();
        }
    }

    public void ShootMultiple() {
        if (weapon == null) {
            StartCoroutine(AttackMelee());
            return;
        }

        if (weapon.fireRate == 0 || isReloading) {
            return;
        }

        if (Time.time > timeToFire) {
            timeToFire = Time.time + 1 / weapon.fireRate;
            TryToShoot();
        }
    }

    public void ReloadWeapon() {
        StartCoroutine(Reload());
    }

    private float timeToMeleeAttack = 0;

    IEnumerator AttackMelee() {
        if (Time.time > timeToMeleeAttack) {
            timeToMeleeAttack = Time.time + 1 / meleeAttackSpeed;
            characterAnimator.MeleeAttack();

            yield return new WaitForSeconds(meleeAttackSpeed / 2f); //Animation fluency purpose
            
            GameObject target = meleePoint.GetMeleeTarget();

            if (target != null) {
                Debug.Log(target);
                target.GetComponentInParent<CharacterStats>().TakeDamage(selfStats.meleeDamage.GetValue());
            }
        } else {
            yield return null;
        }
    }

    private IEnumerator Reload() {
        isReloading = true;

        if (weapon.Reload(inventory)) {
            characterAnimator.ReloadWeapon(weapon.reloadTime);
            yield return new WaitForSeconds(weapon.reloadTime);
        } else {
            yield return null;
        }

        isReloading = false;
    }

    void TryToShoot() {
        if (weapon.bullets > 0) {
            shootManager.Shoot(weapon);
        } else {
            equipment.EquipWeapon(null);
        }
    }
}
