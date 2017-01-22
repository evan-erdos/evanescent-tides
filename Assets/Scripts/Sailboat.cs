using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Adventure.EvanescentTides;
using UnityEngine.SceneManagement;

public class Sailboat : Thing {
    new Rigidbody rigidbody;
    float angular, throttle;
	bool jump;
    [SerializeField] protected float health = 1000;
    [SerializeField] protected float manuevering = 100;
    [SerializeField] protected float thrust = 1000;
    [SerializeField] protected WaveEvent onKill = new WaveEvent();
    public event WaveAction KillEvent;
    public void Kill() => KillEvent(this, new WaveArgs());
    void Awake() {
        Moon.ship = this;
        rigidbody = GetComponent<Rigidbody>();
        rigidbody.centerOfMass = Vector3.down*3;
        onKill.AddListener((o,e) => OnKill());
        onKill.AddListener((o,e) => OnKill2());
        KillEvent += (o,e) => onKill?.Invoke(o,e);
        // DontDestroyOnLoad(gameObject);
    }

    void Update() => (angular, throttle, jump) =
        (Input.GetAxis("Horizontal"), 
			Input.GetAxis("Vertical"), 
			Input.GetButtonDown("Jump"));

    void FixedUpdate() {
        if (transform.position.y<-20) Kill();
		if (jump) rigidbody.AddForce(Vector3.up*1000);
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

    void OnKill2() => StartCoroutine(Killing());

    bool isKilling;
    IEnumerator Killing() {
        if (isKilling) yield break;
        isKilling = true;
        yield return new WaitForSeconds(5);
        WaveManager.LoadScene(SceneManager.GetActiveScene().name);
        isKilling = false;
    }
}
