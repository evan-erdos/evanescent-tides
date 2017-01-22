using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Goal : MonoBehaviour {
    [SerializeField] string scene = "Fly By Wire";

    void OnTriggerEnter(Collider o) {
        if (o.GetComponentInParent<Sailboat>()) LoadLevel(scene); }

    void LoadLevel(string scene) => SceneManager.LoadSceneAsync(scene);

}
