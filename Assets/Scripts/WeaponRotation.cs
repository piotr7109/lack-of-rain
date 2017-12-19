using UnityEngine;
using UnityStandardAssets._2D;

public class WeaponRotation : MonoBehaviour {
    
    public PlatformerCharacter2D playerGFX;
    Camera cam;

    void Awake() {
        cam = Camera.main;
    }

    void Update() {
        Vector3 difference = cam.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        difference.Normalize();

        if (!playerGFX.m_FacingRight) {
            difference *= -1;
        }

        float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;

        rotZ = Mathf.Clamp(rotZ, -70, 70);
        transform.rotation = Quaternion.Euler(0, 0, rotZ);
    }
}
