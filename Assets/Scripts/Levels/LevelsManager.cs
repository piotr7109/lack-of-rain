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
    public int currentLevel { get; private set; }
    public int experience { get; private set; }
    private PlayerStats stats;
    private GameObject player;

    public delegate void onExperienceChangedCallback();
    public onExperienceChangedCallback onExperienceChanged;

    void Start() {
        currentLevel = 0;
        experience = 0;
        levels = LevelsSerialization.LoadLevelsData();
        stats = PlayerStats.instance;
        player = PlayerManager.instance.player;
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
