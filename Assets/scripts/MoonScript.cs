using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoonScript : MonoBehaviour {

	public static MoonScript singleton {get; private set;}
	


	// Use this for initialization
	void Awake () {
		singleton = this;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
