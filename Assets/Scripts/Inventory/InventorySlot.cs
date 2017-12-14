using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour {

    public Image icon;
    public Button removeButton;
    public Image equipMark;
    public Text amountText;

    Item item;

    public void AddItem(Item newItem) {
        item = newItem;

        icon.sprite = item.icon;
        icon.enabled = true;
        removeButton.interactable = !newItem.equiped;
        equipMark.enabled = newItem.equiped;
        ShowAmountMark(item);
    }

    public void ClearSlot() {
        item = null;
        icon.sprite = null;
        icon.enabled = false;
        removeButton.interactable = false;
        equipMark.enabled = false;
        amountText.enabled = false;
    }

    void ShowAmountMark(Item item) {
        if (item is Bullets) {
            amountText.text = (item as Bullets).amount + "";
            amountText.enabled = true;
        } else {
            amountText.enabled = false;
        }
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
