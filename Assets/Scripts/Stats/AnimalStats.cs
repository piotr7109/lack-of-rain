using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalStats : CharacterStats {
    public int experienceGained = 100;
    public Animator anim;

    void Start() {
        anim = GetComponentInChildren<Animator>();
    }

    public override void Die() {
        if (!anim.GetBool("Died")) {
            base.Die();
            LevelsManager.instance.AddExperience(experienceGained);
            anim.SetBool("Died", true);
            Destroy(gameObject, 3f);
        }
    }
}
