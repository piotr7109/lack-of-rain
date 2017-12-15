using UnityEngine;

public class BulletController : MonoBehaviour {

    private bool hasHit = false;
    [HideInInspector]
    public int damage;
    public float lifeTime = 5f;
    public float groundLifeTime = 2f;

    void Awake() {
        Destroy(gameObject, lifeTime);
    }

    void OnCollisionEnter2D(Collision2D collider) {
        if (hasHit) {
            return;
        }

        if (collider.transform.tag == "Enemy") {
            CharacterStats targetStats = collider.transform.GetComponent<CharacterStats>();

            targetStats.TakeDamage(damage);
            Destroy(gameObject);
        }

        hasHit = true;
        Destroy(gameObject, groundLifeTime);
    }
}
