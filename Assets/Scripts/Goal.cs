using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Goal : MonoBehaviour {
    [SerializeField] string scene = "Fly By Wire";

    void OnTriggerEnter(Collider o) {
        var ship = o.GetComponentInParent<Sailboat>();
        if (!ship) return;
        ship.transform.position = Vector3.zero;
        LoadLevel(scene);
        if (o.GetComponentInParent<Sailboat>()) LoadLevel(scene);
    }

    void LoadLevel(string scene) => StartCoroutine(Killing());

    bool isKilling;
    IEnumerator Killing() {
        if (isKilling) yield break;
        isKilling = true;
        yield return new WaitForSeconds(0.1f);
        WaveManager.LoadScene(scene);
        isKilling = false;
    }

}
