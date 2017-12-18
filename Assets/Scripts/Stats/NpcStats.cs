using UnityEngine;
using System.Collections.Generic;

public class NpcStats : CharacterStats {
    public List<GameObject> items;

    public override void Die() {
        base.Die();
        DropItems();
        Destroy(gameObject/*, 2f*/);
    }

    void DropItems() {
        for (int i = 0; i < items.Count; i++) {
            Vector2 pos = new Vector2(transform.position.x + i * .1f, transform.position.y);
            Instantiate(items[i], pos, transform.rotation);
        }
    }
}
