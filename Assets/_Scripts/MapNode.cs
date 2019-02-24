using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapNode : MonoBehaviour {
	
	public MapNodeClicker clicker;
	public int index;
	
	public City cityInfo;


	protected void Start() {
		Troop t = cityInfo.troops;
		cityInfo = new City();
		cityInfo.Init("Node " + index);
		cityInfo.troops = t;
		Refresh();
	}

	public void Refresh() {
		GetComponent<SpriteRenderer>().color = Team.GetTeamColor(cityInfo.troops.team);
	}

	private void OnMouseDown() {
		clicker.NodeClicked(this);
	}

}
