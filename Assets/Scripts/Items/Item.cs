using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject {

    public Sprite icon = null;
    new public string name = "New Item";

    //[HideInInspector]
    public bool equiped = false;

    public virtual void Use(Equipment equipment) { }
    public virtual void Use(CharacterStats stats) { }
    public virtual void Use(Inventory inventory) { }

    public virtual string GetAmount() {
        return "";
    }
}
