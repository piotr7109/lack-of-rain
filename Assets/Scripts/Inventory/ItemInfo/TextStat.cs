using UnityEngine;
using UnityEngine.UI;

public class TextStat : MonoBehaviour {

    private Text textValue; 

    void Awake() {
        textValue = transform.Find("Value").GetComponent<Text>();

    }

    public void SetValue(float value) {
        textValue.text = value + "";

    }

    public void SetValue(string value) {
        textValue.text = value;
    }
}
