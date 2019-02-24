using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CityController : MonoBehaviour {
	
	public GUIController gui;
	
	private int buildMode;


	private void Start() {
		TurnController.instance.OnEndTurn += EndTurn;
	}
	
	private void EndTurn() {
		City city = PlayerController.instance.currentCity;
		city.Produce();
		
		if (city.buildQueue != null && city.GetTot(Res.IND) >= city.buildQueue.cost) {
			city.ChangeTot(Res.IND, city.buildQueue.cost);
			city.buildings.Add(city.buildQueue);
			city.buildQueue = null;
		}
		else if (city.unitQueue != null && city.GetTot(Res.IND) >= city.unitQueue.cost) {

		}
		gui.UpdateCity();
	}
	
	public void UpdateGUI() {
		gui.UpdateCity();
		if (buildMode == 0)
			gui.UpdateBuildQueue();
		else 
			gui.UpdateTrainQueue();
		gui.UpdateUnitList();
	}
	
	public void Build() {
		int index = gui.buildList.value -1;
		if (index == -1) {
			//PlayerController.instance.currentCity.CancelBuild();
		}
		else {
			StartBuild(index);
		}
	}
	
	public void StartBuild(int index) {
		City city = PlayerController.instance.currentCity;
		if (city.buildQueue != null || city.unitQueue != null) {
			Debug.Log("FILLED");
			return;
		}

		if (buildMode == 0) {
			city.buildQueue = city.available[index];
			city.available.RemoveAt(index);
			gui.UpdateBuildQueue();
		}
		else if (buildMode == 1) {
			city.unitQueue = city.trainables[index];
			gui.UpdateTrainQueue();
		}
	}

	public void ChangeBuildType(int type) {
		buildMode = type;
		if (buildMode == 0) {
			gui.UpdateBuildQueue();
		}
		else if (buildMode == 1) {
			gui.UpdateTrainQueue();
		}
	}
}
