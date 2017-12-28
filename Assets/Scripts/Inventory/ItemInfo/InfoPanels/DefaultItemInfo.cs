using UnityEngine;

public class DefaultItemInfo : MonoBehaviour {

    public TextStat itemName;

    public virtual void Show(Item item) {
        gameObject.SetActive(true);
        itemName.SetValue(item.name);

    }

    public virtual void Hide() {
        gameObject.SetActive(false);
    }
}
