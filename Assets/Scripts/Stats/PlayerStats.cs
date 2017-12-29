using UnityEngine;
using System.Collections;

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
    private GFXManager gfxManager;

    void Start() {
        gfxManager = GFXManager.instance;

        EquipmentManager.instance.onArmourChanged += OnArmourChanged;
        EquipmentManager.instance.onWeaponChanged += OnWeaponChanged;

        StartCoroutine(TakeRadiationDamage());
    }

    void Update() {
        HealthEffects();
    }

    void HealthEffects() {
        float healthPercentage = (float)currentHealth / (float)maxHealth;
        
        gfxManager.VignetteEffect(Mathf.Clamp01(-healthPercentage + 0.8f));
    }

    void OnArmourChanged(Armour item) {
        if (item != null) {
            int modifier = Mathf.CeilToInt(item.armourModifier * item.condition / 100);
            armour.SetModifier(modifier);
            radiationResistance.SetModifier(item.radiationResistance);
            movementSpeed.SetModifier(-item.movementReduction);
        } else {
            armour.SetModifier(0);
            radiationResistance.SetModifier(0);
            movementSpeed.SetModifier(0);
        }

        character.m_MaxSpeed = movementSpeed.GetValue();

        SubscribeChange();
    }

    void OnWeaponChanged(Weapon item) {
        damage.SetBaseValue(item != null ? item.damage : 0);
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

    public override void IncreaseRadiationLevel(int gamma) {
        base.IncreaseRadiationLevel(gamma);

        SubscribeChange();
    }

    IEnumerator TakeRadiationDamage() {
        while (true) {
            if (radiationLevel > 0) {
                currentHealth--;

                if (currentHealth <= 0) {
                    Die();
                }

                SubscribeChange();

                yield return new WaitForSeconds(radiationResistance.GetValue() / ((float)radiationLevel / 25));
            } else {
                yield return new WaitForSeconds(1);
            }
        }
    }

    public override void Die() {
        base.Die();
    }

    private void SubscribeChange() {
        if (onStatsChangedCallback != null) {
            onStatsChangedCallback.Invoke();
        }
    }
}
