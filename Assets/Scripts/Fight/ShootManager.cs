using UnityEngine;
using UnityStandardAssets._2D;
using System.Collections;

public class ShootManager : MonoBehaviour {

    private PlatformerCharacter2D character;
    private Transform firePoint;
    private GameObject muzzleFlash;
    private Equipment equipment;

    public void setParameters(PlatformerCharacter2D character, Transform firePoint, Equipment equipment) {
        this.character = character;
        this.firePoint = firePoint;
        this.equipment = equipment;

        muzzleFlash = firePoint.Find("MuzzleFlash").gameObject;
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
            equipment.SubscribeChange();
            MakeEffects();

            if (!turnedRight) {
                bullet.transform.localScale = bullet.transform.localScale *= -1;
            }
        }
    }

    void MakeEffects() {
        float random = Random.Range(0f, 1f);

        if (random > .3f) {
            StartCoroutine(MuzzleFlashEffect());
        }
    }

    IEnumerator MuzzleFlashEffect() {
        muzzleFlash.SetActive(true);

        yield return new WaitForSeconds(.1f);

        muzzleFlash.SetActive(false);
    }
}
