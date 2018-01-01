using UnityEngine;

public class EnemyRotation : CharacterRotation {

    private Transform player;

    void Start() {
        player = PlayerManager.instance.player.transform;
    }

    protected override Vector3 GetLookTarget() {
        return player.position;
    }
}
