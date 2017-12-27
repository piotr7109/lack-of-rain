using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TextHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {

    public Color hoverColor;
    private Color originalColor;

    private Text text;

    void Start() {
        text = GetComponent<Text>();
        originalColor = text.color;
    }

    public void OnPointerEnter(PointerEventData eventData) {
        text.color = hoverColor;
    }

    public void OnPointerExit(PointerEventData eventData) {
        text.color = originalColor;
    }
   
}
