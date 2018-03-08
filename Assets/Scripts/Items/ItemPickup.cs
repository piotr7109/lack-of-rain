public class ItemPickup : Interactable {

    public Item item;

    public override void Start() {
        base.Start();
        SetTooltipSprite(prefabsManager.pickUpIcon);
    }

    public override void Interact(Inventory inventory) {
        base.Interact(inventory);
        PickUp(inventory);
    }

    void PickUp(Inventory inventory) {
        if (inventory.Add(Instantiate(item) as Item)) {
            Destroy(gameObject);
        }
    }

}
