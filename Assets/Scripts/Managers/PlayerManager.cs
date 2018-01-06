using UnityEngine;

public class PlayerManager : MonoBehaviour {

    #region Singleton

    public static PlayerManager instance;
    
    void Awake() {
        instance = this;
    }

    #endregion

    public GameObject player;

    void Start() {
        player = GameObject.Find("Player");
    }
}
