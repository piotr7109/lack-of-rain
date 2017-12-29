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

        RecognizeTarget(collider.transform);
        hasHit = true;
        Destroy(gameObject, groundLifeTime);
    }

    void RecognizeTarget(Transform target) {
        if (target.tag == "Enemy" || target.tag == "Player") {
            target.GetComponent<CharacterStats>().TakeDamage(damage);
            Destroy(gameObject);

            Debug.Log(target.gameObject.name);
        }
    }
}
