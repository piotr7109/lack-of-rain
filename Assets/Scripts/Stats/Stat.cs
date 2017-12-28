using UnityEngine;

[System.Serializable]
public class Stat {

    [SerializeField]
    private int baseValue;

    private int modifier = 0;

    public int GetValue() {
        return baseValue + modifier;
    }

    public int GetBase() {
        return baseValue;
    }

    public int GetModifier() {
        return modifier;
    }

    public void SetBaseValue(int value) {
        baseValue = value;
    }

    public void IncreaseBaseValue(int value) {
        baseValue += value;
    }

    public void SetModifier(int modifier) {
        this.modifier = modifier;
    }
}
