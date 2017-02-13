
using UnityEngine;
using System.Collections;
using System;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player1 : MonoBehaviour {

    //Player parameters
    [Range(1, 2)] //Enables a nifty slider in the editor
    public int playerNumber = 1; //Indicates what player this is: P1 or P2
    public float moveSpeed = 5f;
    public bool canDropBombs1 = true; //Can the player drop bombs?
    public bool canMove = true; //Can the player move?

    private int bombs1 = 2;//Amount of bombs the player has left to drop, gets decreased as the player drops a bomb, increases as an owned bomb explodes
	public int lives = 5;
	private bool canDamage = true;
	private bool dead = false;
    //Prefabs
    public GameObject[] bombPrefab;
	private int bomba=0;

    //Cached components
    private Rigidbody rigidBody;
    private Transform myTransform;
    private Animator animator;

	//Live Image Components
	public Image playerLiveIm;
	public Sprite[] imagenes;
	public Image[] itemsImages;

	public GlobalStateManager1 globalManager;
    // Use this for initialization
    void Start() {
        //Cache the attached components for better performance and less typing
        rigidBody = GetComponent<Rigidbody>();
        myTransform = transform;
        animator = myTransform.FindChild("PlayerModel").GetComponent<Animator>();
		this.playerLiveIm.sprite = imagenes [4];
		this.playerLiveIm.transform.localScale = new Vector3(1.5f,1f,1f);
		for (int i = 0; i < itemsImages.Length; i++) {
			itemsImages [i].enabled = false;
		}
		canDamage = true;
    }

    // Update is called once per frame
    void Update() {
        UpdateMovement();
    }

    private void UpdateMovement() {
        animator.SetBool("Walking", false);

        if (!canMove) { //Return if player can't move
            return;
        }

        //Depending on the player number, use different input for moving
        if (playerNumber == 1) {
            UpdatePlayer1Movement();
        }
    }

    /// <summary>
    /// Updates Player 1's movement and facing rotation using the WASD keys and drops bombs using Space
    /// </summary>
    private void UpdatePlayer1Movement() {
		if (Input.GetKey(KeyCode.UpArrow)) { //Up movement
			rigidBody.velocity = new Vector3(rigidBody.velocity.x, rigidBody.velocity.y, moveSpeed);
			myTransform.rotation = Quaternion.Euler(0, 0, 0);
			animator.SetBool("Walking", true);
		}

		if (Input.GetKey(KeyCode.LeftArrow)) { //Left movement
			rigidBody.velocity = new Vector3(-moveSpeed, rigidBody.velocity.y, rigidBody.velocity.z);
			myTransform.rotation = Quaternion.Euler(0, 270, 0);
			animator.SetBool("Walking", true);
		}

		if (Input.GetKey(KeyCode.DownArrow)) { //Down movement
			rigidBody.velocity = new Vector3(rigidBody.velocity.x, rigidBody.velocity.y, -moveSpeed);
			myTransform.rotation = Quaternion.Euler(0, 180, 0);
			animator.SetBool("Walking", true);
		}

		if (Input.GetKey(KeyCode.RightArrow)) { //Right movement
			rigidBody.velocity = new Vector3(moveSpeed, rigidBody.velocity.y, rigidBody.velocity.z);
			myTransform.rotation = Quaternion.Euler(0, 90, 0);
			animator.SetBool("Walking", true);
			//		Debug.Log (bombs2);
			//		Debug.Log (canDropBombs2);
		}

        if (canDropBombs1 && Input.GetKeyDown(KeyCode.Space)) { //Drop bomb
			if (bombs1 <= 0) {
				canDropBombs1 = false;
			} else {
				GameObject bomb = DropBomb();
				bomb.GetComponent<Bomb1Player> ().Player = 1;
				bombs1--;

			}
        }
    }



	private GameObject DropBomb() {
		if (bombPrefab[bomba]) {
			Vector3 pos = Vector3.zero;
			GameObject bomb = Instantiate (bombPrefab[bomba]) as GameObject;
			pos.x = Mathf.RoundToInt (myTransform.position.x);
			pos.y = myTransform.position.y;
			pos.z = Mathf.RoundToInt (myTransform.position.z);
			bomb.transform.position = pos;
			bomb.transform.rotation = bombPrefab[bomba].transform.rotation;
			return bomb;
		} else
			return null;
    }

    public void OnTriggerEnter(Collider other) {
        
		switch(other.tag){
		case "Explosion":
			if (canDamage) {
				this.lives--;
				//		Debug.Log ("P" + playerNumber + " hit by explosion!");
				if (lives == 0) {
					this.playerLiveIm.sprite = null;
					dead = true;
					globalManager.PlayerDied (playerNumber);
					Destroy (gameObject);
				} else {
					this.setLiveImage ();
					StartCoroutine (ChangeDamage ());
				}
			}
			break;
		case "ItemCorazon":
	//		Debug.Log ("Item Corazon");
			if (this.lives < 5) {
				this.lives++;
				this.setLiveImage ();
			}
			Destroy (other.gameObject);
			globalManager.NumItems = globalManager.NumItems - 1;
			break;
		case "ItemBomba1":
	//		Debug.Log ("Item Bomba");
			StartCoroutine(AddBomb1());
			Destroy (other.gameObject);
			globalManager.NumItems = globalManager.NumItems - 1;
			break;
		case "ItemPatines":
			StartCoroutine (ChangeSpeed ());
			Destroy (other.gameObject);
			globalManager.NumItems = globalManager.NumItems - 1;
			break;

		case "ItemLengua":
			StartCoroutine (ChangeBomb ());
			Destroy (other.gameObject);
			globalManager.NumItems = globalManager.NumItems - 1;
			break;

		case "ItemMuerte":
			this.lives = this.lives - 2;
			if (lives <= 0) {
				this.playerLiveIm.sprite = null;
				dead = true;
				globalManager.PlayerDied (playerNumber);
				Destroy (gameObject);
			} else {
				this.setLiveImage ();		
			}
			Destroy (other.gameObject);
			break;

		case "Lanzador":
			this.globalManager.GetComponent<GlobalStateManager1> ().StopAllCoroutines ();
			if (globalManager.HardLevel) {
				SceneManager.LoadScene ("InitialScreen");
			} else {
				SceneManager.LoadScene ("Game1Player2");
			}break;

		default:
			break;
		}
	}


	void OnCollisionEnter(Collision col){
		if (col.gameObject.tag == "Enemy") {
			this.lives--;
			if (this.lives <= 0) {
				this.playerLiveIm.sprite = null;
				dead = true;
				globalManager.PlayerDied (playerNumber);
				Destroy (gameObject);
			} else {
				this.setLiveImage ();
			}
		}
	}


	IEnumerator ChangeDamage(){
		this.canDamage = false;
		yield return new WaitForSeconds (1);
		this.canDamage = true;
	}

	IEnumerator ChangeSpeed(){
		this.itemsImages [2].enabled = true;
		this.moveSpeed = this.moveSpeed * 2.0f;
		yield return new WaitForSeconds (6);
		this.moveSpeed = this.moveSpeed / 2.0f;
		this.itemsImages [2].enabled = false;
	}

	IEnumerator AddBomb1(){
		this.itemsImages [1].enabled = true;
		bombs1++;
		canDropBombs1 = true;
		yield return new WaitForSeconds (6);
		bombs1--;
		this.itemsImages [1].enabled = false;
		}
		
	IEnumerator ChangeBomb (){
		this.itemsImages [0].enabled = true;
		bomba = 1;
		yield return new WaitForSeconds (8);
		bomba = 0;
		this.itemsImages [0].enabled = false;
	}

	public void addBomb1(){
		bombs1++;
		canDropBombs1 = true;
	}


	public int Bombs1{
		get{return this.bombs1; }

		set{this.bombs1 = value; }
	}


	private void setLiveImage(){
		switch (lives) {
		case 1: 
			playerLiveIm.sprite = imagenes [0];
			playerLiveIm.transform.localScale = new Vector3 (.3f, 1f, 1f);
			break;
		case 2: 
			playerLiveIm.sprite = imagenes [1];
			playerLiveIm.transform.localScale = new Vector3 (.5f, 1f, 1f);
			break;
		case 3: 
			playerLiveIm.sprite = imagenes [2];
			playerLiveIm.transform.localScale = new Vector3 (.8f, 1f, 1f);
			break;
		case 4: 
			playerLiveIm.sprite = imagenes [3];
			playerLiveIm.transform.localScale = new Vector3 (1.1f, 1f, 1f);
			break;
		case 5: 
			playerLiveIm.sprite = imagenes [4];
			playerLiveIm.transform.localScale = new Vector3 (1.5f, 1f, 1f);
			break;
		default:
			break;	
		}
	}

}
