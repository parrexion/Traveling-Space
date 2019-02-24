using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UnitType { SOLDIER, SPEAR, ARCHER, ALCHEMY }

[System.Serializable]
public class Unit {

	public UnitType type;
	public int amount;

	private static int[] atk = { 30, 20, 40, 25 };
	private static int[] def = { 20, 30, 10, 35 };


	public int AtkPower() {
		return atk[(int)type] * amount;
	}

	public int DefPower() {
		return def[(int)type] * amount;
	}




	public static int AtkPower(UnitType unitType) {
		return atk[(int)unitType];
	}
	public static int DefPower(UnitType unitType) {
		return def[(int)unitType];
	}

}
