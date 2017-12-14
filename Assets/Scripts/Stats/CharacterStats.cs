using UnityEngine;

public class CharacterStats : MonoBehaviour {

    public int maxHealth = 100;
    public int currentHealth { get; private set; }
    public int radiationLevel { get; private set; }

    public Stat damage;

    public Stat armour;
    public Stat radiationResistance;
    public Stat movementSpeed;

    public void Awake() {
        currentHealth = maxHealth;
        radiationLevel = 0;
    }

    void Update() {
        if(Input.GetKeyDown(KeyCode.T)) {
            TakeDamage(20);
        }
    }

    public void Heal(UsableItem item) {
        currentHealth += item.healthRestoration;
        Debug.Log(item.healthRestoration);
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        radiationLevel -= item.radiationReduction;
        radiationLevel = Mathf.Clamp(radiationLevel, 0, 100);
    }

    public void TakeDamage(int damage) {
        damage -= armour.GetValue();
        damage = Mathf.Clamp(damage, 0, int.MaxValue);
        currentHealth -= damage;

        if(currentHealth <= 0) {
            Die();
        }
    }

    public virtual void Die() {
        Debug.Log(transform.name + " died");
    }
}
