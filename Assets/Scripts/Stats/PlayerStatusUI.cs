using UnityEngine;
using UnityEngine.UI;

public class PlayerStatusUI : MonoBehaviour {
    public Text health;
    public Text gamma;

    PlayerStats stats;

    void Start() {
        stats = PlayerStats.instance;
        stats.onStatsChangedCallback += UpdateUI;
        UpdateUI();
    }

    void UpdateUI() {
        health.text = stats.currentHealth + "";
        gamma.text = stats.radiationLevel + "";
    }

}
