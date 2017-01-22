using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moon : MonoBehaviour {
    new Rigidbody rigidbody;
    new AudioSource audio;
    Vector3 velocity, lastPosition;
    public static Moon singleton {get;private set;}
    public static Sailboat ship {get;set;}
    [SerializeField] protected float weight;
    [SerializeField] protected float movespeed;
    [SerializeField] protected AudioClip wind;
    public float Weight => weight;

    void Awake() {
        singleton = this;
        rigidbody = GetComponent<Rigidbody>();
        audio = GetComponent<AudioSource>();
        (audio.pitch,audio.clip) = (0.125f,wind);
    }

    void Update() => audio.volume = Mathf.InverseLerp(
        0, 10, rigidbody.velocity.magnitude);

    void FixedUpdate() {
        lastPosition = transform.position;
        velocity = new Vector3(
            Input.GetAxis("Right Horizontal") * movespeed, 0,
            - Input.GetAxis("Right Vertical") * movespeed);
        if (!Physics.SphereCast(
                origin: transform.position+rigidbody.velocity*Time.fixedDeltaTime*2,
                radius: 2,
                direction: Vector3.down,
                hitInfo: out var hit,
                maxDistance: 1000,
                layerMask: 1<<LayerMask.NameToLayer("Water"))) {
            var displacement = -rigidbody.velocity * Time.fixedDeltaTime;
            rigidbody.velocity = Vector3.zero;
            rigidbody.position = lastPosition+displacement;
        } else rigidbody.AddForce(velocity);
    }
}
