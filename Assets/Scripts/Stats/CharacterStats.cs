using UnityEngine;
using System.Collections;
using UnityStandardAssets._2D;

public class CharacterStats : MonoBehaviour {

    public int maxHealth = 100;
    public int currentHealth;
    public int radiationLevel;

    public Stat damage;
    public Stat meleeDamage;
    public Stat armour;
    public Stat radiationResistance;
    public Stat movementSpeed;

    public int experienceGained = 100;

    protected PlatformerCharacter2D character;
    public Animator anim;

    public delegate void OnStatsChanged();
    public OnStatsChanged onStatsChangedCallback;

    public CharacterAnimator characterAnimator;

    private Equipment equipment;
    private Inventory inventory;

    public void Awake() {
        currentHealth = maxHealth;
        radiationLevel = 0;
        character = GetComponent<PlatformerCharacter2D>();
        character.m_MaxSpeed = movementSpeed.GetValue();
    }

    void Start() {
        equipment = GetComponent<Equipment>();
        inventory = GetComponent<Inventory>();
        equipment.onArmourChanged += OnArmourChanged;
        equipment.onWeaponChanged += OnWeaponChanged;

        StartCoroutine(TakeRadiationDamage());
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

    public void Heal(UsableItem item) {
        currentHealth += item.healthRestoration;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        radiationLevel -= item.radiationReduction;
        radiationLevel = Mathf.Clamp(radiationLevel, 0, int.MaxValue);
        SubscribeChange();
    }

    public void IncreaseRadiationLevel(int gamma) {
        radiationLevel += gamma;

        SubscribeChange();
    }

    public void TakeDamage(int damage) {
        damage -= armour.GetValue();
        damage = Mathf.Clamp(damage, 0, int.MaxValue);
        currentHealth -= damage;
        equipment.DamageArmour();

        if (currentHealth <= 0) {
            Die();
        }

        SubscribeChange();
    }

    public void Die() {
        if (!anim.GetBool("Died")) {
            characterAnimator.Die();
            inventory.DropAllItems();
            LevelsManager.instance.AddExperience(experienceGained);
            anim.SetBool("Died", true);
            Destroy(gameObject, 3f);
        }

        SubscribeChange();
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

    private void SubscribeChange() {
        if (onStatsChangedCallback != null) {
            onStatsChangedCallback.Invoke();
        }
    }
}
