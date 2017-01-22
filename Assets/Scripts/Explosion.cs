using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionEnter(Collision col){
		if(col.impulse.magnitude > 100){
			Rigidbody rb = gameObject.AddComponent<Rigidbody>();
			rb.velocity = GetComponentInParent(typef(Rigidbody)).velocity;
			transform.parent = null;
			rb.AddForce(Random.insideUnitSphere * col.impulse.magnitude,ForceMode.Impulse);


		}
	}
}
