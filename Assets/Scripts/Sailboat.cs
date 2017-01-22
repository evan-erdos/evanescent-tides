using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sailboat : MonoBehaviour {
    new Rigidbody rigidbody;
    float angularAxis, throttle;
    [SerializeField] protected float health = 1000;
    [SerializeField] protected float manuevering = 100;
    [SerializeField] protected float thrust = 1000;

    void Awake() => rigidbody = GetComponent<Rigidbody>();
    void Start() => rigidbody.centerOfMass = Vector3.down*3;

    void Update() => (angularAxis, throttle) =
        (Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

    void FixedUpdate() {
        rigidbody.AddForce(Vector3.forward*throttle*thrust);
        rigidbody.AddTorque(Vector3.right*angularAxis*manuevering);
    }
}
