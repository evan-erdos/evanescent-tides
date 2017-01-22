using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterHexTile : MonoBehaviour {
    bool isOddFrame;
    List<WaterHexTile> adjacentTiles = new List<WaterHexTile>();
    float bottom;
    public float Height {get;protected set;}
    public float FlowOut {get; protected set;}
    public float GroundHeight {get; protected set;}
    public float MoonDistance {get; protected set;}
    [SerializeField] protected float initialHeight = 1;
    [SerializeField] protected float waterWeight = 0.15f;
    float moonWeight;
    Transform moon;

    IEnumerator Start() {
        Height = initialHeight;
        bottom = transform.position.y;
        moon = Moon.singleton.transform;
        waterWeight = 0.007f;
        yield return null;
        GetComponents<Collider>().ForEach(o => o.enabled = false);
        // StartCoroutine(CalculatingMoonDistance());
        // StartCoroutine(CalculatingFlow());

    }

    // IEnumerator CalculatingMoonDistance() {
    //     while (true) {
    //         yield return new WaitForSeconds(0.1f);
    //         MoonDistance = CalculateMoonDistances();
    //         moonWeight = Moon.singleton.Weight;
    //     }
    // }

    // IEnumerator CalculatingFlow() {
    //     while (true) foreach (var tile in adjacentTiles) {
    //         yield return new WaitForFixedUpdate();
    //         FlowTile(tile);
    //     }
    // }


    void FixedUpdate() {

        MoonDistance = CalculateMoonDistances();
        moonWeight = Moon.singleton.Weight;

        foreach (var tile in adjacentTiles) FlowTile(tile);
        transform.localScale = new Vector3(1f,Mathf.Clamp(Height,0.5f,4),1f);
    }

    void Flow() => adjacentTiles.ForEach(o => FlowTile(o));

    void FlowTile(WaterHexTile tile) {
        var totalFlow = 0f;
        if (Height > tile.Height) {
            var heightDif = Height - tile.Height;
            var earthFlowOut = ( heightDif / waterWeight );
            totalFlow += earthFlowOut;
        }

        if (tile.MoonDistance < MoonDistance) {
            var moonDif = MoonDistance - tile.MoonDistance;
            totalFlow += ( moonDif * moonWeight /  waterWeight );
        }

        totalFlow *= Time.fixedDeltaTime / 10;
        if (Height > totalFlow) {
            Height = Height - totalFlow;
            tile.Height = tile.Height + totalFlow;
        }
    }

    public float CalculateMoonDistances() {
        var dx = Mathf.Abs(transform.position.x - moon.position.x);
        var dz = Mathf.Abs(transform.position.z - moon.position.z);
        return  Mathf.Pow(Mathf.Pow(dx,2) + Mathf.Pow(dz,2),0.5f);
    }

    void OnTriggerEnter(Collider col) {
        var water = col.gameObject.GetComponent<WaterHexTile>();
        if ((water is null) || adjacentTiles.Contains(water)) return;
        adjacentTiles.Add(water);
    }
}
