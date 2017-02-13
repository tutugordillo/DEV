
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GlobalStateManager : MonoBehaviour {
  
	public GameObject[] items; 

	public float probItem = 0.001f;
	private int maxItems = 3;
	private int numItems = 0;
	private int deadPlayers = 0;
	private int deadPlayerNumber = -1;

	public Text	winner;
	public GameObject panel;

	private GameObject ancla;
	void Start() {
		panel.SetActive (false);
		ancla = new GameObject ("Items");
		InvokeRepeating ("GenerarItem", 4f, 4f);
	}

	public void PlayerDied(int playerNumber) {
		deadPlayers++; 

		if (deadPlayers == 1) {
			deadPlayerNumber = playerNumber; 
			Invoke("CheckPlayersDeath", .3f);
			Invoke ("LoadInitial", 3.5f);
		}
    }

/*	void FixedUpdate(){
		float prob = Random.value;
		//Debug.Log (prob);
		if (numItems < maxItems && prob <probItem) {
			GenerarItem();
		}
	}*/

	void GenerarItem(){
		if (numItems < maxItems) {
			int indice = Random.Range (0, items.Length);
			GameObject obj = Instantiate (items [indice], ancla.transform) as GameObject;
			int posX = Random.Range (1, 9);
			int posZ = Random.Range (1, 8);
			while (posZ % 2 == 0) {
				posZ = Random.Range (1, 8);
			}

			while (posX == 2 || posX == 4 || posX == 5 || posX == 7) {
				posX = Random.Range (1, 9);
			}
			obj.transform.position = new Vector3 (posX, 0.5f, posZ);
			numItems++;
		}

	}

	void CheckPlayersDeath() {
		if (deadPlayers == 1) { 
			
			if (deadPlayerNumber == 1) {
				panel.SetActive (true);
				winner.text = "¡El jugador 2 es el ganador!";
	//			Debug.Log("Player 2 is the winner!");

			} else { 
				panel.SetActive (true);
				winner.text = "¡El jugador 1 es el ganador!";
	//			Debug.Log("Player 1 is the winner!");
			}

		} else { 
	//		Debug.Log("The game ended in a draw!");
		}
	}

	void LoadInitial(){
		SceneManager.LoadScene ("InitialScreen");

	}

	public int NumItems{
		get{ return this.numItems;}

		set{ this.numItems = value;}
	}
}
