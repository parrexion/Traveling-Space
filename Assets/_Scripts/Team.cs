using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TeamColor { NEUTRAL, BLUE, RED, YELLOW, GREEN }

public static class Team {
	
	public static Color GetTeamColor(TeamColor team) {
		switch(team) {
			case TeamColor.BLUE:
				return Color.blue;
			case TeamColor.RED:
				return Color.red;
			case TeamColor.YELLOW:
				return Color.yellow;
			case TeamColor.GREEN:
				return Color.green;
			default:
				return Color.white;
		}
	}
}
