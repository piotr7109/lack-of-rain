using UnityEngine;
using UnityStandardAssets._2D;

public class WeaponRotation : MonoBehaviour {

    public PlatformerCharacter2D playerGFX;
    Camera cam;

    public Transform playerBody;

    private float initialRotation = 90;

    void Awake() {
        cam = Camera.main;
    }

    void Update() {
        Vector3 difference = cam.ScreenToWorldPoint(Input.mousePosition) - playerBody.transform.position;
        difference.y -= .5f;
        difference.Normalize();
        SetPlayerDirection(difference.x > 0);

        if (!playerGFX.m_FacingRight) {
            difference *= -1;
        }

        float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;

        rotZ = Mathf.Clamp(rotZ, -70, 70) + initialRotation * (playerGFX.m_FacingRight ? 1 : -1);
        playerBody.eulerAngles = new Vector3(0, 0, rotZ);
    }

    void SetPlayerDirection(bool right) {
        Vector3 scale = playerGFX.transform.localScale;

        if (right) {
            scale.x = Mathf.Abs(scale.x);
            playerGFX.m_FacingRight = true;
        } else {
            scale.x = Mathf.Abs(scale.x) * -1;
            playerGFX.m_FacingRight = false;
        }

        playerGFX.transform.localScale = scale;
    }
}
