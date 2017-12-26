using UnityEngine;

public class PhysicsTweaks : MonoBehaviour {
	void Start () {
        Physics2D.IgnoreLayerCollision(9, 10, true);
        Physics2D.IgnoreLayerCollision(10, 10, true);
    }
}
