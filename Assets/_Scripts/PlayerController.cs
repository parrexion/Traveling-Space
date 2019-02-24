using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState { NONE, LOAD, MAIN, MAP, CITY }

public class PlayerController : MonoBehaviour {

	#region Singleton
	public static PlayerController instance = null;
	private void Awake() {
		if (instance) {
			Destroy(gameObject);
		}
		else {
			instance = this;
			DontDestroyOnLoad(gameObject); 
		}
	}
	#endregion

	public GameState state;
	public TeamColor currentTeam;
	public City currentCity;

	[Header("GUI")]
	public Canvas mapCanvas;
	public Canvas cityCanvas;
	public CityController cityController;


    private void Start() {
        mapCanvas.enabled = true;
		cityCanvas.enabled = false;
    }

	public void OpenCity() {
		if (state != GameState.MAP)
			return;
		cityCanvas.enabled = true;
		state = GameState.CITY;
		cityController.UpdateGUI();
	}

	public void OpenMap() {
		if (state != GameState.CITY)
			return;
		cityCanvas.enabled = false;
		state = GameState.MAP;
	}
}
