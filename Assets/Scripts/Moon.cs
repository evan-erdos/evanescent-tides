using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moon : MonoBehaviour {
    public static Moon singleton {get; private set;}
    [SerializeField] protected float weight;
    [SerializeField] protected float movespeed;
    Vector3 velocity;
    public float Weight => weight;

    void Awake() => singleton = this;

    void Update() {
        // make this should be adjust to the camera angle
        velocity = new Vector3(
            Input.GetAxis("Right Horizontal") * movespeed,0f,
            Input.GetAxis("Right Vertical") * -1f * movespeed);
        transform.Translate(velocity * Time.deltaTime);
        transform.position = new Vector3(
            Mathf.Clamp(transform.position.x,-40,40),
            transform.position.y,
            Mathf.Clamp(transform.position.z,-10,60));
    }

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
