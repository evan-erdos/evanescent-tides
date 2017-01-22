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

	void OnTriggerEnter(Collider col){
		print("!");
		float alot = 10;
		if(true){
			Rigidbody rb = gameObject.AddComponent<Rigidbody>();
			rb.velocity = GetComponentInParent<Rigidbody>().velocity;
			transform.parent = null;
			rb.AddForce(Random.insideUnitSphere * alot,ForceMode.Impulse);


		}
	}
}
