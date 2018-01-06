public class ItemPickup : Interactable {

    public Item item;

    public override void Start() {
        base.Start();
        SetTooltipSprite(prefabsManager.pickUpIcon);
    }

    public override void Interact() {
        base.Interact();
        PickUp();
    }

    void PickUp() {
        if (Inventory.instance.Add(Instantiate(item) as Item)) {
            Destroy(gameObject);
        }
    }

}
