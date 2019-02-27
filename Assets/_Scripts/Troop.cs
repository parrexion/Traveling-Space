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
		Unit found = GetUnit(unit);

		if(found == null) {
			unitList.Add(new Unit() { type = unit, amount = amount });
		} else {
			found.amount += amount;
		}
	}

	public Unit GetUnit(UnitType type) {
		for(int i = 0; i < unitList.Count; i++) {
			if(unitList[i].type == type) {
				return unitList[i];
			}
		}
		return null;
	}

	public void KillUnits(Troop survivors) {
		if (survivors == null) {
			for(int i = 0; i < unitList.Count; i++) {
				unitList[i].amount = 0;
			}
		}
		else {
			for(int i = 0; i < unitList.Count; i++) {
				Unit found = survivors.GetUnit(unitList[i].type);
				unitList[i].amount = (found == null) ? 0 : found.amount;
			}
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
		bool attackerWin = (atkPwr >= defPwr);

		Troop survivors = new Troop();
		List<UnitType> army = new List<UnitType>();
		int armySize = 0;
		Troop winner = (attackerWin) ? atkTroop : defTroop;
		for(int i = 0; i < winner.unitList.Count; i++) {
			armySize += winner.unitList[i].amount;
			for(int u = 0; u < winner.unitList[i].amount; u++) {
				army.Add(winner.unitList[i].type);
			}
		}

		Debug.Log("Army size:  " + armySize);
		UnitType type;
		Debug.Log("Fighting...");
		int stronger = (attackerWin) ? atkPwr : defPwr;
		int weaker = (attackerWin) ? defPwr : atkPwr;
		while (stronger >= weaker) {
			int index = Random.Range(0, armySize);
			type = army[index];
			army.RemoveAt(index);
			survivors.AddUnit(type, 1);
			stronger -= Unit.AtkPower(type);
			armySize--;
		}
		Debug.Log("Survivors");
		survivors.Print();

		if (attackerWin) {
			atkTroop.KillUnits(survivors);
			defTroop.KillUnits(null);
		}
		else {
			atkTroop.KillUnits(null);
			defTroop.KillUnits(survivors);
		}	
	}
}
