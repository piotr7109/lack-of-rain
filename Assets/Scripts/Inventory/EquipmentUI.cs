using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentUI : MonoBehaviour {

    public Image weaponIcon;
    public Image armourIcon;

    private EquipmentManager equipmentManager;

    void Start() {
        equipmentManager = EquipmentManager.instance;
        Inventory.instance.onItemChangedCallback += UpdateUI;
        UpdateUI();
    }

    void UpdateUI() {
        SetSlotIcon(weaponIcon, equipmentManager.weapon);
        SetSlotIcon(armourIcon, equipmentManager.armour);
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
