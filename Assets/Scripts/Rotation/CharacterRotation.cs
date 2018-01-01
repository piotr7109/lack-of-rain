using UnityEngine;
using UnityStandardAssets._2D;

public abstract class CharacterRotation : MonoBehaviour {

    public PlatformerCharacter2D characterGFX;
    public Transform playerBody;

    public Animator anim;

    private float initialRotation = 90;

    protected abstract Vector3 GetLookTarget();

    void Update() {
        if (!anim.GetBool("Died")) {
            SetCharacterRotation();
        }
    }

    void SetCharacterRotation() {
        Vector3 difference = GetLookTarget() - playerBody.transform.position;
        difference.y -= .5f;
        difference.Normalize();
        SetCharacterDirection(difference.x > 0);

        if (!characterGFX.m_FacingRight) {
            difference *= -1;
        }

        float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;

        rotZ = Mathf.Clamp(rotZ, -70, 70) + initialRotation * (characterGFX.m_FacingRight ? 1 : -1);
        playerBody.eulerAngles = new Vector3(0, 0, rotZ);
    }

    void SetCharacterDirection(bool right) {
        Vector3 scale = characterGFX.transform.localScale;

        if (right) {
            scale.x = Mathf.Abs(scale.x);
            characterGFX.m_FacingRight = true;
        } else {
            scale.x = Mathf.Abs(scale.x) * -1;
            characterGFX.m_FacingRight = false;
        }

        characterGFX.transform.localScale = scale;
    }
}
