using UnityEngine;
using UnityStandardAssets._2D;

public class PlayerController : MonoBehaviour {

    private PlatformerCharacter2D character2D;
    private bool isJumping;
    private Camera cam;
    private WeaponShooting shooting;
    private CharacterRotation rotation;

    void Start() {
        cam = Camera.main;
        character2D = PlayerManager.player.GetComponent<PlatformerCharacter2D>();
        shooting = PlayerManager.shooting;
        rotation = PlayerManager.rotation;
    }

    void Update() {
        if (!isJumping) {
            isJumping = Input.GetButtonDown("Jump");
        }

        if (Input.GetButtonDown("Fire1")) {
            shooting.ShootSingle();
        } else if (Input.GetButton("Fire1")) {
            shooting.ShootMultiple();
        } else if (Input.GetButtonDown("Reload")) {
            shooting.ReloadWeapon();
        }

        rotation.SetLookTarget(cam.ScreenToWorldPoint(Input.mousePosition));
    }

    void FixedUpdate() {
        bool crouch = Input.GetKey(KeyCode.LeftControl);

        character2D.Move(GetDirection(), crouch, isJumping);
        isJumping = false;
    }

    float GetDirection() {
        if (Input.GetKey(KeyCode.A)) {
            return -1;
        } else if (Input.GetKey(KeyCode.D)) {
            return 1;
        }

        return 0;
    }
}
