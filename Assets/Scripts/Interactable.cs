using UnityEngine;

public class Interactable : MonoBehaviour {

    public float radius = 3f;

    protected Transform player;
    
    private GameObject interactionTooltip;

    protected static PrefabsManager prefabsManager;

    public virtual void Start() {
        if (prefabsManager == null) {
            prefabsManager = PrefabsManager.instance;
        }

        player = PlayerManager.instance.player.transform;
        CreateTooltip();
    }

    void CreateTooltip() {
        GameObject tooltipPrefab = prefabsManager.interactionTooltip;
        interactionTooltip = Instantiate(tooltipPrefab, transform) as GameObject;
        interactionTooltip.transform.localPosition = tooltipPrefab.transform.position;
        interactionTooltip.SetActive(false);
        SetTooltipSprite(prefabsManager.interactionIcon);
    }

    protected void SetTooltipSprite(Sprite sprite) {
        interactionTooltip.GetComponent<SpriteRenderer>().sprite = sprite;
    }

    void Update() {
        float distance = Vector2.Distance(player.position, transform.position);

        if (distance <= radius) {
            interactionTooltip.SetActive(true);

            if (Input.GetButtonDown("Action")) {
                Interact();
            }
        } else {
            interactionTooltip.SetActive(false);
        }
    }

    public virtual void Interact() {
    }

    protected virtual void OnDrawGizmosSelected() {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
