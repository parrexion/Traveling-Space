using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapNodeClicker : MonoBehaviour {
	
	public Text cityText;
	public MapNode[] mapNodes;
	public Transform selection;
	public Transform target;

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
			Troop.Fight(startCity.cityInfo.troops, targetCity.cityInfo.troops);
		}
		startCity.Refresh();
		targetCity.Refresh();
		UpdateCurrentCity(targetCity);
	}
}
