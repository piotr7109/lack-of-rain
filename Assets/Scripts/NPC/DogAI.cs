using UnityEngine;
using UnityStandardAssets._2D;

public class DogAI : MonoBehaviour {

    private PlatformerCharacter2D character;
    public CharacterStats stats;
    private Transform player;
    public Animator anim;

    // Use this for initialization
    void Start () {
        player = PlayerManager.player.transform;

        character = GetComponentInParent<PlatformerCharacter2D>();
        stats = GetComponentInParent<CharacterStats>();

    }
	
	// Update is called once per frame
	void Update () {
        if (!anim.GetBool("Died")) {
           // ChaseAndAttack();
        }

    }
}
