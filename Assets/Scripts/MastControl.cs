/* Ben Scott * @evan-erdos * bescott@andrew.cmu.edu * 2016-12-28 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MastControl : MonoBehaviour {
    new Rigidbody rigidbody;
    [SerializeField] float range = 30; // deg
    bool jump;
    float mast;

    void Awake() => rigidbody = GetComponentInParent<Rigidbody>();
    // void Update() => jump = Input.GetButton("Jump");
    void Update() => mast = -Input.GetAxis("Horizontal");

    void FixedUpdate() => transform.localRotation = Quaternion.Slerp(
        transform.localRotation, Quaternion.Euler(
            x: 0, z: 0, y: Mathf.Clamp(range*mast,range,-range)),
        Time.fixedDeltaTime);
}

