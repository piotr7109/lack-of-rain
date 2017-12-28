using UnityEngine;
using UnityEngine.UI;

public class SliderStat : MonoBehaviour {

    private Text textValue;
    private Slider sliderValue;

    void Start() {
        textValue = transform.Find("Value").GetComponent<Text>();
        sliderValue = transform.Find("Slider").GetComponent<Slider>();
    }

    public void SetValue(float value, int maxValue = 100) {
        textValue.text = value + "";
        sliderValue.value = value / maxValue;
    }
}
