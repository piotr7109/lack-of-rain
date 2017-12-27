using System.Collections;
using UnityEngine;

public class RadiationArea : MonoBehaviour {

    public int radiationStrength = 1;
    public float radius = 5f;
    private PlayerStats stats;
    private Transform player;
    private GFXManager gfxManager;

    void Start() {
        player = PlayerManager.instance.player.transform;
        stats = player.GetComponent<PlayerStats>();
        gfxManager = GFXManager.instance;

        StartCoroutine(IncreaseRadiation());
    }

    IEnumerator IncreaseRadiation() {
        while (true) {
            yield return new WaitForSeconds(1f);

            float distance = Vector2.Distance(player.position, transform.position);
            float id = GetInstanceID();

            if (distance <= radius) {
                stats.IncreaseRadiationLevel(radiationStrength);
                gfxManager.GrainEffect(radiationStrength, id);
            } else {
                gfxManager.GrainEffectTurnOff(id);
            }
        }
    }

    void OnDrawGizmosSelected() {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
