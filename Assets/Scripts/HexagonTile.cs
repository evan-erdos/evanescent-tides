using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexagonTile : MonoBehaviour {
    [SerializeField] protected GameObject hexPrefab;
    [SerializeField] protected float height = 1.5f;

    void Awake() {
        // var instance = Instantiate(hexPrefab) as GameObject;
        // instance.transform.parent = transform;
        // instance.transform.localPosition = Vector3.up*height;
    }
}
