using UnityEngine;

[CreateAssetMenu(fileName = "Mushrooms", menuName = "Inventory/Mushrooms")]
public class Mushrooms : UsableItem {

    public override void Use() {
        base.Use();
        GFXManager.instance.DrugEffect();
        GFXManager.instance.DrunkEffect();
    }
}