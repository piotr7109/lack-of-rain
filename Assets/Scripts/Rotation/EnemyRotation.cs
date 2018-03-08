using UnityEngine;

public class EnemyRotation : CharacterRotation {

    private Transform player;

    void Start() {
        player = PlayerManager.player.transform;
    }
}
