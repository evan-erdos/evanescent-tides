using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxPrototype : MonoBehaviour {
	private List<BoxPrototype> adjacentBoxes;
	private float height;
	public float Height{get;set;}
	private float flowOut;
	public float FlowOut{
		get{ return flowOut; }
		protected set{ }
		}
	[SerializeField]
	private float initialHeight;
	private GameObject moon;
	[SerializeField]
	private float waterWeight;
	[SerializeField]
	private float moonWeight;
	private float bottom;



	// Use this for initialization
	void Start () {
		adjacentBoxes = new List<BoxPrototype>();
		Height = initialHeight;
		bottom = transform.position.y;
		moon = MoonScript.singleton.gameObject;
		
	}
	
	// Update is called once per frame
	void Update () {
		flow();
		transform.localScale = new Vector3(1f,Height,1f);	
		transform.position = new Vector3(transform.position.x,bottom,transform.position.z);
	}

	void flow(){
		foreach(BoxPrototype box in adjacentBoxes){
			float totalFlow = 0;
			print(this.gameObject);
			if(Height > box.Height){
				float heightDif = box.Height - height;
				float earthFlowOut = ( heightDif / waterWeight );
				totalFlow += earthFlowOut;
				print(earthFlowOut.ToString() + " " + "earth");
			}
			if (calcMoonDistance() > box.calcMoonDistance()){
				float moonDif = calcMoonDistance() - box.calcMoonDistance();
				float moonFlowOut =  ( moonDif * moonWeight /  waterWeight );
				totalFlow += moonFlowOut;
				print(moonFlowOut.ToString() + " " + "moon");
			}


			totalFlow *= Time.deltaTime / 100;
			FlowOut = totalFlow;
			print(totalFlow);
			if(Height > 0){
				Height = Height - totalFlow;
				box.Height = box.Height + totalFlow;
			}


		}
	}

	public float calcMoonDistance(){
		float dx = Mathf.Abs(transform.position.x - moon.transform.position.x);
		float dz = Mathf.Abs(transform.position.z - moon.transform.position.z);
		return  Mathf.Pow(Mathf.Pow(dx,2) + Mathf.Pow(dz,2),.5f);
	}

	void OnTriggerEnter(Collider col){

		BoxPrototype box = col.gameObject.GetComponent<BoxPrototype>();
		if(box != null){
			adjacentBoxes.Add(box);
		}
	}
}
