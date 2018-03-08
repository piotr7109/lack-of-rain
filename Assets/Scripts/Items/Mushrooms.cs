using UnityEngine;

[CreateAssetMenu(fileName = "Mushrooms", menuName = "Inventory/Mushrooms")]
public class Mushrooms : UsableItem {

    public override void Use(CharacterStats stats) {
        base.Use(stats);
        //TODO
        GFXManager.instance.DrugEffect();
        GFXManager.instance.DrunkEffect();
    }
}