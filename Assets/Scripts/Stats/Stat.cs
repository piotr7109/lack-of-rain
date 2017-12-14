using UnityEngine;

[System.Serializable]
public class Stat {

    [SerializeField]
    private int baseValue;

    private int modifier = 0;

    public int GetValue() {
        return baseValue + modifier;
    }

    public void SetModifier(int modifier) {
        this.modifier = modifier;
    }
}
