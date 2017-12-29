using UnityEngine;

public class Head : MonoBehaviour {

    void OnCollisionEnter2D(Collision2D collider) {

        if (collider.gameObject.tag == "Bullet") {
            TakeHeadshotDamage(collider.gameObject);
        }
    }

    void TakeHeadshotDamage(GameObject bullet) {
        BulletController bulletController = bullet.GetComponent<BulletController>();
        CharacterStats stats = GetComponentInParent<CharacterStats>();
        float random = Random.Range(0, 1f);

        stats.TakeDamage((int)(bulletController.damage * random));
    }
}
