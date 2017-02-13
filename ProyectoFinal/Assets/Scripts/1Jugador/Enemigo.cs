using UnityEngine;
using System.Collections;

public class Enemigo : MonoBehaviour {

	public int lifes=1;
	public GameObject targetObj;
	// Use this for initialization
	void Start () {
		targetObj = GameObject.Find ("Global State Manager");
	}
	
	// Update is called once per frame
	void Update () {
		if (lifes <= 0) {
			Destroy (this.gameObject);
		}
	}


	public void OnTriggerEnter(Collider other) {
		lifes--;
		if (other.tag == "Explosion") {
			if (lifes <= 0) {
				Destroy (this.gameObject);
				targetObj.SendMessage ("CheckEnemies");
			}
		}
	}
}
