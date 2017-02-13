﻿using UnityEngine;
using System.Collections;

public class Destructible : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider other){
		if (other.CompareTag("Explosion")) {
			Destroy (this.gameObject);
		}
	}
}
