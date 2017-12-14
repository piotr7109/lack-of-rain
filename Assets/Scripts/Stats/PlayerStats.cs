using UnityEngine;

public class PlayerStats : CharacterStats {
    #region Singleton
    public static PlayerStats instance;

    new void Awake() {
        base.Awake();
        instance = this;
    }

    #endregion

    public delegate void OnStatsChanged();
    public OnStatsChanged onStatsChangedCallback;

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

        SubscribeChange();
    }

    void OnWeaponChanged(Weapon item) {
        damage.SetModifier(item != null ? item.damage : 0);
        SubscribeChange();
    }

    public override void Heal(UsableItem item) {
        base.Heal(item);
        SubscribeChange();
    }

    public override void TakeDamage(int damage) {
        base.TakeDamage(damage);
        SubscribeChange();
    }

    private void SubscribeChange() {
        if (onStatsChangedCallback != null) {
            onStatsChangedCallback.Invoke();
        }
    }
}
