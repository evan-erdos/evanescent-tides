using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterHexTile : MonoBehaviour {
    List<WaterHexTile> adjacentTiles = new List<WaterHexTile>();
    float bottom;
    public float Height {get;protected set;}
    public float FlowOut {get; protected set;}
    public float GroundHeight {get; protected set;}
    [SerializeField] protected float initialHeight = 1;
    [SerializeField] protected float waterWeight = 0.15f;
    [SerializeField] protected float moonWeight = 1f;
    Transform moon;

    IEnumerator Start() {
        Height = initialHeight;
        bottom = transform.position.y;
        moon = Moon.singleton.transform;
        yield return new WaitForSeconds(0.1f);
        print(adjacentTiles.Count);
    }

    void Update() {
        Flow();
        var finalHeight = Mathf.Clamp(Height, 0.5f, 4f);
        transform.localScale = new Vector3(1f,finalHeight,1f);
    }

    void Flow() {
        foreach (var box in adjacentTiles) {
            var totalFlow = 0f;
            if (Height > box.Height) {
                var heightDif = Height - box.Height;
                // var heightDif = box.Height - Height;
                var earthFlowOut = ( heightDif / waterWeight );
                totalFlow += earthFlowOut;
            }

            if (CalculateMoonDistances() > box.CalculateMoonDistances()){
                var moonDif = CalculateMoonDistances() - box.CalculateMoonDistances();
                var moonFlowOut =  ( moonDif * moonWeight /  waterWeight );
                totalFlow += moonFlowOut;
            }

            totalFlow *= Time.deltaTime / 100;
            FlowOut = totalFlow;
            if (Height > 0){
                Height = Height - totalFlow;
                box.Height = box.Height + totalFlow;
            }
        }
    }

    public float CalculateMoonDistances() {
        var dx = Mathf.Abs(transform.position.x - moon.position.x);
        var dz = Mathf.Abs(transform.position.z - moon.position.z);
        return  Mathf.Pow(Mathf.Pow(dx,2) + Mathf.Pow(dz,2),.5f);
    }

    void OnTriggerEnter(Collider col) {
        var water = col.gameObject.GetComponent<WaterHexTile>();
        GroundHeight = col.gameObject.GetComponent<IHexTile>()?.Height ?? 0;
        if(water != null) adjacentTiles.Add(water);
    }
}
