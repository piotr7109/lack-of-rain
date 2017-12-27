using UnityEngine;
using UnityStandardAssets._2D;

public class EnemyNpc : MonoBehaviour {

    private PlatformerCharacter2D character;

    protected Transform player;

    public float chaseRadius = 25;
    public float attackRadius = 15;
    public float meleeAttackSpeed = 5f;
    public float meleeRadius = 1.2f;
    public Weapon weapon;
    public Transform weaponTransform;
    public CharacterStats stats;
    private ShootManager shootManager;

    void Awake() {
        enabled = false;
    }

    void Start() {
        player = PlayerManager.instance.player.transform;

        character = GetComponent<PlatformerCharacter2D>();
        stats = transform.GetComponent<CharacterStats>();
        shootManager = gameObject.AddComponent<ShootManager>();
        shootManager.setParameters(character, weaponTransform.Find("FirePoint"));
    }

    void FixedUpdate() {
        if (enabled) {
            ChaseAndAttack();
        }
    }

    void ChaseAndAttack() {
        float distance = Vector2.Distance(player.position, transform.position);

        if (weapon != null) {
            if (distance <= attackRadius) {
                Attack();
            } else if (distance <= chaseRadius) {
                Chase();
            }
        } else {
            if (distance <= meleeRadius) {
                AttackMelee();
            } else if (distance <= chaseRadius) {
                Chase();
            }
        }

        if (distance <= chaseRadius) {
            FacePlayer();
        }
    }

    void FacePlayer() {
        Vector3 difference = player.position - transform.position;
        difference.Normalize();

        int direction = difference.x >= 0 ? 1 : -1;

        character.m_FacingRight = difference.x >= 0;

        character.transform.localScale = new Vector2(Mathf.Abs(character.transform.localScale.x) * direction, character.transform.localScale.y);
        difference *= direction;

        float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;

        weaponTransform.rotation = Quaternion.Euler(0, 0, rotZ);
    }

    private Vector2 prevPos;

    void Chase() {
        int direction = player.position.x > transform.position.x ? 1 : -1;

        character.Move(stats.movementSpeed.GetValue() * direction, false, false);

        if (prevPos.x == transform.position.x) {
            character.Move(0, false, true);
        }

        prevPos = transform.position;
    }

    private float timeToFire = 0;
    private float timeToMeleeAttack = 0;

    void Attack() {
        if (weapon != null) {
            if (weapon.fireRate == 0) {
                Shoot();
            } else if (Time.time > timeToFire) {
                timeToFire = Time.time + 1 / weapon.fireRate;
                Shoot();
            }
        }
    }

    void Shoot() {
        shootManager.Shoot(weapon);
    }

    void AttackMelee() {
        if (Time.time > timeToMeleeAttack) {
            timeToMeleeAttack = Time.time + 1 / meleeAttackSpeed;
        }
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
