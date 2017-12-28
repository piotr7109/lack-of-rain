using UnityEngine;
using UnityStandardAssets._2D;

public class WeaponRotation : MonoBehaviour {
    
    public PlatformerCharacter2D playerGFX;
    Camera cam;

    public Transform playerBody;
    public Transform firePoint;

    void Awake() {
        cam = Camera.main;
    }

    void Update() {
        Vector3 difference = cam.ScreenToWorldPoint(Input.mousePosition) - playerBody.transform.position;
        difference.y -= .5f;
        difference.Normalize();


        if (!playerGFX.m_FacingRight) {
            difference *= -1;
        }

        float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;

        rotZ = Mathf.Clamp(rotZ, -70, 70) + 90 * (!playerGFX.m_FacingRight ? -1 : 1);
        playerBody.eulerAngles = new Vector3(0, 0, rotZ);
    }
}
