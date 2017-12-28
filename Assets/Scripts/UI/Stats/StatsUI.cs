using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsUI : MonoBehaviour {

    public StatUI health;
    public StatUI damage;
    public StatUI armour;
    public StatUI gammaResistance;
    public StatUI movementSpeed;

    private PlayerStats stats;

    void Start() {
        stats = PlayerStats.instance;
        stats.onStatsChangedCallback += UpdateUI;
        UpdateUI();
    }

    void UpdateUI() {
        health.UpdateStat(stats.currentHealth + "/" + stats.maxHealth, 0);
        damage.UpdateStat(stats.damage.GetBase() + "", stats.damage.GetModifier());
        armour.UpdateStat(stats.armour.GetBase() + "", stats.armour.GetModifier());
        gammaResistance.UpdateStat(stats.radiationResistance.GetBase() + "", stats.radiationResistance.GetModifier());
        movementSpeed.UpdateStat(stats.movementSpeed.GetBase() + "", stats.movementSpeed.GetModifier());
    }

}
