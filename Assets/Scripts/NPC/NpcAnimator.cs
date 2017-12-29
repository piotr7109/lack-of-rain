using UnityEngine;

public class NpcAnimator : MonoBehaviour {

    public GameObject weaponGameObject;
    EnemyNpc enemyNpc;

    Animator anim;

    void Start() {
        anim = GetComponent<Animator>();
        enemyNpc = GetComponent<EnemyNpc>();
        WeaponVisibility();

    }

    void WeaponVisibility() {
        bool show = enemyNpc.weapon != null;

        anim.SetBool("HasWeapon", show);
        weaponGameObject.SetActive(show);
    }
}
