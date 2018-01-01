using UnityEngine;
using System.Collections.Generic;

public class NpcStats : CharacterStats {
    public List<GameObject> items;
    public int experienceGained = 100;

    public Animator anim;

    public override void Die() {
        if (!anim.GetBool("Died")) {
            base.Die();
            DropItems();
            LevelsManager.instance.AddExperience(experienceGained);
            anim.SetBool("Died", true);
            Destroy(gameObject, 3f);
        }
    }

    void DropItems() {
        for (int i = 0; i < items.Count; i++) {
            Vector2 pos = new Vector2(transform.position.x + i * .1f, transform.position.y);
            Instantiate(items[i], pos, transform.rotation);
        }
    }
}
