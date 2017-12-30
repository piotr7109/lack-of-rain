using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyPosition : MonoBehaviour {

    public Transform leg;
    
	// Update is called once per frame
	void Update () {
        transform.position = leg.position;
	}
}
