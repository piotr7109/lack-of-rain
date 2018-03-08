using UnityEngine;
using UnityEngine.UI;

public class EquipmentUI : MonoBehaviour {

    public Image weaponIcon;
    public Image armourIcon;

    private Equipment equipment;

    void Start() {
        equipment = PlayerManager.equipment;
        PlayerManager.inventory.onItemChangedCallback += UpdateUI;
        UpdateUI();
    }

    void UpdateUI() {
        SetSlotIcon(weaponIcon, equipment.weapon);
        SetSlotIcon(armourIcon, equipment.armour);
    }

    void SetSlotIcon(Image slotIcon, Item item) {
        if (item != null) {
            slotIcon.sprite = item.icon;
            slotIcon.enabled = true;
        } else {
            slotIcon.enabled = false;
            slotIcon.sprite = null;
        }
    }
}
