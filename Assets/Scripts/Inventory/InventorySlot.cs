using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {

    public Image icon;
    public Button removeButton;
    public Image equipMark;
    public Text amountText;
    private ItemInfoUI itemInfoUI;

    Item item;

    void Start() {
        itemInfoUI = ItemInfoUI.instance;
    }

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

    public void OnPointerEnter(PointerEventData eventData) {
        if (item != null) {
            itemInfoUI.ShowInfo(item, transform.position);
        }
    }

    public void OnPointerExit(PointerEventData eventData) {
        itemInfoUI.HideInfo();
    }
}
