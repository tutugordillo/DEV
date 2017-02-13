using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Items : MonoBehaviour {

	// Use this for initialization

	public void OnTriggerEnter(Collider other) {
		if (GameObject.Find ("Player1Jugador") == null) {
			if (other.gameObject.tag == "Explosion") {
				GameObject.Find ("Global State Manager").GetComponent<GlobalStateManager> ().NumItems--;
				Destroy (this.gameObject);
			}
		}
			
	}
		

}
