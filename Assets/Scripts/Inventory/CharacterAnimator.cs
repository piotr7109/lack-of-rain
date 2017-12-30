using UnityEngine;
using System.Collections;

public class CharacterAnimator : MonoBehaviour {

    public GameObject weaponGameObject;

    public Animator bodyAnim;
    EquipmentManager equipmentManager;

    void Start() {
        equipmentManager = EquipmentManager.instance;
        equipmentManager.onWeaponChanged += UpdateWeapon;
    }

    void UpdateWeapon(Weapon weapon) {
        bool hasWeapon = weapon != null;

        bodyAnim.SetBool("HasWeapon", hasWeapon);
        StartCoroutine(ShowWeapon(hasWeapon));
    }

    IEnumerator ShowWeapon(bool show) {
        weaponGameObject.SetActive(show);
        yield return new WaitForSeconds(1.5f);

    }
}
