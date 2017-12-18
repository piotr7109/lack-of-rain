using UnityEngine;
using System.Collections.Generic;

public class Inventory : MonoBehaviour {

    #region Singleton

    public static Inventory instance;

    void Awake() {
        if (instance != null) {
            Debug.LogWarning("More than one instance of Inventory found!");
        }

        instance = this;
    }

    #endregion

    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedCallback;

    public int space = 20;
    public Transform player;
    public Transform defaultItem;

    public List<Item> items = new List<Item>();

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

    public void DropItem(Item item) {
        Vector3 spawnPosition = new Vector3(player.position.x, player.position.y, player.position.y);
        Transform itemDrop = Object.Instantiate(defaultItem, spawnPosition, defaultItem.rotation) as Transform;

        itemDrop.name = item.name;
        itemDrop.gameObject.GetComponent<ItemPickup>().item = item;
        itemDrop.gameObject.GetComponent<SpriteRenderer>().sprite = item.icon;
        Remove(item);
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

    public void SubscribeChange() {
        if (onItemChangedCallback != null) {
            onItemChangedCallback.Invoke();
        }
    }

}
