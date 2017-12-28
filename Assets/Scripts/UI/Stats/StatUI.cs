using UnityEngine;
using UnityEngine.UI;

public class StatUI : MonoBehaviour {

    private Text textValue;
    private Text bonusValue;

    private Color positive = new Color(227, 227, 0);
    private Color negative = new Color(176, 0, 0);

    void Awake() {
        textValue = transform.Find("Value").GetComponent<Text>();

        Transform bonusTransform = transform.Find("Bonus");

        if (bonusTransform != null) {
            bonusValue = bonusTransform.GetComponent<Text>();
        }
    }

    public void UpdateStat(string value, float bonus = 0) {
        textValue.text = value;

        if (bonusValue != null) {
            bonusValue.text = bonus >= 0 ? ("+" + bonus) : ("" + bonus);
            bonusValue.color = bonus >= 0 ? positive : negative;
        }
    }
}
