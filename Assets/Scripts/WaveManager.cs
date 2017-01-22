using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour {
    void Start() => DontDestroyOnLoad(gameObject);
}
