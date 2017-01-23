using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WaveManager : MonoBehaviour {
    static bool wait;
    public static WaveManager singleton {get;private set;}
    void Awake() => singleton = this;
    void Start() => DontDestroyOnLoad(gameObject);

    public static void LoadScene(string sceneName) =>
        singleton.StartCoroutine(LoadingScene(sceneName));

    static IEnumerator LoadingScene(string sceneName) {
        if (wait) yield break;
        wait = true;
        SceneManager.LoadSceneAsync(sceneName);
        yield return new WaitForSeconds(10);
        wait = false;
    }
}
