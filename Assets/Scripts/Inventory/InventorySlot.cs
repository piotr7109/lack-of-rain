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
        amountText.text = item.GetAmount() + "";
        amountText.enabled = true;
    }

    public void OnRemoveButton() {
        item.DropItem();
    }

    public void UseItem() {
        if (item != null) {
            item.Use();
        }
    }
}
