using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour {

    public Image icon;
    public Button removeButton;
    public Image equipMark;

    Item item;

    public void AddItem(Item newItem) {
        item = newItem;

        icon.sprite = item.icon;
        icon.enabled = true;
        removeButton.interactable = !newItem.equiped;
        equipMark.enabled = newItem.equiped;
    }

    public void ClearSlot() {
        item = null;
        icon.sprite = null;
        icon.enabled = false;
        removeButton.interactable = false;
        equipMark.enabled = false;
    }

    public void OnRemoveButton() {
        item.RemoveFromInventory();
    }

    public void UseItem() {
        if (item != null) {
            item.Use();
        }
    }
}
