using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour {
    new Rigidbody rigidbody;
    float x, y; // , sensitivity = 1, range = 30;

    void Awake() => rigidbody = GetComponent<Rigidbody>();
    void Start() => DontDestroyOnLoad(gameObject);

    // void Update() => (x,y) =
    //     (Input.GetAxis("Swivel Horizontal"), Input.GetAxis("Swivel Vertical"));

    void FixedUpdate() {
        // rigidbody.AddTorque(new Vector3(y: x*sensitivity, x: y*sensitivity, z: 0));
        // transform.localEulerAngles = new Vector3(
        //     y: x*sensitivity, x: Mathf.Clamp(y*sensitivity,-range,range), z: 0);
    }

}
