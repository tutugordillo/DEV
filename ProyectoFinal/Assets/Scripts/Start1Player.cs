using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Start1Player : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	void Update () {
		if(Input.GetKeyDown(KeyCode.Space)){
			SceneManager.LoadScene ("Game1Player");
		}
	}
}
