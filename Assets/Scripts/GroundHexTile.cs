using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GroundHexTile : MonoBehaviour, IHexTile {
    [SerializeField] protected GameObject hexPrefab;
    [SerializeField] protected float height = 1.5f;
    public float Height => height;
    

    void Awake() {
        height = transform.position.y + 1;	
        // var instance = Instantiate(hexPrefab) as GameObject;
        // instance.transform.parent = transform;
        // instance.transform.localPosition = Vector3.up*height;
    }
}
