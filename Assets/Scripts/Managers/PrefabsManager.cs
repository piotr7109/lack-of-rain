using UnityEngine;

public class PrefabsManager : MonoBehaviour {

    #region Singleton

    public static PrefabsManager instance;

    void Awake() {
        instance = this;
    }

    #endregion

    public GameObject interactionTooltip;
    public Sprite interactionIcon;
    public Sprite pickUpIcon;
    public Sprite talkIcon;
    
    public Transform defaultItem;

}
