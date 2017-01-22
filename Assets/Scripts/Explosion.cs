using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Adventure.EvanescentTides;

public class Explosion : Thing {
	public void Detonate(Rigidbody parent) {
        var rigidbody = GetComponent<Rigidbody>();
        if (rigidbody is null) rigidbody = gameObject.AddComponent<Rigidbody>();
        rigidbody.transform.parent = null;
        rigidbody.velocity = parent.velocity;
        rigidbody.angularVelocity = parent.angularVelocity;
        rigidbody.AddForce(Random.insideUnitSphere);
        var buoy = gameObject.AddComponent<Buoyancy>();
		buoy.density = 800;
	}
}
