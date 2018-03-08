using UnityEngine;

[CreateAssetMenu(fileName = "Vodka", menuName = "Inventory/Vodka")]
public class Vodka : UsableItem {

    public override void Use(CharacterStats stats) {
        base.Use(stats);
        //TODO
        //GFXManager.instance.DrunkEffect();
    }    
}