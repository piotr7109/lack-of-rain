using UnityEngine;
using UnityStandardAssets._2D;

public class EnemyNpc : MonoBehaviour {

    private PlatformerCharacter2D character;

    protected Transform player;

    public float chaseRadius = 25;
    public float attackRadius = 15;
    public float meleeRadius = 1.2f;
    public Transform firePoint;
    public CharacterStats stats;
    public CharacterRotation characterRotation;
    public EnemyShooting weaponShooting;

    public Animator anim;

    void Awake() {
        enabled = false;
        weaponShooting.wantToShoot = false;
    }

    void Start() {
        player = PlayerManager.instance.player.transform;

        character = GetComponentInParent<PlatformerCharacter2D>();
        stats = GetComponentInParent<CharacterStats>();
    }

    void Update() {
        if (enabled && !anim.GetBool("Died")) {
            ChaseAndAttack();
        }
    }

    void ChaseAndAttack() {
        float distance = Vector2.Distance(player.position, transform.position);

        if (weaponShooting.weapon != null) {
            if (distance <= attackRadius) {
                Attack();
            } else if (distance <= chaseRadius) {
                Chase();
            }
        } else {
            if (distance <= meleeRadius) {
                Attack();
            } else if (distance <= chaseRadius) {
                Chase();
            }
        }

        if (distance > chaseRadius) {
            weaponShooting.wantToShoot = false;
        }
    }

    private Vector2 prevPos;

    void Chase() {
        int direction = player.position.x > transform.position.x ? 1 : -1;

        weaponShooting.wantToShoot = false;
        character.Move(direction, false, false);

        if (prevPos.x == transform.position.x) {
            //character.Move(0, false, true);
        }

        prevPos = transform.position;
    }

    void Attack() {
        character.Move(0, false, false);
        weaponShooting.wantToShoot = true;
    }

    void OnDrawGizmosSelected() {
        Gizmos.color = Color.gray;
        Gizmos.DrawWireSphere(transform.position, chaseRadius);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRadius);
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, meleeRadius);
    }
}
