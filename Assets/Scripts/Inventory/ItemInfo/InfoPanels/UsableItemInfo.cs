using UnityEngine;

public class UsableItemInfo : DefaultItemInfo {

    public SliderStat heal;
    public SliderStat radiationReduction;

    public override void Show(Item item) {
        base.Show(item);

        UsableItem usableItem = item as UsableItem;
        heal.SetValue(usableItem.healthRestoration, 150);
        radiationReduction.SetValue(usableItem.radiationReduction, 150);
    }
}
