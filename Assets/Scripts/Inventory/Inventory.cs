using UnityEngine;
using System.Collections.Generic;

public class Inventory : MonoBehaviour {

    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedCallback;

    public int space = 20;

    public List<Item> items = new List<Item>();

    void Awake() {
        for (int i = 0; i < items.Count; i++) {
            items[i] = Instantiate(items[i]);
        }
    }

    public bool Add(Item item) {
        if (items.Count >= space) {
            return false;
        }

        items.Add(item);
        SubscribeChange();

        return true;
    }

    public void Remove(Item item) {
        items.Remove(item);
        SubscribeChange();
    }

    public void DropAllItems() {
        items.ForEach(item => DropItem(item, false));
        items.Clear();
    }

    public void DropItem(Item item, bool remove = true) {
        Transform defaultItem = PrefabsManager.instance.defaultItem;
        Vector2 spawnPosition = new Vector2(transform.position.x + Random.Range(-1f, 1f), transform.position.y);
        Transform itemDrop = Instantiate(defaultItem, spawnPosition, defaultItem.rotation) as Transform;
        
        itemDrop.name = item.name;
        itemDrop.gameObject.GetComponent<ItemPickup>().item = item;
        itemDrop.gameObject.GetComponent<SpriteRenderer>().sprite = item.icon;

        if (remove) {
            Remove(item);
        }
    }

    public int FindBullets(WeaponType weaponType, int amount) {
        Ammo ammo = items.Find(item => item is Ammo && ((Ammo)item).type == weaponType) as Ammo;

        if (ammo != null) {
            if (amount >= ammo.amount) {
                Remove(ammo);

                return ammo.amount;
            } else {
                ammo.amount -= amount;

                return amount;
            }
        }

        return 0;
    }

    public Item FindItem(string itemName) {
        return items.Find(item => item.name == itemName);
    }

    public void SubscribeChange() {
        if (onItemChangedCallback != null) {
            onItemChangedCallback.Invoke();
        }
    }

}
