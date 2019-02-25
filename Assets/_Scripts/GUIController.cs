using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUIController : MonoBehaviour {
	
	[Header("Stock")]
	public Text foodTot;
	public Text indTot;
	public Text goldTot;
	public Text infTot;
	public Text stabilityTot;

	[Header("Hardware")]
	public Text[] hardwareText;
	
	[Header("Production")]
	public Text nodeName;
	public Text pop;
	public Text foodProd;
	public Text indProd;
	public Text goldProd;
	public Text infProd;
	
	[Header("Build queue")]
	public Text buildQueue;
	public Text buildTime;
	public Dropdown buildList;
	public Button buildingButton;
	public Button unitButton;

	[Header("Unit list")]
	public Text[] unitTexts;


	public void UpdateCity() {
		City city = PlayerController.instance.currentCity;
		nodeName.text = city.nodeName;
		int turns = Mathf.CeilToInt((City.GROWTH - city.GetTot(Res.DATA)) / (float)city.GetProd(Res.DATA));
		pop.text = "POP:  " + city.pop + " - New in " + turns + " Turns";
		Building build = city.GetBulidQueue();
		if (build != null) {
			turns = Mathf.CeilToInt((build.cost - city.GetTot(Res.PROCESS)) / (float)city.GetProd(Res.PROCESS));
			buildTime.text = "Done in " + turns + " Turns";
		}
		else {
			buildTime.text = "";
		}
		foodTot.text = "FOOD:  " + city.GetTot(Res.DATA);
		indTot.text = "IND:  " + city.GetTot(Res.PROCESS);
		goldTot.text = "GOLD:  " + city.GetTot(Res.GRAPHICS);
		infTot.text = "INF:  " + city.GetTot(Res.EVOLVE);
		stabilityTot.text = "STAB:  " + city.GetTot(Res.STABILITY) + "%";
		
		foodProd.text = "FOOD:  " + city.GetProd(Res.DATA);
		indProd.text = "IND:  " + city.GetProd(Res.PROCESS);
		goldProd.text = "GOLD:  " + city.GetProd(Res.GRAPHICS);
		infProd.text = "INF:  " + city.GetProd(Res.EVOLVE);

		for (int i = 0; i < hardwareText.Length; i++) {
			if (i >= city.hardwares.Count)
				hardwareText[i].text = "";
			else {
				hardwareText[i].text = city.hardwares[i].type.ToString();
			}
		}

		Building b = city.GetBulidQueue();
		buildQueue.text = (b != null) ? b.type.ToString() : "---";
	}

	public void UpdateBuildQueue() {
		City city = PlayerController.instance.currentCity;
		Building build = city.GetBulidQueue();
		if (build != null) {
			int turns = Mathf.CeilToInt((build.cost - city.GetTot(Res.PROCESS)) / (float)city.GetProd(Res.PROCESS));
			buildTime.text = "Done in " + turns + " Turns";
		}
		else {
			buildTime.text = "";
		}
		buildQueue.text = (build != null) ? build.type.ToString() : "---";

		buildList.ClearOptions();
		buildList.AddOptions(GetAvailableNames());
		buildingButton.interactable = false;
		unitButton.interactable = true;
	}

	public void UpdateTrainQueue() {
		City city = PlayerController.instance.currentCity;
		TrainUnit train = city.GetTrainQueue();
		if (train != null) {
			int amount = train.ProduceAmount(city.GetTot(Res.PROCESS) + city.GetProd(Res.PROCESS));
			buildTime.text = "Training " + amount + " per turn";
		}
		else {
			buildTime.text = "";
		}
		buildQueue.text = (train != null) ? train.type.ToString() : "---";

		buildList.ClearOptions();
		buildList.AddOptions(GetTrainablesNames());
		buildingButton.interactable = true;
		unitButton.interactable = false;
	}

	public void UpdateUnitList() {
		Troop troops = PlayerController.instance.currentCity.troops;
		for (int i = 0; i < unitTexts.Length; i++) {
			if (i >= troops.unitList.Count) {
				unitTexts[i].text = "";
			}
			else {
				unitTexts[i].text = troops.unitList[i].type + ":  " + troops.unitList[i].amount;
			}
		}
	}
	
	private List<string> GetAvailableNames() {
		List<Building> available = PlayerController.instance.currentCity.available;
		List<string> nameList = new List<string>();
		nameList.Add("---");
		for (int i = 0; i < available.Count; i++) {
			nameList.Add(available[i].type.ToString());
		}
		return nameList;
	}
	
	private List<string> GetTrainablesNames() {
		List<TrainUnit> trainables = PlayerController.instance.currentCity.trainables;
		List<string> nameList = new List<string>();
		nameList.Add("---");
		for (int i = 0; i < trainables.Count; i++) {
			nameList.Add(trainables[i].type.ToString());
		}
		return nameList;
	}
}
