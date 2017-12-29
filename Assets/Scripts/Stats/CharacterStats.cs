using UnityEngine;
using UnityStandardAssets._2D;

public class CharacterStats : MonoBehaviour {

    public int maxHealth = 100;
    public int currentHealth { get; set; }
    public int radiationLevel { get; set; }

    public Stat damage;
    public Stat armour;
    public Stat radiationResistance;
    public Stat movementSpeed;

    protected PlatformerCharacter2D character;

    public void Awake() {
        currentHealth = maxHealth;
        radiationLevel = 0;
        character = GetComponent<PlatformerCharacter2D>();
        character.m_MaxSpeed = movementSpeed.GetValue();
    }

    public virtual void Heal(UsableItem item) {
        currentHealth += item.healthRestoration;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        radiationLevel -= item.radiationReduction;
        radiationLevel = Mathf.Clamp(radiationLevel, 0, int.MaxValue);
    }

    public virtual void IncreaseRadiationLevel(int gamma) {
        radiationLevel += gamma;
    }

    public virtual void TakeDamage(int damage) {
        damage -= armour.GetValue();
        damage = Mathf.Clamp(damage, 0, int.MaxValue);
        currentHealth -= damage;

        if (currentHealth <= 0) {
            Die();
        }
    }

    public virtual void Die() {
        Debug.Log(transform.name + " died");
    }
}
