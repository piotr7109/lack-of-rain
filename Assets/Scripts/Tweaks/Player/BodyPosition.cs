using UnityEngine;

public class BodyPosition : MonoBehaviour {

    public Transform leg;
    
	void Update () {
        transform.position = leg.position;
	}
}
