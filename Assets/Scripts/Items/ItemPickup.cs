using UnityEngine;

public class ItemPickup : Interactable {

    public Item item;

    public override void Interact() {
        base.Interact();
        PickUp();
    }

    void PickUp() {
        Item itemClone = Object.Instantiate(item) as Item;

        if (itemClone.icon == null) {
            itemClone.icon = transform.GetComponent<SpriteRenderer>().sprite;
        }

        if (Inventory.instance.Add(itemClone)) {
            Destroy(gameObject);
        }
    }

}
