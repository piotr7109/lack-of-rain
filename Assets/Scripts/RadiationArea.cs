using System.Collections;
using UnityEngine;

public class RadiationArea : MonoBehaviour {

    public int radiationStrength = 1;
    public float radius = 5f;
    private GFXManager gfxManager;

    void Start() {
        gfxManager = GFXManager.instance;
    }

    void OnTriggerEnter2D(Collider2D collider) {
        if (collider.gameObject == PlayerManager.player) {
            StartCoroutine(IncreaseRadiation(collider.transform));
        }
    }

    void OnTriggerExit2D(Collider2D collider) {
        if (collider.gameObject == PlayerManager.player) {
            StopAllCoroutines();
            gfxManager.GrainEffectTurnOff(GetInstanceID());
        }
    }

    IEnumerator IncreaseRadiation(Transform character) {
        while (true) {
            yield return new WaitForSeconds(1f);

            character.GetComponent<CharacterStats>().IncreaseRadiationLevel(radiationStrength);
            gfxManager.GrainEffect(radiationStrength, GetInstanceID());
        }
    }
}
