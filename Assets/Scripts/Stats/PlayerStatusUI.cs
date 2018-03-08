using UnityEngine;
using UnityEngine.UI;

public class PlayerStatusUI : MonoBehaviour {

    public Text health;
    public Text gamma;

    private GFXManager gfxManager;

    CharacterStats stats;

    void Start() {
        stats = PlayerManager.stats;
        gfxManager = GFXManager.instance;

        stats.onStatsChangedCallback += UpdateUI;
        UpdateUI();
    }

    void UpdateUI() {
        float healthPercentage = (float)stats.currentHealth / (float)stats.maxHealth;

        gfxManager.ChangeSaturation(-((float)stats.radiationLevel / 100) + 1);
        gfxManager.VignetteEffect(Mathf.Clamp01(-healthPercentage + 0.8f));

        health.text = stats.currentHealth + "";
        gamma.text = stats.radiationLevel + "";
    }

}
