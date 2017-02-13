using UnityEngine;
using System.Collections;

public class Bomb : MonoBehaviour {

	public int longExplosion = 2;

	public GameObject explosionPrefab;


	public LayerMask levelMask;
	private bool exploded = false;

	private int player; //It refers to the player that throw this bomb


	// Use this for initialization
	void Start () {
		Invoke("Explode",3f);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	 void Explode(){
		GameObject explosion = Instantiate(explosionPrefab);
		explosion.transform.position = this.transform.position;

		StartCoroutine(CreateExplosions(Vector3.forward));
		StartCoroutine(CreateExplosions(Vector3.right));
		StartCoroutine(CreateExplosions(Vector3.back));
		StartCoroutine(CreateExplosions(Vector3.left));

		GetComponent<MeshRenderer>().enabled = false; 
		transform.FindChild("Collider").gameObject.SetActive(false);
		this.exploded = true;
		Destroy(gameObject, .3f);
		if (this.player == 1) {
			if (GameObject.Find ("Player 1")) {
				GameObject.Find ("Player 1").GetComponent<Player> ().addBomb1 ();		
			} else {
				GameObject.Find ("Player1Jugador").GetComponent<Player1> ().addBomb1 ();
			}
		} else if(this.player==2){
			GameObject.Find ("Player 2").GetComponent<Player> ().addBomb2 ();
		}
	}

	private IEnumerator CreateExplosions(Vector3 direction) {
		for (int i = 1; i <=this.longExplosion; i++) { 
			
			RaycastHit hit; 

			Physics.Raycast(transform.position + new Vector3(0,.5f,0), direction, out hit, i, levelMask); 


			if (!hit.collider) { 
				Instantiate(explosionPrefab, transform.position + (i * direction),
					 
					explosionPrefab.transform.rotation); 
				
			} else { 
				break; 
			}
			yield return new WaitForSeconds(.05f); 
		}
	}

	void OnTriggerEnter(Collider other){
		if (!exploded && other.CompareTag("Explosion")) {
			CancelInvoke("Explode"); 
			Explode(); 
		}
	}

	public int Player{
		set{this.player = value; }

		get{ return this.player; }
	}

	public int LongExplosion{
		set{this.longExplosion = value; }

		get{ return this.longExplosion;}
	}





}


