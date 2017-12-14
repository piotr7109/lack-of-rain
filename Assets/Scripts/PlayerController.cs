using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour {

    public Camera cam;

    private Interactable currFocus;

    void Awake() {
        if (cam == null) {
            cam = Camera.main;
        }
    }

    void Update() {
        if (EventSystem.current.IsPointerOverGameObject()) {
            return;
        }

        if (Input.GetButtonDown("Action")) {
            makeAction();
        }
    }

    void makeAction() {
        if (currFocus != null) {
            float distance = Vector2.Distance(transform.position, currFocus.transform.position);

            if (distance <= currFocus.radius) {
                currFocus.Interact();
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collider) {
        Interactable interactable = collider.transform.GetComponent<Interactable>();

        if (interactable != null) {
            currFocus = interactable;
        }
    }
}
