using UnityEngine;
using System.Collections;

public class EnemyStats : CharacterStats {

	public override void Die() {
        Destroy(gameObject/*, 2f*/);
    }
}
