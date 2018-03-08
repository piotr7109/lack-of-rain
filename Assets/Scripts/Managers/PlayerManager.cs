using UnityEngine;
using UnityStandardAssets._2D;

public class PlayerManager : MonoBehaviour {

    public GameObject playerReference;

    [HideInInspector]
    public static GameObject player;

    public static CharacterStats stats;
    public static Equipment equipment;
    public static Inventory inventory;
    public static WeaponShooting shooting;
    public static CharacterRotation rotation;

    void Awake() {
        player = playerReference;
        stats = player.GetComponent<CharacterStats>();
        equipment = player.GetComponent<Equipment>();
        inventory = player.GetComponent<Inventory>();
        shooting = player.GetComponentInChildren<WeaponShooting>();
        rotation = player.GetComponentInChildren<CharacterRotation>();

        DisableAI();
        SetTags();
        SetCamera();
    }

    void DisableAI() {
        Transform AI = player.transform.Find("AI");
        AI.GetComponent<EnemyAI>().enabled = false;
        AI.GetComponent<Npc>().enabled = false;
    }

    void SetTags() {
        MeleePoint meleePoint = player.GetComponentInChildren<MeleePoint>();

        player.tag = "Player";
        meleePoint.targetTag = "Enemy";
    }

    void SetCamera() {
        Camera2DFollow cam = FindObjectOfType<Camera2DFollow>();

        cam.target = player.transform;
    }
}
