using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Troop {

	public TeamColor team = TeamColor.NEUTRAL;
	public List<Unit> unitList = new List<Unit>();


	public bool IsEmpty() {
		return (unitList.Count == 0);
	}

	public int AtkPower() {
		int sum = 0;
		for(int i = 0; i < unitList.Count; i++) {
			sum += unitList[i].AtkPower();
		}
		return sum;
	}

	public int DefPower() {
		int sum = 0;
		for(int i = 0; i < unitList.Count; i++) {
			sum += unitList[i].DefPower();
		}
		return sum;
	}

	public void AddUnit(Unit unit) {
		AddUnit(unit.type, unit.amount);
	}

	public void AddUnit(UnitType unit, int amount) {
		int index = -1;
		for(int j = 0; j < unitList.Count; j++) {
			if(unitList[j].type == unit) {
				index = j;
				break;
			}
		}

		if(index == -1) {
			unitList.Add(new Unit() { type = unit, amount = amount });
		} else {
			unitList[index].amount += amount;
		}
	}

	public void Absorb(Troop other) {
		team = other.team;
		for(int i = 0; i < other.unitList.Count; i++) {
			Unit unit = other.unitList[i];
			int index = -1;
			for(int j = 0; j < unitList.Count; j++) {
				if (unitList[j].type == unit.type) {
					index = j;
					break;
				}
			}

			if (index == -1) {
				unitList.Add(unit);
			}
			else {
				unitList[index].amount += unit.amount;
			}
		}
		other.team = TeamColor.NEUTRAL;
		other.unitList.Clear();
	}

	public void Print() {
		for(int i = 0; i < unitList.Count; i++) {
			Debug.Log(unitList[i].type + ":   " + unitList[i].amount);
		}
	}




	public static void Fight(Troop atkTroop, Troop defTroop) {
		int atkPwr = atkTroop.AtkPower();
		int defPwr = defTroop.DefPower();

		Troop survivors = new Troop();
		List<UnitType> army = new List<UnitType>();
		int armySize = 0;
		Troop winner = (atkPwr > defPwr) ? atkTroop : defTroop;
		for(int i = 0; i < atkTroop.unitList.Count; i++) {
			armySize += atkTroop.unitList[i].amount;
			for(int u = 0; u < atkTroop.unitList[i].amount; u++) {
				army.Add(atkTroop.unitList[i].type);
			}
		}
		if (atkPwr > defPwr) {
		}

		Debug.Log("Army size:  " + armySize);
		UnitType type;
		Debug.Log("Fighting...");
		while (atkPwr >= defPwr) {
			int index = Random.Range(0, armySize);
			type = army[index];
			army.RemoveAt(index);
			survivors.AddUnit(type, 1);
			atkPwr -= Unit.AtkPower(type);
			armySize--;
		}
		Debug.Log("Survivors");
		survivors.Print();
	}
}
