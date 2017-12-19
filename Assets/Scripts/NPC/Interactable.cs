using UnityEngine;

public class Interactable : MonoBehaviour {

    public float radius = 3f;

    protected Transform player;

    public virtual void Start() {
        player = PlayerManager.instance.player.transform;
    }

    void Update() {
        float distance = Vector2.Distance(player.position, transform.position);

        if (distance <= radius) {

            if (Input.GetButtonDown("Action")) {
                Interact();
            }
        }
    }

    public virtual void Interact() {
    }

    protected virtual void OnDrawGizmosSelected() {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
