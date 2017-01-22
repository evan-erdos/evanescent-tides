using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Adventure.EvanescentTides;

public class Sailboat : Thing {
    new Rigidbody rigidbody;
    float angular, throttle;
    [SerializeField] protected float health = 1000;
    [SerializeField] protected float manuevering = 100;
    [SerializeField] protected float thrust = 1000;
    [SerializeField] protected WaveEvent onKill = new WaveEvent();
    public event WaveAction KillEvent;
    public void Kill() => KillEvent(this, new WaveArgs());
    void Awake() {
        rigidbody = GetComponent<Rigidbody>();
        rigidbody.centerOfMass = Vector3.down*3;
        onKill.AddListener((o,e) => OnKill());
        KillEvent += (o,e) => onKill?.Invoke(o,e);
        DontDestroyOnLoad(gameObject);
    }

    void Update() => (angular, throttle) =
        (Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

    void FixedUpdate() {
        rigidbody.AddForce(-transform.forward*throttle*thrust);
        rigidbody.AddTorque(transform.up*angular*manuevering);
        transform.rotation = Quaternion.Slerp(
            transform.rotation,
            Quaternion.LookRotation(transform.forward, transform.up),
            Time.fixedDeltaTime);
    }

    void OnCollisionEnter(Collision c) => Damage(c.impulse.magnitude);

    void Damage(float damage) {
        if (damage>1) health -= damage;
        if (0>health) Kill();
    }

    void OnKill() {
        foreach (var x in GetComponentsInChildren<Explosion>())
            if (x.GetComponent<Rigidbody>()) continue;
            else x.gameObject.AddComponent<Rigidbody>();
        GetComponentsInChildren<Explosion>().ForEach(o =>
            o.gameObject.layer = LayerMask.NameToLayer("Default"));
        GetComponentsInChildren<Explosion>().ForEach(o => o.Detonate(rigidbody));
        enabled = false;
    }
}
