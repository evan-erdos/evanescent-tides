using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sailboat : MonoBehaviour {
    new Rigidbody rigidbody;
    float angularAxis, throttle;
    [SerializeField] float health = 1000;
    [SerializeField] float thrust = 1000;

    void Start() => rigidbody = GetComponent<Rigidbody>();

    void Update() => (angularAxis,thrust) =
        (Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

    void FixedUpdate() {
        rigidbody.AddForce(Vector3.forward*throttle*thrust);
        rigidbody.AddTorque(Vector3.right*throttle*thrust);
    }
}
