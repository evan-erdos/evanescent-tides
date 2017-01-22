using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayButton : MonoBehaviour {
	[SerializeField] string scene = "Isle Of Entry";

	// Use this for initialization
	public void onClick(){
		SceneManager.LoadSceneAsync("Base");

		SceneManager.LoadSceneAsync(scene);

	}
}
