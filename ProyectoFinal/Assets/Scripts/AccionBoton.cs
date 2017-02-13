using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AccionBoton : MonoBehaviour {

	public	void Load1Players(){
		SceneManager.LoadScene ("Instrucciones1Jugador");
	}

	public void Load2Players(){
		SceneManager.LoadScene ("Instrucciones2Jugadores");
	}

	public void QuitGame(){
		Application.Quit ();
	}
}
