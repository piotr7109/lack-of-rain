using UnityEngine;

public class ArmourInfo : DefaultItemInfo {
    
    public SliderStat armourModdifier;
    public SliderStat radiationResistance;
    public SliderStat condition;
    public SliderStat movementReduction;

	public override void Show(Item item) {
        base.Show(item);

        Armour armour = item as Armour;
        armourModdifier.SetValue(armour.armourModifier, 20);
        radiationResistance.SetValue(armour.radiationResistance, 20);
        condition.SetValue(armour.condition);
        movementReduction.SetValue(armour.movementReduction, 20);
    }
}
