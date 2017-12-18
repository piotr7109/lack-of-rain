using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject {

    public Sprite icon = null;
    new public string name = "New Item";

    [HideInInspector]
    public bool equiped = false;

    public virtual void Use() { }

    public void RemoveFromInventory() {
        Inventory.instance.Remove(this);
    }

    public void DropItem() {
        Inventory.instance.DropItem(this);
    }

    public virtual string GetAmount() {
        return "";
    }
}
