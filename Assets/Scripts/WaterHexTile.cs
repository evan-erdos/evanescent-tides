using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterHexTile : MonoBehaviour {
    List<WaterHexTile> adjacentTiles = new List<WaterHexTile>();
    float bottom;
    public float Height {get;protected set;}
    public float FlowOut {get; protected set;}
    public float GroundHeight {get; protected set;}
    public float MoonDistance {get; protected set;}
    [SerializeField] protected float initialHeight = 1;
    [SerializeField] protected float waterWeight = 0.15f;
    [SerializeField] protected float moonWeight = 1f;
    Transform moon;

    IEnumerator Start() {
        Height = initialHeight;
        bottom = transform.position.y;
        moon = Moon.singleton.transform;
        yield return new WaitForSeconds(0.1f);
        GetComponents<Collider>().ForEach(o => o.enabled = false);
        while (true) {
            yield return new WaitForSeconds(0.1f);
            MoonDistance = CalculateMoonDistances();
        }
    }

    void FixedUpdate() {
        Flow();
        var finalHeight = Mathf.Clamp(Height, 0.5f, 4f);
        transform.localScale = new Vector3(1f,finalHeight,1f);
    }

    void Flow() => adjacentTiles.ForEach(o => FlowTile(o));

    void FlowTile(WaterHexTile tile) {
        var totalFlow = 0f;
        if (Height > tile.Height) {
            var heightDif = Height - tile.Height;
            var earthFlowOut = ( heightDif / waterWeight );
            totalFlow += earthFlowOut;
        }

        if (MoonDistance < tile.MoonDistance) {
            var moonDif = MoonDistance - tile.MoonDistance;
            var moonFlowOut =  ( moonDif * moonWeight /  waterWeight );
            totalFlow += moonFlowOut;
        }

        totalFlow *= Time.fixedDeltaTime / 10;
        FlowOut = totalFlow;
        if (Height > totalFlow && GroundHeight + Height > tile.GroundHeight) {
            Height = Height - totalFlow;
            tile.Height = tile.Height + totalFlow;
        }
    }

    public float CalculateMoonDistances() {
        var dx = Mathf.Abs(transform.position.x - moon.position.x);
        var dz = Mathf.Abs(transform.position.z - moon.position.z);
        // var displacement = moon.position - transform.position;
        // return displacement.sqrMagnitude*0.01f;
        return  Mathf.Pow(Mathf.Pow(dx,2) + Mathf.Pow(dz,2),0.5f);
    }

    void OnTriggerEnter(Collider col) {
        var water = col.gameObject.GetComponent<WaterHexTile>();
        GroundHeight = col.gameObject.GetComponent<IHexTile>()?.Height ?? 0;
        if(water != null) adjacentTiles.Add(water);
    }
}
