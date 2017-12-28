public class WeaponInfo : DefaultItemInfo {
    
    public SliderStat damage;
    public SliderStat fireRate;
    public SliderStat magazineSize;
    public SliderStat bulletSpeed;
    public SliderStat accuracy;
    public SliderStat reloadTime;
    public TextStat type;

    public override void Show(Item item) {
        base.Show(item);

        Weapon weapon = item as Weapon;
        damage.SetValue(weapon.damage);
        fireRate.SetValue(weapon.effectSpawnRate, 40);
        magazineSize.SetValue(weapon.magazineSize);
        bulletSpeed.SetValue(weapon.bulletSpeed);
        accuracy.SetValue(AimSpreadToAccuracy(weapon.aimSpread));
        reloadTime.SetValue(weapon.reloadTime);
        type.SetValue(weapon.type.ToString());
    }

    private float AimSpreadToAccuracy(float value) {
        if (value > 0) {
            return 1 / value;
        }

        return 100;
    }

}
