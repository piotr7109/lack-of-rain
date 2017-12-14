public class PlayerStats : CharacterStats {
    #region Singleton
    public static PlayerStats instance;

    void Awake() {
        base.Awake();
        instance = this;
    }

    #endregion

    void Start() {
        EquipmentManager.instance.onArmourChanged += OnArmourChanged;
        EquipmentManager.instance.onWeaponChanged += OnWeaponChanged;
    }

    void OnArmourChanged(Armour item) {
        if (item != null) {
            armour.SetModifier(item.armourModifier);
            radiationResistance.SetModifier(item.radiationResistance);
            movementSpeed.SetModifier(-item.movementReduction);
            
        } else {
            armour.SetModifier(0);
            radiationResistance.SetModifier(0);
            movementSpeed.SetModifier(0);
        }
    }

    void OnWeaponChanged(Weapon item) {
        damage.SetModifier(item != null ? item.damage : 0);
    }
}
