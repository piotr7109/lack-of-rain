using UnityEngine;
using UnityStandardAssets._2D;

public class ShootManager : MonoBehaviour {

    private PlatformerCharacter2D character;
    private Transform firePoint;

    public void setParameters(PlatformerCharacter2D character, Transform firePoint) {
        this.character = character;
        this.firePoint = firePoint;
    }

    private float timeToSpawnEffect = 0;

    public void Shoot(Weapon weapon) {
        if (Time.time >= timeToSpawnEffect) {
            bool turnedRight = character.m_FacingRight;
            float aimSpreadFactor = Random.Range(-weapon.aimSpread, weapon.aimSpread);
            Quaternion rotation = firePoint.rotation * Quaternion.Euler(0, 0, aimSpreadFactor);
            GameObject bullet = Instantiate(weapon.bulletPrefab, firePoint.position, rotation) as GameObject;
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            Vector2 direction = turnedRight ? Vector2.right : Vector2.left;

            rb.AddForce(rotation * direction * weapon.bulletSpeed * 10);

            bullet.GetComponent<BulletController>().damage = weapon.damage;

            timeToSpawnEffect = Time.time + 1 / weapon.effectSpawnRate;
            weapon.Shoot();

            if (!turnedRight) {
                bullet.transform.localScale = bullet.transform.localScale *= -1;
            }
        }
    }
}
