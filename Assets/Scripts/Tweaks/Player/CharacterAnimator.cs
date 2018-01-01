using UnityEngine;
using UnityStandardAssets._2D;
using System.Collections;

public class CharacterAnimator : MonoBehaviour {

    public GameObject machineGun;
    public GameObject pistol;
    public GameObject knife;
    public Transform bodyTransform;

    public GameObject leftArmIK;
    public GameObject rightArmIk;

    public Animator bodyAnim;
    public Animator legsAnim;
    public CharacterRotation weaponRotation;
    public WeaponShooting weaponShooting;
    private PlatformerCharacter2D playerGFX;

    EquipmentManager equipmentManager;

    void Start() {
        playerGFX = GetComponentInParent<PlatformerCharacter2D>();
        UpdateWeapon(weaponShooting.weapon);
    }

    public void UpdateWeapon(Weapon weapon) {
        HideWeapons();
        int weaponType = weapon != null ? (int)weapon.type : 0;

        bodyAnim.SetInteger("WeaponType", weaponType);
        StartCoroutine(ShowWeapon(weaponType));
    }

    IEnumerator ShowWeapon(int weaponType) {
        EnableIks();
        switch (weaponType) {
            case 0:
                knife.SetActive(true);
                break;
            case 1:
                pistol.SetActive(true);
                break;
            case 2:
                machineGun.SetActive(true);
                break;
        }

        ResetRotation();

        yield return new WaitForSeconds(.7f);
        EnableIks(false);
    }

    public void ReloadWeapon(float reloadTime) {
        StartCoroutine(ReloadAnim(reloadTime));
    }

    IEnumerator ReloadAnim(float reloadTime) {
        EnableIks();
        bodyAnim.SetBool("Reloading", true);
        ResetRotation();

        yield return new WaitForSeconds(reloadTime);

        bodyAnim.SetBool("Reloading", false);
        EnableIks(false);
    }

    public void Die() {
        EnableIks();
        weaponShooting.enabled = false;
        ResetRotation();
        bodyAnim.SetBool("Died", true);
        legsAnim.SetBool("Died", true);
    }

    public void MeleeAttack() {
        StartCoroutine(PerformMeleeAttack());
    }

    IEnumerator PerformMeleeAttack() {
        int attackMode = Random.Range(0, 2); //TODO

        EnableIks();
        ResetRotation();
        bodyAnim.SetInteger("AttackMode", attackMode);

        yield return new WaitForSeconds(2f);

        bodyAnim.SetInteger("AttackMode", -1);
        EnableIks(false);
    }

    void EnableIks(bool enable = true) {
        weaponRotation.enabled = !enable;
        leftArmIK.SetActive(enable);
        rightArmIk.SetActive(enable);
    }

    void HideWeapons() {
        machineGun.SetActive(false);
        pistol.SetActive(false);
        knife.SetActive(false);
    }

    void ResetRotation() {
        bodyTransform.eulerAngles = new Vector3(0, 0, 90 * (playerGFX.m_FacingRight ? 1 : -1));
    }
}
