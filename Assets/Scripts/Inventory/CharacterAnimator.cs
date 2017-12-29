using UnityEngine;
using System.Collections;

public class CharacterAnimator : MonoBehaviour {

    public GameObject weaponGameObject;

    Animator anim;
    EquipmentManager equipmentManager;

    void Start() {
        equipmentManager = EquipmentManager.instance;
        equipmentManager.onWeaponChanged += UpdateWeapon;
        anim = GetComponent<Animator>();
    }

    void UpdateWeapon(Weapon weapon) {
        bool hasWeapon = weapon != null;

        anim.SetBool("HasWeapon", hasWeapon);
        StartCoroutine(ShowWeapon(hasWeapon));
    }

    IEnumerator ShowWeapon(bool show) {
        weaponGameObject.SetActive(show);
        yield return new WaitForSeconds(1.5f);

    }
}
