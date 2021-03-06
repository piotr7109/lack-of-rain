﻿using UnityEngine;

public class InventoryUI : MonoBehaviour {

    public Transform itemsParent;
    public GameObject inventoryUI;

    Inventory inventory;

    InventorySlot[] slots;

    void Start() {
        inventory = PlayerManager.inventory;
        inventory.onItemChangedCallback += UpdateUI;

        slots = itemsParent.GetComponentsInChildren<InventorySlot>();
    }

    void Update() {
        if (Input.GetButtonDown("Inventory")) {
            if (inventoryUI.activeSelf) {
                CloseInventory();
            } else {
                OpenInventory();
            }
        }

        if (Input.GetButtonDown("Cancel")) {
            CloseInventory();
        }
    }

    void OpenInventory() {
        TimeManager.instance.StopTime();
        UpdateUI();
        inventoryUI.SetActive(true);
    }

    void CloseInventory() {
        TimeManager.instance.RestoreTime();
        inventoryUI.SetActive(false);
    }

    void UpdateUI() {
        for (int i = 0; i < slots.Length; i++) {
            if (i < inventory.items.Count) {
                slots[i].AddItem(inventory.items[i]);
            } else {
                slots[i].ClearSlot();
            }
        }
    }
}
