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
    public WeaponRotation weaponRotation;
    public WeaponController weaponController;
    private PlatformerCharacter2D playerGFX;

    EquipmentManager equipmentManager;

    void Start() {
        playerGFX = PlayerManager.instance.player.GetComponent<PlatformerCharacter2D>();
        equipmentManager = EquipmentManager.instance;
        equipmentManager.onWeaponChanged += UpdateWeapon;
        weaponController.onReloading += ReloadWeapon;
    }

    void UpdateWeapon(Weapon weapon) {
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

        bodyTransform.eulerAngles = new Vector3(0, 0, 90 * (playerGFX.m_FacingRight ? 1 : -1));

        yield return new WaitForSeconds(.7f);
        EnableIks(false);
    }

    void ReloadWeapon(float reloadTime) {
        StartCoroutine(ReloadAnim(reloadTime));
    }

    IEnumerator ReloadAnim(float reloadTime) {
        EnableIks();
        bodyAnim.SetBool("Reloading", true);
        bodyTransform.eulerAngles = new Vector3(0, 0, 90 * (playerGFX.m_FacingRight ? 1 : -1));

        yield return new WaitForSeconds(reloadTime);

        bodyAnim.SetBool("Reloading", false);
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
}
