using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayButton : MonoBehaviour {
	[SerializeField] string scene = "Isle Of Entry";
	public void onClick() => WaveManager.LoadScene(scene);
}
