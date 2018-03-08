using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelsManager : MonoBehaviour {

    #region Singleton

    public static LevelsManager instance;

    void Awake() {
        instance = this;
    }

    #endregion

    public Transform levelUpParticles;
    private List<Level> levels;
    public int currentLevel { get; set; }
    public int experience { get; set; }
    private CharacterStats stats;
    private GameObject player;

    public delegate void onExperienceChangedCallback();
    public onExperienceChangedCallback onExperienceChanged;

    void Start() {
        levels = LevelsSerialization.LoadLevelsData();
        stats = PlayerManager.stats;
        player = PlayerManager.player;
    }

    public void AddExperience(int amount) {
        experience += amount;

        if (CanLevelUp()) {
            LevelUp();
        }

        SubscribeChange();
    }

    private void LevelUp() {
        Level nextLevel = levels[++currentLevel];

        stats.maxHealth += nextLevel.healthBonus;
        stats.radiationResistance.IncreaseBaseValue(nextLevel.radiationResistenceBonus);

        stats.currentHealth = stats.maxHealth;

        LevelUpEffect();

        if (CanLevelUp()) {
            LevelUp();
        }
    }

    private void LevelUpEffect() {
        Transform particles = Instantiate(levelUpParticles, player.transform.position, player.transform.rotation);

        Destroy(particles.gameObject, 10f);
    } 

    private bool CanLevelUp() {
        return levels.Count > currentLevel + 1 && levels[currentLevel + 1].experienceRequired <= experience;
    }

    void SubscribeChange() {
        if (onExperienceChanged != null) {
            onExperienceChanged.Invoke();
        }
    }
}
