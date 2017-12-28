using UnityEngine;

public class ItemInfoUI : MonoBehaviour {

    #region Singleton

    public static ItemInfoUI instance;
    void Awake() {
        instance = this;
    }

    #endregion

    public WeaponInfo weaponInfo;
    public ArmourInfo armourInfo;
    public UsableItemInfo usableItemInfo;
    public DefaultItemInfo defaultItemInfo;

    void Start() {
        HideInfo();
    }

    public void ShowInfo(Item item, Vector2 position) {
        transform.position = position;

        if (item is Weapon) {
            weaponInfo.Show(item as Weapon);
        } else if (item is Armour) {
            armourInfo.Show(item as Armour);
        } else if (item is UsableItem) {
            usableItemInfo.Show(item as UsableItem);
        } else {
            defaultItemInfo.Show(item);
        }
    }

    public void HideInfo() {
        weaponInfo.Hide();
        armourInfo.Hide();
        usableItemInfo.Hide();
        defaultItemInfo.Hide();
    }
}
