using UnityEngine;

public class PlayerRotation : CharacterRotation {

    Camera cam;

    void Awake() {
        cam = Camera.main;
    }

    protected override Vector3 GetLookTarget() {
        return cam.ScreenToWorldPoint(Input.mousePosition);
    }
}
