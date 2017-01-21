using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moon : MonoBehaviour {
    public static Moon singleton {get; private set;}
    void Awake() => singleton = this;

    IEnumerator OnMouseOver() {
        while (Input.GetButton("Fire1")) {
            var v0 = Input.mousePosition;
            var v1 = Camera.main.ScreenToWorldPoint(v0);
            v0 = new Vector3(
                Input.mousePosition.x,
                Input.mousePosition.y,
                Vector3.Distance(v1,transform.position));
            yield return new WaitForEndOfFrame();
        }
    }
}
