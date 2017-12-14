using UnityEngine;

[CreateAssetMenu(fileName = "New Bullets", menuName = "Inventory/Equipment/Bullets")]
public class Bullets : Item {

    public WeaponType type;
    public int amount;

    public override void Use() { }
}
