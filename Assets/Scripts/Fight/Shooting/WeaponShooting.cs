using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityStandardAssets._2D;

public abstract class WeaponShooting : MonoBehaviour {

    private float timeToFire = 0;
    public Weapon weapon;
    public Transform firePoint;
    public float meleeAttackSpeed = 1f;
    public float meleeAttackDamage;
    public PlatformerCharacter2D characterGFX;
    public CharacterAnimator characterAnimator;
    private CharacterStats selfStats;

    private bool isReloading = false;
    private ShootManager shootManager;

    void Awake() {
        shootManager = gameObject.AddComponent<ShootManager>();
        shootManager.setParameters(characterGFX, firePoint);
        selfStats = GetComponentInParent<CharacterStats>();
    }

    void Update() {
        bool wantShoot = WantShoot();

        if (weapon == null) {
            if (wantShoot) {
                StartCoroutine(AttackMelee());
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

    private float timeToMeleeAttack = 0;
    private List<GameObject> meleeTargets = new List<GameObject>();
    protected abstract string GetTargetTag();

    IEnumerator AttackMelee() {
        if (Time.time > timeToMeleeAttack) {
            timeToMeleeAttack = Time.time + 1 / meleeAttackSpeed;
            characterAnimator.MeleeAttack();

            yield return new WaitForSeconds(meleeAttackSpeed / 2f); //Animation fluency purpose

            if (meleeTargets.Count > 0) {
                GameObject target = GetMeleeTarget();

                if (target != null) {
                    Debug.Log(target);
                    target.GetComponentInParent<CharacterStats>().TakeDamage(selfStats.damage.GetValue());
                }
            }
        } else {
            yield return null;
        }
    }

    GameObject GetMeleeTarget() {
        bool turnedRight = characterGFX.m_FacingRight;

        meleeTargets.RemoveAll(item => item == null);
        return meleeTargets.Find(target => {
            float targetX = target.transform.position.x;
            float selfX = transform.position.x;

            return turnedRight ? targetX >= selfX : targetX <= selfX;
        });
    }

    void OnTriggerEnter2D(Collider2D collider) {
        if (collider.tag == GetTargetTag()) {
            GameObject target = collider.gameObject;

            if (!meleeTargets.Contains(target)) {
                meleeTargets.Add(target);
            }
        }
    }

    void OnTriggerExit2D(Collider2D collider) {
        if (collider.tag == GetTargetTag()) {
            meleeTargets.Remove(collider.gameObject);
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
