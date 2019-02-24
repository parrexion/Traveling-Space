using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurnController : MonoBehaviour {

	#region Singleton
	public static TurnController instance = null;
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

	public delegate void EndTurn();
	public EndTurn OnEndTurn;

	public int turn;
	public Text turnText;


    private void Start() {
        EndTurnStuff();
		OnEndTurn += EndTurnStuff;
    }

	private void EndTurnStuff() {
		turn++;
		turnText.text = "Turn:  " + turn;
	}

	public void ButtonEndTurn() {
		OnEndTurn?.Invoke();
	}
}
