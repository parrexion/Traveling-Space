using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Res { FOOD, IND, GOLD, INF, STABILITY }

[System.Serializable]
public class City {

    public const int GROWTH = 3000;
	public const int EAT = 20;
	public const int POP_UNSTABILITY = -4;
	
	public string nodeName;
	public int pop;
	public List<Hardware> hardwares = new List<Hardware>();
	
	private int foodTot;
	private int indTot;
	private int goldTot;
	private int infTot;
	private int stabilityTot;
	
	private int foodProd;
	private int indProd;
	private int goldProd;
	private int infProd;
	private int stabProd;
	
	[Header("Buildings")]
	public Building buildQueue;
	public TrainUnit unitQueue;
	public List<Building> buildings = new List<Building>();
	public List<Building> available = new List<Building>();
	public List<TrainUnit> trainables = new List<TrainUnit>();

	[Header("Units")]
	public Troop troops;
	

	public void Init(string name) {
		nodeName = name;
		pop = 1;
		
		foodTot = 20;
		indTot = 15;
		goldTot = 10;
		infTot = 5;
		stabilityTot = 100;

		stabProd = POP_UNSTABILITY;
		for (int i = 0; i < 4; i++) {
			hardwares.Add(Hardware.CreateHardwareRandom());
			foodProd += hardwares[i].dataProd;
			indProd += hardwares[i].processProd;
			goldProd += hardwares[i].graphicProd;
			infProd += hardwares[i].evolveProd;
			stabProd += hardwares[i].stabilityProd;
		}
		
		buildings.Add(Building.CreateBuilding(BuildType.BASE));
		available.Add(Building.CreateBuilding(BuildType.GRANARY));
		available.Add(Building.CreateBuilding(BuildType.MINE));
		available.Add(Building.CreateBuilding(BuildType.MARKET));
		available.Add(Building.CreateBuilding(BuildType.LIBRARY));
		troops = new Troop();
	}

	public void ChangeTot(Res type, int amount) {
		switch (type) {
		case Res.FOOD:
			foodTot += amount;
			break;
		case Res.GOLD:
			goldTot += amount;
			break;
		case Res.IND:
			indTot += amount;
			break;
		case Res.INF:
			infTot += amount;
			break;
		}
	}
	
	public int GetTot(Res type) {
		switch (type) {
		case Res.FOOD:
			return foodTot;
		case Res.GOLD:
			return goldTot;
		case Res.IND:
			return indTot;
		case Res.INF:
			return infTot;
		case Res.STABILITY:
			return stabilityTot + pop * stabProd;
		default:
			return -1;
		}
	}
	
	public int GetProd(Res type) {
		switch (type) {
		case Res.FOOD:
			return (foodProd - EAT) * pop + BuildingProd(type);
		case Res.GOLD:
			return goldProd * pop - TotalUpkeep() + BuildingProd(type);
		case Res.IND:
			return indProd * pop + BuildingProd(type);
		case Res.INF:
			return infProd * pop + BuildingProd(type);
		default:
			return -1;
		}
	}
	
	public void Produce() {
		foodTot += GetProd(Res.FOOD);
		indTot += GetProd(Res.IND);
		goldTot += GetProd(Res.GOLD);
		infTot += GetProd(Res.INF);
		
		if (foodTot >= GROWTH) {
			foodTot -= GROWTH;
			pop++;
		}
	}
	
	public int TotalUpkeep() {
		int sum = 0;
		for (int i = 0; i < buildings.Count; i++) {
			sum += buildings[i].upkeep;
		}
		return sum;
	}
	
	private int BuildingProd(Res type) {
		int sum = 0;
		for (int i = 0; i < buildings.Count; i++) {
			Building b = buildings[i];
			switch (type) {
			case Res.FOOD:
				sum += b.foodProd;
				break;
			case Res.GOLD:
				sum += b.goldProd;
				break;
			case Res.IND:
				sum += b.indProd;
				break;
			case Res.INF:
				sum += b.infProd;
				break;
			case Res.STABILITY:
				sum += b.stability;
				break;
			default:
				break;
			}
		}
		return sum;
	}
	
	public void CancelBuild() {
		if (buildQueue != null) {
			available.Add(buildQueue);
			buildQueue = null;
		}
		unitQueue = null;
	}

	public TrainUnit GetTrainQueue() {
		return unitQueue;
	}
	
	public Building GetBulidQueue() {
		return buildQueue;
	}
}
