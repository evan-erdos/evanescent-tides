using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Adventure.EvanescentTides;

public class WaterHexTile : Thing {
    bool isOddFrame;
    int index;
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
        moonWeight = Moon.singleton.Weight;
        yield return null;
        // print(adjacentTiles.Count);
        GetComponents<Collider>().ForEach(o => o.enabled = false);
    }

    void FixedUpdate() {
        MoonDistance = CalculateMoonDistances();
        foreach (var tile in adjacentTiles) Flow(tile);
        transform.localScale = new Vector3(1f,Mathf.Clamp(Height,0.5f,4),1f);
    }


    void Flow(WaterHexTile tile) {
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
