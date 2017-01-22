/* Ben Scott * @evan-erdos * bescott@andrew.cmu.edu * 2016-12-28 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MastControl : MonoBehaviour {
    [SerializeField] float range = 30; // deg
    bool jump;

    void Update() => jump = Input.GetButton("Jump");

    void FixedUpdate() => transform.localRotation = Quaternion.Slerp(
        transform.localRotation,
        Quaternion.Euler(
            x: 0, z: 0, y: Mathf.Clamp(range*(jump?-1:1),range,-range)),
        Time.fixedDeltaTime*10);
}

