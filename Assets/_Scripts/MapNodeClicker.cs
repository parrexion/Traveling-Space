using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapNodeClicker : MonoBehaviour {
	
	public Text cityText;
	public MapNode[] mapNodes;
	public Transform selection;
	public Transform target;

	[Header("Prod")]
	public Text popText;
	public Text[] prodList;

	[Header("Fight Screen")]
	public GameObject fightScreen;
	public Transform unitPrefab;
	public Transform attackParent;
	public Transform defendParent;
	public GameObject fightButton;

	[Header("Units")]
	public Text unitPower;
	public Text[] unitList;

	private bool moveMode;
	private MapNode startCity;
	private MapNode targetCity;


	private void Start() {
		for(int i = 0; i < mapNodes.Length; i++) {
			mapNodes[i].index = i;
		}
		fightScreen.SetActive(false);
	}

	public void NodeClicked(MapNode clicked) {
		if (moveMode) {
			SetTarget(clicked);
		}
		else {
			UpdateCurrentCity(clicked);
		}
	}

	private void UpdateCurrentCity(MapNode node) {
		PlayerController.instance.currentCity = node.cityInfo;
		startCity = node;
		cityText.text = node.cityInfo.nodeName;
		selection.transform.position = node.transform.position;

		popText.text = "POP:  " + startCity.cityInfo.pop;
		prodList[0].text = "DATA:   " + startCity.cityInfo.GetBaseProd(Res.DATA);
		prodList[1].text = "PROC:   " + startCity.cityInfo.GetBaseProd(Res.PROCESS);
		prodList[2].text = "GRAPH:   " + startCity.cityInfo.GetBaseProd(Res.GRAPHICS);
		prodList[3].text = "EVOL:   " + startCity.cityInfo.GetBaseProd(Res.EVOLVE);

		unitPower.text = string.Format("A: {0} , D: {1}", node.cityInfo.troops.AtkPower(), node.cityInfo.troops.DefPower());
		for(int i = 0; i < unitList.Length; i++) {
			if (i >= node.cityInfo.troops.unitList.Count) {
				unitList[i].text = "";
			}
			else {
				unitList[i].text = node.cityInfo.troops.unitList[i].type + ":   " + node.cityInfo.troops.unitList[i].amount;
			}
		}
	}

	private void SetTarget(MapNode city) {
		targetCity = city;
		target.transform.position = targetCity.transform.position;
	}

	public void MoveButton() {
		if (moveMode) {
			if (targetCity) {
				MoveTroops();
			}
			target.transform.position = new Vector3(100, 100, 100);
			moveMode = false;
		}
		else {
			if (startCity.cityInfo.troops.IsEmpty())
				return;
			moveMode = true;
			targetCity = null;
			target.transform.position = startCity.transform.position;
		}
	}

	private void MoveTroops() {
		if (startCity.cityInfo.troops.team == targetCity.cityInfo.troops.team || targetCity.cityInfo.troops.team == TeamColor.NEUTRAL) {
			targetCity.cityInfo.troops.Absorb(startCity.cityInfo.troops);
		}
		else {
			ShowFightScreen();
		}
		startCity.Refresh();
		targetCity.Refresh();
		UpdateCurrentCity(targetCity);
	}

	private void ShowFightScreen() {
		fightScreen.SetActive(true);
		fightButton.SetActive(true);
		for(int i = 0; i < attackParent.childCount; i++) {
			Destroy(attackParent.GetChild(i).gameObject);
		}
		for(int i = 0; i < defendParent.childCount; i++) {
			Destroy(defendParent.GetChild(i).gameObject);
		}
		Troop attack = startCity.cityInfo.troops;
		Troop defend = targetCity.cityInfo.troops;
		for(int i = 0; i < attack.unitList.Count; i++) {
			Transform t = Instantiate(unitPrefab, attackParent);
			t.GetComponentsInChildren<Text>()[0].text = attack.unitList[i].amount.ToString();
			t.GetComponentsInChildren<Text>()[1].text = "0";
		}
		for(int i = 0; i < defend.unitList.Count; i++) {
			Transform t = Instantiate(unitPrefab, defendParent);
			t.GetComponentsInChildren<Text>()[0].text = defend.unitList[i].amount.ToString();
			t.GetComponentsInChildren<Text>()[1].text = "0";
		}
	}

	private void ShowFightDeathScreen() {
		fightButton.SetActive(false);
		Troop attack = startCity.cityInfo.troops;
		Troop defend = targetCity.cityInfo.troops;
		for(int i = 0; i < attack.unitList.Count; i++) {
			attackParent.GetChild(i).GetComponentsInChildren<Text>()[2].text = attack.unitList[i].amount.ToString();
		}
		for(int i = 0; i < defend.unitList.Count; i++) {
			defendParent.GetChild(i).GetComponentsInChildren<Text>()[2].text = attack.unitList[i].amount.ToString();
		}
	}

	public void StartFight() {
		Troop.Fight(startCity.cityInfo.troops, targetCity.cityInfo.troops);
		ShowFightDeathScreen();
	}
}
