
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GlobalStateManager1 : MonoBehaviour {
  
	public GameObject[] items; 
	public int numEnemies = 4;
	public GameObject finish;
	public GameObject enemigosPrefab;
	public bool hardLevel=false;
	public Text perdido;
	public GameObject panel;

	public float leveltime;
	public Text tiempoInfo;
	public GameObject tiempoPanel;


	public float probItem = 0.001f;
	private int maxItems = 3;
	private int numItems = 0;
	private int deadPlayers = 0;
	private int deadPlayerNumber = -1;

	private GameObject ancla;
	private GameObject anclaEnemigos;
	private Vector3 pos;


	void Start() {
		panel.SetActive (false);
		ancla = new GameObject ("Items");
		anclaEnemigos = GameObject.Find ("Enemies");
		if (hardLevel) {
			leveltime = 65;
			this.pos = new Vector3 (4f, 0.5f, 1f);
		} else {
			leveltime =50;
			this.pos = new Vector3 (1f, 0.5f, 1f);
		}
		StartCoroutine (ControlTime ());
		//InvokeRepeating ("GenerarItem", 4f, 4f);
	}

	IEnumerator ControlTime(){
		this.tiempoPanel.SetActive (false);
		yield return new WaitForSeconds (leveltime);
		InvokeRepeating ("LightTimePanel",0.1f,0.3f);
		this.tiempoInfo.text = "10";
		yield return new WaitForSeconds (1);
		this.tiempoInfo.text = "";
		yield return new WaitForSeconds (5);
		this.tiempoInfo.text = "5";
		yield return new WaitForSeconds (1);
		this.tiempoInfo.text = "4";
		yield return new WaitForSeconds (1);
		this.tiempoInfo.text = "3";
		yield return new WaitForSeconds (1);
		this.tiempoInfo.text = "2";
		yield return new WaitForSeconds (1);
		this.tiempoInfo.text = "1";
		yield return new WaitForSeconds (1);
		CancelInvoke ();
		this.tiempoPanel.SetActive (false);
		this.tiempoInfo.text = "";
		this.panel.SetActive (true);
		this.perdido.text = "Lo siento, !se te acabo el tiempo!";
		Invoke ("ReloadLevel", 3.5f);
	}

	void LightTimePanel(){
		if (this.tiempoPanel.activeSelf) {
			this.tiempoPanel.SetActive (false);
		} else {
			this.tiempoPanel.SetActive (true);
		}
	}

	void spawnEnemy(){
		GameObject enemigo1 = Instantiate (enemigosPrefab, anclaEnemigos.transform) as GameObject;
		enemigo1.transform.position = new Vector3 (1f,0.5f,1f);
		enemigo1.GetComponent<Enemigo> ().targetObj = this.gameObject;
		this.numEnemies++;
	}

	void spawnEnemies(){
		GameObject enemigo1 = Instantiate (enemigosPrefab, anclaEnemigos.transform) as GameObject;
		enemigo1.transform.position = new Vector3 (5f,0.5f,3f);

		GameObject enemigo2 = Instantiate (enemigosPrefab, anclaEnemigos.transform) as GameObject;
		enemigo2.transform.position = new Vector3 (3f,0.5f,5f);

		GameObject enemigo3 = Instantiate (enemigosPrefab, anclaEnemigos.transform) as GameObject;
		enemigo3.transform.position = new Vector3 (7f,0.5f,7f);

		GameObject enemigo4 = Instantiate (enemigosPrefab, anclaEnemigos.transform) as GameObject;
		enemigo4.transform.position = new Vector3 (1f,0.5f,2f);
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

	void GenerateItemAt(Vector3 position){
		//Debug.Log ("Comienza");
		if (numItems < maxItems) {
			int indice = Random.Range (0, items.Length);
			GameObject obj = Instantiate (items [indice], ancla.transform) as GameObject;
			obj.transform.position = new Vector3(position.x,0.5f,position.z);
			numItems++;
		}
	}

	void CheckPlayersDeath() {
		if (deadPlayers == 1) { 
			
			if (deadPlayerNumber == 1) {
				panel.SetActive (true);
				perdido.text = "Lo siento, ¡has perdido!";
						Invoke ("ReloadLevel", 2.5f);

			}
		}
	}

	void ReloadLevel(){
		if (!hardLevel) {
			SceneManager.LoadScene ("Game1Player");
		} else {
			SceneManager.LoadScene ("Game1Player2");
		}
	}


	void CheckEnemies(){
		this.numEnemies--;
		if (numEnemies == 0) {
			GameObject obj = Instantiate (this.finish) as GameObject;
			obj.transform.position = pos;
		}
	}

	void LoadInitial(){
		SceneManager.LoadScene ("InitialScreen");

	}
		

	public int NumItems{
		get{ return this.numItems;}

		set{ this.numItems = value;}
	}

	public bool HardLevel{
		get{return this.hardLevel; }

		set{this.hardLevel = value; }
	}
}
